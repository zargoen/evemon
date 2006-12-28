using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class SkillBrowser : UserControl
    {
        public SkillBrowser()
        {
            InitializeComponent();
            this.splitContainer2.RememberDistanceKey = "SkillBrowser";
        }

        public CharacterInfo GrandCharacterInfo
        {
//            get { return m_grandCharacterInfo; }
            set
            {
//                m_grandCharacterInfo = value;
                skillSelectControl.GrandCharacterInfo = value;
            }
        }

        private Plan m_plan;
        public Plan Plan
        {
            get { return m_plan; }
            set 
            {
                m_plan = value;
                skillTreeDisplay.Plan = value;
                skillSelectControl.Plan = value;
            }
        }

        public bool WorksafeMode
        {
            set {
                //skillSelectControl.WorksafeMode = value;
                skillTreeDisplay.WorksafeMode = value;
            }
        }

        Skill m_selectedSkill;
        public Skill SelectedSkill
        {
            get { return m_selectedSkill; }
            set
            {
                m_selectedSkill = value;
                skillTreeDisplay.RootSkill = value;
                UpdatePlanControl();
            }
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

        // Update the items in the 'Plan To' Combobox
        private void UpdatePlanSelect()
        {
            PlanSelectShowing thisPss = PlanSelectShowing.None;
            int plannedTo = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (m_selectedSkill.Level < i)
                {
                    int x = 1 << i;
                    thisPss = (PlanSelectShowing)((int)thisPss + x);
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

        // react to changes in the 'Plan To' Combobox
        private void cbPlanSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = (string)cbPlanSelect.Items[cbPlanSelect.SelectedIndex];
            int setLevel = 0;
            if (s.StartsWith("Level "))
            {
                string r = s.Substring(6);
                setLevel = Skill.GetIntForRoman(r);
            }
            m_planSelectSelected = setLevel;
            PlanTo(setLevel);
        }

        public void UpdatePlanControl()
        {
            /// TODO: fix the update of the entire control
            //if (planEditor.Visible)
            //{
            //    UpdateStatusBar();
            //    return;
            //}

            if (m_selectedSkill == null)
            {
                pnlPlanControl.Visible = false;
                skillTreeDisplay.Visible = false;
            }
            else
            {
                lblSkillClass.Text = m_selectedSkill.SkillGroup.Name;
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
                skillTreeDisplay.Visible = true;
            }

            /// TODO fix the update of the entire control
            //Parent.UpdateStatusBar();
        }

        private bool SetPlanLabel(Label label, int level)
        {
            bool isPlanned = m_plan.IsPlanned(m_selectedSkill, level);
            StringBuilder sb = new StringBuilder();
            sb.Append("Level ");
            sb.Append(Skill.GetRomanForInt(level));
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
                sb.Append(Skill.TimeSpanToDescriptiveText(tts, DescriptiveTextOptions.IncludeCommas));
      
                TimeSpan prts = m_selectedSkill.GetTrainingTimeToLevel(level - 1) +
                                m_selectedSkill.GetPrerequisiteTime();
                if (prts > TimeSpan.Zero)
                {
                    sb.Append(" (plus ");
                    sb.Append(Skill.TimeSpanToDescriptiveText(prts, DescriptiveTextOptions.IncludeCommas));
                    sb.Append(")");
                }
                else
                {
                    // we're displaying the next level to train - show %complete
                     if (m_selectedSkill.Level != 5)
                    {
                        
                        double percentDone = m_selectedSkill.GetPercentDone();
                        sb.Append(String.Format(" ({0} complete)", percentDone.ToString("P0")));
                    }
                }
                //button.Enabled = !isPlanned;
                //if (m_selectedSkill.InTraining && m_selectedSkill.TrainingToLevel == level)
                //    button.Enabled = false;
            }
            label.Text = sb.ToString();
            return isPlanned;
        }

        private void tmrSkillTick_Tick(object sender, EventArgs e)
        {
            UpdatePlanControl();
        }

        private void skillSelectControl_SelectedSkillChanged(object sender, EventArgs e)
        {
            SelectedSkill = skillSelectControl.SelectedSkill;
        }

        // React to clicks in the Skill Tree panel
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

        private bool SetMenuItemState(ToolStripMenuItem mi, Skill gs, int level)
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
                    mi.Text = "Plan to Level " + Skill.GetRomanForInt(level) + " (Already Planned)";
                    mi.Enabled = false;
                    return true;
                }
                else
                {
                    TimeSpan ts = gs.GetPrerequisiteTime() + gs.GetTrainingTimeToLevel(level);
                    mi.Text = "Plan to Level " + Skill.GetRomanForInt(level) + " (" +
                              Skill.TimeSpanToDescriptiveText(ts, DTO_OPTS) + ")";
                    mi.Enabled = true;
                }
            }
            else
            {
                if (isKnown)
                {
                    mi.Text = "Plan to Level " + Skill.GetRomanForInt(level) + " (Already Known)";
                    mi.Enabled = false;
                }
                else if (isTraining)
                {
                    mi.Text = "Plan to Level " + Skill.GetRomanForInt(level) + " (Currently Training)";
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

        #region Replace all this with calls to Plan.PlanTo()
        private bool ShouldAdd(Skill gs, int level, IEnumerable<Plan.Entry> list, string Note)
        {
            if (gs.Level < level && !m_plan.IsPlanned(gs, level))
            {
                foreach (Plan.Entry pe in list)
                {
                    if (pe.SkillName == gs.Name)
                    {
                        // If we don't have a note, use the one provided
                        if (Note != "" && !pe.Notes.Contains(Note))
                            pe.Notes = pe.Notes + ", " + Note;
                        if (pe.Level == level)
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

            List<Plan.Entry> planEntries = new List<Plan.Entry>();
            string Note = m_selectedSkill.Name;
            AddPrerequisiteEntries(m_selectedSkill, planEntries,Note);
            for (int i = 1; i <= level; i++)
            {
                if (ShouldAdd(m_selectedSkill, i, planEntries, Note))
                {
                    Plan.Entry pe = new Plan.Entry();
                    pe.SkillName = m_selectedSkill.Name;
                    pe.Notes = Note;
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

        private void AddPrerequisiteEntries(Skill gs, List<Plan.Entry> planEntries, string Note)
        {
            foreach (Skill.Prereq pp in gs.Prereqs)
            {
                Skill pgs = pp.Skill;
                AddPrerequisiteEntries(pgs, planEntries, Note);
                for (int i = 1; i <= pp.RequiredLevel; i++)
                {
                    if (ShouldAdd(pgs, i, planEntries, Note))
                    {
                        Plan.Entry pe = new Plan.Entry();
                        pe.SkillName = pgs.Name;
                        pe.Level = i;
                        pe.Notes = Note;
                        pe.EntryType = Plan.Entry.Type.Prerequisite;
                        planEntries.Add(pe);
                    }
                }
            }
        }
        #endregion Replace all this with calls to Plan.PlanTo()

    }
}
