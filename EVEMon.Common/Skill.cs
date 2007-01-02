using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace EVEMon.Common
{
    public class Skill
    {
        private CharacterInfo m_owner;
        private bool m_public;
        private string m_name;
        private int m_id;
        private string m_description;
        private string m_descriptionNl;
        private EveAttribute m_primaryAttribute;
        private EveAttribute m_secondaryAttribute;
        private int m_rank;
        private IEnumerable<Prereq> m_prereqs;
        private static IDictionary<string, Skill> sm_allSkills;
        private int m_currentSkillPoints;
        private int m_lastConfirmedLvl;

        public Skill(CharacterInfo gci, bool pub, string name, int id, string description,
                          EveAttribute a1, EveAttribute a2, int rank, IEnumerable<Prereq> prereqs)
        {
            m_owner = gci;
            m_public = pub;
            m_name = name;
            m_id = id;
            m_description = description;
            m_descriptionNl = description;
            m_primaryAttribute = a1;
            m_secondaryAttribute = a2;
            m_rank = rank;
            m_prereqs = prereqs;
            m_lastConfirmedLvl = 0;
        }

        public int LastConfirmedLvl
        {
            get { return m_lastConfirmedLvl; }
            set { m_lastConfirmedLvl = value; }
        }

        private SkillGroup m_skillGroup;

        public SkillGroup SkillGroup
        {
            get { return m_skillGroup; }
        }

        internal void SetSkillGroup(SkillGroup gsg)
        {
            if (m_skillGroup != null)
            {
                throw new InvalidOperationException("can only set skillgroup once");
            }

            m_skillGroup = gsg;
        }

        public int CurrentSkillPoints
        {
            get
            {
                if (m_inTraining)
                {
                    TimeSpan timeRemainSpan = m_estimatedCompletion - DateTime.Now;
                    if (timeRemainSpan <= TimeSpan.Zero)
                    {
                        return GetPointsRequiredForLevel(m_trainingToLevel);
                    }

                    return GetPointsRequiredForLevel(m_trainingToLevel) - GetPointsForTimeSpan(timeRemainSpan);
                }
                else
                {
                    return m_currentSkillPoints;
                }
            }
            set
            {
                if (m_currentSkillPoints != value)
                {
                    m_currentSkillPoints = value;
                    OnChanged();
                }
            }
        }

        public bool IsLearningSkill
        {
            get { return (this.AttributeModified != EveAttribute.None); }
        }

        public EveAttribute AttributeModified
        {
            get
            {
                if (m_name == "Analytical Mind" || m_name == "Logic")
                {
                    return EveAttribute.Intelligence;
                }
                else if (m_name == "Empathy" || m_name == "Presence")
                {
                    return EveAttribute.Charisma;
                }
                else if (m_name == "Instant Recall" || m_name == "Eidetic Memory")
                {
                    return EveAttribute.Memory;
                }
                else if (m_name == "Iron Will" || m_name == "Focus")
                {
                    return EveAttribute.Willpower;
                }
                else if (m_name == "Spatial Awareness" || m_name == "Clarity")
                {
                    return EveAttribute.Perception;
                }
                return EveAttribute.None;
            }
        }

        private bool m_known = false;

        public bool Known
        {
            get { return m_known; }
            set
            {
                if (m_known != value)
                {
                    m_known = value;
                    OnChanged();
                }
            }
        }

        private int GetPointsForTimeSpan(TimeSpan span)
        {
            // m = points/(primAttr+(secondaryAttr/2))
            // ... so ...
            // m * (primAttr+(secondaryAttr/2)) = points

            double primAttr = m_owner.GetEffectiveAttribute(m_primaryAttribute);
            double secondaryAttr = m_owner.GetEffectiveAttribute(m_secondaryAttribute);
            double points = span.TotalMinutes * (primAttr + (secondaryAttr / 2));
            return Convert.ToInt32(Math.Ceiling(points));
        }

        private TimeSpan GetTimeSpanForPoints(int points)
        {
            return GetTimeSpanForPoints(points, null);
        }

        private TimeSpan GetTimeSpanForPoints(int points, EveAttributeScratchpad scratchpad)
        {
            double primAttr = m_owner.GetEffectiveAttribute(m_primaryAttribute, scratchpad);
            double secondaryAttr = m_owner.GetEffectiveAttribute(m_secondaryAttribute, scratchpad);
            double minutes = Convert.ToDouble(points) / (primAttr + (secondaryAttr / 2));
            return TimeSpan.FromMinutes(minutes);
        }

        public int GetPointsRequiredForLevel(int level)
        {
            if (level == 0)
            {
                return 0;
            }

            /* original formula is not accurate and required lots of tweaks. The new formula is accurate and matches other skill
               point formulae both on forums and in other skill tools */
            //int pointsForLevel = Convert.ToInt32(250*m_rank*Math.Pow(32, Convert.ToDouble(level - 1)/2));

            int pointsForLevel = Convert.ToInt32(Math.Ceiling(Math.Pow(2, (2.5 * level) - 2.5) * 250 * m_rank));
            return pointsForLevel;
        }

        /// <summary>
        /// Calculates the percentage trained (in terms of skill points) of the next level of this skill.
        /// If the skill is already at level 5, we return 100.0
        /// </summary>
        /// <returns>percent of skill points for the next level that have already been trained</returns>
        public double GetPercentDone()
        {
            if (Level == 5) return 100.0;

            int reqToThisLevel = GetPointsRequiredForLevel(Level);
            int pointsInThisLevel = CurrentSkillPoints - reqToThisLevel;
            int reqToNextLevel = GetPointsRequiredForLevel(Level + 1);
            double deltaPointsOfLevel = Convert.ToDouble(reqToNextLevel - reqToThisLevel);
            return pointsInThisLevel / deltaPointsOfLevel;
        }

        public bool Public
        {
            get { return m_public; }
        }

        public string Name
        {
            get { return m_name; }
        }

        public int Id
        {
            get { return m_id; }
        }

        public string Description
        {
            get { return m_description; }
        }

        public string DescriptionNl
        {
            //get { return m_descriptionNl.Replace(@".", ".\n"); }
            get { return WordWrap(m_descriptionNl, 100); }
        }

        public string WordWrap(string text, int maxLength)
        {
            text = text.Replace("\n", " ");
            text = text.Replace("\r", " ");
            text = text.Replace(".", ". ");
            text = text.Replace(">", "> ");
            text = text.Replace("\t", " ");
            text = text.Replace(",", ", ");
            text = text.Replace(";", "; ");

            string[] Words = text.Split(' ');
            int currentLineLength = 0;
            ArrayList Lines = new ArrayList(text.Length / maxLength);
            string currentLine = "";
            bool InTag = false;

            foreach (string currentWord in Words)
            {
                //ignore html
                if (currentWord.Length > 0)
                {
                    if (currentWord.Substring(0, 1) == "<")
                    {
                        InTag = true;
                    }
                    if (InTag)
                    {
                        //handle filenames inside html tags
                        if (currentLine.EndsWith("."))
                        {
                            currentLine += currentWord;
                        }
                        else
                        {
                            currentLine += " " + currentWord;
                        }
                        if (currentWord.IndexOf(">") > -1)
                        {
                            InTag = false;
                        }
                    }
                    else
                    {
                        if (currentLineLength + currentWord.Length + 1 < maxLength)
                        {
                            currentLine += " " + currentWord;
                            currentLineLength += (currentWord.Length + 1);
                        }
                        else
                        {
                            Lines.Add(currentLine);
                            currentLine = currentWord;
                            currentLineLength = currentWord.Length;
                        }
                    }
                }
            }
            if (currentLine != "")
            {
                Lines.Add(currentLine);
            }
            string[] textLinesStr = new string[Lines.Count];
            Lines.CopyTo(textLinesStr, 0);

            string strWrapped = "";
            foreach (string line in textLinesStr)
            {
                strWrapped += line + "\n";
            }

            return strWrapped;
        }

        public EveAttribute PrimaryAttribute
        {
            get { return m_primaryAttribute; }
        }

        public EveAttribute SecondaryAttribute
        {
            get { return m_secondaryAttribute; }
        }

        public int Rank
        {
            get { return m_rank; }
        }

        public int Level
        {
            get { return CalculateLevel(); }
        }

        private int CalculateLevel()
        {
            if (m_inTraining)
            {
                return this.TrainingToLevel - 1;
            }
            int csp = this.CurrentSkillPoints;
            int result = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (GetPointsRequiredForLevel(i) <= csp)
                {
                    result = i;
                }
            }
            return result;
        }

        public bool IsFullyTrained()
        {
            int csp = this.CurrentSkillPoints;
            int lvl = CalculateLevel();
            return (csp == GetPointsRequiredForLevel(lvl + 1));
        }

        public bool IsPartiallyTrained()
        {
            int csp = this.CurrentSkillPoints;
            int lvl = CalculateLevel();
            return (csp > GetPointsRequiredForLevel(lvl));
        }

        public IEnumerable<Prereq> Prereqs
        {
            get
            {
                return m_prereqs;
            }
        }

        public static IDictionary<string, Skill> AllSkills
        {
            get
            {
                return sm_allSkills;
            }
            set
            {
                sm_allSkills = value;
            }
        }

        public static void PrepareAllPrerequisites()
        {
            // We have loaded all skills so we can now bake in all the pre-req skills
            foreach (Skill s in Skill.AllSkills.Values)
            {
                foreach (Prereq pr in s.Prereqs)
                {
                    Skill gs = AllSkills[pr.Name];
                    pr.SetSkill(gs);
                }
            }
        }

        private void OnChanged()
        {
            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void OnTrainingStatusChanged()
        {
            if (TrainingStatusChanged != null)
            {
                TrainingStatusChanged(this, new EventArgs());
            }
        }

        public event EventHandler Changed;
        public event EventHandler TrainingStatusChanged;

        public class Prereq
        {
            private string m_name;
            private Skill m_pointedSkill;
            private int m_requiredLevel;

            public string Name
            {
                get { return m_name; }
            }

            public Skill Skill
            {
                get { return m_pointedSkill; }
            }

            internal void SetSkill(Skill gs)
            {
                if (m_pointedSkill != null)
                {
                    throw new InvalidOperationException("pointed skill can only be set once");
                }
                m_pointedSkill = gs;
            }

            public int RequiredLevel
            {
                get { return m_requiredLevel; }
            }

            internal Prereq(string name, int requiredLevel)
            {
                m_name = name;
                m_pointedSkill = null;
                m_requiredLevel = requiredLevel;
            }
        }

        private bool m_inTraining = false;
        private int m_trainingToLevel = 0;
        private DateTime m_estimatedCompletion = DateTime.MaxValue;

        public bool InTraining
        {
            get { return m_inTraining; }
        }

        public int TrainingToLevel
        {
            get { return m_trainingToLevel; }
        }

        public DateTime EstimatedCompletion
        {
            get { return m_estimatedCompletion; }
        }

        internal void SetTrainingInfo(int trainingToLevel, DateTime estimatedCompletion)
        {
            if (!m_inTraining || m_trainingToLevel != trainingToLevel)
            {
                OnTrainingStatusChanged();
            }
            m_inTraining = true;
            m_trainingToLevel = trainingToLevel;
            m_estimatedCompletion = estimatedCompletion;
            OnChanged();
        }

        internal void StopTraining()
        {
            m_inTraining = false;
            m_trainingToLevel = 0;
            m_estimatedCompletion = DateTime.MaxValue;
            OnChanged();
        }

        public TimeSpan GetTrainingTimeToLevel(int level)
        {
            int currentSp = this.CurrentSkillPoints;
            int desiredSp = this.GetPointsRequiredForLevel(level);
            if (desiredSp <= currentSp)
            {
                return TimeSpan.Zero;
            }
            return this.GetTimeSpanForPoints(desiredSp - currentSp);
        }

        public TimeSpan GetTrainingTimeOfLevelOnly(int level)
        {
            return GetTrainingTimeOfLevelOnly(level, false);
        }

        public TimeSpan GetTrainingTimeOfLevelOnly(int level, bool includeCurrentSP)
        {
            return GetTrainingTimeOfLevelOnly(level, includeCurrentSP, null);
        }

        public TimeSpan GetTrainingTimeOfLevelOnly(int level, bool includeCurrentSP, EveAttributeScratchpad scratchpad)
        {
            int startSp = GetPointsRequiredForLevel(level - 1);
            int endSp = GetPointsRequiredForLevel(level);
            if (includeCurrentSP)
            {
                startSp = Math.Max(startSp, this.CurrentSkillPoints);
            }
            if (endSp <= startSp)
            {
                return TimeSpan.Zero;
            }
            return this.GetTimeSpanForPoints(endSp - startSp, scratchpad);
        }

        #region GetPrerequisiteTime overloads
        public TimeSpan GetPrerequisiteTime()
        {
            bool junk = false;
            return GetPrerequisiteTime(new Dictionary<Skill, int>(), ref junk);
        }

        public TimeSpan GetPrerequisiteTime(Dictionary<Skill, int> alreadyCountedList)
        {
            bool junk = false;
            return GetPrerequisiteTime(alreadyCountedList, ref junk);
        }

        public TimeSpan GetPrerequisiteTime(ref bool timeIsCurrentlyTraining)
        {
            return GetPrerequisiteTime(new Dictionary<Skill, int>(), ref timeIsCurrentlyTraining);
        }

        public TimeSpan GetPrerequisiteTime(Dictionary<Skill, int> alreadyCountedList,
                                            ref bool timeIsCurrentlyTraining)
        {
            TimeSpan result = TimeSpan.Zero;
            foreach (Prereq pp in this.Prereqs)
            {
                Skill gs = pp.Skill;
                if (gs.InTraining)
                {
                    timeIsCurrentlyTraining = true;
                }

                int fromPoints = gs.CurrentSkillPoints;
                int toPoints = gs.GetPointsRequiredForLevel(pp.RequiredLevel);
                if (alreadyCountedList.ContainsKey(gs))
                {
                    fromPoints = alreadyCountedList[gs];
                }
                if (fromPoints < toPoints)
                {
                    result += gs.GetTimeSpanForPoints(toPoints - fromPoints);
                }
                alreadyCountedList[gs] = Math.Max(fromPoints, toPoints);

                result += gs.GetPrerequisiteTime(alreadyCountedList, ref timeIsCurrentlyTraining);
            }
            return result;
        }
        #endregion

        public bool PrerequisitesMet
        {
            get
            {
                foreach (Prereq pp in this.Prereqs)
                {
                    if (pp.Skill.Level < pp.RequiredLevel)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool HasAsPrerequisite(Skill gs)
        {
            int neededLevel;
            return HasAsPrerequisite(gs, out neededLevel, true);
        }

        public bool HasAsPrerequisite(Skill gs, out int neededLevel)
        {
            return HasAsPrerequisite(gs, out neededLevel, true);
        }

        public bool HasAsPrerequisite(Skill gs, out int neededLevel, bool recurse)
        {
            foreach (Prereq pp in this.Prereqs)
            {
                if (pp.Skill == gs)
                {
                    neededLevel = pp.RequiredLevel;
                    return true;
                }
                if (recurse && pp.Skill.HasAsPrerequisite(gs, out neededLevel))
                {
                    return true;
                }
            }
            neededLevel = 0;
            return false;
        }

        public static string TimeSpanToDescriptiveText(TimeSpan ts, DescriptiveTextOptions dto)
        {
            StringBuilder sb = new StringBuilder();
            BuildDescriptiveFragment(sb, ts.Days, dto, "days");
            BuildDescriptiveFragment(sb, ts.Hours, dto, "hours");
            BuildDescriptiveFragment(sb, ts.Minutes, dto, "minutes");
            BuildDescriptiveFragment(sb, ts.Seconds, dto, "seconds");
            if (sb.Length == 0)
            {
                sb.Append("(none)");
            }
            return sb.ToString();
        }

        private static void BuildDescriptiveFragment(StringBuilder sb, int p, DescriptiveTextOptions dto, string dstr)
        {
            if (((dto & DescriptiveTextOptions.IncludeZeroes) == 0) && p == 0)
            {
                return;
            }
            if ((dto & DescriptiveTextOptions.IncludeCommas) != 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(p.ToString());
            if ((dto & DescriptiveTextOptions.SpaceText) != 0)
            {
                sb.Append(' ');
            }
            if ((dto & DescriptiveTextOptions.UppercaseText) != 0)
            {
                dstr = dstr.ToUpper();
            }
            if ((dto & DescriptiveTextOptions.FullText) != 0)
            {
                if (p == 1)
                {
                    dstr = dstr.Substring(0, dstr.Length - 1);
                }
                sb.Append(dstr);
            }
            else
            {
                sb.Append(dstr[0]);
            }
        }

        public static string GetRomanForInt(int number)
        {
            switch (number)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
                case 5:
                    return "V";
                default:
                    return "(none)";
            }
        }

        public static int GetIntForRoman(string r)
        {
            if (r == "I")
            {
                return 1;
            }
            else if (r == "II")
            {
                return 2;
            }
            else if (r == "III")
            {
                return 3;
            }
            else if (r == "IV")
            {
                return 4;
            }
            else if (r == "V")
            {
                return 5;
            }
            return 0;
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region Appearance in List Box
        private const int SKILL_DETAIL_HEIGHT = 31;

        public static int Height
        {
            get {
                Font fontr = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
                int PAD_TOP = 2;
                return Math.Max(fontr.Height * 2 + PAD_TOP * 2, SKILL_DETAIL_HEIGHT);
            }
        }

        public void Draw(DrawItemEventArgs e)
        {
            Font fontr = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
            Graphics g = e.Graphics;

            if (m_inTraining)
            {
                g.FillRectangle(Brushes.LightSteelBlue, e.Bounds);
            }
            else if ((e.Index % 2) == 0)
            {
                g.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                g.FillRectangle(Brushes.LightGray, e.Bounds);
            }

            using (Font boldf = new Font(fontr, FontStyle.Bold))
            {
                double percentComplete = 1.0f;
                if (this.Level == 0)
                {
                    int NextLevel = this.Level + 1;
                    percentComplete = Convert.ToDouble(m_currentSkillPoints) /
                                      Convert.ToDouble(GetPointsRequiredForLevel(NextLevel));
                }
                else if (this.Level < 5)
                {
                    int pointsToNextLevel = this.GetPointsRequiredForLevel(Math.Min(this.Level + 1, 5));
                    int pointsToThisLevel = this.GetPointsRequiredForLevel(this.Level);
                    int pointsDelta = pointsToNextLevel - pointsToThisLevel;
                    percentComplete = Convert.ToDouble(this.CurrentSkillPoints - pointsToThisLevel) /
                                      Convert.ToDouble(pointsDelta);
                }

                string skillName = this.Name + " " + GetRomanForInt(this.Level);
                string rankText = " (Rank " + this.Rank.ToString() + ")";
                string spText = "SP: " + this.CurrentSkillPoints.ToString("#,##0") + "/" +
                                this.GetPointsRequiredForLevel(Math.Min(this.Level + 1, 5)).ToString("#,##0");
                string levelText = "Level " + this.Level.ToString();
                string pctText = percentComplete.ToString("0%") + " Done";

                int PAD_TOP = 2;
                int PAD_LEFT = 6;
                int PAD_RIGHT = 7;
                int LINE_VPAD = 0;

                Size skillNameSize =
                    TextRenderer.MeasureText(g, skillName, boldf, new Size(0, 0),
                                             TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                Size levelTextSize =
                    TextRenderer.MeasureText(g, levelText, fontr, new Size(0, 0),
                                             TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                Size pctTextSize =
                    TextRenderer.MeasureText(g, pctText, fontr, new Size(0, 0),
                                             TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

                TextRenderer.DrawText(g, skillName, boldf, new Point(e.Bounds.Left + PAD_LEFT, e.Bounds.Top + PAD_TOP),
                                      Color.Black);
                TextRenderer.DrawText(g, rankText, fontr,
                                      new Point(e.Bounds.Left + PAD_LEFT + skillNameSize.Width, e.Bounds.Top + PAD_TOP),
                                      Color.Black);
                TextRenderer.DrawText(g, spText, fontr,
                                      new Point(e.Bounds.Left + PAD_LEFT,
                                                e.Bounds.Top + PAD_TOP + skillNameSize.Height + LINE_VPAD), Color.Black);

                // Boxes
                int BOX_WIDTH = 57;
                int BOX_HEIGHT = 14;
                int SUBBOX_HEIGHT = 8;
                int BOX_HPAD = 6;
                int BOX_VPAD = 2;
                g.DrawRectangle(Pens.Black,
                                new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT, e.Bounds.Top + PAD_TOP, BOX_WIDTH,
                                              BOX_HEIGHT));
                int bWidth = (BOX_WIDTH - 4 - 3) / 5;
                for (int bn = 1; bn <= 5; bn++)
                {
                    Rectangle brect =
                        new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT + 2 + (bWidth * (bn - 1)) + (bn - 1),
                                      e.Bounds.Top + PAD_TOP + 2, bWidth, BOX_HEIGHT - 3);
                    if (bn <= this.Level)
                    {
                        g.FillRectangle(Brushes.Black, brect);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.DarkGray, brect);
                    }
                }

                // Percent Bar
                g.DrawRectangle(Pens.Black,
                                new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT,
                                              e.Bounds.Top + PAD_TOP + BOX_HEIGHT + BOX_VPAD, BOX_WIDTH, SUBBOX_HEIGHT));
                Rectangle pctBarRect = new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT + 2,
                                                     e.Bounds.Top + PAD_TOP + BOX_HEIGHT + BOX_VPAD + 2,
                                                     BOX_WIDTH - 3, SUBBOX_HEIGHT - 3);
                g.FillRectangle(Brushes.DarkGray, pctBarRect);
                int fillWidth = Convert.ToInt32(
                    Math.Round(Convert.ToDouble(pctBarRect.Width) * percentComplete));
                if (fillWidth > 0)
                {
                    Rectangle fillRect = new Rectangle(pctBarRect.X, pctBarRect.Y,
                                                       fillWidth, pctBarRect.Height);
                    g.FillRectangle(Brushes.Black, fillRect);
                }
                TextRenderer.DrawText(g, levelText, fontr,
                                      new Point(
                                          e.Bounds.Right - BOX_WIDTH - PAD_RIGHT - BOX_HPAD - levelTextSize.Width,
                                          e.Bounds.Top + PAD_TOP), Color.Black);
                TextRenderer.DrawText(g, pctText, fontr,
                                      new Point(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT - BOX_HPAD - pctTextSize.Width,
                                                e.Bounds.Top + PAD_TOP + levelTextSize.Height + LINE_VPAD), Color.Black);
            }
        }
        #endregion
    }

    [Flags]
    public enum DescriptiveTextOptions
    {
        Default = 0,
        FullText = 1,
        UppercaseText = 2,
        SpaceText = 4,
        IncludeCommas = 8,
        IncludeZeroes = 16
    }

    public class EveAttributeScratchpad
    {
        private int m_learningLevelBonus;
        private int[] m_attributeBonuses = new int[5] { 0, 0, 0, 0, 0 };

        public int LearningLevelBonus
        {
            get { return m_learningLevelBonus; }
            set { m_learningLevelBonus = value; }
        }

        public void AdjustLearningLevelBonus(int adjustmentAmount)
        {
            m_learningLevelBonus += adjustmentAmount;
        }

        public int GetAttributeBonus(EveAttribute attribute)
        {
            return m_attributeBonuses[(int)attribute];
        }

        public void AdjustAttributeBonus(EveAttribute attribute, int adjustmentAmount)
        {
            m_attributeBonuses[(int)attribute] += adjustmentAmount;
        }

        public void ApplyALevelOf(Skill gs)
        {
            if (gs.Name == "Learning")
            {
                this.AdjustLearningLevelBonus(1);
            }
            else if (gs.IsLearningSkill)
            {
                this.AdjustAttributeBonus(gs.AttributeModified, 1);
            }
        }
    }
}
