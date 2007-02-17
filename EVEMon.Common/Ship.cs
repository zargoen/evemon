using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    public class Ship 
    {
        private static Ship[] sm_ships = null;

        public static Ship[] GetShips()
        {
            if (sm_ships == null)
            {
                string shipfile = System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\eve-ships2.xml.gz";
                if (!File.Exists(shipfile))
                {
                    throw new ApplicationException(shipfile + " not found!");
                }
                using (FileStream s = new FileStream(shipfile, FileMode.Open, FileAccess.Read))
                using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
                {
                    try
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Ship[]));
                        sm_ships = (Ship[])xs.Deserialize(zs);
                    }
                    catch(InvalidCastException)
                    {
                        // deserialization failed - probably in design mode
                        return null;
                    }
                }
            }
            return sm_ships;
        }

        private int m_id = -1;

        [XmlAttribute]
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_name = String.Empty;

        [XmlAttribute]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private string m_description = String.Empty;

        [XmlAttribute]
        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private string m_race = String.Empty;

        [XmlAttribute]
        public string Race
        {
            get { return m_race; }
            set { m_race = value; }
        }

        private string m_type = String.Empty;

        [XmlAttribute]
        public string Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        private List<EntityProperty> m_properties = new List<EntityProperty>();

        [XmlArrayItem("prop")]
        public List<EntityProperty> Properties
        {
            get { return m_properties; }
        }

        private List<EntityRequiredSkill> m_requiredSkills = new List<EntityRequiredSkill>();

        [XmlArrayItem("skill")]
        public List<EntityRequiredSkill> RequiredSkills
        {
            get { return m_requiredSkills; }
        }
    }
 }
