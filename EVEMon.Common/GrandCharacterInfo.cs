using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    public class GrandCharacterInfo
    {
        private string m_name;
        private int m_characterId;
        private string m_race;
        private string m_bloodLine;
        private string m_gender;
        private string m_corporationName;
        private string m_EVEFolder;
        private Decimal m_balance;
        private GrandEveAttributes m_attributes = new GrandEveAttributes();
        private MonitoredList<GrandEveAttributeBonus> m_attributeBonuses = new MonitoredList<GrandEveAttributeBonus>();
        private Dictionary<string, GrandSkillGroup> m_skillGroups = new Dictionary<string, GrandSkillGroup>();

        public GrandCharacterInfo(int characterId, string name)
        {
            m_characterId = characterId;
            m_name = name;

            BuildSkillTree();
            m_attributeBonuses.Changed +=
                new EventHandler<ChangedEventArgs<GrandEveAttributeBonus>>(m_attributeBonuses_Changed);
        }

        private void BuildSkillTree()
        {
            string skillfile = System.AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\eve-skills2.xml.gz";
            if (!File.Exists(skillfile))
            {
                throw new ApplicationException(skillfile + " not found!");
            }
            using (FileStream s = new FileStream(skillfile, FileMode.Open, FileAccess.Read))
            using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
            {
                // Init the static list of all skills
                GrandSkill.AllSkills = new Dictionary<string, GrandSkill>();

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(zs);

                foreach (XmlElement sgel in xdoc.SelectNodes("/skills/c"))
                {
                    List<GrandSkill> skills = new List<GrandSkill>();
                    foreach (XmlElement sel in sgel.SelectNodes("s"))
                    {
                        List<GrandSkill.Prereq> prereqs = new List<GrandSkill.Prereq>();
                        foreach (XmlElement pel in sel.SelectNodes("p"))
                        {
                            GrandSkill.Prereq p = new GrandSkill.Prereq(
                                pel.GetAttribute("n"),
                                Convert.ToInt32(pel.GetAttribute("l")));
                            prereqs.Add(p);
                        }

                        bool _pub = (sel.GetAttribute("p") != "false");
                        string _name = sel.GetAttribute("n");
                        int _id = Convert.ToInt32(sel.GetAttribute("i"));
                        string _desc = sel.GetAttribute("d");
                        EveAttribute _primAttr =
                            (EveAttribute)Enum.Parse(typeof(EveAttribute), sel.GetAttribute("a1"), true);
                        EveAttribute _secAttr =
                            (EveAttribute)Enum.Parse(typeof(EveAttribute), sel.GetAttribute("a2"), true);
                        int _rank = Convert.ToInt32(sel.GetAttribute("r"));

                        GrandSkill gs = new GrandSkill(
                            this, _pub, _name, _id, _desc, _primAttr, _secAttr, _rank,
                            prereqs);
                        gs.Changed += new EventHandler(gs_Changed);
                        gs.TrainingStatusChanged += new EventHandler(gs_TrainingStatusChanged);

                        skills.Add(gs);
                        GrandSkill.AllSkills[gs.Name] = gs;
                    }

                    GrandSkillGroup gsg = new GrandSkillGroup(sgel.GetAttribute("n"), Convert.ToInt32(sgel.GetAttribute("g")),
                                                              skills);
                    this.m_skillGroups[gsg.Name] = gsg;
                }
            }

            GrandSkill.PrepareAllPrerequisites();
        }

        

        private int m_suppressed;
        private Queue<InternalEvent> m_events = new Queue<InternalEvent>();
        private Dictionary<string, bool> m_coalescedEventTable = new Dictionary<string, bool>();

        private delegate void InternalEvent();


        private void FireEvent(InternalEvent evt, string coalesceKey)
        {
            lock (m_events)
            {
                if (m_suppressed == 0)
                {
                    evt();
                }
                else
                {
                    if (String.IsNullOrEmpty(coalesceKey) || !m_coalescedEventTable.ContainsKey(coalesceKey))
                    {
                        m_events.Enqueue(evt);
                        if (!String.IsNullOrEmpty(coalesceKey))
                        {
                            m_coalescedEventTable[coalesceKey] = true;
                        }
                    }
                }
            }
        }

        public void SuppressEvents()
        {
            lock (m_events)
            {
                m_suppressed++;
            }
        }

        public void ResumeEvents()
        {
            lock (m_events)
            {
                m_suppressed--;
                if (m_suppressed <= 0)
                {
                    m_suppressed = 0;
                    while (m_events.Count > 0)
                    {
                        m_events.Dequeue()();
                    }
                    m_coalescedEventTable.Clear();
                }
            }
        }

        private void gs_Changed(object sender, EventArgs e)
        {
            GrandSkill gs = (GrandSkill)sender;
            if (gs == m_currentlyTrainingSkill && gs.InTraining == false)
            {
                m_currentlyTrainingSkill = null;
            }
            else if (gs.InTraining)
            {
                m_currentlyTrainingSkill = gs;
            }

            OnSkillChanged(gs);
        }

        public string Name
        {
            get { return m_name; }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                    OnBioInfoChanged();
                }
            }
        }

        public int CharacterId
        {
            get { return m_characterId; }
            set
            {
                if (m_characterId != value)
                {
                    m_characterId = value;
                    OnBioInfoChanged();
                }
            }
        }

        public string Race
        {
            get { return m_race; }
            set
            {
                if (m_race != value)
                {
                    m_race = value;
                    OnBioInfoChanged();
                }
            }
        }

        public string Bloodline
        {
            get { return m_bloodLine; }
            set
            {
                if (m_bloodLine != value)
                {
                    m_bloodLine = value;
                    OnBioInfoChanged();
                }
            }
        }

        public string Gender
        {
            get { return m_gender; }
            set
            {
                if (m_gender != value)
                {
                    m_gender = value;
                    OnBioInfoChanged();
                }
            }
        }

        public string CorporationName
        {
            get { return m_corporationName; }
            set
            {
                if (m_corporationName != value)
                {
                    m_corporationName = value;
                    OnBioInfoChanged();
                }
            }
        }

        private void OnBioInfoChanged()
        {
            FireEvent(delegate
                          {
                              if (BioInfoChanged != null)
                              {
                                  BioInfoChanged(this, new EventArgs());
                              }
                          }, "bioinfo");
        }

        public event EventHandler BioInfoChanged;

        public string EVEFolder
        {
            get { return m_EVEFolder; }
            set
            {
                if (m_EVEFolder != value)
                {
                    m_EVEFolder = value;
                }
            }
        }

        public Decimal Balance
        {
            get { return m_balance; }
            set
            {
                m_balance = value;
                OnBalanceChanged();
            }
        }

        private void OnBalanceChanged()
        {
            FireEvent(delegate
                          {
                              if (BalanceChanged != null)
                              {
                                  BalanceChanged(this, new EventArgs());
                              }
                          }, "balance");
        }

        public event EventHandler BalanceChanged;

        public int GetBaseAttribute(EveAttribute attribute)
        {
            return m_attributes[attribute];
        }

        public void SetBaseAttribute(EveAttribute attribute, int value)
        {
            if (m_attributes[attribute] != value)
            {
                m_attributes[attribute] = value;
                OnAttributeChanged();
            }
        }

        private void OnAttributeChanged()
        {
            FireEvent(delegate
                          {
                              if (AttributeChanged != null)
                              {
                                  AttributeChanged(this, new EventArgs());
                              }
                          }, "attribute");
        }

        public event EventHandler AttributeChanged;

        public int BaseIntelligence
        {
            get { return GetBaseAttribute(EveAttribute.Intelligence); }
            set { SetBaseAttribute(EveAttribute.Intelligence, value); }
        }

        public int BaseCharisma
        {
            get { return GetBaseAttribute(EveAttribute.Charisma); }
            set { SetBaseAttribute(EveAttribute.Charisma, value); }
        }

        public int BasePerception
        {
            get { return GetBaseAttribute(EveAttribute.Perception); }
            set { SetBaseAttribute(EveAttribute.Perception, value); }
        }

        public int BaseMemory
        {
            get { return GetBaseAttribute(EveAttribute.Memory); }
            set { SetBaseAttribute(EveAttribute.Memory, value); }
        }

        public int BaseWillpower
        {
            get { return GetBaseAttribute(EveAttribute.Willpower); }
            set { SetBaseAttribute(EveAttribute.Willpower, value); }
        }

        public double GetEffectiveAttribute(EveAttribute attribute)
        {
            return GetEffectiveAttribute(attribute, null);
        }

        public double GetEffectiveAttribute(EveAttribute attribute, EveAttributeScratchpad scratchpad)
        {
            return GetEffectiveAttribute(attribute, scratchpad, true, true);
        }

        public double GetEffectiveAttribute(EveAttribute attribute, EveAttributeScratchpad scratchpad,
                                            bool includeLearning, bool includeImplants)
        {
            double result = Convert.ToDouble(m_attributes[attribute]);
            double learningBonus = 1.0F;
            double implant_value = 0.0;
            if (includeImplants)
            {
                bool manual_override = false;
                foreach (GrandEveAttributeBonus geab in m_attributeBonuses)
                {
                    if (geab.EveAttribute == attribute)
                    {
                        if (geab.Manual == false && manual_override == false)
                            implant_value += geab.Amount;
                        if (geab.Manual == true)
                        {
                            if (manual_override == false)
                            {
                                implant_value = 0.0;
                                manual_override = true;
                            }
                            implant_value += geab.Amount;
                        }
                    }
                }
                result += implant_value;
            }

            // XXX: include implants on scratchpad?
            GrandSkillGroup learningSg = m_skillGroups["Learning"];
            switch (attribute)
            {
                case EveAttribute.Intelligence:
                    result += learningSg["Analytical Mind"].Level;
                    result += learningSg["Logic"].Level;
                    break;
                case EveAttribute.Charisma:
                    result += learningSg["Empathy"].Level;
                    result += learningSg["Presence"].Level;
                    break;
                case EveAttribute.Memory:
                    result += learningSg["Instant Recall"].Level;
                    result += learningSg["Eidetic Memory"].Level;
                    break;
                case EveAttribute.Willpower:
                    result += learningSg["Iron Will"].Level;
                    result += learningSg["Focus"].Level;
                    break;
                case EveAttribute.Perception:
                    result += learningSg["Spatial Awareness"].Level;
                    result += learningSg["Clarity"].Level;
                    break;
            }
            if (scratchpad != null)
            {
                result += scratchpad.GetAttributeBonus(attribute);
            }

            if (includeLearning)
            {
                int learningLevel = learningSg["Learning"].Level;
                if (scratchpad != null)
                {
                    learningLevel += scratchpad.LearningLevelBonus;
                }

                learningBonus = 1.0 + (0.02 * learningLevel);

                return (result * learningBonus);
            }
            else
            {
                return result;
            }
        }

        public double getImplantValue(EveAttribute eveAttribute)
        {
            double result = 0.0;
            bool manual_override = false;
            foreach (GrandEveAttributeBonus geab in m_attributeBonuses)
            {
                if (geab.EveAttribute == eveAttribute)
                {
                    if (geab.Manual == false && manual_override == false)
                        result += geab.Amount;
                    if (geab.Manual == true)
                    {
                        if (manual_override == false)
                        {
                            result = 0.0;
                            manual_override = true;
                        }
                        result += geab.Amount;
                    }
                }
            }
            return result;
        }

        public double LearningBonus
        {
            get { return 1 + (m_skillGroups["Learning"]["Learning"].Level * 0.02); }
        }

        public double EffectiveIntelligence
        {
            get { return GetEffectiveAttribute(EveAttribute.Intelligence); }
        }

        public double EffectiveCharisma
        {
            get { return GetEffectiveAttribute(EveAttribute.Charisma); }
        }

        public double EffectivePerception
        {
            get { return GetEffectiveAttribute(EveAttribute.Perception); }
        }

        public double EffectiveMemory
        {
            get { return GetEffectiveAttribute(EveAttribute.Memory); }
        }

        public double EffectiveWillpower
        {
            get { return GetEffectiveAttribute(EveAttribute.Willpower); }
        }

        public double GetAttributeBonusFromImplants(EveAttribute eveAttribute)
        {
            double result = 0.0F;
            double learningBonus = 1.0F;
            bool manual_override = false;
            foreach (GrandEveAttributeBonus geab in m_attributeBonuses)
            {
                if (geab.EveAttribute == eveAttribute)
                {
                    if (geab.Manual == false && manual_override == false)
                        result += geab.Amount;
                    if (geab.Manual == true)
                    {
                        if (manual_override == false)
                        {
                            result = 0.0;
                            manual_override = true;
                        }
                        result += geab.Amount;
                    }
                }
            }
            int learningLevel = m_skillGroups["Learning"]["Learning"].Level;
            learningBonus = 1.0 + (0.02 * learningLevel);
            return result * learningBonus;
        }

        public IList<GrandEveAttributeBonus> AttributeBonuses
        {
            get { return m_attributeBonuses; }
        }

        private void m_attributeBonuses_Changed(object sender, EventArgs e)
        {
            OnAttributeBonusChanged();
        }

        private void OnAttributeBonusChanged()
        {
            OnAttributeChanged();
            FireEvent(delegate
                          {
                              if (AttributeBonusChanged != null)
                              {
                                  AttributeBonusChanged(this, new EventArgs());
                              }
                          }, "attributebonus");
        }

        public event EventHandler AttributeBonusChanged;

        public Dictionary<string, GrandSkillGroup> SkillGroups
        {
            get { return m_skillGroups; }
        }

        private int m_cachedSkillPointTotal = -1;
        private int m_cachedKnownSkillCount = -1;
        private int m_cachedMaxedSkillCount = -1;

        private List<GrandSkill> m_skillsChanged = new List<GrandSkill>();

        private void OnSkillChanged(GrandSkill gs)
        {
            m_cachedSkillPointTotal = -1;
            m_cachedKnownSkillCount = -1;
            lock (m_skillsChanged)
            {
                m_skillsChanged.Add(gs);
            }

            FireEvent(delegate
                          {
                              GrandSkill[] mySkillsChanged;
                              lock (m_skillsChanged)
                              {
                                  mySkillsChanged = new GrandSkill[m_skillsChanged.Count];
                                  m_skillsChanged.CopyTo(mySkillsChanged);
                                  m_skillsChanged.Clear();
                              }
                              if (SkillChanged != null)
                              {
                                  SkillChanged(this, new SkillChangedEventArgs(mySkillsChanged));
                              }
                          }, "skill");
            if (m_skillGroups["Learning"].Contains(gs.Name))
            {
                OnAttributeChanged();
            }
        }

        void gs_TrainingStatusChanged(object sender, EventArgs e)
        {
            FireEvent(delegate
            {
                if (TrainingSkillChanged != null)
                {
                    TrainingSkillChanged(this, new EventArgs());
                }
            }, "trainingSkill");
        }

        public event SkillChangedHandler SkillChanged;
        public event EventHandler TrainingSkillChanged;

        public GrandSkill GetSkill(string skillName)
        {
            foreach (GrandSkillGroup gsg in m_skillGroups.Values)
            {
                GrandSkill gs = gsg[skillName];
                if (gs != null)
                {
                    return gs;
                }
            }
            return null;
        }

        public int SkillPointTotal
        {
            get
            {
                if (m_cachedSkillPointTotal == -1)
                {
                    m_cachedSkillPointTotal = 0;
                    foreach (GrandSkillGroup gsg in m_skillGroups.Values)
                    {
                        foreach (GrandSkill gs in gsg)
                        {
                            if (!gs.InTraining)
                            {
                                m_cachedSkillPointTotal += gs.CurrentSkillPoints;
                            }
                        }
                    }
                }

                GrandSkill cts = this.CurrentlyTrainingSkill;
                if (cts == null)
                {
                    return m_cachedSkillPointTotal;
                }
                else
                {
                    return m_cachedSkillPointTotal + cts.CurrentSkillPoints;
                }
            }
        }

        public int KnownSkillCount
        {
            get
            {
                if (m_cachedKnownSkillCount == -1)
                {
                    m_cachedKnownSkillCount = 0;
                    foreach (GrandSkillGroup gsg in m_skillGroups.Values)
                    {
                        m_cachedKnownSkillCount += gsg.KnownCount;
                    }
                }
                return m_cachedKnownSkillCount;
            }
        }

        public int MaxedSkillCount
        {
            get
            {
                if (m_cachedMaxedSkillCount == -1)
                {
                    m_cachedMaxedSkillCount = 0;
                    foreach (GrandSkillGroup gsg in m_skillGroups.Values)
                    {
                        m_cachedMaxedSkillCount += gsg.MaxedCount;
                    }
                }
                return m_cachedMaxedSkillCount;
            }
        }

        private GrandSkill m_currentlyTrainingSkill;

        public GrandSkill CurrentlyTrainingSkill
        {
            get { return m_currentlyTrainingSkill; }
        }

        public void CancelCurrentSkillTraining()
        {
            if (m_currentlyTrainingSkill != null)
            {
                m_currentlyTrainingSkill.StopTraining();
            }
        }

        private bool m_isCached = false;

        public bool IsCached
        {
            get { return m_isCached; }
            set
            {
                if (m_isCached != value)
                {
                    m_isCached = value;
                    OnBioInfoChanged();
                }
            }
        }

        public void AssignFromSerializableCharacterInfo(SerializableCharacterInfo ci)
        {
            this.SuppressEvents();
            this.Name = ci.Name;
            this.CharacterId = ci.CharacterId;
            this.IsCached = ci.IsCached;
            this.Gender = ci.Gender;
            this.Race = ci.Race;
            this.Bloodline = ci.BloodLine;
            this.CorporationName = ci.CorpName;
            if (ci.IsCached == true)
            {
                this.EVEFolder = ci.EVEFolder;
            }
            this.Balance = ci.Balance;

            this.BaseIntelligence = ci.Attributes.BaseIntelligence;
            this.BaseCharisma = ci.Attributes.BaseCharisma;
            this.BasePerception = ci.Attributes.BasePerception;
            this.BaseMemory = ci.Attributes.BaseMemory;
            this.BaseWillpower = ci.Attributes.BaseWillpower;

            List<GrandEveAttributeBonus> manualBonuses = new List<GrandEveAttributeBonus>();
            foreach (GrandEveAttributeBonus tb in this.AttributeBonuses)
            {
                if (tb.Manual)
                {
                    manualBonuses.Add(tb);
                }
            }

            this.AttributeBonuses.Clear();
            foreach (SerializableEveAttributeBonus bonus in ci.AttributeBonuses.Bonuses)
            {
                GrandEveAttributeBonus geab =
                    new GrandEveAttributeBonus(bonus.Name, bonus.EveAttribute, bonus.Amount, bonus.Manual);
                this.AttributeBonuses.Add(geab);
            }
            foreach (GrandEveAttributeBonus tb in manualBonuses)
            {
                this.AttributeBonuses.Add(tb);
            }

            foreach (SerializableSkillGroup sg in ci.SkillGroups)
            {
                GrandSkillGroup gsg = m_skillGroups[sg.Name];
                foreach (SerializableSkill s in sg.Skills)
                {
                    GrandSkill gs = gsg[s.Name];
                    if (gs == null)
                    {
                        MessageBox.Show("The character cache contains the unknown skill " + s.Name + ", which will be removed.",
                                    "Unknown skill", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        gs.CurrentSkillPoints = s.SkillPoints;
                        gs.Known = true;
                        gs.LastConfirmedLvl = gs.Level;
                    }
                }
            }
            this.check_training_skills(ci.SkillInTraining);
            this.ResumeEvents();
        }

        public void check_old_skill()
        {// This function has now been cleaned.... I think it still works properly too, which is a bonus.
            // This is called from CharacterMonitor.cs when a fresh XML file has failed to be obtained.
            // In other words, this is the default behaviour when there is a problem

            // check if old skill is complete in the current character data and if not, set to currenttrainingskill

            //first we must check that the old_skill skill is still the skill that is currently training, and that
            // the estimated completion is within 3 minutes 30 seconds of the old one
            // if not, then it needs canceling, this of course assumes that the old_skill data
            // has been updated properly!
            // Using 3 minutes 30 seconds as a ball park for standard deviation due to download
            // to settings file lag etc. Feel free to reduce it so long as we don't start getting issues from it being to short.
            if (this.CurrentlyTrainingSkill != null && old_skill != null && (old_skill.old_SkillName == null || old_skill.old_SkillName != this.CurrentlyTrainingSkill.Name || ((TimeSpan)old_skill.old_estimated_completion.Subtract(this.CurrentlyTrainingSkill.EstimatedCompletion)).Duration() > new TimeSpan(0, 3, 30)))
                this.CancelCurrentSkillTraining();
            if (!first_run && old_skill != null)
            {
                // If this isn't the first run and old_skill has actually been initalised
                // 
                // The code for the first run is at the bottom of the section in normal running,
                // This section isn't normal running, this is called when something has gone wrong in normal running
                if (old_skill.old_SkillName != null && this.GetSkill(old_skill.old_SkillName) != null)
                {
                    // first we look at the status of the skill indicated by old_skill and check on it's progress
                    // for this we need a few bool values to use as flags.
                    // Could probaly use an enumerated type for this
                    // Both default to negative
                    bool add = false;
                    bool check = false;
                    string skill_name = old_skill.old_SkillName;
                    int level = old_skill.old_TrainingToLevel;
                    if (old_skill.old_skill_completed)
                    {
                        // Check to see if the oldskill has NOT completed (This does happen from time to time)
                        if (this.GetSkill(skill_name).CurrentSkillPoints < this.GetSkill(skill_name).GetPointsRequiredForLevel(level))
                        {
                            // so we need to check the skill alerts and remove this skill as it hasn't completed yet.
                            // This assumes no one has looked at the skill alerts in a while.
                            check = true;
                        }
                    }
                    else
                    {
                        // Check old skill for completion
                        if (this.GetSkill(skill_name).CurrentSkillPoints >= this.GetSkill(skill_name).GetPointsRequiredForLevel(level))
                        {
                            // So we need to add this skill (if it's not already there)
                            // to the skill alerts as according to the current XML it's done.
                            // This skill should have been added when it completed during run time
                            // but this might be the first time it's been looked at
                            // due to the initial startup failing to get the XML

                            // First set the skill points before the skill gets cancelled as currently training
                            if (this.GetSkill(skill_name).InTraining && this.GetSkill(skill_name).TrainingToLevel == level)
                            {
                                this.GetSkill(skill_name).CurrentSkillPoints = this.GetSkill(skill_name).GetPointsRequiredForLevel(level);
                                this.CancelCurrentSkillTraining();
                            }
                            old_skill.old_skill_completed = true;
                            add = true;
                        }
                        // This is out here as the above checks to see if it's been completed NOW,
                        // when the original old_skill.old_skill_complete it may have been a guess
                        // so in that case, 'add' would still be false.
                        check = true;
                    }
                    // This is where we use the two flags
                    if (check)
                    {
                        OnDownloadAttemptComplete(this.Name, skill_name, add);
                    }
                }
                if (old_skill.old_SkillName != null && this.CurrentlyTrainingSkill == null)
                {
                    // Now we start having some fun with the Currently training skill values.
                    GrandSkill newTrainingSkill = this.GetSkill(old_skill.old_SkillName);
                    // Check we actually have a skill in training
                    string skill_name = old_skill.old_SkillName;
                    int level = old_skill.old_TrainingToLevel;
                    if (newTrainingSkill != null)
                    {
                        // See if the old_skill in the current details has completed it's training
                        if (this.GetSkill(skill_name).CurrentSkillPoints >= this.GetSkill(skill_name).GetPointsRequiredForLevel(level))
                        {
                            if (old_skill.old_skill_completed)
                            {
                                // Right, so it's completed, but old_skill has already been flagged as dealt with in this regard... so...
                                // Oh yeah, if you don't do this skill points for some odd reason reset to the old XML values when you cancel the skill training so...
                                this.GetSkill(skill_name).CurrentSkillPoints = this.GetSkill(skill_name).CurrentSkillPoints;
                            }
                            if (!old_skill.old_skill_completed)
                            {
                                // Right, so the skill needs to be flagged as done.
                                // Oh yeah, if you don't do this skill points for some odd reason reset to the old XML values when you cancel the skill training so...
                                this.GetSkill(skill_name).CurrentSkillPoints = this.GetSkill(skill_name).CurrentSkillPoints;
                                old_skill.old_skill_completed = true;
                                // Oh, yeah, we need to add this skill to the alerts...
                                // The alerter takes care of whether it's already there or not.
                                OnDownloadAttemptComplete(this.Name, old_skill.old_SkillName, true);
                            }
                        }
                        else if (this.GetSkill(skill_name).CurrentSkillPoints < this.GetSkill(skill_name).GetPointsRequiredForLevel(level))
                        {
                            // Here is where we set the currently training skill according to the last
                            // known skill in training.
                            // To make doubly sure we have no old training skills lurking ...out with the old...
                            this.CancelCurrentSkillTraining();
                            // ...and in with the new
                            newTrainingSkill.SetTrainingInfo(level, old_skill.old_estimated_completion);
                        }
                    }
                }
                // Now to activate normal runtime skill completion monitoring
                m_attempted_dl_complete = true;
            }
        }

        public void check_training_skills(SerializableSkillInTraining SkillInTraining)
        {
            // This is called from AssignFromSerializableCharacterInfo(SerializableCharacterInfo ci)
            // This is where normal running takes you in the standard run of the mill operation of EVEMon
            // 
            // check if old skill is complete in the current character data and if not, set to currenttrainingskill
            if (this.CurrentlyTrainingSkill != null && (SkillInTraining == null || (SkillInTraining != null && SkillInTraining.SkillName != this.CurrentlyTrainingSkill.Name) || ((TimeSpan)SkillInTraining.EstimatedCompletion.Subtract(this.CurrentlyTrainingSkill.EstimatedCompletion)).Duration() > new TimeSpan(0, 3, 30)))
            {
                // Skill or current expected completion time changed since previous update.
                this.CancelCurrentSkillTraining();
            }
            if (!first_run)
            {
                if (old_skill != null && old_skill.old_SkillName != null && this.GetSkill(old_skill.old_SkillName) != null)
                {
                    bool add = false;
                    bool check = false;
                    string skill_name = old_skill.old_SkillName;
                    int level = old_skill.old_TrainingToLevel;
                    if (old_skill.old_skill_completed)
                    {
                        if (this.GetSkill(skill_name).CurrentSkillPoints < this.GetSkill(skill_name).GetPointsRequiredForLevel(level))
                        {
                            check = true;
                        }
                    }
                    else
                    {
                        if (this.GetSkill(skill_name).CurrentSkillPoints >= this.GetSkill(skill_name).GetPointsRequiredForLevel(level))
                        {// Check old skill for completion
                            old_skill.old_skill_completed = true;
                            add = true;
                        }
                        else if (this.GetSkill(skill_name).CurrentSkillPoints < this.GetSkill(skill_name).GetPointsRequiredForLevel(level))
                        {// If this is literally the second pass then the old_skill values need to be checked so that the rest is consistently coded
                            old_skill.old_SkillName = null;
                            old_skill.old_TrainingToLevel = 0;
                            old_skill.old_skill_completed = false;
                            old_skill.old_estimated_completion = DateTime.MaxValue;
                        }
                        check = true;
                    }
                    if (check)
                    {
                        OnDownloadAttemptComplete(this.Name, skill_name, add);
                    }
                }
                if (SkillInTraining != null && this.CurrentlyTrainingSkill == null)
                {
                    // Now we depart even more from the version above.
                    // We have to deal with making this character actually show that he is learning the
                    // skill the XML file says he's learning. But we do this carefully as it may be complete
                    string skill_name = SkillInTraining.SkillName;
                    int level = SkillInTraining.TrainingToLevel;
                    GrandSkill newTrainingSkill = this.GetSkill(skill_name);
                    if (newTrainingSkill != null)
                    {
                        if (SkillInTraining.NeededPoints <= SkillInTraining.CurrentPoints)
                        {
                            if (old_skill.old_skill_completed && skill_name == old_skill.old_SkillName && level == old_skill.old_TrainingToLevel)
                            {
                                newTrainingSkill.CurrentSkillPoints = newTrainingSkill.GetPointsRequiredForLevel(level);
                            }
                            if (old_skill == null || !old_skill.old_skill_completed || old_skill.old_SkillName == null || (old_skill.old_SkillName != null && (skill_name != old_skill.old_SkillName || (skill_name == old_skill.old_SkillName && SkillInTraining.TrainingToLevel != old_skill.old_TrainingToLevel))))
                            {
                                this.GetSkill(skill_name).CurrentSkillPoints = newTrainingSkill.GetPointsRequiredForLevel(level);
                                old_skill = new OldSkillinfo(skill_name, SkillInTraining.TrainingToLevel, true, SkillInTraining.EstimatedCompletion);
                                OnDownloadAttemptComplete(this.Name, skill_name, true);
                            }
                        }
                        else if (SkillInTraining.NeededPoints > SkillInTraining.CurrentPoints)
                        {
                            newTrainingSkill.SetTrainingInfo(level, SkillInTraining.EstimatedCompletion);
                        }
                    }
                }
                // Now to activate normal runtime skill completion monitoring
                m_attempted_dl_complete = true;
            }
            if (first_run)
            {
                // This is where the old_skill values are initalised,
                // it's here to avoid accidentally triggering any other code on this pass.
                // Order is everything in this section!!
                if (SkillInTraining != null)
                {
                    GrandSkill newTrainingSkill = this.GetSkill(SkillInTraining.SkillName);
                    if (newTrainingSkill != null)
                    {
                        old_skill = new OldSkillinfo(SkillInTraining.SkillName, SkillInTraining.TrainingToLevel, newTrainingSkill.CurrentSkillPoints >= SkillInTraining.NeededPoints, SkillInTraining.EstimatedCompletion);
                    }
                }
                first_run = false;
            }
        }

        private bool first_run = true;
        private bool m_attempted_dl_complete = false;
        private OldSkillinfo old_skill = new OldSkillinfo();

        public OldSkillinfo OldTrainingSkill
        {
            get { return old_skill; }
            set { old_skill = value; }
        }

        public bool DL_Complete
        {
            get { return m_attempted_dl_complete; }
        }

        public delegate void DownloadAttemptCompletedHandler(object sender, DownloadAttemptCompletedEventArgs oldskill);

        public event DownloadAttemptCompletedHandler DownloadAttemptCompleted;

        public class DownloadAttemptCompletedEventArgs : EventArgs
        {
            private string m_skillName;

            public string SkillName
            {
                get { return m_skillName; }
            }

            private string m_characterName;

            public string CharacterName
            {
                get { return m_characterName; }
            }

            private bool m_complete;

            public bool Complete
            {
                get { return m_complete; }
            }

            public DownloadAttemptCompletedEventArgs(string CharacterName, string skillName, bool Complete)
            {
                m_skillName = skillName;
                m_characterName = CharacterName;
                m_complete = Complete;
            }
        }

        public void trigger_skill_complete(string CharacterName, string skillName)
        { // Basically trigger the event when a skill completes between downloads
            GrandSkill newlyCompletedSkill = this.GetSkill(skillName);
            if (newlyCompletedSkill != null)
            {
                newlyCompletedSkill.CurrentSkillPoints = newlyCompletedSkill.GetPointsRequiredForLevel(newlyCompletedSkill.TrainingToLevel);
                old_skill = new OldSkillinfo(newlyCompletedSkill.Name, newlyCompletedSkill.TrainingToLevel, true, DateTime.MinValue);
                this.CancelCurrentSkillTraining();
            }
            OnDownloadAttemptComplete(CharacterName, skillName, true);
        }

        private void OnDownloadAttemptComplete(string CharacterName, string skillName, bool Complete)
        {
            if (String.IsNullOrEmpty(CharacterName) || String.IsNullOrEmpty(skillName))
            {
                return;
            }
            GrandSkill temp = this.GetSkill(skillName);
            if (DownloadAttemptCompleted != null && temp != null)
            {
                DownloadAttemptCompletedEventArgs e = new DownloadAttemptCompletedEventArgs(CharacterName, skillName, Complete);
                DownloadAttemptCompleted(this, e);
            }
        }

        public SerializableCharacterInfo ExportSerializableCharacterInfo()
        {
            SerializableCharacterInfo ci = new SerializableCharacterInfo();
            ci.Name = this.Name;
            ci.CharacterId = this.CharacterId;
            ci.Gender = this.Gender;
            ci.Race = this.Race;
            ci.BloodLine = this.Bloodline;
            ci.CorpName = this.CorporationName;
            ci.EVEFolder = this.EVEFolder; // to CI
            ci.Balance = this.Balance;

            ci.Attributes.BaseIntelligence = this.BaseIntelligence;
            ci.Attributes.BaseCharisma = this.BaseCharisma;
            ci.Attributes.BasePerception = this.BasePerception;
            ci.Attributes.BaseMemory = this.BaseMemory;
            ci.Attributes.BaseWillpower = this.BaseWillpower;

            foreach (GrandEveAttributeBonus geab in this.AttributeBonuses)
            {
                SerializableEveAttributeBonus eab = null;
                switch (geab.EveAttribute)
                {
                    case EveAttribute.Intelligence:
                        eab = new SerializableIntelligenceBonus();
                        break;
                    case EveAttribute.Charisma:
                        eab = new SerializableCharismaBonus();
                        break;
                    case EveAttribute.Perception:
                        eab = new SerializablePerceptionBonus();
                        break;
                    case EveAttribute.Memory:
                        eab = new SerializableMemoryBonus();
                        break;
                    case EveAttribute.Willpower:
                        eab = new SerializableWillpowerBonus();
                        break;
                }
                if (eab != null)
                {
                    eab.Name = geab.Name;
                    eab.Amount = geab.Amount;
                    eab.Manual = geab.Manual;
                    ci.AttributeBonuses.Bonuses.Add(eab);
                }
            }

            foreach (GrandSkillGroup gsg in this.SkillGroups.Values)
            {
                SerializableSkillGroup sg = new SerializableSkillGroup();
                bool added = false;
                foreach (GrandSkill gs in gsg)
                {
                    if (gs.CurrentSkillPoints > 0)
                    {
                        SerializableSkill s = new SerializableSkill();
                        s.Name = gs.Name;
                        s.Id = gs.Id;
                        s.GroupId = gsg.ID;
                        s.Level = gs.Level;
                        s.Rank = gs.Rank;
                        s.SkillPoints = gs.CurrentSkillPoints;
                        sg.Skills.Add(s);
                        added = true;
                    }
                }
                if (added)
                {
                    sg.Name = gsg.Name;
                    sg.Id = gsg.ID;
                    ci.SkillGroups.Add(sg);
                }
            }

            GrandSkill gsit = this.CurrentlyTrainingSkill;
            if (gsit != null)
            {
                SerializableSkillInTraining sit = new SerializableSkillInTraining();
                sit.SkillName = gsit.Name;
                sit.TrainingToLevel = gsit.TrainingToLevel;
                sit.CurrentPoints = gsit.CurrentSkillPoints;
                sit.EstimatedCompletion = gsit.EstimatedCompletion;
                sit.NeededPoints = gsit.GetPointsRequiredForLevel(gsit.TrainingToLevel);
                ci.SkillInTraining = sit;
            }

            return ci;
        }

        public TimeSpan GetTrainingTimeToMultipleSkills(IEnumerable<Pair<GrandSkill, int>> skills)
        {
            Dictionary<GrandSkill, int> trainedAlready = new Dictionary<GrandSkill, int>();
            TimeSpan result = TimeSpan.Zero;

            foreach (Pair<GrandSkill, int> ts in skills)
            {
                result += ts.A.GetPrerequisiteTime(trainedAlready);
                for (int i = 1; i <= ts.B; i++)
                {
                    int pointsReq = ts.A.GetPointsRequiredForLevel(i);
                    bool needTrain = true;
                    if ((trainedAlready.ContainsKey(ts.A) &&
                         trainedAlready[ts.A] >= pointsReq) ||
                        ts.A.Level >= ts.B)
                    {
                        needTrain = false;
                    }
                    if (needTrain)
                    {
                        result += ts.A.GetTrainingTimeOfLevelOnly(i, true);
                        trainedAlready[ts.A] = ts.A.GetPointsRequiredForLevel(i);
                    }
                }
            }

            return result;
        }
    }



    public delegate void SkillChangedHandler(object sender, SkillChangedEventArgs e);

    public class SkillChangedEventArgs : EventArgs
    {
        private GrandSkill[] m_skillList;

        public ICollection<GrandSkill> SkillList
        {
            get { return m_skillList; }
        }

        public SkillChangedEventArgs(GrandSkill gs)
        {
            m_skillList = new GrandSkill[1];
            m_skillList[0] = gs;
        }

        public SkillChangedEventArgs(GrandSkill[] skillList)
        {
            m_skillList = new GrandSkill[skillList.Length];
            skillList.CopyTo(m_skillList, 0);
        }
    }

    public enum ChangeType
    {
        Added,
        Removed,
        Cleared
    }

    public class ChangedEventArgs<T> : EventArgs
    {
        private T m_item;
        private ChangeType m_changeType;

        public T Item
        {
            get { return m_item; }
        }

        public ChangeType ChangeType
        {
            get { return m_changeType; }
        }

        public ChangedEventArgs(T item, ChangeType changeType)
        {
            m_item = item;
            m_changeType = changeType;
        }
    }

    public class ClearedEventArgs<T> : EventArgs
    {
        private IEnumerable<T> m_items;

        public IEnumerable<T> Items
        {
            get { return m_items; }
        }

        public ClearedEventArgs(IEnumerable<T> items)
        {
            m_items = items;
        }
    }



    public class GrandEveAttributes
    {
        private int[] m_values = new int[5] { 0, 0, 0, 0, 0 };

        public int this[EveAttribute attribute]
        {
            get { return m_values[(int)attribute]; }
            set { m_values[(int)attribute] = value; }
        }
    }

    public class GrandEveAttributeBonus
    {
        private string m_name;
        private EveAttribute m_attribute;
        private int m_amount;
        private bool m_manual = false;

        public string Name
        {
            get { return m_name; }
        }

        public EveAttribute EveAttribute
        {
            get { return m_attribute; }
        }

        public int Amount
        {
            get { return m_amount; }
        }

        public bool Manual
        {
            get { return m_manual; }
        }

        public GrandEveAttributeBonus(string name, EveAttribute attr, int amount)
            : this(name, attr, amount, false)
        {
        }

        public GrandEveAttributeBonus(string name, EveAttribute attr, int amount, bool manual)
        {
            m_name = name;
            m_attribute = attr;
            m_amount = amount;
            m_manual = manual;
        }
    }




}
