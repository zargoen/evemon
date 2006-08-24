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

            // Pass all settings, plan and character info to the child controls
            skillTreeDisplay.Plan = m_plan;

            skillSelectControl.GrandCharacterInfo = m_grandCharacterInfo;
            skillSelectControl.Plan = m_plan;

            planEditor.Settings = m_settings;

            planEditor.Plan = m_plan;

            shipBrowser.Plan = m_plan;
            shipBrowser.GrandCharacterInfo = m_grandCharacterInfo;

            itemBrowser.Plan = m_plan;

            // See if this is a new plan
            if (m_plan.Entries.Count == 0)
                // jump straight to the skill browser, otherwis
                tabControl1.SelectedTab = tpSkillBrowser;
            else
                // Jump to the plan queue
                tabControl1.SelectedTab = tpPlanQueue;

            // Force an update
            m_settings_WorksafeChanged(null, null);
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

            // Update the control
            UpdatePlanControl();
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
            skillTreeDisplay.WorksafeMode = m_settings.WorksafeMode;
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
            lblSkillClass.Text = gs.SkillGroup.Name;
            skillTreeDisplay.RootSkill = m_selectedSkill;

            UpdatePlanControl();
        }

        [Flags]
        private enum PlanSelectShowing
        {
            None = 1,
            One = 2,
            Two = 4,
            Three = 8,
            Four = 16,
            Five = 32
        }

        private PlanSelectShowing m_planSelectShowing;
        private int m_planSelectSelected;

        private void UpdatePlanSelect()
        {
            PlanSelectShowing thisPss = PlanSelectShowing.None;
            int plannedTo = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (m_selectedSkill.Level < i)
                {
                    int x = 1 << i;
                    thisPss = (PlanSelectShowing) ((int) thisPss + x);
                }
                if (m_plan.IsPlanned(m_selectedSkill, i))
                {
                    plannedTo = i;
                }
            }
            if (thisPss != m_planSelectShowing || plannedTo != m_planSelectSelected)
            {
                cbPlanSelect.SelectedIndexChanged -= new EventHandler(cbPlanSelect_SelectedIndexChanged);

                cbPlanSelect.Items.Clear();
                if ((thisPss & PlanSelectShowing.None) != 0)
                {
                    cbPlanSelect.Items.Add("Not Planned");
                    if (plannedTo == 0)
                    {
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                    }
                }
                if ((thisPss & PlanSelectShowing.One) != 0)
                {
                    cbPlanSelect.Items.Add("Level I");
                    if (plannedTo == 1)
                    {
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                    }
                }
                if ((thisPss & PlanSelectShowing.Two) != 0)
                {
                    cbPlanSelect.Items.Add("Level II");
                    if (plannedTo == 2)
                    {
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                    }
                }
                if ((thisPss & PlanSelectShowing.Three) != 0)
                {
                    cbPlanSelect.Items.Add("Level III");
                    if (plannedTo == 3)
                    {
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                    }
                }
                if ((thisPss & PlanSelectShowing.Four) != 0)
                {
                    cbPlanSelect.Items.Add("Level IV");
                    if (plannedTo == 4)
                    {
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                    }
                }
                if ((thisPss & PlanSelectShowing.Five) != 0)
                {
                    cbPlanSelect.Items.Add("Level V");
                    if (plannedTo == 5)
                    {
                        cbPlanSelect.SelectedIndex = cbPlanSelect.Items.Count - 1;
                    }
                }

                m_planSelectShowing = thisPss;
                m_planSelectSelected = plannedTo;
                cbPlanSelect.SelectedIndexChanged += new EventHandler(cbPlanSelect_SelectedIndexChanged);
            }
        }

        private void cbPlanSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = (string) cbPlanSelect.Items[cbPlanSelect.SelectedIndex];
            int setLevel = 0;
            if (s.StartsWith("Level "))
            {
                string r = s.Substring(6);
                setLevel = GrandSkill.GetIntForRoman(r);
            }
            m_planSelectSelected = setLevel;
            PlanTo(setLevel);
        }

        private void UpdatePlanControl()
        {
            if (planEditor.Visible)
            {
                UpdateStatusBar();
                return;
            }

            if (m_selectedSkill == null)
            {
                pnlPlanControl.Visible = false;
            }
            else
            {
                lblSkillName.Text = m_selectedSkill.Name + " (" + m_selectedSkill.Rank + ")";
                textboxDescription.Text = m_selectedSkill.Description;
                lblAttributes.Text = "Primary: " + m_selectedSkill.PrimaryAttribute.ToString() + ", " +
                                     "Secondary: " + m_selectedSkill.SecondaryAttribute.ToString();

                //int plannedTo = 0;
                bool anyPlan = false;
                bool tPlan;
                tPlan = SetPlanLabel(lblLevel1Time, 1);
                //if (tPlan)
                //plannedTo = 1;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel2Time, 2);
                //if (tPlan)
                //plannedTo = 2;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel3Time, 3);
                //if (tPlan)
                //plannedTo = 3;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel4Time, 4);
                //if (tPlan)
                //plannedTo = 4;
                anyPlan = anyPlan || tPlan;
                tPlan = SetPlanLabel(lblLevel5Time, 5);
                //if (tPlan)
                //plannedTo = 5;
                anyPlan = anyPlan || tPlan;
                //btnCancelPlan.Enabled = anyPlan;

                UpdatePlanSelect();

                //if (plannedTo > 0)
                //{
                //    lblPlanDescription.Text = "Currently planned to level " +
                //        GrandSkill.GetRomanForInt(plannedTo);
                //}
                //else
                //{
                //    lblPlanDescription.Text = "Not currently planned.";
                //}
                pnlPlanControl.Visible = true;
            }

            UpdateStatusBar();
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

        private bool SetPlanLabel(Label label, int level)
        {
            bool isPlanned = m_plan.IsPlanned(m_selectedSkill, level);
            StringBuilder sb = new StringBuilder();
            sb.Append("Level ");
            sb.Append(GrandSkill.GetRomanForInt(level));
            sb.Append(": ");
            if (m_selectedSkill.Level >= level)
            {
                sb.Append("Already known");
                //button.Enabled = false;
            }
            else
            {
                TimeSpan tts = m_selectedSkill.GetTrainingTimeOfLevelOnly(level);
                if (m_selectedSkill.Level == level - 1)
                {
                    tts = m_selectedSkill.GetTrainingTimeToLevel(level);
                }
                sb.Append(GrandSkill.TimeSpanToDescriptiveText(tts, DescriptiveTextOptions.IncludeCommas));
                TimeSpan prts = m_selectedSkill.GetTrainingTimeToLevel(level - 1) +
                                m_selectedSkill.GetPrerequisiteTime();
                if (prts > TimeSpan.Zero)
                {
                    sb.Append(" (plus ");
                    sb.Append(GrandSkill.TimeSpanToDescriptiveText(prts, DescriptiveTextOptions.IncludeCommas));
                    sb.Append(")");
                }
                //button.Enabled = !isPlanned;
                //if (m_selectedSkill.InTraining && m_selectedSkill.TrainingToLevel == level)
                //    button.Enabled = false;
            }
            label.Text = sb.ToString();
            return isPlanned;
        }

        private void skillTreeDisplay_SkillClicked(object sender, SkillClickedEventArgs e)
        {
            if (e.Skill == m_selectedSkill)
            {
                if (e.Button == MouseButtons.Right)
                {
                    bool isPlanned = false;
                    isPlanned = SetMenuItemState(miPlanTo1, e.Skill, 1) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo2, e.Skill, 2) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo3, e.Skill, 3) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo4, e.Skill, 4) || isPlanned;
                    isPlanned = SetMenuItemState(miPlanTo5, e.Skill, 5) || isPlanned;
                    miCancelPlanMenu.Enabled = isPlanned;
                    cmsSkillContext.Show(skillTreeDisplay, e.Location);
                }
            }
        }

        private const DescriptiveTextOptions DTO_OPTS =
            DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.UppercaseText;

        private bool SetMenuItemState(ToolStripMenuItem mi, GrandSkill gs, int level)
        {
            bool isKnown = false;
            bool isPlanned = false;
            bool isTraining = false;
            if (level <= gs.Level)
            {
                isKnown = true;
            }
            if (!isKnown)
            {
                if (gs.InTraining && gs.TrainingToLevel == level)
                {
                    isTraining = true;
                }
                else
                {
                    foreach (Plan.Entry pe in m_plan.Entries)
                    {
                        if (pe.Skill == gs && pe.Level == level)
                        {
                            isPlanned = true;
                            break;
                        }
                    }
                }
            }

            if (!isKnown && !isTraining)
            {
                if (isPlanned)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanForInt(level) + " (Already Planned)";
                    mi.Enabled = false;
                    return true;
                }
                else
                {
                    TimeSpan ts = gs.GetPrerequisiteTime() + gs.GetTrainingTimeToLevel(level);
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanForInt(level) + " (" +
                              GrandSkill.TimeSpanToDescriptiveText(ts, DTO_OPTS) + ")";
                    mi.Enabled = true;
                }
            }
            else
            {
                if (isKnown)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanForInt(level) + " (Already Known)";
                    mi.Enabled = false;
                }
                else if (isTraining)
                {
                    mi.Text = "Plan to Level " + GrandSkill.GetRomanForInt(level) + " (Currently Training)";
                    mi.Enabled = false;
                }
            }
            return false;
        }

        private void miPlanTo1_Click(object sender, EventArgs e)
        {
            PlanTo(1);
        }

        private void miPlanTo2_Click(object sender, EventArgs e)
        {
            PlanTo(2);
        }

        private void miPlanTo3_Click(object sender, EventArgs e)
        {
            PlanTo(3);
        }

        private void miPlanTo4_Click(object sender, EventArgs e)
        {
            PlanTo(4);
        }

        private void miPlanTo5_Click(object sender, EventArgs e)
        {
            PlanTo(5);
        }

        private bool ShouldAdd(GrandSkill gs, int level, IEnumerable<Plan.Entry> list)
        {
            if (gs.Level < level && !m_plan.IsPlanned(gs, level))
            {
                foreach (Plan.Entry pe in list)
                {
                    if (pe.SkillName == gs.Name && pe.Level == level)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private void PlanTo(int level)
        {
            if (level == 0)
            {
                RemoveFromPlan();
                return;
            }

            //MessageBox.Show(this, "Planning not yet implemented.", "Not Yet Implemented", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            List<Plan.Entry> planEntries = new List<Plan.Entry>();
            AddPrerequisiteEntries(m_selectedSkill, planEntries);
            for (int i = 1; i <= level; i++)
            {
                if (ShouldAdd(m_selectedSkill, i, planEntries))
                {
                    Plan.Entry pe = new Plan.Entry();
                    pe.SkillName = m_selectedSkill.Name;
                    if (i == level)
                    {
                        pe.EntryType = Plan.Entry.Type.Planned;
                        //pe.PrerequisiteForName = String.Empty;
                        //pe.PrerequisiteForLevel = -1;
                    }
                    else
                    {
                        pe.EntryType = Plan.Entry.Type.Prerequisite;
                        //pe.PrerequisiteForName = m_selectedSkill.Name;
                        //pe.PrerequisiteForLevel = level + 1;
                    }
                    pe.Level = i;
                    planEntries.Add(pe);
                }
            }
            if (planEntries.Count > 0)
            {
                ConfirmSkillAdd(planEntries);
            }
            else
            {
                for (int i = 5; i > level; i--)
                {
                    Plan.Entry pe = m_plan.GetEntry(m_selectedSkill.Name, i);
                    if (pe != null)
                    {
                        m_plan.RemoveEntry(pe);
                    }
                }
            }
            UpdatePlanControl();
        }

        private void ConfirmSkillAdd(List<Plan.Entry> planEntries)
        {
            using (AddPlanConfirmWindow f = new AddPlanConfirmWindow(planEntries))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_plan.AddList(planEntries);
                }
            }
        }

        private void AddPrerequisiteEntries(GrandSkill gs, List<Plan.Entry> planEntries)
        {
            foreach (GrandSkill.Prereq pp in gs.Prereqs)
            {
                GrandSkill pgs = pp.Skill;
                AddPrerequisiteEntries(pgs, planEntries);
                for (int i = 1; i <= pp.RequiredLevel; i++)
                {
                    if (ShouldAdd(pgs, i, planEntries))
                    {
                        Plan.Entry pe = new Plan.Entry();
                        pe.SkillName = pgs.Name;
                        pe.Level = i;
                        pe.EntryType = Plan.Entry.Type.Prerequisite;
                        //pe.PrerequisiteForName = gs.Name;
                        //pe.PrerequisiteForLevel = 1;
                        planEntries.Add(pe);
                    }
                }
            }
        }

        private void tmrSkillTick_Tick(object sender, EventArgs e)
        {
            UpdatePlanControl();
        }

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

        private void RemoveFromPlan()
        {
            //m_plan.RemoveEntry(m_plan.GetEntry(m_selectedSkill,5));
            UpdatePlanControl();
        }

        private void miCancelAll_Click(object sender, EventArgs e)
        {
            RemoveFromPlan();
        }

        private void miCancelThis_Click(object sender, EventArgs e)
        {
            RemoveFromPlan();
        }

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

        private void skillSelectControl_SelectedSkillChanged(object sender, EventArgs e)
        {
            SelectSkill(skillSelectControl.SelectedSkill);
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
