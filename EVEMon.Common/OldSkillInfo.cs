using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    [XmlRoot("OldSkillinfo")]
    public class OldSkillinfo
    {
        private string m_old_SkillName;

        [XmlAttribute("old_SkillName")]
        public string old_SkillName
        {
            get { return m_old_SkillName; }
            set { m_old_SkillName = value; }
        }

        private int m_old_TrainingToLevel;

        [XmlAttribute("old_TrainingToLevel")]
        public int old_TrainingToLevel
        {
            get { return m_old_TrainingToLevel; }
            set { m_old_TrainingToLevel = value; }
        }

        private bool m_old_skill_completed;

        [XmlAttribute("old_skill_completed")]
        public bool old_skill_completed
        {
            get { return m_old_skill_completed; }
            set { m_old_skill_completed = value; }
        }

        private DateTime m_old_estimated_completion;

        [XmlAttribute("old_estimated_completion")]
        public DateTime old_estimated_completion
        {
            get { return m_old_estimated_completion; }
            set { m_old_estimated_completion = value; }
        }

        public OldSkillinfo()
        {
            m_old_SkillName = null;
            m_old_TrainingToLevel = 0;
        }

        public OldSkillinfo(string old_SkillName, int old_TrainingToLevel)
        {
            m_old_SkillName = old_SkillName;
            m_old_TrainingToLevel = old_TrainingToLevel;
            m_old_skill_completed = false;
            m_old_estimated_completion = DateTime.MaxValue;
        }

        public OldSkillinfo(string old_SkillName, int old_TrainingToLevel, bool old_skill_completed, DateTime old_estimated_completion)
        {
            m_old_SkillName = old_SkillName;
            m_old_TrainingToLevel = old_TrainingToLevel;
            m_old_skill_completed = old_skill_completed;
            m_old_estimated_completion = old_estimated_completion;
        }
    }
}
