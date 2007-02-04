using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace EVEMon.Common
{
    public class Item
    {
        private ItemCategory m_category;

        [XmlIgnore]
        public ItemCategory ParentCategory
        {
            get { return m_category; }
            set { m_category = value; }
        }

        private int m_id = -1;

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_icon = String.Empty;

        public string Icon
        {
            get { return m_icon; }
            set { m_icon = StringTable.GetSharedString(value); }
        }

        private string m_name = String.Empty;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private string m_description = String.Empty;

        public string Description
        {
            get { return m_description; }
            set { m_description = StringTable.GetSharedString(value); }
        }

        private string m_metagroup = String.Empty;

        public string Metagroup
        {
            get { return m_metagroup; }
            set { m_metagroup = StringTable.GetSharedString(value); }
        }

        private List<ItemProperty> m_properties = new List<ItemProperty>();

        [XmlArrayItem("prop")]
        public List<ItemProperty> Properties
        {
            get { return m_properties; }
        }

        private List<ItemRequiredSkill> m_requiredSkills = new List<ItemRequiredSkill>();

        [XmlArrayItem("skill")]
        public List<ItemRequiredSkill> RequiredSkills
        {
            get { return m_requiredSkills; }
        }

        public override string ToString()
        {
            return m_name;
        }

        private int m_slotIndex = -1;
        public int SlotIndex
        {
            get
            {
                if (m_slotIndex != -1)
                    return m_slotIndex;
                foreach (ItemProperty prop in m_properties)
                {
                    if (prop.Name != "Slot type")
                        continue;
                    switch (prop.Value)
                    {
                        default:
                            m_slotIndex = 0; // FIXME: debug warning?
                            break;
                        case "High slot":
                            m_slotIndex = 1;
                            break;
                        case "Mid slot":
                            m_slotIndex = 2;
                            break;
                        case "Low slot":
                            m_slotIndex = 3;
                            break;
                    }
                    return m_slotIndex;
                }
                m_slotIndex = 0; // No slot
                return m_slotIndex;
            }
        }
    }

    public class ItemProperty
    {
        private string m_name;

        [XmlAttribute]
        public string Name
        {
            get { return m_name; }
            set { m_name = StringTable.GetSharedString(value); }
        }

        private string m_value;
        private Regex m_3dp = new Regex(@"\.dd0");

        [XmlAttribute]
        public string Value
        {
            get
            {
                // The item database sometimes shows bonus amd muliplier figures with a negative number
                // (e.g shield hardners) so fix it...
                if (m_value.StartsWith("-") && (Name.Contains("bonus") || Name.Contains("multiplier")) && m_value.Contains("%"))
                {
                    return m_value.Substring(1).Trim();
                }
                else
                {
                    return m_value.Trim();
                }

            }
            set 
            {
              m_value = StringTable.GetSharedString(value); 
            }
        }

        public override string ToString()
        {
            return m_name + ": " + m_value;
        }

    }

    public class ItemRequiredSkill
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

    public class ItemCategory
    {
        private static WeakReference<ItemCategory> m_rootCategory;

        public static ItemCategory GetRootCategory()
        {
            ItemCategory rootCat = null;
            if (m_rootCategory != null)
            {
                rootCat = m_rootCategory.Target;
            }
            if (rootCat == null)
            {
                string itemfile = System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\eve-items2.xml.gz";
                if (!File.Exists(itemfile))
                {
                    throw new ApplicationException(itemfile + " not found!");
                }
                using (FileStream s = new FileStream(itemfile, FileMode.Open, FileAccess.Read))
                using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
                {
                    using (StringTable.GetInstanceScope())
                    {
                        XmlSerializer xs = new XmlSerializer(typeof (ItemCategory));
                        rootCat = xs.Deserialize(zs) as ItemCategory;
                    }
                }

                m_rootCategory = new WeakReference<ItemCategory>(rootCat);
            }
            return rootCat;
        }

        public ItemCategory()
        {
            m_subCategories = new MonitoredList<ItemCategory>();
            m_subCategories.Changed += new EventHandler<ChangedEventArgs<ItemCategory>>(m_subCategories_Changed);

            m_items = new MonitoredList<Item>();
            m_items.Changed += new EventHandler<ChangedEventArgs<Item>>(m_items_Changed);
        }

        private void m_items_Changed(object sender, ChangedEventArgs<Item> e)
        {
            switch (e.ChangeType)
            {
                case ChangeType.Added:
                    e.Item.ParentCategory = this;
                    break;
                case ChangeType.Removed:
                    e.Item.ParentCategory = null;
                    break;
            }
        }

        private void m_subCategories_Changed(object sender, ChangedEventArgs<ItemCategory> e)
        {
            switch (e.ChangeType)
            {
                case ChangeType.Added:
                    e.Item.ParentCategory = this;
                    break;
                case ChangeType.Removed:
                    e.Item.ParentCategory = null;
                    break;
            }
        }

        private ItemCategory m_parentCategory;

        [XmlIgnore]
        public ItemCategory ParentCategory
        {
            get { return m_parentCategory; }
            set { m_parentCategory = value; }
        }

        private string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private MonitoredList<ItemCategory> m_subCategories;

        public MonitoredList<ItemCategory> Subcategories
        {
            get { return m_subCategories; }
        }

        private MonitoredList<Item> m_items;

        public MonitoredList<Item> Items
        {
            get { return m_items; }
        }
    }
}
