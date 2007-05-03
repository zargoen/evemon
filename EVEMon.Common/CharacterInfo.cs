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
    public class CharacterInfo
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
        private DateTime m_expires;
        
        // These are the new replacements for m_attributeBonuses
        private MonitoredList<UserImplant> m_CurrentImplants = new MonitoredList<UserImplant>();
        private Dictionary<string, ImplantSet> m_implantSets = new Dictionary<string, ImplantSet>();

        // This is to keep track of the number of times you've tried to dl this character and been unsuccessful.
        private int m_downloadfailed = 0;

        private Dictionary<string, SkillGroup> m_skillGroups = new Dictionary<string, SkillGroup>();
        private Dictionary<int, Skill> m_AllSkillsByID = new Dictionary<int, Skill>();

        public CharacterInfo(int characterId, string name)
        {
            m_characterId = characterId;
            m_name = name;

            BuildSkillTree();
            m_CurrentImplants.Changed +=
                new EventHandler<ChangedEventArgs<UserImplant>>(m_implantSet_Changed);
        }

        private void BuildSkillTree()
        {
            // This needs optimising - why are we loading static data
            // everytime for each character. We need to refactor this
            // so all the static skill data is only created once.

            ///
            /// Note from Eewec - I've done my best to optimise this code.
            /// Before, every character had a copy of the data, AND the skill class had a reference to
            /// the skill data that had been loaded for the last loaded character.
            /// Now, it only gets loaded for each character.
            /// 
            Dictionary<string, Skill> SkillsByName = new Dictionary<string, Skill>();

            string skillfile = System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\eve-skills2.xml.gz";
            if (!File.Exists(skillfile))
            {
                throw new ApplicationException(skillfile + " not found!");
            }
            using (FileStream s = new FileStream(skillfile, FileMode.Open, FileAccess.Read))
            using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(zs);

                foreach (XmlElement sgel in xdoc.SelectNodes("/skills/c"))
                {
                    List<Skill> skills = new List<Skill>();
                    foreach (XmlElement sel in sgel.SelectNodes("s"))
                    {
                        string _name = sel.GetAttribute("n");

                        List<Skill.Prereq> prereqs = new List<Skill.Prereq>();
                        foreach (XmlElement pel in sel.SelectNodes("p"))
                        {
                            Skill.Prereq p = new Skill.Prereq(
                                pel.GetAttribute("n"),
                                Convert.ToInt32(pel.GetAttribute("l")));
                            prereqs.Add(p);
                        }

                        bool _pub = (sel.GetAttribute("p") != "false");
                        int _id = Convert.ToInt32(sel.GetAttribute("i"));
                        string _desc = sel.GetAttribute("d");
                        EveAttribute _primAttr =
                            (EveAttribute)Enum.Parse(typeof(EveAttribute), sel.GetAttribute("a1"), true);
                        EveAttribute _secAttr =
                            (EveAttribute)Enum.Parse(typeof(EveAttribute), sel.GetAttribute("a2"), true);
                        int _rank = Convert.ToInt32(sel.GetAttribute("r"));
                        int _cost = Convert.ToInt32(sel.GetAttribute("c"));
                        Skill gs = new Skill(
                            this, _pub, _name, _id, _desc, _primAttr, _secAttr, _rank, _cost, false, prereqs);
                        gs.Changed += new EventHandler(gs_Changed);
                        gs.TrainingStatusChanged += new EventHandler(gs_TrainingStatusChanged);
                        m_AllSkillsByID[_id] = gs;
                        skills.Add(gs);
                        SkillsByName[_name] = gs;
                    }

                    string _group = sgel.GetAttribute("n");
                    int _number = Convert.ToInt32(sgel.GetAttribute("g"));
                    SkillGroup gsg = new SkillGroup(_group, _number, skills);
                    this.m_skillGroups[gsg.Name] = gsg;

                    foreach (string os in Settings.GetInstance().GetOwnedBooksForCharacter(m_name))
                    {
                        if (this.m_skillGroups[gsg.Name].Contains(os))
                            this.m_skillGroups[gsg.Name][os].Owned = true;
                    }
                }
            }

            foreach (Skill s in m_AllSkillsByID.Values)
            {
                foreach (Skill.Prereq pr in s.Prereqs)
                {
                    Skill gs = SkillsByName[pr.Name];
                    pr.SetSkill(gs);
                }
            }

            // cleanup on aisle 3!
            AutoShrink.Dirty();
        }
        
        private int m_suppressed;
        private Queue<InternalEvent> m_events = new Queue<InternalEvent>();
        private Dictionary<string, bool> m_coalescedEventTable = new Dictionary<string, bool>();

        #region custom event handling - to allow events to be suppressed and queued
        private delegate void InternalEvent();

        private void FireEvent(InternalEvent evt, string coalesceKey)
        {
            lock (m_events)
            {
                if (m_suppressed == 0)
                {
                    // we're not suppressing events so kick it off.
                    evt();
                }
                else
                {
                    // We're supressing events
                    // have we already got a queued event of this key?
                    if (String.IsNullOrEmpty(coalesceKey) || !m_coalescedEventTable.ContainsKey(coalesceKey))
                    {
                        // no, add it to the pending queue
                        m_events.Enqueue(evt);
                        if (!String.IsNullOrEmpty(coalesceKey))
                        {
                            // If we have a key, flag that we've queued an event for this key.
                            m_coalescedEventTable[coalesceKey] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Switch off event firing - cvan be callled cumulativly.
        /// A supression counter is incremented every time this is invoked.
        /// </summary>
        public void SuppressEvents()
        {
            lock (m_events)
            {
                m_suppressed++;
            }
        }

        /// <summary>
        /// Resume events. Will fire any queued events when the supression counter is 0
        /// </summary>
        public void ResumeEvents()
        {
            lock (m_events)
            {
                m_suppressed--;
                if (m_suppressed <= 0)
                {
                    m_suppressed = 0;
                    // fire any queued events
                    while (m_events.Count > 0)
                    {
                        // dequeue and fire.
                        m_events.Dequeue()();
                    }
                    // and empty the cache of event type keys
                    m_coalescedEventTable.Clear();
                }
            }
        }

        #endregion

        /// <summary>
        /// Called when skill changes sp, changes known status or starts/stops training
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gs_Changed(object sender, EventArgs e)
        {
            Skill gs = (Skill)sender;
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

        public Dictionary<string,ImplantSet> implantSets
        {
            get { return m_implantSets; }
            set 
            {
                if (m_implantSets != value)
                {
                    m_implantSets = value;
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
            if (includeImplants)
            {
                result += getImplantValue(attribute);
            }

            // XXX: include implants on scratchpad?
            SkillGroup learningSg = m_skillGroups["Learning"];
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
            double result = 0.0F;
            foreach (UserImplant geab in ImplantBonuses)
            {
                if (geab.Slot - 1 == (int)eveAttribute)
                {
                    result = geab.Bonus;
                    break;
                }
            }
            return result;
        }

        public string getImplantName(EveAttribute eveAttribute)
        {
            if (eveAttribute == EveAttribute.None)
                return "???";
            string result = string.Empty;
            foreach (UserImplant geab in ImplantBonuses)
            {
                if (geab.Slot - 1 == (int)eveAttribute)
                {
                    result = geab.Name;
                    break;
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

            result = getImplantValue(eveAttribute);

            int learningLevel = m_skillGroups["Learning"]["Learning"].Level;
            learningBonus = 1.0 + (0.02 * learningLevel);
            return result * learningBonus;
        }

        public IList<UserImplant> ImplantBonuses
        {
            get { return m_CurrentImplants; }
        }

        private void m_implantSet_Changed(object sender, EventArgs e)
        {
            OnImplantSetChanged();
        }

        private void OnImplantSetChanged()
        {
            OnAttributeChanged();
            FireEvent(delegate
                          {
                              if (ImplantSetChanged != null)
                              {
                                  ImplantSetChanged(this, new EventArgs());
                              }
                          }, "implantset");
        }

        public event EventHandler ImplantSetChanged;

        public Dictionary<string, SkillGroup> SkillGroups
        {
            get { return m_skillGroups; }
        }

        public Dictionary<int, Skill> AllSkillsByTypeID
        {
            get { return m_AllSkillsByID; }
        }

        private int m_cachedSkillPointTotal = -1;
        private int m_cachedKnownSkillCount = -1;
        private Dictionary<int, int> m_cachedLevelSkillsCount = null;

        private List<Skill> m_skillsChanged = new List<Skill>();

        /// <summary>
        /// Fired when skill changes sp, changes Known status or starts/stops training
        /// </summary>
        /// <param name="gs"></param>
        private void OnSkillChanged(Skill gs)
        {
            m_cachedSkillPointTotal = -1;
            m_cachedKnownSkillCount = -1;
            m_cachedLevelSkillsCount = null;
            lock (m_skillsChanged)
            {
                m_skillsChanged.Add(gs);
            }

            FireEvent(delegate
                          {
                              Skill[] mySkillsChanged;
                              lock (m_skillsChanged)
                              {
                                  mySkillsChanged = new Skill[m_skillsChanged.Count];
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

        public Skill GetSkill(string skillName)
        {
            foreach (SkillGroup gsg in m_skillGroups.Values)
            {
                Skill gs = gsg[skillName];
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
                    foreach (SkillGroup gsg in m_skillGroups.Values)
                    {
                        foreach (Skill gs in gsg)
                        {
                            if (!gs.InTraining)
                            {
                                m_cachedSkillPointTotal += gs.CurrentSkillPoints;
                            }
                        }
                    }
                }

                Skill cts = this.CurrentlyTrainingSkill;
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
                    foreach (SkillGroup gsg in m_skillGroups.Values)
                    {
                        m_cachedKnownSkillCount += gsg.KnownCount;
                    }
                }
                return m_cachedKnownSkillCount;
            }
        }

        public int SkillCountAtLevel(int level)
        {
            if (m_cachedLevelSkillsCount == null)
            {
                m_cachedLevelSkillsCount = new Dictionary<int, int>();
                for (int i=1;i<6;i++)
                {
                    m_cachedLevelSkillsCount.Add(i,0);
                    foreach (SkillGroup sg in m_skillGroups.Values)
                    {
                        m_cachedLevelSkillsCount[i] += sg.GetSkillsAtLevel(i);
                    }
                }
            }
            return m_cachedLevelSkillsCount[level];
        }

        private Skill m_currentlyTrainingSkill;

        public Skill CurrentlyTrainingSkill
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

        public DateTime XMLExpires
        {
            get { return m_expires; }
            set { m_expires = value; }
        }

        public void AssignFromSerializableSkillTrainingInfo(SerializableSkillTrainingInfo sti)
        {
            this.SuppressEvents();
            this.check_training_skills(sti);
            this.ResumeEvents();
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
            this.XMLExpires = ci.XMLExpires;
            if (ci.IsCached == true)
            {
                this.EVEFolder = ci.EVEFolder;
            }
            this.Balance = ci.Balance;

            bool getcurrent = false;

            if (this.implantSets.Count == 0)
            {
                if (ci.ImplantSets.Count == 0)
                    getcurrent = true;
                foreach (SerializableImplantSet x in ci.ImplantSets)
                {
                    UserImplant[] z = new UserImplant[10];
                    foreach (UserImplant y in x.Implants)
                    {
                        if (y != null)
                            z[y.Slot - 1] = y;
                    }
                    string key = string.Empty;
                    switch (x.Number)
                    {
                        case 0:
                            key = "Auto";
                            break;
                        case 1:
                            key = "Current";
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            key = "Clone " + (x.Number - 1);
                            break;
                        default:
                            key = "Unknown";
                            break;
                    }
                    this.implantSets.Add(key, new ImplantSet(z));
                }
            }

            this.BasePerception = ci.Attributes.BasePerception;
            this.BaseMemory = ci.Attributes.BaseMemory;
            this.BaseWillpower = ci.Attributes.BaseWillpower;
            this.BaseIntelligence = ci.Attributes.BaseIntelligence;
            this.BaseCharisma = ci.Attributes.BaseCharisma;

            List<UserImplant> manualBonuses = new List<UserImplant>();
            foreach (UserImplant x in this.ImplantBonuses)
            {
                if (x.Manual && getcurrent)
                    manualBonuses.Add(x);
            }

            this.ImplantBonuses.Clear();

            Slot[] Implants = Slot.GetImplants();

            if (this.implantSets.ContainsKey("Auto"))
                this.implantSets.Remove("Auto");

            foreach (SerializableEveAttributeBonus bonus in ci.AttributeBonuses.Bonuses)
            {
                int slot = UserImplant.AttribToSlot(bonus.EveAttribute);
                if (!bonus.Manual)
                {
                    if (slot != 0)
                    {
                        if (!this.implantSets.ContainsKey("Auto"))
                        {
                            this.implantSets.Add("Auto", new ImplantSet());
                        }
                        this.implantSets["Auto"][slot - 1] = new UserImplant(slot, Implants[slot - 1][bonus.Name], bonus.Manual);
                    }
                }
                else
                {
                    if (slot != 0 && getcurrent)
                    {
                        if (!this.implantSets.ContainsKey("Current"))
                        {
                            this.implantSets.Add("Current", new ImplantSet());
                        }
                        Implant x = Implants[slot - 1][bonus.Name];
                        if (x == null)
                        {
                            x = new Implant();
                            x.Name = bonus.Name;
                            x.Bonus = bonus.Amount;
                        }
                        this.implantSets["Current"][slot - 1] = new UserImplant(slot, x, bonus.Manual);
                    }
                }
            }
            foreach (UserImplant tb in manualBonuses)
            {
                if (tb.Manual)
                {
                    if (tb.Slot != 0)
                    {
                        if (!this.implantSets.ContainsKey("Current"))
                        {
                            this.implantSets.Add("Current", new ImplantSet());
                        }
                        Implant x = Implants[tb.Slot - 1][tb.Name];
                        if (x == null)
                        {
                            x = new Implant();
                            x.Name = tb.Name;
                            x.Bonus = tb.Bonus;
                        }
                        this.implantSets["Current"][tb.Slot - 1] = new UserImplant(tb.Slot, x, tb.Manual);
                    }
                }
            }
            if (this.implantSets.ContainsKey("Auto"))
            {
                if (!this.implantSets.ContainsKey("Current"))
                {
                    for (int i = 0; i < this.implantSets["Auto"].Array.GetLength(0); i++)
                    {
                        UserImplant x = this.implantSets["Auto"].Array[i];
                        if (x != null)
                            this.ImplantBonuses.Add(x);
                    }
                }
                else
                {
                    for (int i = 0; i < Math.Max(this.implantSets["Auto"].Array.GetLength(0), this.implantSets["Current"].Array.GetLength(0)); i++)
                    {
                        UserImplant x = null;
                        if (i < this.implantSets["Auto"].Array.GetLength(0))
                            x = this.implantSets["Auto"].Array[i];
                        UserImplant y = null;
                        if (i < this.implantSets["Current"].Array.GetLength(0))
                            y = this.implantSets["Current"].Array[i];
                        if (y != null)
                            this.ImplantBonuses.Add(y);
                        else if (x != null)
                            this.ImplantBonuses.Add(x);
                    }
                }
            }
            else if (this.implantSets.ContainsKey("Current"))
            {
                for (int i = 0; i < this.implantSets["Current"].Array.GetLength(0); i++)
                {
                    UserImplant x = this.implantSets["Current"].Array[i];
                    if (x != null)
                        this.ImplantBonuses.Add(x);
                }
            }

            foreach (SerializableSkillGroup sg in ci.SkillGroups)
            {
                SkillGroup gsg = m_skillGroups[sg.Name];
                foreach (SerializableSkill s in sg.Skills)
                {
                    Skill gs = gsg[s.Name];
                    if (gs == null)
                    {
                        MessageBox.Show("The character cache contains the unknown skill " + s.Name + ", which will be removed.",
                                    "Unknown skill", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        gs.CurrentSkillPoints = s.SkillPoints;
                        gs.Known = true;
                        if (ci.TimeLeftInCache != -1)
                            gs.LastConfirmedLvl = gs.Level;
                        else
                            gs.LastConfirmedLvl = s.LastConfirmedLevel;
                    }
                }
            }
            check_training_skills(ci.TrainingSkillInfo);
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
                    Skill newTrainingSkill = this.GetSkill(old_skill.old_SkillName);
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

        public void UpdateOwnedSkills()
        {
            List<String> owned = new List<string>();
            foreach (SkillGroup sg in m_skillGroups.Values)
            {
                owned.AddRange(sg.OwnedSkills());
            }
            Settings.GetInstance().SetOwnedBooks(m_name, owned);
        }

        private SerializableSkillTrainingInfo m_SkillInTraining;

        public SerializableSkillTrainingInfo SerialSIT
        {
            get { return m_SkillInTraining; }
        }

        public void check_training_skills(SerializableSkillTrainingInfo SkillInTraining)
        {
            // This is called from AssignFromSerializableCharacterInfo(SerializableCharacterInfo ci)
            // This is where normal running takes you in the standard run of the mill operation of EVEMon
            // 
            // check if old skill is complete in the current character data and if not, set to currenttrainingskill
            if (this.CurrentlyTrainingSkill != null && (SkillInTraining == null || (SkillInTraining != null && SkillInTraining.TrainingSkillWithTypeID != this.CurrentlyTrainingSkill.Id) || ((TimeSpan)(((DateTime)SkillInTraining.getTrainingEndTime.Subtract(TimeSpan.FromMilliseconds(SkillInTraining.TQOffset))).Subtract(this.CurrentlyTrainingSkill.EstimatedCompletion))).Duration() > new TimeSpan(0, 3, 30)))
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
                    string skill_name = m_AllSkillsByID[SkillInTraining.TrainingSkillWithTypeID].Name;
                    int level = SkillInTraining.TrainingSkillToLevel;
                    Skill newTrainingSkill = this.GetSkill(skill_name);
                    int EstCurrentSP = SkillInTraining.EstimatedCurrentPoints;
                    if (newTrainingSkill != null)
                    {
                        if (SkillInTraining.TrainingSkillDestinationSP <= EstCurrentSP)
                        {
                            if (old_skill.old_skill_completed && skill_name == old_skill.old_SkillName && level == old_skill.old_TrainingToLevel)
                            {
                                newTrainingSkill.CurrentSkillPoints = newTrainingSkill.GetPointsRequiredForLevel(level);
                            }
                            if (old_skill == null || !old_skill.old_skill_completed || old_skill.old_SkillName == null || (old_skill.old_SkillName != null && (skill_name != old_skill.old_SkillName || (skill_name == old_skill.old_SkillName && SkillInTraining.TrainingSkillToLevel != old_skill.old_TrainingToLevel))))
                            {
                                this.GetSkill(skill_name).CurrentSkillPoints = newTrainingSkill.GetPointsRequiredForLevel(level);
                                old_skill = new OldSkillinfo(skill_name, SkillInTraining.TrainingSkillToLevel, true, ((DateTime)SkillInTraining.getTrainingEndTime.Subtract(TimeSpan.FromMilliseconds(SkillInTraining.TQOffset))).ToLocalTime());
                                OnDownloadAttemptComplete(this.Name, skill_name, true);
                            }
                        }
                        else if (SkillInTraining.TrainingSkillDestinationSP > EstCurrentSP)
                        {
                            m_SkillInTraining = SkillInTraining;
                            newTrainingSkill.SetTrainingInfo(level, ((DateTime)SkillInTraining.getTrainingEndTime.Subtract(TimeSpan.FromMilliseconds(SkillInTraining.TQOffset))).ToLocalTime());
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
                    string _name = m_AllSkillsByID[SkillInTraining.TrainingSkillWithTypeID].Name;
                    Skill newTrainingSkill = this.GetSkill(_name);
                    if (newTrainingSkill != null)
                    {
                        old_skill = new OldSkillinfo(_name, SkillInTraining.TrainingSkillToLevel, newTrainingSkill.CurrentSkillPoints >= SkillInTraining.TrainingSkillDestinationSP, ((DateTime)SkillInTraining.getTrainingEndTime.Subtract(TimeSpan.FromMilliseconds(SkillInTraining.TQOffset))).ToLocalTime());
                    }
                    m_SkillInTraining = SkillInTraining;
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
            Skill newlyCompletedSkill = this.GetSkill(skillName);
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
            Skill temp = this.GetSkill(skillName);
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
            ci.XMLExpires = this.XMLExpires;

            ci.ImplantSets.Clear();
            foreach (string x in this.implantSets.Keys)
            {
                SerializableImplantSet z = new SerializableImplantSet(this.implantSets[x].Array);
                switch (x)
                { 
                    case "Auto":
                        z.Number = 0;
                        break;
                    case "Current":
                        z.Number = 1;
                        break;
                    case "Clone 1":
                        z.Number = 2;
                        break;
                    case "Clone 2":
                        z.Number = 3;
                        break;
                    case "Clone 3":
                        z.Number = 4;
                        break;
                    case "Clone 4":
                        z.Number = 5;
                        break;
                    case "Clone 5":
                        z.Number = 6;
                        break;
                    default:
                        z.Number = -1;
                        break;
                }
                ci.ImplantSets.Add(z);
            }

            ci.Attributes.BaseIntelligence = this.BaseIntelligence;
            ci.Attributes.BaseCharisma = this.BaseCharisma;
            ci.Attributes.BasePerception = this.BasePerception;
            ci.Attributes.BaseMemory = this.BaseMemory;
            ci.Attributes.BaseWillpower = this.BaseWillpower;

            foreach (UserImplant geab in this.ImplantBonuses)
            {
                SerializableEveAttributeBonus eab = null;
                switch (geab.Slot)
                {
                    case 1:
                        eab = new SerializablePerceptionBonus();
                        break;
                    case 2:
                        eab = new SerializableMemoryBonus();
                        break;
                    case 3:
                        eab = new SerializableWillpowerBonus();
                        break;
                    case 4:
                        eab = new SerializableIntelligenceBonus();
                        break;
                    case 5:
                        eab = new SerializableCharismaBonus();
                        break;
                }
                if (eab != null)
                {
                    eab.Name = geab.Name;
                    eab.Amount = geab.Bonus;
                    eab.Manual = geab.Manual;
                    ci.AttributeBonuses.Bonuses.Add(eab);
                }
            }

            foreach (SkillGroup gsg in this.SkillGroups.Values)
            {
                SerializableSkillGroup sg = new SerializableSkillGroup();
                bool added = false;
                foreach (Skill gs in gsg)
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
                        s.LastConfirmedLevel = gs.LastConfirmedLvl;
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

            ci.TrainingSkillInfo = this.m_SkillInTraining;
                /*
                SerializableSkillInTraining sit = new SerializableSkillInTraining();
                sit.SkillName = gsit.Name;
                sit.TrainingToLevel = gsit.TrainingToLevel;
                sit.CurrentPoints = gsit.CurrentSkillPoints;
                sit.EstimatedCompletion = gsit.EstimatedCompletion;
                sit.NeededPoints = gsit.GetPointsRequiredForLevel(gsit.TrainingToLevel);
                ci.SkillInTraining = sit;
                */

            return ci;
        }

        public TimeSpan GetTrainingTimeToMultipleSkills(IEnumerable<Pair<Skill, int>> skills)
        {
            Dictionary<Skill, int> trainedAlready = new Dictionary<Skill, int>();
            TimeSpan result = TimeSpan.Zero;

            foreach (Pair<Skill, int> ts in skills)
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

        public int DownloadFailed
        {
            get
            {
                return m_downloadfailed;
            }
            set
            {
                m_downloadfailed = value;
            }
        }
    }

    public delegate void SkillChangedHandler(object sender, SkillChangedEventArgs e);

    public class SkillChangedEventArgs : EventArgs
    {
        private Skill[] m_skillList;

        public ICollection<Skill> SkillList
        {
            get { return m_skillList; }
        }

        public SkillChangedEventArgs(Skill gs)
        {
            m_skillList = new Skill[1];
            m_skillList[0] = gs;
        }

        public SkillChangedEventArgs(Skill[] skillList)
        {
            m_skillList = new Skill[skillList.Length];
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
