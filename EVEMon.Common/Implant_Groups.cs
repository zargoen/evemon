using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    public class ImplantSet
    {
        // This stores the implant as the typeID.
        private string[] m_values = new string[10] { "", "", "", "", "", "", "", "", "", "" };

        public ImplantSet(string[] a)
        {
            m_values = a;
        }

        public string this[int implant]
        {
            get { return m_values[implant]; }
            set { m_values[implant] = value; }
        }

        public string[] Array
        {
            get { return m_values; }
            set { m_values = value; }
        }
    }
    
    public class Slot
    {
        private int m_slot = -1;

        [XmlAttribute]
        public int Number
        {
            get { return m_slot; }
            set { m_slot = value; }
        }

        private string m_attribute = String.Empty;

        [XmlAttribute("a")]
        public string Attribute
        {
            get { return m_attribute; }
            set { m_attribute = StringTable.GetSharedString(value); }
        }

        private List<Implant> m_Implants = new List<Implant>();

        [XmlArrayItem("Implant")]
        public List<Implant> ImplantList
        {
            get { return m_Implants; }
        }

        private static Slot[] sm_implants = null;

        public static Slot[] GetImplants()
        {
            if (sm_implants == null)
            {
                string implantfile = System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\eve-implants2.xml.gz";
                if (!File.Exists(implantfile))
                {
                    throw new ApplicationException(implantfile + " not found!");
                }
                using (FileStream s = new FileStream(implantfile, FileMode.Open, FileAccess.Read))
                using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
                {
                    try
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Slot[]));
                        sm_implants = (Slot[])xs.Deserialize(zs);
                    }
                    catch (InvalidCastException)
                    {
                        // deserialization failed - probably in design mode
                        return null;
                    }
                }
            }
            return sm_implants;
        }
    }

    public class Implant
    {
        private int m_tech2 = 1;

        [XmlAttribute]
        public int Tech2
        {
            get { return m_tech2; }
            set { m_tech2 = value; }
        }

        private int m_bonus = 0;

        [XmlAttribute]
        public int Bonus
        {
            get { return m_bonus; }
            set { m_bonus = value; }
        }

        private int m_id = -1;

        [XmlAttribute]
        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_icon = String.Empty;

        [XmlAttribute]
        public string Icon
        {
            get { return m_icon; }
            set { m_icon = StringTable.GetSharedString(value); }
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
            set { m_description = StringTable.GetSharedString(value); }
        }

        private List<ImplantProperty> m_properties = new List<ImplantProperty>();

        [XmlArrayItem("prop")]
        public List<ImplantProperty> Properties
        {
            get { return m_properties; }
        }

        private List<ImplantRequiredSkill> m_requiredSkills = new List<ImplantRequiredSkill>();

        [XmlArrayItem("skill")]
        public List<ImplantRequiredSkill> RequiredSkills
        {
            get { return m_requiredSkills; }
        }

        public override string ToString()
        {
            return m_name;
        }
    }

    public class ImplantProperty
    {
        private string m_name;

        [XmlAttribute]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private string m_value;

        [XmlAttribute]
        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        public override string ToString()
        {
            return m_name + ": " + m_value;
        }
    }

    public class ImplantRequiredSkill
    {
        private string m_name;

        [XmlAttribute]
        public string Name
        {
            get { return m_name; }
            set { m_name = StringTable.GetSharedString(value); }
        }

        private int m_level;

        [XmlAttribute]
        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }
    }
}
