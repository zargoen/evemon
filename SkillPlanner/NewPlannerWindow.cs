using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class NewPlannerWindow : EVEMonForm
    {
        public NewPlannerWindow()
        {
            InitializeComponent();
        }

        private Settings m_settings;
        private GrandCharacterInfo m_grandCharacterInfo;
        private Plan m_plan;

        public NewPlannerWindow(Settings s, GrandCharacterInfo gci, Plan p)
            : this()
        {
            m_settings = s;
            m_grandCharacterInfo = gci;
            m_plan = p;

            // Todo: The plan should already have it's character info set.
            m_plan.GrandCharacterInfo = m_grandCharacterInfo;

            skillBrowser.Plan = m_plan;
            skillBrowser.GrandCharacterInfo = gci;

            planEditor.Settings = m_settings;
            planEditor.Plan = m_plan;
            planEditor.PlannerWindow = this;

            shipBrowser.Plan = m_plan;
            shipBrowser.GrandCharacterInfo = m_grandCharacterInfo;

            itemBrowser.Plan = m_plan;

            // See if this is a new plan
            if (m_plan.Entries.Count == 0)
                // jump straight to the skill browser
                tabControl1.SelectedTab = tpSkillBrowser;
            else
                // Jump to the plan queue
                tabControl1.SelectedTab = tpPlanQueue;

            // Force an update
            m_settings_WorksafeChanged(null, null);
        }

        public void ShowSkillTree()
        {
            tabControl1.SelectedTab = tpSkillBrowser;
        }

        public void ShowSkillTree(GrandSkill gs)
        {
            skillBrowser.SelectedSkill = gs;
            ShowSkillTree();
        }

        private void NewPlannerWindow_Shown(object sender, EventArgs e)
        {
            m_showing = true;
        }

        private bool m_showing = false;

        private void NewPlannerWindow_Load(object sender, EventArgs e)
        {
            // Watch for changes to worksafe settings and plan changes
            m_plan.Changed += new EventHandler<EventArgs>(m_plan_Changed);
            m_settings.WorksafeChanged += new EventHandler<EventArgs>(m_settings_WorksafeChanged);

            // Set the title
            this.Text = m_grandCharacterInfo.Name + " [" + m_plan.Name + "] - EVEMon Skill Planner";

            // Show the hint tip
            TipWindow.ShowTip("planner",
                              "Welcome to the Skill Planner",
                              "Select skills to add to your plan using the list on the left. To " +
                              "view the list of skills you've added to your plan, choose " +
                              "\"View Plan\" from the dropdown in the upper left.");
            UpdateStatusBar();
        }

        private void NewPlannerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Remove event handlers
            m_plan.Changed -= new EventHandler<EventArgs>(m_plan_Changed);
            m_settings.WorksafeChanged -= new EventHandler<EventArgs>(m_settings_WorksafeChanged);

            foreach (WeakReference<ImplantCalculator> ric in m_calcWindows)
            {
                ImplantCalculator ic = ric.Target;
                if (ic != null && ic.Visible)
                {
                    ic.Close();
                }
            }
            m_calcWindows.Clear();
        }

        private void m_settings_WorksafeChanged(object sender, EventArgs e)
        {
            skillBrowser.WorksafeMode = m_settings.WorksafeMode;
        }

        private void m_plan_Changed(object sender, EventArgs e)
        {
            m_settings.Save();
            UpdateStatusBar();
        }

        private GrandSkill m_selectedSkill = null;

        private void SelectSkill(GrandSkill gs)
        {
            m_selectedSkill = gs;
            skillBrowser.SelectedSkill = gs;
        }

        private bool m_suggestionTipUp = false;

        private void UpdateStatusBar()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                                                  {
                                                      UpdateStatusBar();
                                                  }));
                return;
            }

            TimeSpan res = m_plan.GetTotalTime(null);
            slblStatusText.Text = String.Format("{0} Skill{1} Planned ({2} Unique Skill{3}). Total training time: {4}",
                                                m_plan.Entries.Count,
                                                m_plan.Entries.Count == 1 ? "" : "s",
                                                m_plan.UniqueSkillCount,
                                                m_plan.UniqueSkillCount == 1 ? "" : "s",
                                                GrandSkill.TimeSpanToDescriptiveText(res,
                                                                                     DescriptiveTextOptions.FullText |
                                                                                     DescriptiveTextOptions.
                                                                                         IncludeCommas |
                                                                                     DescriptiveTextOptions.SpaceText));

            if (m_plan.HasAttributeSuggestion)
            {
                tslSuggestion.Visible = true;
                if (m_showing && !m_suggestionTipUp)
                {
                    m_suggestionTipUp = true;
                    TipWindow.ShowTip("suggestion",
                                      "Plan Suggestion",
                                      "EVEMon has analyzed your plan and has come up with a " +
                                      "suggestion of learning skills that you can add that will " +
                                      "lower the overall training time of the plan. To view this " +
                                      "suggestion and the resulting change in plan time, click the " +
                                      "\"Suggestion\" link in the planner status bar.");
                    m_suggestionTipUp = false;
                }
            }
            else
            {
                tslSuggestion.Visible = false;
            }
        }

        /*
        private void btnPlanTo1_Click(object sender, EventArgs e)
        {
            PlanTo(1);
        }

        private void btnPlanTo2_Click(object sender, EventArgs e)
        {
            PlanTo(2);
        }

        private void btnPlanTo3_Click(object sender, EventArgs e)
        {
            PlanTo(3);
        }

        private void btnPlanTo4_Click(object sender, EventArgs e)
        {
            PlanTo(4);
        }

        private void btnPlanTo5_Click(object sender, EventArgs e)
        {
            PlanTo(5);
        }

        private void btnRemoveFromPlan_Click(object sender, EventArgs e)
        {
            RemoveFromPlan();
        }


        private void miCancelAll_Click(object sender, EventArgs e)
        {
            RemoveFromPlan();
        }

        private void miCancelThis_Click(object sender, EventArgs e)
        {
            RemoveFromPlan();
        }

        private void RemoveFromPlan()
        {
            //m_plan.RemoveEntry(m_plan.GetEntry(m_selectedSkill,5));
            UpdatePlanControl();
        }
        */

        private void tsbDeletePlan_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete this plan?",
                "Delete Plan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            m_settings.RemovePlanFor(m_grandCharacterInfo.Name, m_plan.Name);
        }

        private void tslSuggestion_Click(object sender, EventArgs e)
        {
            using (SuggestionWindow f = new SuggestionWindow(m_plan))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            // XXX: apply plan change

            // List<Plan.Entry> nonLearningEntries = new List<Plan.Entry>();
            m_plan.SuppressEvents();
            try
            {
                //foreach (Plan.Entry pe in m_plan.Entries)
                //{
                //    GrandSkill gs = pe.Skill;
                //    if (!gs.IsLearningSkill && gs.Name != "Learning")
                //        nonLearningEntries.Add(pe);
                //}
                //m_plan.ClearEntries();
                //foreach (Plan.Entry pe in nonLearningEntries)
                //{
                //    m_plan.Entries.Add(pe);
                //}
                IEnumerable<Plan.Entry> entries = m_plan.GetSuggestions();
                int i = 0;
                foreach (Plan.Entry pe in entries)
                {
                    m_plan.Entries.Insert(i, pe);
                    i++;
                }

                //Ensures any existing learning skills in the plan get moved to
                //account for higher levels being inserted at the front...
                m_plan.CheckForMissingPrerequisites();

                // Arrange the learning skills in the plan in optimal order
                PlanSorter.SortPlan(m_plan, PlanSortType.NoChange, true);
            }
            finally
            {
                m_plan.ResumeEvents();
            }
        }

        private void tsbCopyForum_Click(object sender, EventArgs e)
        {
            PlanTextOptions pto = (PlanTextOptions) m_settings.DefaultCopyOptions.Clone();
            using (CopySaveOptionsWindow f = new CopySaveOptionsWindow(pto, m_plan, true))
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.Cancel)
                {
                    return;
                }
                if (f.SetAsDefault)
                {
                    m_settings.DefaultCopyOptions = pto;
                    m_settings.Save();
                }
            }

            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                m_plan.SaveAsText(sw, pto, true);
                sw.Flush();
                string s = Encoding.Default.GetString(ms.ToArray());
                Clipboard.SetText(s);
            }

            MessageBox.Show("The skill plan has been copied to the clipboard in a " +
                            "format suitable for forum posting.", "Plan Copied", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private enum SaveType
        {
            None = 0,
            Emp = 1,
            Xml = 2,
            Text = 3
        }

        private void tsbSaveAs_Click(object sender, EventArgs e)
        {
            sfdSave.FileName = m_plan.GrandCharacterInfo.Name + " Skill Plan";
            sfdSave.FilterIndex = (int) SaveType.Emp;
            DialogResult dr = sfdSave.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            string fileName = sfdSave.FileName;
            try
            {
                PlanTextOptions pto = null;
                if ((SaveType) sfdSave.FilterIndex == SaveType.Text)
                {
                    pto = (PlanTextOptions) m_settings.DefaultSaveOptions.Clone();
                    using (CopySaveOptionsWindow f = new CopySaveOptionsWindow(pto, m_plan, false))
                    {
                        f.ShowDialog();
                        if (f.DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                        if (f.SetAsDefault)
                        {
                            m_settings.DefaultSaveOptions = pto;
                            m_settings.Save();
                        }
                    }
                }

                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    switch ((SaveType) sfdSave.FilterIndex)
                    {
                        case SaveType.Emp:
                            using (GZipStream gzs = new GZipStream(fs, CompressionMode.Compress))
                            {
                                SerializePlanTo(gzs);
                            }
                            break;
                        case SaveType.Xml:
                            SerializePlanTo(fs);
                            break;
                        case SaveType.Text:
                            SaveAsText(fs, pto);
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (IOException err)
            {
                ExceptionHandler.LogException(err, true);
                MessageBox.Show("There was an error writing out the file:\n\n" + err.Message,
                                "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SerializePlanTo(Stream s)
        {
            XmlSerializer xs = new XmlSerializer(typeof (Plan));
            xs.Serialize(s, m_plan);
        }

        private void SaveAsText(Stream fs, PlanTextOptions pto)
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                m_plan.SaveAsText(sw, pto, false);
            }
        }

        private List<WeakReference<ImplantCalculator>> m_calcWindows = new List<WeakReference<ImplantCalculator>>();

        private void tsbImplantCalculator_Click(object sender, EventArgs e)
        {
            // Remove closed windows
            for (int i = 0; i < m_calcWindows.Count; i++)
            {
                bool needRemove = true;
                ImplantCalculator thisIc = m_calcWindows[i].Target;
                if (thisIc != null)
                {
                    needRemove = !thisIc.Visible;
                }
                if (needRemove)
                {
                    m_calcWindows.RemoveAt(i);
                    i--;
                }
            }

            ImplantCalculator ic = new ImplantCalculator(m_grandCharacterInfo, m_plan);
            m_calcWindows.Add(new WeakReference<ImplantCalculator>(ic));

            ic.Show();
        }

    }

    public class PlannerWindowFactory : IPlannerWindowFactory
    {
        #region IPlannerWindowFactory Members
        public Form CreateWindow(Settings s, GrandCharacterInfo gci, Plan p)
        {
            return new NewPlannerWindow(s, gci, p);
        }
        #endregion
    }
}
