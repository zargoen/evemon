using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.NetworkLogger;
using EVEMon.SkillPlanner;
using EVEMon.WindowRelocator;

namespace EVEMon
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if !DEBUG
            InstanceManager im = InstanceManager.GetInstance();
            if (!im.CreatedNew)
            {
                im.Signal();
                return;
            }
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            bool startMinimized = false;

            foreach (string targ in Environment.GetCommandLineArgs())
            {
                if (targ == "-netlog")
                {
                    StartNetlog();
                    if (m_logger == null)
                    {
                        return;
                    }
                }

                if (targ == "-startMinimized")
                {
                    startMinimized = true;
                }
            }

            m_settingKey = String.Empty;

            Plan.PlannerWindowFactory = new PlannerWindowFactory();
            EveSession.MainThread = Thread.CurrentThread;
            InstallerDeleter.Schedule();
            
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Application.Run(new Form1(ca));
            s_settings = Settings.LoadFromKey(m_settingKey);
            if (s_settings.UseLogitechG15Display) {
                try
                {
                    LCD = EVEMon.LogitechG15.Lcdisplay.Instance();
                    LcdThread = new Thread(LCD.Start);
                   LcdThread.Name = "LCD Thread";
                    LcdThread.Start();
                }
                catch (Exception exx)
                {
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }
                    else
                    {
                        LcdErrorsEncountered = true;
                        if (LcdThread != null && LcdThread.IsAlive && LCD != null)
                        {
                            LCD.Stop();
                            LcdThread.Join();
                            LCD.Dispose();
                        }
                        LCD = null;
                        LcdThread = null;
                    }
                }
            }            

            Application.Run(new MainWindow(s_settings, startMinimized));
            s_settings.Save();

            SetRelocatorState(false);
            if (m_logger != null)
            {
                m_logger.Dispose();
            }
            if (LcdThread != null && LcdThread.IsAlive)
            {
                if (LCD != null)
                {
                    LCD.Stop();
                    LcdThread.Join();
                    LCD.Dispose();
                }
                LCD = null;
                LcdThread = null;
            }

        }

        private static MainWindow m_mainWindow;

        public static MainWindow MainWindow
        {
            get { return m_mainWindow; }
            set { m_mainWindow = value; }
        }

        private static IDisposable m_logger;

        private static void StartNetlog()
        {
            m_logger = Logger.StartLogging();
        }

        private static bool m_relocatorRunning = false;

        public static void SetRelocatorState(bool state)
        {
            if (!state && !m_relocatorRunning)
            {
                return;
            }
            InternalSetRelocatorState(state);
        }

        public static EVEMon.LogitechG15.Lcdisplay LCD;
        private static Thread LcdThread;
        private static bool LcdErrorsEncountered = false;
        private static void UseLogitechG15DisplayChanged(object sender, EventArgs e)
        {
            if (LcdErrorsEncountered)
                return;
            if (s_settings.UseLogitechG15Display)
            {
                LCD = EVEMon.LogitechG15.Lcdisplay.Instance();
                LcdThread = new Thread(LCD.Start);
                LcdThread.Name = "LCD Thread";
                LcdThread.Start();
                s_settings.UseLogitechG15Display = true;
                s_settings.Save();
            }
            else
            {
                if (LcdThread != null && LcdThread.IsAlive)
                {
                    if (LCD != null)
                    {
                        LCD.Stop();
                        LcdThread.Join();
                        LCD.Dispose();
                    }
                }
                LCD = null;
                LcdThread = null;
            }
        }


        private static void InternalSetRelocatorState(bool state)
        {
            if (state)
            {
                m_relocatorRunning = true;
                Relocator.Start(Settings.RelocateTargetScreen);
            }
            else
            {
                m_relocatorRunning = false;
                Relocator.Stop();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowError(e.ExceptionObject as Exception);
            Environment.Exit(1);
        }

        public static string SettingKey
        {
            get { return m_settingKey; }
        }

        public static Settings Settings
        {
            get { return s_settings; }
        }

        private static string m_settingKey;
        private static Settings s_settings;
        private static bool m_showWindowOnError = true;

        private static void ShowError(Exception e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            else if (m_showWindowOnError)
            {
                m_showWindowOnError = false;
                using (UnhandledExceptionWindow f = new UnhandledExceptionWindow(e))
                {
                    f.ShowDialog();
                }
            }
        }
    }
}
