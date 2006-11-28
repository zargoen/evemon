using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using EVEMon.Common;
using EVEMon.G15Display;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace EVEMon
{
    class G15Interface
    {
        private static Thread m_G15Thread = null;
        private static bool m_threadRunning = false;
        private static DateTime lastCharUpdate = DateTime.Now.Subtract(TimeSpan.FromSeconds(5));
        private static int m_tick = 0;
        private static int m_view = 0;
        private static int m_stick = 0;
        private static int m_sview = 0;

        private static object m_viewObject = new object();

        private static Image m_currentView;

        public static Image CurrentView { get { lock (m_viewObject) { return ((Image)m_currentView); } } set { lock (m_viewObject) { m_currentView = (Image)value.Clone(); } } }

        private static Dictionary<string, GrandCharacterInfo> m_Chars = new Dictionary<string, GrandCharacterInfo>();


        private static int m_sleep = 0;

        public static void Splash()
        {
            Bitmap bmp = DisplayManager.CreateG15Bitmap();
            Graphics g = Graphics.FromImage(bmp);
            if (File.Exists(Path.Combine(Application.StartupPath, "g15splash.gif")))
            {
                g.DrawImage(Image.FromFile(Path.Combine(Application.StartupPath, "g15splash.gif")), new Point(0, 0));
            }
            else
            {
                g.DrawImage(global::EVEMon.Properties.Resources.evemon_g15, new Point(0, 0));
            }
            m_sleep = 2500;
            CurrentView = bmp;
            DisplayManager.Display(bmp, LCDImagePriority.Alert);
        }
        public static void ResetOptions()
        {
            m_tick = 0;
            m_view = 0;
            m_stick = 0;
            m_sview = 0;
            if (Program.Settings.G15UseG15Display != DisplayManager.IsOpen)
            {
                if (DisplayManager.IsOpen)
                {
                    Stop();
                }
                else
                {
                    Init();
                }
            }
        }


        private static void CharListUpdate()
        {
            List<string> names = new List<string>();
            foreach (CharLoginInfo info in Program.Settings.CharacterList)
            {
                if (m_Chars.ContainsKey(info.CharacterName))
                {
                    GrandCharacterInfo i = m_Chars[info.CharacterName];
                }
                else
                {
                    GrandCharacterInfo i = Program.MainWindow.GetGrandCharacterInfo(info.CharacterName);
                    m_Chars.Add(info.CharacterName, i);
                    i.DownloadAttemptCompleted += new GrandCharacterInfo.DownloadAttemptCompletedHandler(DownloadAttemptCompleted);
                }
                names.Add(info.CharacterName);
            }
            List<string> rem = new List<string>();
            foreach (string n in m_Chars.Keys)
            {
                if (!names.Contains(n))
                    rem.Add(n);
            }
            foreach (string r in rem)
            {
                m_Chars[r].DownloadAttemptCompleted -= new GrandCharacterInfo.DownloadAttemptCompletedHandler(DownloadAttemptCompleted);
                m_Chars.Remove(r);
            }
        }

        private static DateTime last = DateTime.Now;
        private static void Update()
        {
            SortedList<TimeSpan, GrandCharacterInfo> gcis = new SortedList<TimeSpan, GrandCharacterInfo>();
            List<GrandCharacterInfo> all = new List<GrandCharacterInfo>(m_Chars.Values);

            foreach (GrandCharacterInfo gci in all)
            {
                GrandSkill gs = gci.CurrentlyTrainingSkill;
                if (gs == null)
                    continue;
                TimeSpan ts = gs.EstimatedCompletion.Subtract(DateTime.Now);
                if (ts > TimeSpan.Zero)
                {
                    while (gcis.ContainsKey(ts))
                        ts = ts + TimeSpan.FromMilliseconds(1);
                    gcis.Add(ts, gci);
                }
            }


            Bitmap bmp = DisplayManager.CreateG15Bitmap();
            Graphics g = Graphics.FromImage(bmp);

            int updsp = (int)((TimeSpan)DateTime.Now.Subtract(last)).TotalMilliseconds;
            last = DateTime.Now;
            int skoffset = 0;
            int offset = 0;
            GrandCharacterInfo skill, view;
            int up;

            if (gcis.Count > 0)
            {
                if (!Program.Settings.G15StaticSkill)
                {
                    m_stick += updsp;
                    up = (int)Program.Settings.G15SkillInterval.Add(TimeSpan.FromSeconds(2)).TotalMilliseconds;
                    if (m_stick >= up)
                    {
                        m_sview++;
                        m_stick -= up;
                    }
                    if (m_stick < TimeSpan.FromSeconds(1).TotalMilliseconds)
                    {
                        skoffset = (int)((TimeSpan.FromSeconds(1).TotalMilliseconds - m_stick) / 100);
                    }
                    else if (m_stick > (Program.Settings.G15SkillInterval.Add(TimeSpan.FromSeconds(1)).TotalMilliseconds))
                    {
                        skoffset = (int)((m_stick - (Program.Settings.G15SkillInterval.Add(TimeSpan.FromSeconds(1)).TotalMilliseconds)) / 100);
                    }
                    while (m_sview < 0)
                        m_sview += all.Count;
                    if (m_sview >= all.Count)
                        m_sview = m_sview % all.Count;
                    skill = all[m_sview];
                }
                else
                {
                    while (m_sview < 0)
                        m_sview += gcis.Count;
                    if (m_sview >= gcis.Count)
                        m_sview = m_sview % gcis.Count;
                    skill = gcis.Values[m_sview];
                }
            }
            else
            {
                skill = null;
            }
            if (all.Count > 0)
            {
                if (!Program.Settings.G15Static)
                {
                    m_tick += updsp;
                    up = (int)Program.Settings.G15Interval.Add(TimeSpan.FromSeconds(2)).TotalMilliseconds;
                    if (m_tick >= up)
                    {
                        m_view++;
                        m_tick -= up;
                    }
                    if (m_tick < TimeSpan.FromSeconds(1).TotalMilliseconds)
                    {
                        offset = (int)((TimeSpan.FromSeconds(1).TotalMilliseconds - m_tick) / 100);
                    }
                    else if (m_tick > (Program.Settings.G15Interval.Add(TimeSpan.FromSeconds(1)).TotalMilliseconds))
                    {
                        offset = (int)((m_tick - (Program.Settings.G15SkillInterval.Add(TimeSpan.FromSeconds(1)).TotalMilliseconds)) / 100);
                    }
                }
                while (m_view < 0)
                    m_view += all.Count;
                if (m_view >= all.Count)
                    m_view = m_view % all.Count;
                view = all[m_view];
            }
            else
            {
                view = null;
            }

            Font df = new Font("Tahoma", 7f, FontStyle.Regular);
            Brush bfo = DisplayManager.Drawing.WhiteBrush;
            Brush bbg = Brushes.Black;
            Pen pfo = DisplayManager.Drawing.WhitePen;
            Pen pbg = Pens.Black;
            Color cfo = Color.White;
            Color cbg = Color.Black;
            Point cNameS = new Point(1, 0 + skoffset);
            Point cSkillS = new Point(6, 9 + skoffset);
            Point rNameS = new Point(1, 21 + offset);
            Point rSkillS = new Point(6, 30 + offset);

            if (skill == null)
            {
                TextRenderer.DrawText(g, "No Character Training", df, cNameS, cfo, cbg, TextFormatFlags.Left);
            }
            else
            {
                TextRenderer.DrawText(g, skill.Name, df, cNameS, cfo,TextFormatFlags.Left);
                if (skill.CurrentlyTrainingSkill != null)
                {
                    string time = GrandSkill.TimeSpanToDescriptiveText(skill.CurrentlyTrainingSkill.EstimatedCompletion.Subtract(DateTime.Now), DescriptiveTextOptions.IncludeCommas);
                    SizeF times = g.MeasureString(time, df);
                    Rectangle skillb = new Rectangle((int)((DisplayManager.Width - 1) - times.Width), cNameS.Y, (int)times.Width, (int)times.Height);
                    TextRenderer.DrawText(g, time, df, skillb, cfo, TextFormatFlags.Right);
                }
                g.FillRectangle(bbg, new Rectangle(1, 10, DisplayManager.Width - 3, 10));
                if (skill.CurrentlyTrainingSkill != null)
                {
                    TextRenderer.DrawText(g, string.Format("{0} {1}", skill.CurrentlyTrainingSkill.Name, GrandSkill.GetRomanForInt(skill.CurrentlyTrainingSkill.TrainingToLevel)), df, cSkillS, cfo);
                }
                else
                {
                    TextRenderer.DrawText(g, "No skill training", df, cSkillS, cfo);
                }
                g.FillRectangle(bbg, new Rectangle(1, 22, DisplayManager.Width - 3, 10));
            }
            if (view == null)
            {
                TextRenderer.DrawText(g, "No Characters loaded", df, rNameS, cfo, cbg, TextFormatFlags.Left);
            }
            else
            {
                TextRenderer.DrawText(g, view.Name, df, rNameS, cfo, TextFormatFlags.Left);
                string balance = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:n} ISK", Math.Round(view.Balance, 2));
                Point bal = new Point(((DisplayManager.Width - 2) - (int)g.MeasureString(balance, df).Width), rSkillS.Y);
                string skills = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:#,#} SP", view.SkillPointTotal);
                Point ski = new Point(((DisplayManager.Width - 2) - (int)g.MeasureString(skills, df).Width), rNameS.Y);
                TextRenderer.DrawText(g, skills, df, ski, cfo);
                g.FillRectangle(bbg, new Rectangle(1, 31, DisplayManager.Width - 3, 10));
                TextRenderer.DrawText(g, balance, df, bal, cfo);
            }



            g.DrawRectangle(Pens.Black, new Rectangle(1, 1, DisplayManager.Width - 3, DisplayManager.Height - 3));
            g.DrawLine(pbg, new Point(1, 20), new Point(DisplayManager.Width - 1, 20));
            g.DrawLine(pfo, new Point(1, 21), new Point(DisplayManager.Width - 1, 21));
            g.DrawLine(pbg, new Point(1, 22), new Point(DisplayManager.Width - 1, 22));
            g.DrawRectangle(DisplayManager.Drawing.WhitePen, new Rectangle(0, 0, DisplayManager.Width - 1, DisplayManager.Height - 1));

            CurrentView = bmp;
            if (m_sleep == 0)
                DisplayManager.Display(bmp);
            
        }

        private static void DownloadAttemptCompleted(object sender, GrandCharacterInfo.DownloadAttemptCompletedEventArgs oldskill)
        {
            if (oldskill.Complete)
            {
                Font displayFont = new Font("Tahoma", 7f, FontStyle.Regular);
                Bitmap bmp = DisplayManager.CreateG15Bitmap();
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(global::EVEMon.Properties.Resources.evemon_g15_sc, new Point(0, 0));
                TextRenderer.DrawText(g, oldskill.CharacterName, displayFont, new Rectangle(1, 21, DisplayManager.Width - 3, 10), Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                TextRenderer.DrawText(g, "completed " + oldskill.SkillName, displayFont, new Rectangle(6, 31, DisplayManager.Width - 3, 10), Color.Black, TextFormatFlags.Left | TextFormatFlags.NoClipping | TextFormatFlags.NoPadding);
                m_sleep = 3000;
                CurrentView = bmp;
                DisplayManager.Display(bmp, LCDImagePriority.Alert);
            }

        }

        private static void Tick()
        {
            if (((TimeSpan)DateTime.Now.Subtract(lastCharUpdate)).TotalSeconds > 10)
            {
                CharListUpdate();
            }
            Update();
        }

        private static void G15ThreadMain()
        {
            try
            {
                while (true)
                {
                    Tick();
                    if (m_sleep > 0)
                    {
                        Thread.Sleep(m_sleep);
                        m_sleep = 0;
                    }
                    else
                        Thread.Sleep((int)Program.Settings.G15UpdateSpeed.TotalMilliseconds);
                }
            }
            catch(ThreadAbortException )
            {
                
            }
        }


        public static void Init()
        {
            if (Program.Settings.G15UseG15Display == true)
            {
                DisplayManager.Init("EVEMon", false, true);
                DisplayManager.OnConfigRequest += new ConfigHandler(DisplayManager_OnConfigRequest);
                DisplayManager.OnButton1Down += new ButtonHandler(DisplayManager_OnButton1Down);
                DisplayManager.OnButton2Down += new ButtonHandler(DisplayManager_OnButton2Down);
                DisplayManager.OnButton3Down += new ButtonHandler(DisplayManager_OnButton3Down);
                DisplayManager.OnButton4Down += new ButtonHandler(DisplayManager_OnButton4Down);


                Splash();
                StartThread();

                Console.WriteLine("Interval: {0}", Program.Settings.G15Interval);
                Console.WriteLine("SkillInterval: {0}", Program.Settings.G15SkillInterval);
                Console.WriteLine("Static: {0}", Program.Settings.G15Static);
                Console.WriteLine("SkillStatic: {0}", Program.Settings.G15StaticSkill);
                Console.WriteLine("UpdateSpeed: {0}", Program.Settings.G15UpdateSpeed);
            }
        }

        public static void DisplayManager_OnButton4Down()
        {
            m_view++;
            m_tick = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
            //throw new Exception("The method or operation is not implemented.");
        }

        public static void DisplayManager_OnButton3Down()
        {
            m_view--;
            m_tick = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
            //throw new Exception("The method or operation is not implemented.");
        }

        public static void DisplayManager_OnButton2Down()
        {
            m_sview++;
            m_stick = (int)TimeSpan.FromSeconds(1).TotalMilliseconds; 
            //throw new Exception("The method or operation is not implemented.");
        }

        public static void DisplayManager_OnButton1Down()
        {
            m_sview--;
            m_stick = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
            //throw new Exception("The method or operation is not implemented.");
        }

        private static void DisplayManager_OnConfigRequest()
        {
            using (SettingsForm sf = new SettingsForm(Program.Settings))
            {
                sf.ShowDialog();
            }
        }
        public static void Stop()
        {
            DisplayManager.Close();

            EndThread();
        }
        private static void StartThread()
        {
            if (!m_threadRunning)
            {
                m_G15Thread = new Thread(new ThreadStart(G15ThreadMain));
                m_G15Thread.Name = "G15 Update Thread";
                m_G15Thread.Start();
                m_threadRunning = true;
            }
        }
        private static void EndThread()
        {
            Splash();
            if (m_threadRunning)
            {
                m_G15Thread.Abort();
                m_threadRunning = false;
            }
            Thread.Sleep(3000);
        }
    }
}
