using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    [XmlRoot("plan")]
    public class Plan
    {
        public Plan()
        {
            //m_entries = new MonitoredList<Plan.Entry>();
            m_entries.Changed += new EventHandler<ChangedEventArgs<Plan.Entry>>(Entries_Changed);
            m_entries.Cleared += new EventHandler<ClearedEventArgs<Plan.Entry>>(Entries_Cleared);
        }

        #region Members
        // Plan Name
        private string m_planName;
        [XmlIgnore]
        public string Name
        {
            get { return m_planName; }
            set { m_planName = value; }
        }

        // The Character
        private CharacterInfo m_grandCharacterInfo = null;
        [XmlIgnore]
        public CharacterInfo GrandCharacterInfo
        {
            get { return m_grandCharacterInfo; }
            set
            {
                this.SuppressEvents();
                try
                {
                    m_attributeSuggestion = null;
                    if (m_grandCharacterInfo != null)
                    {
                        m_grandCharacterInfo.SkillChanged -= new SkillChangedHandler(GrandCharacterInfo_SkillChanged);
                    }
                    m_grandCharacterInfo = value;
                    if (m_grandCharacterInfo != null)
                    {
                        m_grandCharacterInfo.SkillChanged += new SkillChangedHandler(GrandCharacterInfo_SkillChanged);

                        CheckForCompletedSkills();
                        CheckForMissingPrerequisites();
                    }
                }
                finally
                {
                    this.ResumeEvents();
                }
            }
        }

        private void GrandCharacterInfo_SkillChanged(object sender, SkillChangedEventArgs e)
        {
            CheckForCompletedSkills();
        }

        // The Plan Entries
        private MonitoredList<Plan.Entry> m_entries = new MonitoredList<Plan.Entry>();
        [XmlArrayItem("entry")]
        public MonitoredList<Plan.Entry> Entries
        {
            get { return m_entries; }
        }

        // Handle Events on the monitored list
        private void Entries_Cleared(object sender, ClearedEventArgs<Plan.Entry> e)
        {
            foreach (Plan.Entry pe in e.Items)
            {
                pe.Plan = null;
            }
        }

        // Handle Events on the monitored list
        private void Entries_Changed(object sender, ChangedEventArgs<Plan.Entry> e)
        {
            m_uniqueSkillCount = -1;
            m_attributeSuggestion = null;
            switch (e.ChangeType)
            {
                case ChangeType.Added:
                    e.Item.Plan = this;
                    break;
                case ChangeType.Removed:
                    e.Item.Plan = null;
                    break;
            }
            OnChange();
        }

        // Generate events when the plan changes
        public event EventHandler<EventArgs> Changed;
        private void OnChange()
        {
            FireEvent(delegate
                          {
                              if (Changed != null)
                              {
                                  Changed(this, new EventArgs());
                              }
                          }, "change");
        }
        #endregion Members
                
        private ColumnPreference m_columnPreference = new ColumnPreference();

        public ColumnPreference ColumnPreference
        {
            get { return m_columnPreference; }
            set { m_columnPreference = value; }
        }

        #region Event suppression
        private object m_eventLock = new object();
        private int m_suppression;
        private delegate void FireEventInvoker();
        private Queue<FireEventInvoker> m_firedEvents = new Queue<FireEventInvoker>();
        private Dictionary<string, bool> m_eventsInQueue = new Dictionary<string, bool>();

        public void SuppressEvents()
        {
            lock (m_eventLock)
            {
                m_suppression++;
            }
        }

        public void ResumeEvents()
        {
            lock (m_eventLock)
            {
                m_suppression--;
                if (m_suppression <= 0)
                {
                    m_suppression = 0;
                    while (m_firedEvents.Count > 0)
                    {
                        FireEventInvoker fei = m_firedEvents.Dequeue();
                        fei();
                    }
                    m_eventsInQueue.Clear();
                }
            }
        }

        private void FireEvent(FireEventInvoker fei, string key)
        {
            lock (m_eventLock)
            {
                if (m_suppression > 0)
                {
                    if (String.IsNullOrEmpty(key) || !m_eventsInQueue.ContainsKey(key))
                    {
                        m_firedEvents.Enqueue(fei);
                        if (!String.IsNullOrEmpty(key))
                        {
                            m_eventsInQueue.Add(key, true);
                        }
                    }
                }
                else
                {
                    fei();
                }
            }
        }
        #endregion Event suppression
        
        #region Suggestions
        private bool? m_attributeSuggestion = null;

        [XmlIgnore]
        public bool HasAttributeSuggestion
        {
            get
            {
                if (m_attributeSuggestion == null)
                {
                    CheckForAttributeSuggestion();
                }
                return m_attributeSuggestion.Value;
            }
        }

        public IEnumerable<Plan.Entry> GetSuggestions()
        {
            List<Plan.Entry> result = new List<Plan.Entry>();

            TimeSpan baseTime = GetTotalTime(null);
            CheckForTimeBenefit("Instant Recall", "Eidetic Memory", baseTime, result);
            CheckForTimeBenefit("Analytical Mind", "Logic", baseTime, result);
            CheckForTimeBenefit("Spatial Awareness", "Clarity", baseTime, result);
            CheckForTimeBenefit("Iron Will", "Focus", baseTime, result);
            CheckForTimeBenefit("Empathy", "Presence", baseTime, result);

            return result;
        }

        private void CheckForAttributeSuggestion()
        {
            if (GrandCharacterInfo == null)
            {
                m_attributeSuggestion = false;
                return;
            }

            TimeSpan baseTime = GetTotalTime(null);

            m_attributeSuggestion = CheckForTimeBenefit("Analytical Mind", "Logic", baseTime);
            if (m_attributeSuggestion == true)
            {
                return;
            }
            m_attributeSuggestion = CheckForTimeBenefit("Spatial Awareness", "Clarity", baseTime);
            if (m_attributeSuggestion == true)
            {
                return;
            }
            m_attributeSuggestion = CheckForTimeBenefit("Iron Will", "Focus", baseTime);
            if (m_attributeSuggestion == true)
            {
                return;
            }
            m_attributeSuggestion = CheckForTimeBenefit("Instant Recall", "Eidetic Memory", baseTime);
            if (m_attributeSuggestion == true)
            {
                return;
            }
            m_attributeSuggestion = CheckForTimeBenefit("Empathy", "Presence", baseTime);
        }

        private bool CheckForTimeBenefit(string skillA, string skillB, TimeSpan baseTime)
        {
            return CheckForTimeBenefit(skillA, skillB, baseTime, null);
        }

        private bool CheckForTimeBenefit(string skillA, string skillB, TimeSpan baseTime, List<Plan.Entry> entries)
        {
            Skill gsa = GrandCharacterInfo.SkillGroups["Learning"][skillA];
            Skill gsb = GrandCharacterInfo.SkillGroups["Learning"][skillB];

            TimeSpan bestTime = baseTime;
            TimeSpan addedTrainingTime = TimeSpan.Zero;
            Skill bestGs = null;
            int bestLevel = -1;
            int added = 0;
            for (int i = 0; i < 10; i++)
            {
                int level;
                Skill gs;
                if (i < 4)
                {
                    gs = gsa;
                    level = i + 1;
                }
                else if (i < 8)
                {
                    gs = gsb;
                    level = i - 3;
                }
                else if (i < 9)
                {
                    gs = gsa;
                    level = i - 3;
                }
                else
                {
                    gs = gsb;
                    level = i - 4;
                }

                if (gs.Level < level && !this.IsPlanned(gs, level))
                {
                    EveAttributeScratchpad scratchpad = new EveAttributeScratchpad();
                    scratchpad.AdjustAttributeBonus(gs.AttributeModified, added++);
                    addedTrainingTime += gs.GetTrainingTimeOfLevelOnly(level, true, scratchpad);
                    scratchpad.AdjustAttributeBonus(gs.AttributeModified, 1);

                    TimeSpan thisTime = GetTotalTime(scratchpad) + addedTrainingTime;
                    if (thisTime < bestTime)
                    {
                        bestTime = thisTime;
                        bestGs = gs;
                        bestLevel = level;
                    }
                }
            }

            if (entries != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    int level;
                    Skill gs;
                    if (i < 4)
                    {
                        gs = gsa;
                        level = i + 1;
                    }
                    else if (i < 8)
                    {
                        gs = gsb;
                        level = i - 3;
                    }
                    else if (i < 9)
                    {
                        gs = gsa;
                        level = i - 3;
                    }
                    else
                    {
                        gs = gsb;
                        level = i - 4;
                    }
                    if (gs.Level < level && !this.IsPlanned(gs, level))
                    {
                        if ((level < 5 && ((level <= bestLevel && gs == bestGs) || (bestGs == gsb && gs == gsa))) ||
                            ((level == 5 && bestLevel == 5) && ((gs == bestGs) || (bestGs == gsb && gs == gsa))))
                        {
                            Plan.Entry pe = new Plan.Entry();
                            pe.SkillName = gs.Name;
                            pe.Level = level;
                            pe.EntryType = Plan.Entry.Type.Planned;
                            entries.Add(pe);
                        }
                    }
                }
            }

            return (bestGs != null);
        }
        #endregion Suggestions

        #region Statistics
        [XmlIgnore]
        public TimeSpan TotalTrainingTime
        {
            get { return GetTotalTime(null); }
        }

        public TimeSpan GetTotalTime(EveAttributeScratchpad scratchpad)
        {
            TimeSpan ts = TimeSpan.Zero;
            if (scratchpad == null)
            {
                scratchpad = new EveAttributeScratchpad();
            }
            foreach (Plan.Entry pe in m_entries)
            {
                if (pe.Skill == null)
                {
                    continue;
                }
                ts += pe.Skill.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad);
                scratchpad.ApplyALevelOf(pe.Skill);
            }
            return ts;
        }

        private int m_uniqueSkillCount = -1;

        [XmlIgnore]
        public int UniqueSkillCount
        {
            get
            {
                if (m_uniqueSkillCount == -1)
                {
                    CountUniqueSkills();
                }
                return m_uniqueSkillCount;
            }
        }

        private void CountUniqueSkills()
        {
            Dictionary<string, bool> counted = new Dictionary<string, bool>();
            m_uniqueSkillCount = 0;
            foreach (Plan.Entry pe in m_entries)
            {
                if (!counted.ContainsKey(pe.SkillName))
                {
                    m_uniqueSkillCount++;
                    counted[pe.SkillName] = true;
                }
            }
        }
        #endregion Statistics


        public void CheckForMissingPrerequisites()
        {
            this.SuppressEvents();
            try
            {
                for (int i = 0; i < m_entries.Count; i++)
                {
                    bool jumpBack = false;
                    Plan.Entry pe = m_entries[i];
                    Skill gs = pe.Skill;
                    //skill has been removed
                    if (gs == null)
                    {
                        this.RemoveEntry(pe);
                        return;
                    }

                    foreach (Skill.Prereq pp in gs.Prereqs)
                    {
                        Skill pgs = pp.Skill;
                        int prIndex = GetIndexOf(pgs.Name, pp.RequiredLevel);
                        if (prIndex == -1 && pgs.Level < pp.RequiredLevel)
                        {
                            // Not in the plan, and needs to be trained...
                            Plan.Entry npe = new Plan.Entry();
                            npe.SkillName = pgs.Name;
                            npe.Level = pp.RequiredLevel;
                            npe.EntryType = Plan.Entry.Type.Prerequisite;
                            m_entries.Insert(i, npe);
                            jumpBack = true;
                            i -= 1;
                            break;
                        }
                        else if (prIndex > i)
                        {
                            // In the plan, but in invalid order...
                            Plan.Entry mve = m_entries[prIndex];
                            m_entries.RemoveAt(prIndex);
                            m_entries.Insert(i, mve);
                            jumpBack = true;
                            i -= 1;
                            break;
                        }
                    }
                    if (!jumpBack)
                    {
                        if (pe.Level - 1 > gs.Level)
                        {
                            // The previous level is needed in the plan...
                            int prIndex = GetIndexOf(gs.Name, pe.Level - 1);
                            if (prIndex == -1)
                            {
                                // ...it's not in the plan.
                                Plan.Entry ppe = new Plan.Entry();
                                ppe.SkillName = gs.Name;
                                ppe.Level = pe.Level - 1;
                                m_entries.Insert(i, ppe);
                                jumpBack = true;
                                i -= 1;
                            }
                            else if (prIndex > i)
                            {
                                // ..it's in the plan, but it's after this.
                                Plan.Entry mve = m_entries[prIndex];
                                m_entries.RemoveAt(prIndex);
                                m_entries.Insert(i, mve);
                                jumpBack = true;
                                i -= 1;
                            }
                        }
                    }
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        private int GetIndexOf(string skillName, int level)
        {
            for (int i = 0; i < m_entries.Count; i++)
            {
                Plan.Entry pe = m_entries[i];
                if (pe.SkillName == skillName && pe.Level == level)
                {
                    return i;
                }
            }
            return -1;
        }

        private void CheckForCompletedSkills()
        {
            this.SuppressEvents();
            try
            {
                for (int i = 0; i < m_entries.Count; i++)
                {
                    Plan.Entry pe = m_entries[i];
                    Skill gs = GrandCharacterInfo.GetSkill(pe.SkillName);
                    if (gs == null || pe.Level > 5 || pe.Level < 1)
                    {
                        try
                        {
                            this.RemoveEntry(pe);
                            return;
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException("The plan contains " + pe.SkillName + ", which is an unknown skill");
                        }
                    }
                    if (gs.LastConfirmedLvl >= pe.Level)
                    {
                        m_entries.RemoveAt(i);
                        i--;
                    }
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public bool IsPlanned(Skill gs)
        {
            foreach (Plan.Entry pe in m_entries)
            {
                if (pe.Skill == gs)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsPlanned(Skill gs, int level)
        {
            foreach (Plan.Entry pe in m_entries)
            {
                if (pe.SkillName == gs.Name && pe.Level == level)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Used to build a list of prerequites and preceding levels of a skill when adding a new skill
        /// to the plan.  As we check for prereqs and previous levels of skill, we build up a candidate
        /// list of plan entries. 
        /// 
        /// This method firstly ascertains that this Skill is neither already trained or already planned.
        /// Then we examine the candidate list of skills that have been added so far, if any previous levels
        /// of this skill are in the candidate list and we have passed in a Note,
        /// then append the new note to those previous levels of this skill in the candiates list.
        /// </summary>
        /// <param name="gs">The Skill we're adding</param>
        /// <param name="level">And the level we want to add</param>
        /// <param name="list">The working candidate list of skills that are going to be added prior to this. NB The Note field of the entries in this list can be modified in this method.</param>
        /// <param name="Note">If the skill should be added, then append the note to any previous levels of this skill that are in the candidate list</param>
        /// <returns>true if the skill should be added to the candidate list or false if not</returns>
        
        private bool ShouldAdd(Skill gs, int level, IEnumerable<Plan.Entry> list, string Note)
        {
            // check that the current level is less than the planned level and not planned
            if (gs.Level < level && !IsPlanned(gs, level))
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

        /// <summary>
        /// See PlanTo(Skill,level,Note)
        /// </summary>
        /// <param name="gs"></param>
        /// <param name="level"></param>
        public void PlanTo(Skill gs, int level)
        {
            PlanTo(gs, level, gs.Name);
        }

        /// <summary>
        /// Check if the plan contains the given Skill at the specified level.
        /// There are 3 outcomes:
        /// 1 If  the skill is trained or planned to that level already, do nothing.
        /// 2 If the skill is not trained or planned to the requested level then add the skill and 
        /// any prerequisites.
        /// 3 If the skill is already planned to a higher level than requested, then remove the higher level plan entries.
        /// For outcomes 1 and 2, the final skill level is tagged as "Planned" and previous levels and prereq skills are tagged as "Prerequisites"
        /// For outcome 3, the level we want to plan to is changed from "Preqreq" to "Planned"
        /// </summary>
        /// <param name="gs">The skill we want to plan</param>
        /// <param name="level">The level we want to train to</param>
        /// <param name="note">The reason we want to train this skill</param>
        public void PlanTo(Skill gs, int level,string note)
        {
            if (level == 0)
            {
                //RemoveFromPlan();
                RemoveEntry(GetEntry(gs.Name, level));
                return;
            }

            // Build up a list of any prereqs a previous levels
            List<Plan.Entry> planEntries = new List<Plan.Entry>();

            // First of all, add any untrained prerequisutes this skill needs
            AddPrerequisiteEntries(gs, planEntries, note);

            // Add all levels of this skill from 1 to required level
            for (int i = 1; i <= level; i++)
            {
                // Determine if this level of skill should be added to the plan entries
                if (ShouldAdd(gs, i, planEntries, note))
                {
                    Plan.Entry pe = new Plan.Entry();
                    pe.SkillName = gs.Name;
                    pe.Notes = note;
                    if (i == level)
                        pe.EntryType = Plan.Entry.Type.Planned;
                    else
                        pe.EntryType = Plan.Entry.Type.Prerequisite;

                    pe.Level = i;
                    planEntries.Add(pe);
                }
            }

            // OK we've examined the prereqs and previous levels for the skill...
            // See if we need to add or remove entries
            if (planEntries.Count > 0)
            {
                // we're increasing the level, so add the entries
                AddList(planEntries);
            }
            else
            {
                // There's nothing to add, so we know the skill at this level...
                // remove all levels of the skill higher than the one planned
                for (int i = 5; i > level; i--)
                {
                    Plan.Entry pe = GetEntry(gs.Name, i);
                    if (pe != null)
                        RemoveEntry(pe);
                }
            }

            // Now make the planned level a planned rather than a pre-req skill
            {
                Plan.Entry pe = GetEntry(gs.Name, level);
                pe.EntryType = Entry.Type.Planned;
            }
        }

        public void AddList(List<Plan.Entry> planEntries)
        {
            this.SuppressEvents();
            try
            {
                foreach (Plan.Entry pe in planEntries)
                {
                    if (pe.AddNoteonly)
                    {
                        Plan.Entry pn = GetEntry(pe.SkillName, pe.Level);
                        if (pn != null )
                         if (!pn.Notes.Contains(pe.Notes))
                        {
                            if (pn.Notes != "") pn.Notes = pn.Notes + ", ";
                            pn.Notes = pn.Notes + pe.Notes;
                            m_entries.ForceUpdate(pn, ChangeType.Added);
                        }
                    }
                    else
                    {
                        m_entries.Add(pe);
                    }
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public bool RemoveEntry(Plan.Entry pe)
        {
            this.SuppressEvents();
            try
            {
                //see if it's a "no longer available" skill - this fix for Squadron Command in 1.1.6 by Anders
                if (pe.Skill == null)
                {
                    m_entries.Remove(pe);
                    return true;
                }
                foreach (Plan.Entry tpe in m_entries)
                {
                    Skill tSkill = tpe.Skill;
                    int thisMinNeeded;
                    if (pe.Skill == tpe.Skill && tpe.Level > pe.Level)
                    {
                        return false;
                    }
                    if (tSkill.HasAsPrerequisite(pe.Skill, out thisMinNeeded))
                    {
                        if (thisMinNeeded >= pe.Level)
                        {
                            return false;
                        }
                    }
                }
                m_entries.Remove(pe);
                return true;
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public bool RemoveEntry(Skill gs, bool removePrerequisites, bool nonPlannedOnly)
        {
            this.SuppressEvents();
            try
            {
                int minNeeded = 0;
                // Verify nothing else in the plan requires this...
                foreach (Plan.Entry pe in m_entries)
                {
                    Skill tSkill = pe.Skill;
                    int thisMinNeeded;
                    if (tSkill.HasAsPrerequisite(gs, out thisMinNeeded))
                    {
                        if (thisMinNeeded == 5) // All are needed, fail now
                        {
                            return false;
                        }
                        if (thisMinNeeded > minNeeded)
                        {
                            minNeeded = thisMinNeeded;
                        }
                    }
                }
                // Remove this skill...
                bool anyRemoved = false;
                for (int i = 0; i < m_entries.Count; i++)
                {
                    Plan.Entry tpe = m_entries[i];
                    bool canRemove = (nonPlannedOnly && tpe.EntryType == Plan.Entry.Type.Prerequisite) || !nonPlannedOnly;
                    if (tpe.Skill == gs && tpe.Level > minNeeded && canRemove)
                    {
                        anyRemoved = true;
                        m_entries.RemoveAt(i);
                        i--;
                    }
                }
                if (!anyRemoved)
                {
                    return false;
                }
                if (!removePrerequisites)
                {
                    return true;
                }

                // Remove prerequisites
                foreach (Skill.Prereq pp in gs.Prereqs)
                {
                    RemoveEntry(pp.Skill, true, true);
                }
                return true;
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public Plan.Entry GetEntry(string name, int level)
        {
            foreach (Plan.Entry pe in m_entries)
            {
                if (pe.SkillName == name && pe.Level == level)
                {
                    return pe;
                }
            }
            return null;
        }

        public Plan.Entry GetEntryWithRoman(string name)
        {
            int level = 0;
            for (int i = 1; i <= 5; i++)
            {
                string tRomanSuffix = " " + Skill.GetRomanForInt(i);
                if (name.EndsWith(tRomanSuffix))
                {
                    level = i;
                    name = name.Substring(0, name.Length - tRomanSuffix.Length);
                    break;
                }
            }
            return GetEntry(name, level);
        }

        #region Planner Window
        private static IPlannerWindowFactory m_plannerWindowFactory;

        [XmlIgnore]
        public static IPlannerWindowFactory PlannerWindowFactory
        {
            get { return m_plannerWindowFactory; }
            set { m_plannerWindowFactory = value; }
        }

        private WeakReference<Form> m_plannerWindow;

        public WeakReference<Form> PlannerWindow 
        {
            set { m_plannerWindow = value; }
        }

        public void ShowEditor(Settings s, CharacterInfo gci)
        {
            if (m_plannerWindow != null)
            {
                Form npw = m_plannerWindow.Target;
                if (npw != null)
                {
                    try
                    {
                        if (npw.Visible)
                        {
                            npw.BringToFront();
                            return;
                        }
                        else
                        {
                            npw.Show();
                            return;
                        }
                    }
                    catch (ObjectDisposedException e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                }
                m_plannerWindow = null;
            }

            GrandCharacterInfo = gci;
            Form newWin = m_plannerWindowFactory.CreateWindow(s, this);
            //NewPlannerWindow newWin = new NewPlannerWindow(s, gci, this);
            newWin.Show();
            m_plannerWindow = new WeakReference<Form>(newWin);
        }

        public void CloseEditor()
        {
            if (m_plannerWindow != null)
            {
                Form npw = m_plannerWindow.Target;
                if (npw != null)
                {
                    try
                    {
                        npw.Close();
                    }
                    catch (ObjectDisposedException e)
                    {
                        ExceptionHandler.LogException(e, false);
                    }
                }
                m_plannerWindow = null;
            }
        }
        #endregion Planner Window

        public void SaveAsText(StreamWriter sw, PlanTextOptions pto)
        {
            MarkupType markupType = pto.Markup;
            MethodInvoker writeLine = delegate
                                          {
                                              if (markupType == MarkupType.Html)
                                              {
                                                  sw.WriteLine("<br />");
                                              }
                                              else
                                              {
                                                  sw.WriteLine();
                                              }
                                          };
            MethodInvoker boldStart = delegate
                                          {
                                              switch (markupType)
                                              {
                                                  case MarkupType.None:
                                                      break;
                                                  case MarkupType.Forum:
                                                      sw.Write("[b]");
                                                      break;
                                                  case MarkupType.Html:
                                                      sw.Write("<b>");
                                                      break;
                                              }
                                          };
            MethodInvoker boldEnd = delegate
                                        {
                                            switch (markupType)
                                            {
                                                case MarkupType.None:
                                                    break;
                                                case MarkupType.Forum:
                                                    sw.Write("[/b]");
                                                    break;
                                                case MarkupType.Html:
                                                    sw.Write("</b>");
                                                    break;
                                            }
                                        };

            if (pto.IncludeHeader)
            {
                boldStart();
                if (pto.ShoppingList)
                {
                    sw.Write("Shopping list for {0}", this.GrandCharacterInfo.Name);
                }
                else
                {
                    sw.Write("Skill plan for {0}", this.GrandCharacterInfo.Name);
                }
                boldEnd();
                writeLine();
                writeLine();
            }

            EveAttributeScratchpad scratchpad = new EveAttributeScratchpad();
            TimeSpan totalTrainingTime = TimeSpan.Zero;
            DateTime curDt = DateTime.Now;
            int num = 0;
            foreach (Plan.Entry pe in this.Entries)
            {
                if (pto.ShoppingList && (pe.Skill.Known || pe.Level != 1))
                {
                    // for shopping list, if the skill is known or a non-level-1-unknown, skip
                    continue;
                }
                num++;
                if (pto.EntryNumber)
                {
                    sw.Write("{0}. ", num);
                }
                boldStart();
                if (markupType == MarkupType.Html)
                {
                    sw.Write("<a href='showinfo:{0}'>", pe.Skill.Id);
                }
                sw.Write(pe.SkillName);
                if (markupType == MarkupType.Html)
                {
                    sw.Write("</a>");
                }
                if (!pto.ShoppingList)
                {
                    sw.Write(' ');
                    sw.Write(Skill.GetRomanForInt(pe.Level));
                }
                boldEnd();

                TimeSpan trainingTime = pe.Skill.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad);
                scratchpad.ApplyALevelOf(pe.Skill);
                DateTime startDate = curDt;
                curDt += trainingTime;
                DateTime finishDate = curDt;
                totalTrainingTime += trainingTime;

                if (pto.EntryTrainingTimes || pto.EntryStartDate || pto.EntryFinishDate)
                {
                    sw.Write(" (");
                    bool needComma = false;
                    if (pto.EntryTrainingTimes)
                    {
                        sw.Write(Skill.TimeSpanToDescriptiveText(trainingTime,
                                                                      DescriptiveTextOptions.FullText |
                                                                      DescriptiveTextOptions.IncludeCommas |
                                                                      DescriptiveTextOptions.SpaceText));
                        needComma = true;
                    }
                    if (pto.EntryStartDate)
                    {
                        if (needComma)
                        {
                            sw.Write("; ");
                        }
                        sw.Write("Start: ");
                        sw.Write(startDate.ToString());
                        needComma = true;
                    }
                    if (pto.EntryFinishDate)
                    {
                        if (needComma)
                        {
                            sw.Write("; ");
                        }
                        sw.Write("Finish: ");
                        sw.Write(finishDate.ToString());
                        needComma = true;
                    }
                    sw.Write(')');
                }
                writeLine();
            }
            if (pto.FooterCount || pto.FooterTotalTime || pto.FooterDate)
            {
                writeLine();
                bool needComma = false;
                if (pto.FooterCount)
                {
                    boldStart();
                    sw.Write(num.ToString());
                    boldEnd();
                    sw.Write(" skill");
                    if (num != 1)
                    {
                        sw.Write('s');
                    }
                    needComma = true;
                }
                if (pto.FooterTotalTime)
                {
                    if (needComma)
                    {
                        sw.Write("; ");
                    }
                    sw.Write("Total time: ");
                    boldStart();
                    sw.Write(Skill.TimeSpanToDescriptiveText(totalTrainingTime,
                                                                  DescriptiveTextOptions.FullText |
                                                                  DescriptiveTextOptions.IncludeCommas |
                                                                  DescriptiveTextOptions.SpaceText));
                    boldEnd();
                    needComma = true;
                }
                if (pto.FooterDate)
                {
                    if (needComma)
                    {
                        sw.Write("; ");
                    }
                    sw.Write("Completion: ");
                    boldStart();
                    sw.Write(curDt.ToString());
                    boldEnd();
                    needComma = true;
                }
                writeLine();
            }
        }

        [XmlRoot("Plan.Entry")]
        public class Entry : ICloneable
        {
            public enum Type
            {
                Planned,
                Prerequisite
            }

            public class Prerequisite
            {
                private Entry m_entry;

                public Entry PlanEntry
                {
                    get { return m_entry; }
                }

                internal Prerequisite(Entry entry)
                {
                    m_entry = entry;
                }
            }

            private Plan m_owner;
            private string m_skillName;
            private int m_level;
            private System.Collections.ArrayList m_planGroups;
            private Entry.Type m_entryType;
            private bool m_addNoteonly = false;
            private string m_notes;

            [XmlIgnore]
            public Plan Plan
            {
                get { return m_owner; }
                set
                {
                    if (m_owner != value)
                    {
                        if (m_owner != null)
                        {
                            m_owner.Changed -= new EventHandler<EventArgs>(m_owner_Changed);
                        }
                        m_owner = value;
                        if (m_owner != null)
                        {
                            m_owner.Changed += new EventHandler<EventArgs>(m_owner_Changed);
                        }
                    }
                }
            }

            private IEnumerable<Prerequisite> m_prerequisiteCache;

            private void m_owner_Changed(object sender, EventArgs e)
            {
                m_prerequisiteCache = null;
            }

            /// <summary>
            /// Gets or sets the name of this plan entry's skill.
            /// </summary>
            public string SkillName
            {
                get { return m_skillName; }
                set { m_skillName = value; }
            }

            /// <summary>
            /// Gets or sets the skill level of this plan entry.
            /// </summary>
            public int Level
            {
                get { return m_level; }
                set { m_level = value; }
            }

            public System.Collections.ArrayList PlanGroups
            {
                get
                {
                    if (m_planGroups == null)
                        m_planGroups = new System.Collections.ArrayList();

                    return m_planGroups;
                }
                set { m_planGroups = value; }
            }

            public Type EntryType
            {
                get { return m_entryType; }
                set { m_entryType = value; }
            }

            public string Notes
            {
                get { return (m_notes == null ? "" : m_notes); }
                set { m_notes = value; }
            }

            [XmlIgnore]
            public bool AddNoteonly
            {
                get { return m_addNoteonly; }
                set { m_addNoteonly = value; }
            }

            /// <summary>
            /// Gets the skill of this plan entry.
            /// </summary>
            public Skill Skill
            {
                get
                {
                    if (m_owner == null || m_owner.GrandCharacterInfo == null)
                    {
                        return null;
                    }
                    return m_owner.GrandCharacterInfo.GetSkill(m_skillName);
                }
            }

            /// <summary>
            /// Gets whether this entry can be trained now.
            /// </summary>
            public bool CanTrainNow
            {
                get
                {
                    return Skill.PrerequisitesMet && Level == Skill.Level + 1;
                }
            }

            [XmlIgnore]
            public IEnumerable<Prerequisite> Prerequisites
            {
                get
                {
                    if (m_prerequisiteCache == null)
                    {
                        Dictionary<string, bool> contains = new Dictionary<string, bool>();
                        List<Prerequisite> result = new List<Prerequisite>();
                        BuildPrereqs(this.Skill, this.Level, result, contains);
                        m_prerequisiteCache = result;
                    }
                    return m_prerequisiteCache;
                }
            }

            private void BuildPrereqs(Skill gs, int level, List<Prerequisite> result,
                                      Dictionary<string, bool> contains)
            {
                for (int l = level; l >= 1; l--)
                {
                    string tSkill = gs.Name + " " + Skill.GetRomanForInt(l);
                    if (!contains.ContainsKey(tSkill))
                    {
                        Plan.Entry.Prerequisite pep = new Plan.Entry.Prerequisite(m_owner.GetEntry(gs.Name, l));
                        contains[tSkill] = true;
                        result.Insert(0, pep);
                    }
                }
                foreach (Skill.Prereq pp in gs.Prereqs)
                {
                    string tSkill = pp.Skill + " " + Skill.GetRomanForInt(pp.RequiredLevel);
                    if (!contains.ContainsKey(tSkill))
                    {
                        Prerequisite pep = new Prerequisite(m_owner.GetEntry(pp.Skill.Name,
                                                                                               pp.RequiredLevel));
                        contains[tSkill] = true;
                        result.Insert(0, pep);
                        BuildPrereqs(pp.Skill, pp.RequiredLevel, result, contains);
                    }
                }
            }

            #region ICloneable Members
            public object Clone()
            {
                Plan.Entry pe = new Plan.Entry();
                pe.SkillName = this.SkillName;
                pe.Level = this.Level;
                pe.PlanGroups = (System.Collections.ArrayList)this.PlanGroups.Clone();
                pe.EntryType = this.EntryType;
                pe.Notes = this.Notes;
                return pe;
            }
            #endregion
        }
    }

    public enum MarkupType
    {
        Undefined,
        None,
        Forum,
        Html
    }

    public interface IPlannerWindowFactory
    {
        Form CreateWindow(Settings s, Plan p);
    }


}
