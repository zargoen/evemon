using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    [XmlRoot("SkillTraining")]
    public class SerializableSkillTrainingInfo
    {
        private int m_characterId;

        /// <summary>
        /// This value will always be the value of the characterId of the character your querying unless something went seriously pear shaped
        /// </summary>
        [XmlAttribute("characterID")]
        public int CharacterId
        {
            get { return m_characterId; }
            set { m_characterId = value; }
        }

        private string m_error;

        /// <summary>
        /// If this is non 0 length or not null then an error has occurred
        /// </summary>
        /// <value>"characterID does not belong to you."</value> will be the only value in the entire class other than "characterID"
        /// <value>"You are trying too fast."</value> will be one of three values in the class, the others being "characterID" and "tryAgainIn"
        /// <value>""</value> or <value>null</value> when the operation was successful
        /// This is the variable that needs checking to see if the thing has worked.
        [XmlElement("error")]
        public string Error
        {
            get { return m_error; }
            set { m_error = value; }
        }

        private int m_timer;
        
        /// <summary>
        /// this has a value of <value>900</value> when it's been successful - this value may change at little or no notice.
        /// </summary>
        [XmlElement("tryAgainIn")]
        public int timertonextupdate
        {
            get { return m_timer; }
            set { m_timer = value; }
        }

        private CT m_curTime;

        [XmlElement("currentTime", typeof(SerializableSkillTrainingInfo.CT))]
        public CT CurrentTime
        {
            get { return m_curTime; }
            set { m_curTime = value; }
        }

        [XmlRoot("currentTime")]
        public class CT
        {
            private int m_offset;

            [XmlAttribute("offset")]
            public int Offset
            {
                get { return m_offset; }
                set { m_offset = value; }
            }

            private DateTime m_As_At;

            [XmlElement]
            public string DateTimeAtUpdate
            {
                get { return ConvertDateTimeToTimeString(m_As_At); }
                set { m_As_At = ConvertTimeStringToDateTime(value); }
            }
        }

        private DateTime m_endTime;

        [XmlElement("trainingEndTime")]
        public string TrainingEndTimeString
        {
            get { return ConvertDateTimeToTimeString(m_endTime); }
            set { m_endTime = ConvertTimeStringToDateTime(value); }
        }

        [XmlIgnore]
        public DateTime getTrainingEndTime
        {
            get { return m_endTime; }
        }

        private DateTime m_startTime;

        [XmlElement("trainingStartTime")]
        public string TrainingStartTimeString
        {
            get { return ConvertDateTimeToTimeString(m_startTime); }
            set { m_startTime = ConvertTimeStringToDateTime(value); }
        }

        [XmlIgnore]
        public DateTime getTrainingStartTime
        {
            get { return m_startTime; }
        }

        private int m_typeID;

        [XmlElement("trainingTypeID")]
        public int TrainingSkillWithTypeID
        {
            get { return m_typeID; }
            set { m_typeID = value; }
        }

        private int m_startSP;

        [XmlElement("trainingStartSP")]
        public int TrainingSkillStartSP
        {
            get { return m_startSP; }
            set { m_startSP = value; }
        }

        private int m_destSP;

        [XmlElement("trainingDestinationSP")]
        public int TrainingSkillDestinationSP
        {
            get { return m_destSP; }
            set { m_destSP = value; }
        }

        private int m_toLevel;

        [XmlElement("trainingToLevel")]
        public int TrainingSkillToLevel
        {
            get { return m_toLevel; }
            set { m_toLevel = value; }
        }

        public static string ConvertDateTimeToTimeString(DateTime timeUTC)
        {
            // timeUTC  = yyyy-mm-dd hh:mm:ss
            string result = string.Format("{0:d4}-{1:d2}-{2:d2} {3:d2}:{4:d2}:{5:d2}",
                        timeUTC.Year,
                        timeUTC.Month,
                        timeUTC.Day,
                        timeUTC.Hour,
                        timeUTC.Minute,
                        timeUTC.Second);
            return result;
        }

        public static DateTime ConvertTimeStringToDateTime(string timeUTC)
        {
            // timeUTC  = yyyy-mm-dd hh:mm:ss
            DateTime dt = new DateTime(
                            Int32.Parse(timeUTC.Substring(0, 4)),
                            Int32.Parse(timeUTC.Substring(5, 2)),
                            Int32.Parse(timeUTC.Substring(8, 2)),
                            Int32.Parse(timeUTC.Substring(11, 2)),
                            Int32.Parse(timeUTC.Substring(14, 2)),
                            Int32.Parse(timeUTC.Substring(17, 2)),
                            0,
                            DateTimeKind.Utc);
            return dt;
        }
    }
}
