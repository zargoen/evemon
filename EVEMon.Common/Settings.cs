using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using EVEMon.Common.Schedule;

namespace EVEMon.Common
{
    [XmlRoot("logindata2")]
    public class Settings
    {
        #region G15 Display
        private bool m_g15usedisplay = true;
        private bool m_g15staticskillshow = false;
        private bool m_g15staticshow = false;
        private TimeSpan m_g15skillinterval = TimeSpan.FromSeconds(3);
        private TimeSpan m_g15interval = TimeSpan.FromSeconds(3);
        private TimeSpan m_g15updatespeed = TimeSpan.FromMilliseconds(50);
        public bool G15UseG15Display { get { return m_g15usedisplay; } set { m_g15usedisplay = value; } }
        public bool G15StaticSkill { get { return m_g15staticskillshow; } set { m_g15staticskillshow = value; } }
        public bool G15Static { get { return m_g15staticshow; } set { m_g15staticshow = value; } }

        public double G15SkillIntervalDbl { get { return m_g15skillinterval.TotalMilliseconds; } set { m_g15skillinterval = TimeSpan.FromMilliseconds(value); } }
        public double G15IntervalDbl { get { return m_g15interval.TotalMilliseconds; } set { m_g15interval = TimeSpan.FromMilliseconds(value); } }
        public double G15UpdateSpeedDbl { get { return m_g15updatespeed.TotalMilliseconds; } set { m_g15updatespeed = TimeSpan.FromMilliseconds(value); } }

        [XmlIgnore]
        public TimeSpan G15SkillInterval { get { return m_g15skillinterval; } set { m_g15skillinterval = value; } }
        [XmlIgnore]
        public TimeSpan G15Interval { get { return m_g15interval; } set { m_g15interval = value; } }
        [XmlIgnore]
        public TimeSpan G15UpdateSpeed { get { return m_g15updatespeed; } set { m_g15updatespeed = value; } }

        #endregion

        private string m_username;

        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        private string m_password;

        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        private string m_character;

        public string Character
        {
            get { return m_character; }
            set { m_character = value; }
        }        

        #region Skill Planner Highlighting
        private bool m_HighlightPlannedSkills;
        public event EventHandler<EventArgs> HighlightPlannedSkillsChanged;
        public bool SkillPlannerHighlightPlannedSkills
        {
            get { return m_HighlightPlannedSkills; }
            set { m_HighlightPlannedSkills = value; OnHighlightPlannedSkillsChanged(); }
        }

        private void OnHighlightPlannedSkillsChanged()
        {
            if (HighlightPlannedSkillsChanged != null)
                HighlightPlannedSkillsChanged(this, new EventArgs());
        }

        private bool m_HighlightPrerequisites;
        public event EventHandler<EventArgs> HighlightPrerequisitesChanged;
        public bool SkillPlannerHighlightPrerequisites
        {
            get { return m_HighlightPrerequisites; }
            set { m_HighlightPrerequisites = value; OnHighlightPrerequisitesChanged(); }
        }

        private void OnHighlightPrerequisitesChanged()
        {
            if (HighlightPrerequisitesChanged != null)
                HighlightPrerequisitesChanged(this, new EventArgs());
        }
        #endregion

        private List<CharLoginInfo> m_characterList = new List<CharLoginInfo>();

        public List<CharLoginInfo> CharacterList
        {
            get { return m_characterList; }
            set { m_characterList = value; }
        }

        private ToolTipDisplayOptions m_tooltipOptions = ToolTipDisplayOptions.Name | ToolTipDisplayOptions.Skill | ToolTipDisplayOptions.TimeRemaining;

        public ToolTipDisplayOptions TooltipOptions
        {
            get { return m_tooltipOptions; }
            set { m_tooltipOptions = value; }
        }

        private List<CharFileInfo> m_charFileList = new List<CharFileInfo>();

        public List<CharFileInfo> CharFileList
        {
            get { return m_charFileList; }
            set { m_charFileList = value; }
        }

        private bool m_enableEmailAlert = false;

        public bool EnableEmailAlert
        {
            get { return m_enableEmailAlert; }
            set { m_enableEmailAlert = value; }
        }

        # region XML Update
        private bool m_DisableXMLAutoUpdate;

        public bool DisableXMLAutoUpdate
        {
            get { return m_DisableXMLAutoUpdate; }
            set { m_DisableXMLAutoUpdate = value; }
        }

        private bool m_DeleteCharacterSilently;

        public bool DeleteCharacterSilently
        {
            get { return m_DeleteCharacterSilently; }
            set { m_DeleteCharacterSilently = value; }
        }

        private bool m_KeepCharacterPlans;

        public bool KeepCharacterPlans
        {
            get { return m_KeepCharacterPlans; }
            set { m_KeepCharacterPlans = value; }
        }

        public bool ResetCache()
        {
            if (File.Exists(SettingsFileName))
            {
                try
                {
                    File.Delete(SettingsFileName);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
       

        private bool m_EnableSkillCompleteDialog;

        public bool EnableSkillCompleteDialog
        {
            get { return m_EnableSkillCompleteDialog; }
            set { m_EnableSkillCompleteDialog = value; }
        }
        #endregion

        private bool m_DisableEVEMonVersionCheck;

        public bool DisableEVEMonVersionCheck
        {
            get { return m_DisableEVEMonVersionCheck; }
            set { m_DisableEVEMonVersionCheck = value; }
        }	

        private bool m_enableBalloonTips = true;

        public bool EnableBalloonTips
        {
            get { return m_enableBalloonTips; }
            set { m_enableBalloonTips = value; }
        }

        private bool m_closeToTray = false;

        public bool CloseToTray
        {
            get { return m_closeToTray; }
            set { m_closeToTray = value; }
        }

        #region Email Settings
        private string m_emailServer;

        public string EmailServer
        {
            get { return m_emailServer; }
            set { m_emailServer = value; }
        }

        private bool m_emailServerRequiresSsl = false;

        public bool EmailServerRequiresSsl
        {
            get { return m_emailServerRequiresSsl; }
            set { m_emailServerRequiresSsl = value; }
        }

        private bool m_emailAuthRequired = false;

        public bool EmailAuthRequired
        {
            get { return m_emailAuthRequired; }
            set { m_emailAuthRequired = value; }
        }

        private string m_emailUsername;
        private string m_emailPassword;

        public string EmailAuthUsername
        {
            get { return m_emailUsername; }
            set { m_emailUsername = value; }
        }

        public string EmailAuthPassword
        {
            get { return m_emailPassword; }
            set { m_emailPassword = value; }
        }

        private string m_emailFromAddress;

        public string EmailFromAddress
        {
            get { return m_emailFromAddress; }
            set { m_emailFromAddress = value; }
        }

        private string m_emailToAddress;

        public string EmailToAddress
        {
            get { return m_emailToAddress; }
            set { m_emailToAddress = value; }
        }

        private int m_portNumber;

        public int PortNumber
        {
            get { return m_portNumber; }
            set { m_portNumber = value; }
        }

        private bool m_emailUseShortFormat = false;

        public bool EmailUseShortFormat
        {
            get { return m_emailUseShortFormat; }
            set { m_emailUseShortFormat = value; }
        }

        #endregion // Email Settings

        private SystemTrayDisplayOptions m_systemTrayOptions = SystemTrayDisplayOptions.Minimized;

        public SystemTrayDisplayOptions SystemTrayOptions
        {
            get { return m_systemTrayOptions; }
            set { m_systemTrayOptions = value; }
        }

        public bool SystemTrayOptionsIsNever
        {
            get { return m_systemTrayOptions == SystemTrayDisplayOptions.Never; }
        }

        public bool SystemTrayOptionsIsMinimized
        {
            get { return m_systemTrayOptions == SystemTrayDisplayOptions.Minimized; }
        }

        public bool SystemTrayOptionsIsAlways
        {
            get { return m_systemTrayOptions == SystemTrayDisplayOptions.Always; }
        }

        private string m_ignoreUpdateVersion = "0.0.0.0";

        public string IgnoreUpdateVersion
        {
            get { return m_ignoreUpdateVersion; }
            set {
                Version v = new Version("0.0.0.0");
                try
                {
                    v = new Version(value);
                }
                catch (Exception e)
                {
                    ExceptionHandler.LogException(e, false);
                }
                m_ignoreUpdateVersion = v.ToString();
            }
        }

        private bool m_titleToTime = true;

        public bool TitleToTime
        {
            get { return m_titleToTime; }
            set { m_titleToTime = value; }
        }

        private int m_titleToTimeLayout = 0;

        public int TitleToTimeLayout 
        {
            get { return m_titleToTimeLayout; }
            set { m_titleToTimeLayout = value; }
        }

        #region Plan Settings
        private List<Pair<string, Plan>> m_plans = new List<Pair<string, Plan>>();

        public List<Pair<string, Plan>> Plans
        {
            get { return m_plans; }
        }

        private const string PLAN_DEFAULT = "Default Plan";

        public IEnumerable<string> GetPlansForCharacter(string charName)
        {
            foreach (Pair<string, Plan> x in m_plans)
            {
                if (x.A == charName)
                    yield return PLAN_DEFAULT;
                else if (x.A.StartsWith(charName + "::"))
                    yield return x.A.Substring(charName.Length + 2);
            }
        }

        public Plan GetPlanByName(string charName, string planName)
        {
            Plan p = null;
            foreach (Pair<string, Plan> x in m_plans)
            {
                if (planName == PLAN_DEFAULT && x.A == charName)
                {
                    x.B.Name = PLAN_DEFAULT;
                    p =  x.B;
                    break;
                }
                else if (x.A == charName + "::" + planName)
                {
                    x.B.Name = planName;
                    p =  x.B;
                    break;
                }
            }
            if (p == null)
                return p;

            SerializableCharacterInfo sci = this.GetCharacterInfo(charName);
            if (sci != null)
            {
                GrandCharacterInfo gci = new GrandCharacterInfo(sci.CharacterId, charName);
                gci.AssignFromSerializableCharacterInfo(GetCharacterInfo(charName));

                p.GrandCharacterInfo = gci;
            }
            else
            {
                p.GrandCharacterInfo = null;
            }
            return p;
        }

        public void AddPlanFor(string charName, Plan plan, string planName)
        {
            if (GetPlanByName(charName, planName) != null)
                throw new ApplicationException("That plan already exists.");

            Pair<string, Plan> p = new Pair<string, Plan>();
            if (planName == PLAN_DEFAULT)
                p.A = charName;
            else
                p.A = charName + "::" + planName;
            p.B = plan;
            m_plans.Add(p);

            plan.Name = planName;
            this.Save();
        }

        public void RemovePlanFor(string charName, string planName)
        {
            for (int i = 0; i < m_plans.Count; i++)
            {
                if (planName == PLAN_DEFAULT && m_plans[i].A == charName)
                {
                    Plan p = m_plans[i].B;
                    p.CloseEditor();
                    m_plans.RemoveAt(i);
                    i--;
                }
                else if (m_plans[i].A == charName + "::" + planName)
                {
                    Plan p = m_plans[i].B;
                    p.CloseEditor();
                    m_plans.RemoveAt(i);
                    i--;
                }
            }
            this.Save();
        }

        public bool RenamePlanFor(string charName, string planName, string newName)
        {
            if (GetPlanByName(charName, newName) != null)
                return false;

            bool found = false;
            for (int i = 0; i < m_plans.Count; i++)
            {
                if (planName == PLAN_DEFAULT && m_plans[i].A == charName)
                {
                    m_plans[i].A = charName + "::" + newName;
                    found = true;
                    break;
                }
                else if (m_plans[i].A == charName + "::" + planName)
                {
                    if (newName != PLAN_DEFAULT)
                        m_plans[i].A = charName + "::" + newName;
                    else
                        m_plans[i].A = charName;
                    found = true;
                    break;
                }
            }
            this.Save();
            return found;
        }

        public void RemoveAllPlansFor(string charName)
        {
            for (int i = 0; i < m_plans.Count; i++)
            {
                if (m_plans[i].A.StartsWith(charName + "::") || m_plans[i].A == charName)
                {
                    Plan p = m_plans[i].B;
                    p.CloseEditor();
                    m_plans.RemoveAt(i);
                    i--;
                }
            }
            this.Save();
        }

        public void RearrangePlansFor(string charName, List<string> newOrder)
        {
            List<Pair<string, Plan>> plans = new List<Pair<string, Plan>>();
            for (int i = 0; i < newOrder.Count; i++)
            {
                plans.Add(null);
            }
            for (int i = 0; i < m_plans.Count; i++)
            {
                if (m_plans[i].A.StartsWith(charName + "::") || m_plans[i].A == charName)
                {
                    Pair<string, Plan> tp = m_plans[i];
                    m_plans.RemoveAt(i);
                    i--;

                    bool added = false;
                    string tPlanName = null;
                    if (tp.A == charName)
                        tPlanName = PLAN_DEFAULT;
                    else
                        tPlanName = tp.A.Substring(tp.A.IndexOf("::") + 2);
                    for (int x = 0; x < newOrder.Count; x++)
                    {
                        if (newOrder[x] == tPlanName)
                        {
                            plans[x] = tp;
                            added = true;
                            break;
                        }
                    }
                    if (!added)
                        plans.Add(tp);
                }
            }
            foreach (Pair<string, Plan> p in plans)
            {
                if (p != null)
                    m_plans.Add(p);
            }
            this.Save();
        }
        #endregion Plan Settings

        #region Character Cache
        private List<SerializableCharacterInfo> m_cachedCharacterInfo = new List<SerializableCharacterInfo>();

        public List<SerializableCharacterInfo> CachedCharacterInfo
        {
            get { return m_cachedCharacterInfo; }
        }

        public SerializableCharacterInfo GetCharacterInfo(string charName)
        {
            foreach (SerializableCharacterInfo sci in m_cachedCharacterInfo)
            {
                if (sci.Name == charName)
                    return sci;
            }
            return null;
        }

        public void RemoveCharacterCache(string charName)
        {
            for (int i = 0; i < m_cachedCharacterInfo.Count; i++)
            {
                if (m_cachedCharacterInfo[i].Name == charName)
                    m_cachedCharacterInfo.RemoveAt(i);
            }
        }

        public void SetCharacterCache(SerializableCharacterInfo sci)
        {
            RemoveCharacterCache(sci.Name);
            sci.IsCached = true;
            m_cachedCharacterInfo.Add(sci);
        }

        private List<string> m_confirmedTips = new List<string>();

        public List<string> ConfirmedTips
        {
            get { return m_confirmedTips; }
        }

        private List<Pair<string, string>> m_collapsedGroups = new List<Pair<string, string>>();

        public List<Pair<string, string>> CollapsedGroups
        {
            get { return m_collapsedGroups; }
        }

        private List<Pair<string, OldSkillinfo>> m_oldskilllearnt = new List<Pair<string, OldSkillinfo>>();

        public List<Pair<string, OldSkillinfo>> OldSkillLearnt
        {
            get { return m_oldskilllearnt; }
        }

        private PlanTextOptions m_defaultCopyOptions = new PlanTextOptions();
        private PlanTextOptions m_defaultSaveOptions = new PlanTextOptions();

        public PlanTextOptions DefaultCopyOptions
        {
            get { return m_defaultCopyOptions; }
            set { m_defaultCopyOptions = value; }
        }

        public PlanTextOptions DefaultSaveOptions
        {
            get { return m_defaultSaveOptions; }
            set { m_defaultSaveOptions = value; }
        }
        #endregion // Character Cache

        #region Worksafe Settings
        private bool m_worksafeMode = false;

        public bool WorksafeMode
        {
            get { return m_worksafeMode; }
            set { m_worksafeMode = value; OnWorksafeChanged(); }
        }

        private void OnWorksafeChanged()
        {
            if (WorksafeChanged != null)
                WorksafeChanged(this, new EventArgs());
        }

        public event EventHandler<EventArgs> WorksafeChanged;
        #endregion // Worksafe Settings

        private bool m_playSoundOnSkillComplete = true;

        public bool PlaySoundOnSkillComplete
        {
            get { return m_playSoundOnSkillComplete; }
            set { m_playSoundOnSkillComplete = value; }
        }

        #region In Game Browser server
        private bool m_runIgbServer = true;

        public bool RunIGBServer
        {
            get { return m_runIgbServer; }
            set { m_runIgbServer = value; OnRunIGBServerChanged(); }
        }

        private void OnRunIGBServerChanged()
        {
            if (RunIGBServerChanged != null)
                RunIGBServerChanged(this, new EventArgs());
        }

        public event EventHandler<EventArgs> RunIGBServerChanged;
        #endregion // In Game Browser server

        private bool m_relocateEveWindow = false;

        public bool RelocateEveWindow
        {
            get { return m_relocateEveWindow; }
            set
            {
                m_relocateEveWindow = value;
                OnRelocateEveWindowChanged();
            }
        }

        private void OnRelocateEveWindowChanged()
        {
            if (RelocateEveWindowChanged != null)
                RelocateEveWindowChanged(this, new EventArgs());
        }

        public event EventHandler<EventArgs> RelocateEveWindowChanged;

        private int m_relocateTargetScreen = 0;

        public int RelocateTargetScreen
        {
            get { return m_relocateTargetScreen; }
            set
            {
                if (m_relocateTargetScreen != value)
                {
                    m_relocateTargetScreen = value;
                    OnRelocateEveWindowChanged();
                }
            }
        }

        private int m_skillIconGroup = 0;

        public int SkillIconGroup
        {
            get { return m_skillIconGroup; }
            set { m_skillIconGroup = value; }
        }

        private bool m_useCustomProxySettings = false;

        public bool UseCustomProxySettings
        {
            get { return m_useCustomProxySettings; }
            set { m_useCustomProxySettings = value; }
        }

        private ProxySetting m_httpProxy = new ProxySetting();

        public ProxySetting HttpProxy
        {
            get { return m_httpProxy; }
            set { m_httpProxy = value; }
        }

        #region Tranquility Status
        private bool m_checkTranquilityStatus = true;

        public bool CheckTranquilityStatus
        {
            get { return m_checkTranquilityStatus; }
            set 
            {
                    if (m_checkTranquilityStatus != value)
                    {
                    m_checkTranquilityStatus = value;
                    OnCheckTranquilityStatusChanged();
                    }
            }
        }

        private void OnCheckTranquilityStatusChanged()
        {
            if (CheckTranquilityStatusChanged != null)
                CheckTranquilityStatusChanged(this, new EventArgs());
        }

        public event EventHandler<EventArgs> CheckTranquilityStatusChanged;

        private int m_statusUpdateInterval = 5;

        public int StatusUpdateInterval
        {
            get { return m_statusUpdateInterval; }
            set {m_statusUpdateInterval = value; }
        }
        #endregion // Tranquility Status

        #region Schedule Entries
        private List<ScheduleEntry> m_schedule = new List<ScheduleEntry>();

        [XmlArrayItem("simple", typeof(SimpleScheduleEntry))]
        [XmlArrayItem("recur", typeof(RecurringScheduleEntry))]
        public List<ScheduleEntry> Schedule
        {
            get { return m_schedule; }
            set { m_schedule = value; }
        }
        #endregion //Schedule Entries

        private SerializableDictionary<string, int> m_savedSplitterDistances = new SerializableDictionary<string, int>();

        public SerializableDictionary<string, int> SavedSplitterDistances
        {
            get { return m_savedSplitterDistances; }
            set { m_savedSplitterDistances = value; }
        }
	
        private SerializableDictionary<string, Rectangle> m_savedWindowLocations = new SerializableDictionary<string, Rectangle>();

        public SerializableDictionary<string, Rectangle> SavedWindowLocations
        {
            get { return m_savedWindowLocations; }
            set { m_savedWindowLocations = value; }
        }

        #region Settings File Save / Load
        private const string STORE_FILE_NAME = "evemon-logindata{0}.xml";

        private static string StoreFileName(string key)
        {
            return String.Format(STORE_FILE_NAME, key);
        }

        private static Settings m_instance = null;

        public static Settings GetInstance()
        {
            return LoadFromKey(String.Empty);
        }

        public static Settings LoadFromKey(string key)
        {
            if (m_instance != null)
                return m_instance;

            try
            {
                if (File.Exists(SettingsFileName))
                {
                    using (FileStream fs = new FileStream(SettingsFileName, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        Settings result = (Settings)xs.Deserialize(fs);
                        result.SetKey(key);
                        m_instance = result;
                        return result;
                    }
                }
                else
                {
                    Settings r = LoadFromKeyFromIsoStorage(key);
                    m_instance = r;
                    return r;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, true);
                Settings rr = LoadFromKeyFromIsoStorage(key);
                m_instance = rr;
                return rr;
            }
        }

        private static Settings LoadFromKeyFromIsoStorage(string key)
        {
            try
            {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(StoreFileName(key), FileMode.Open))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(s);

                    if (xdoc.DocumentElement.Name == "logindata")
                    {
                        Settings result = new Settings();
                        result.SetKey(key);
                        result.Username = ((XmlElement)xdoc.SelectSingleNode("/logindata/username")).GetAttribute("value");
                        result.Password = ((XmlElement)xdoc.SelectSingleNode("/logindata/password")).GetAttribute("value");
                        XmlNode cn = xdoc.SelectSingleNode("/logindata/character");
                        if (cn != null)
                        {
                            result.Character = (cn as XmlElement).GetAttribute("value");
                        }
                        s.Close();
                        store.Close();
                        result.Save();
                        return result;
                    }
                    else if (xdoc.DocumentElement.Name == "logindata2")
                    {
                        s.Seek(0, SeekOrigin.Begin);
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        Settings result = (Settings)xs.Deserialize(s);
                        result.SetKey(key);
                        return result;
                    }
                    else
                    {
                        Settings result = new Settings();
                        result.SetKey(key);
                        return result;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                ExceptionHandler.LogException(e, true);
                Settings s = new Settings();
                s.SetKey(key);
                return s;
            }
        }

        private bool m_neverSave = false;

        public void NeverSave()
        {
            m_neverSave = true;
        }

        [XmlIgnore]

        public static string EveMonData
        {
            get
            {   
                string m_DataDir = Directory.GetCurrentDirectory();
                string fn = m_DataDir + "/settings.xml";
                if (!File.Exists(fn))
                {
                    m_DataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EVEMon";
                 }
                 if (!Directory.Exists(m_DataDir))
                     Directory.CreateDirectory(m_DataDir);
                 if (!Directory.Exists(m_DataDir+"\\cache"))
                     Directory.CreateDirectory(m_DataDir+"\\cache");
                 return m_DataDir;
            }
        }

        public static string SettingsFileName
        {
            get
            {
                string fn = EveMonData + "/settings.xml";
#if DEBUG
                fn = EveMonData + "/settings-debug.xml";
#endif
                return fn;
            }
        }

        public void Save()
        {
            if (!m_neverSave)
            {
                string fn = SettingsFileName;

                //using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain())
                //using (IsolatedStorageFileStream s = new IsolatedStorageFileStream(StoreFileName(m_key), FileMode.Create, store))
                using (FileStream s = new FileStream(fn, FileMode.Create, FileAccess.Write))
                {
                    SaveTo(s);
                }
            }
        }

        public void SaveTo(Stream s)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            xs.Serialize(s, this);
        }

        private string m_key;

        private void SetKey(string key)
        {
            m_key = key;
        }

        public static void ResetKey(string p)
        {
            Settings s = new Settings();
            s.SetKey(p);
            s.Save();
        }
        #endregion // Settings File Save / Load

        public bool AddFileCharacter(CharFileInfo cfi)
        {
            // TODO:
            foreach (CharFileInfo tx in m_charFileList)
            {
                if (cfi.Filename == tx.Filename)
                    return false;
            }
            m_charFileList.Add(cfi);
            this.Save();
            return true;
        }

        public bool AddCharacter(CharLoginInfo cli)
        {
            foreach (CharLoginInfo tx in m_characterList)
            {
                if (cli.CharacterName == tx.CharacterName)
                    return false;
            }
            m_characterList.Add(cli);
            this.Save();
            return true;
        }

        #region Character Settings
        public ICharacterSettings GetCharacterSettings(string userName)
        {
            foreach (ICharacterSettings guy in m_characterList)
            {
                if (guy.CharacterName.Equals(userName))
                {
                    return guy;
                }
            }
            foreach (ICharacterSettings guy in m_charFileList)
            {
                if (guy.CharacterName.Equals(userName))
                {
                    return guy;
                }
            }
            return null;
        }
        #endregion

    }

    

    [XmlRoot("proxySetting")]
    public class ProxySetting: ICloneable
    {
        private string m_host = String.Empty;

        public string Host
        {
            get { return m_host; }
            set { m_host = value; }
        }

        private int m_port;

        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        private ProxyAuthType m_authType = ProxyAuthType.None;

        public ProxyAuthType AuthType
        {
            get { return m_authType; }
            set { m_authType = value; }
        }

        private string m_username = String.Empty;

        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        private string m_password = String.Empty;

        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        #region ICloneable Members

        public ProxySetting Clone()
        {
            return (ProxySetting)((ICloneable)this).Clone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    public enum ProxyAuthType
    {
        None,
        SystemDefault,
        Specified
    }

    [Flags]
    public enum ToolTipDisplayOptions
    {
        Name = 1,
        Skill = 2,
        TimeRemaining = 4,
        TimeFinished = 8,
        Blank = 16
    }

    public enum SystemTrayDisplayOptions
    {
        Never = 1,
        Minimized = 2,
        Always = 3
    }

    [XmlRoot]
    public class SerializableDictionary<TKey, TValue>: Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSer = new XmlSerializer(typeof(TValue));

            reader.Read();
            reader.ReadStartElement("dictionary");
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                TKey key = (TKey)keySer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            if(this.Count != 0)
                reader.ReadEndElement();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSer = new XmlSerializer(typeof(TValue));

            writer.WriteStartElement("dictionary");
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                valueSer.Serialize(writer, this[key]);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion
    }
}
