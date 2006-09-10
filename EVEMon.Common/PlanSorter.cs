using System;
using System.Collections.Generic;

namespace EVEMon.Common
{
    public enum PlanSortType
    {
        [PlanSortDescription("No Sorting", "Do not sort skills in the plan.")]
        NoChange,
        [
            PlanSortDescription("Fastest Skills First",
                "Skills with the shortest remaining training time will be ordered first.")]
        FastestFirst
    }

    public class PlanSortDescriptionAttribute : Attribute
    {
        private string m_displayText = String.Empty;
        private string m_description = String.Empty;

        public string DisplayText
        {
            get { return m_displayText; }
            set { m_displayText = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public PlanSortDescriptionAttribute(string displayText, string description)
        {
            m_displayText = displayText;
            m_description = description;
        }
    }

    public class PlanSorter
    {
        private static Pair<string, int>[] sm_idealLearningSkillOrder;

        static PlanSorter()
        {
            // Order borrowed with slight modification from
            // http://myeve.eve-online.com/ingameboard.asp?a=topic&threadID=242786&page=1#2

            List<Pair<string, int>> idealBuilder = new List<Pair<string, int>>();
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Instant Recall", i + 1));
                idealBuilder.Add(new Pair<string, int>("Analytical Mind", i + 1));
                idealBuilder.Add(new Pair<string, int>("Learning", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Instant Recall", 5));
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Eidetic Memory", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Analytical Mind", 5));
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Logic", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Learning", 5));
            for (int i = 0; i < 5; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Spatial Awareness", i + 1));
                idealBuilder.Add(new Pair<string, int>("Iron Will", i + 1));
                idealBuilder.Add(new Pair<string, int>("Empathy", i + 1));
            }
            for (int i = 0; i < 4; i++)
            {
                idealBuilder.Add(new Pair<string, int>("Focus", i + 1));
                idealBuilder.Add(new Pair<string, int>("Clarity", i + 1));
                idealBuilder.Add(new Pair<string, int>("Presence", i + 1));
            }
            idealBuilder.Add(new Pair<string, int>("Eidetic Memory", 5));
            idealBuilder.Add(new Pair<string, int>("Logic", 5));
            idealBuilder.Add(new Pair<string, int>("Focus", 5));
            idealBuilder.Add(new Pair<string, int>("Clarity", 5));
            idealBuilder.Add(new Pair<string, int>("Presence", 5));
            sm_idealLearningSkillOrder = idealBuilder.ToArray();
        }

        public static void SortPlan(Plan p, PlanSortType sortType, bool learningFirst)
        {
            PlanSorter ps = new PlanSorter(p, sortType, learningFirst);
            ps.Sort();
        }

        private Plan m_plan;
        private PlanSortType m_sortType;
        private bool m_learningFirst;

        private PlanSorter(Plan p, PlanSortType sortType, bool learningFirst)
        {
            m_plan = p;
            m_sortType = sortType;
            m_learningFirst = learningFirst;
        }

        private List<Plan.Entry> m_originalOrder = new List<Plan.Entry>();
        private List<Plan.Entry> m_skillsToInsert = new List<Plan.Entry>();

        private void Sort()
        {
            foreach (Plan.Entry pe in m_plan.Entries)
            {
                Plan.Entry xpe = (Plan.Entry)pe.Clone();
                m_originalOrder.Add(xpe);
                m_skillsToInsert.Add(xpe);
            }
            m_plan.SuppressEvents();
            try
            {
                m_plan.ClearEntries();
                if (m_learningFirst)
                {
                    ArrangeLearningSkills();
                }
                ArrangeInSortTypeOrder();
            }
            catch (Exception ex)
            {
                // Sorting failed - logg the exception
                ExceptionHandler.LogException(ex, false);
                // clear the list of entries and re-add in the original order
                m_plan.ClearEntries();
                foreach (Plan.Entry xpe in m_originalOrder)
                {
                    Plan.Entry pe = (Plan.Entry)xpe.Clone();
                    m_plan.AddEntry(pe);
                }
                // Don't use the message box here - instead rethrow the exception and let the caller display it if necessary
                throw ex;
            }
            finally
            {
                m_plan.ResumeEvents();
            }
        }

        private void ArrangeInSortTypeOrder()
        {
            switch (m_sortType)
            {
                case PlanSortType.NoChange:
                    while (m_skillsToInsert.Count > 0)
                    {
                        Plan.Entry pe = (Plan.Entry)m_skillsToInsert[0].Clone();
                        m_plan.AddEntry(pe);
                        m_skillsToInsert.RemoveAt(0);
                    }
                    break;
                case PlanSortType.FastestFirst:
                    ArrangeFastestFirst();
                    break;
            }
        }

        private void ArrangeFastestFirst()
        {
            Dictionary<string, int> trainedLevels = new Dictionary<string, int>();

            while (m_skillsToInsert.Count > 0)
            {
                TimeSpan fastestSpan = TimeSpan.MaxValue;
                Plan.Entry fastestPe = null;
                for (int i = 0; i < m_skillsToInsert.Count; i++)
                {
                    Plan.Entry thisPe = m_skillsToInsert[i];
                    GrandSkill thisSkill = m_plan.GrandCharacterInfo.GetSkill(thisPe.SkillName);
                    TimeSpan thisSpan = thisSkill.GetTrainingTimeOfLevelOnly(thisPe.Level, true);
                    if (thisSpan < fastestSpan)
                    {
                        bool canInsert = true;
                        foreach (GrandSkill.Prereq pp in thisSkill.Prereqs)
                        {
                            if (pp.Skill.Level < pp.RequiredLevel)
                            {
                                if (!trainedLevels.ContainsKey(pp.Name) ||
                                    trainedLevels[pp.Name] < pp.RequiredLevel)
                                {
                                    canInsert = false;
                                    break;
                                }
                            }
                        }
                        if (canInsert)
                        {
                            fastestSpan = thisSpan;
                            fastestPe = thisPe;
                        }
                    }
                }
                if (fastestPe == null)
                {
                    throw new ApplicationException("no suitable skill -- should never happen!");
                }

                m_plan.AddEntry((Plan.Entry)fastestPe.Clone());
                m_skillsToInsert.Remove(fastestPe);
                trainedLevels[fastestPe.SkillName] = fastestPe.Level;
            }
        }

        private void ArrangeLearningSkills()
        {
            for (int i = 0; i < sm_idealLearningSkillOrder.Length; i++)
            {
                string skillname = sm_idealLearningSkillOrder[i].A;
                int level = sm_idealLearningSkillOrder[i].B;
                for (int z = 0; z < m_skillsToInsert.Count; z++)
                {
                    if (m_skillsToInsert[z].SkillName == skillname &&
                        m_skillsToInsert[z].Level == level)
                    {
                        Plan.Entry pe = (Plan.Entry)m_skillsToInsert[z].Clone();
                        m_plan.AddEntry(pe);
                        m_skillsToInsert.RemoveAt(z);
                        break;
                    }
                }
            }
        }
    }
}
