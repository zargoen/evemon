using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class AddPlanConfirmWindow : EVEMonForm
    {
        public AddPlanConfirmWindow()
        {
            InitializeComponent();
        }

        public AddPlanConfirmWindow(IEnumerable<Plan.Entry> entries)
            : this()
        {
            m_entries = entries;
        }

        private IEnumerable<Plan.Entry> m_entries;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AddPlanConfirmWindow_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Plan.Entry pe in m_entries)
            {
                string m_skill = pe.SkillName + " " + GrandSkill.GetRomanForInt(pe.Level);
                if (pe.AddNoteonly)
                {
                    m_skill = m_skill + " (planned)";
                }
                listBox1.Items.Add(m_skill);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private static bool ShouldAdd(Plan p, GrandSkill gs, int level, IEnumerable<Plan.Entry> list, string Note)
        {
            if (gs.Level < level && !p.IsPlanned(gs, level))
            {
                foreach (Plan.Entry pe in list)
                {
                    if (pe.SkillName == gs.Name)
                    {
                        if (Note != "" && !pe.Notes.Contains(Note))
                        {
                            pe.Notes = pe.Notes + ", " + Note;
                        }
                        if (pe.Level == level)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private static void AddPrerequisiteEntries(Plan p, GrandSkill gs, List<Plan.Entry> planEntries, string Note)
        {
            foreach (GrandSkill.Prereq pp in gs.Prereqs)
            {
                GrandSkill pgs = pp.Skill;
                AddPrerequisiteEntries(p, pgs, planEntries, Note);
                for (int i = 1; i <= pp.RequiredLevel; i++)
                {
                    if (p.IsPlanned(pgs, i))
                    {
                        Plan.Entry pe = new Plan.Entry();
                        pe.SkillName = pgs.Name;
                        pe.Level = i;
                        pe.Notes = Note;
                        pe.AddNoteonly = true;
                        pe.EntryType = Plan.Entry.Type.Prerequisite;
                        planEntries.Add(pe);
                    } 
                    else if (ShouldAdd(p, pgs, i, planEntries,Note))
                    {
                        Plan.Entry pe = new Plan.Entry();
                        pe.SkillName = pgs.Name;
                        pe.Level = i;
                        pe.AddNoteonly = false;
                        pe.Notes = Note;
                        pe.EntryType = Plan.Entry.Type.Prerequisite;
                        planEntries.Add(pe);
                    }
                }
            }
        }

        private static void ConfirmSkillAdd(Plan p, List<Plan.Entry> planEntries)
        {
            using (AddPlanConfirmWindow f = new AddPlanConfirmWindow(planEntries))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    p.AddList(planEntries);
                }
            }
        }

        public static void AddSkillsWithConfirm(Plan p, IEnumerable<Pair<string, int>> skillsToAdd,string Note)
        {
            List<Plan.Entry> planEntries = new List<Plan.Entry>();
            foreach (Pair<string, int> ts in skillsToAdd)
            {
                GrandSkill gs = p.GrandCharacterInfo.GetSkill(ts.A);
                if (ShouldAdd(p, gs, ts.B, planEntries, Note))
                {
                    AddPrerequisiteEntries(p, gs, planEntries, Note);
                    for (int i = 1; i <= ts.B; i++)
                    {
                        if (p.IsPlanned(gs, i))
                        {
                            Plan.Entry pe = new Plan.Entry();
                            pe.SkillName = gs.Name;
                            pe.Level = i;
                            pe.Notes = Note;
                            pe.AddNoteonly = true;
                            pe.EntryType = (i == ts.B) ? Plan.Entry.Type.Planned : Plan.Entry.Type.Prerequisite;
                            planEntries.Add(pe);
                        }
                        else if (ShouldAdd(p, gs, i, planEntries, Note))
                        {
                            Plan.Entry pe = new Plan.Entry();
                            pe.SkillName = gs.Name;
                            pe.Level = i;
                            pe.Notes = Note;
                            pe.EntryType = (i == ts.B) ? Plan.Entry.Type.Planned : Plan.Entry.Type.Prerequisite;
                            planEntries.Add(pe);
                        }
                    }
                }
            }
            if (planEntries.Count > 0)
            {
                ConfirmSkillAdd(p, planEntries);
            }
            else
            {
                MessageBox.Show("All the required skills are already in your plan.",
                                "Already Planned", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
