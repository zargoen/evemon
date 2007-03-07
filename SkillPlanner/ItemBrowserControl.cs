using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ItemBrowserControl : UserControl
    {
        public ItemBrowserControl()
        {
            InitializeComponent();
            this.splitContainer1.RememberDistanceKey = "ItemBrowser";
        }

        private bool m_allSkillsKnown;
        private bool m_skillsUnplanned;

        private Plan m_plan = null;

        public Plan Plan
        {
            get { return m_plan; }
            set 
            { 
                m_plan = value;
                itemSelectControl1.Plan = value;
            }
        }

        public Item SelectedItem
        {
            set
            {
                itemSelectControl1.SelectedObject  = value;
                itemSelectControl1_SelectedItemChanged(this, null);
            }
        }

        private void itemSelectControl1_SelectedItemChanged(object sender, EventArgs e)
        {
            Item i = itemSelectControl1.SelectedObject  as Item;
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                c.Visible = (i != null);
            }
            if (i != null)
            {
                Bitmap b = new Bitmap(pbItemIcon.ClientSize.Width, pbItemIcon.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, b.Width, b.Height));
                }
                pbItemIcon.Image = b;

                StringBuilder sb = new StringBuilder();
                ItemCategory cat = i.ParentCategory;
                while (cat != null)
                {
                    sb.Insert(0, cat.Name);
                    cat = cat.ParentCategory;
                    if (cat != null)
                    {
                        sb.Insert(0, " > ");
                    }
                }
                lblItemCategory.Text = sb.ToString();
                lblItemName.Text = i.Name;
                lblItemDescription.Text = i.Description;
                lvItemProperties.BeginUpdate();
                try
                {
                    // remove excess columns that might have been added by 'compare with' earlier
                    while (lvItemProperties.Columns.Count > 2)
                        lvItemProperties.Columns.RemoveAt(2);
                    // (re)construct item properties list
                    lvItemProperties.Items.Clear();

                    ListViewItem listItem = new ListViewItem(new string[] { "Class",i.Metagroup });
                    listItem.Name = "Class";
                    lvItemProperties.Items.Add(listItem);

                    foreach (EntityProperty prop in i.Properties)
                    {
                        listItem = new ListViewItem(new string[] { prop.Name, prop.Value });
                        listItem.Name = prop.Name;
                        lvItemProperties.Items.Add(listItem);
                    }
                }
                finally
                {
                    lvItemProperties.EndUpdate();
                }

                m_allSkillsKnown = true;
                m_skillsUnplanned = false;

                SetSkillLabel(0, lblItemSkill1, i);
                SetSkillLabel(1, lblItemSkill2, i);
                SetSkillLabel(2, lblItemSkill3, i);

                if (!m_allSkillsKnown)
                {
                    List<Pair<Skill, int>> reqSkills = new List<Pair<Skill, int>>();
                    foreach (EntityRequiredSkill irs in i.RequiredSkills)
                    {
                        Pair<Skill, int> p = new Pair<Skill, int>();
                        p.A = m_plan.GrandCharacterInfo.GetSkill(irs.Name);
                        p.B = irs.Level;
                        reqSkills.Add(p);
                    }
                    TimeSpan trainTime = m_plan.GrandCharacterInfo.GetTrainingTimeToMultipleSkills(reqSkills);
                    lblItemTimeRequired.Text = "Training Time: " +
                                               Skill.TimeSpanToDescriptiveText(trainTime,
                                                                                    DescriptiveTextOptions.IncludeCommas |
                                                                                    DescriptiveTextOptions.SpaceText);
                }
                else 
                {
                    lblItemTimeRequired.Text = String.Empty;
                    btnItemSkillsAdd.Enabled = false;
                }
                if (m_skillsUnplanned)
                {
                    btnItemSkillsAdd.Enabled = true;
                }
                else
                {
                    btnItemSkillsAdd.Enabled = false;
                }

                if (!String.IsNullOrEmpty(i.Icon))
                {
                    bool pic_got = false;
                    System.Resources.IResourceReader basic;
                    if (i.ParentCategory != null && i.ParentCategory.Name != "Drone Upgrades" && i.ParentCategory.ParentCategory != null && ((i.ParentCategory.ParentCategory.Name == "Drones") || (i.ParentCategory.ParentCategory.ParentCategory != null && i.ParentCategory.ParentCategory.ParentCategory.Name == "Drones")))
                    {
                        if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\Optional\\Drones64_64.resources"))
                        {
                            basic = new System.Resources.ResourceReader(System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\Optional\\Drones64_64.resources");
                            System.Collections.IDictionaryEnumerator basicx = basic.GetEnumerator();
                            while (basicx.MoveNext())
                            {
                                if (basicx.Key.ToString() == "_" + i.Id)
                                {
                                    pbItemIcon.Image = (System.Drawing.Image)basicx.Value;
                                    pic_got = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\Optional\\Items64_64.resources"))
                        {
                            basic = new System.Resources.ResourceReader(System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\Optional\\Items64_64.resources");
                            System.Collections.IDictionaryEnumerator basicx = basic.GetEnumerator();
                            while (basicx.MoveNext())
                            {
                                if (basicx.Key.ToString() == i.Icon.ToString().Substring(i.Icon.ToString().LastIndexOf('/') + 1, i.Icon.ToString().Substring(i.Icon.ToString().LastIndexOf('/') + 1).Length - 4))
                                {
                                    pbItemIcon.Image = (System.Drawing.Image)basicx.Value;
                                    pic_got = true;
                                }
                            }
                        }
                    }
                    if (!pic_got)
                    {
                        EveSession.GetImageAsync("http://www.eve-online.com" + i.Icon, true,
                                                 delegate(EveSession sess, Image img)
                                                 {
                                                     if (
                                                         itemSelectControl1.SelectedObject == i)
                                                     {
                                                         GotItemImage(i.Id, img);
                                                     }
                                                 });
                    }
                }
            }
        }

        private void SetSkillLabel(int skillNum, Label lblSkill, Item i)
        {
            if (i.RequiredSkills.Count <= skillNum)
            {
                lblSkill.Text = String.Empty;
                return;
            }

            EntityRequiredSkill rs = i.RequiredSkills[skillNum];
            StringBuilder sb = new StringBuilder();
            sb.Append(rs.Name);
            sb.Append(' ');
            sb.Append(Skill.GetRomanForInt(rs.Level));
            if (m_plan != null)
            {
                CharacterInfo gci = m_plan.GrandCharacterInfo;
                Skill gs = gci.GetSkill(rs.Name);
                if (gs != null)
                {
                    if (gs.Level >= rs.Level)
                 {
                     sb.Append(" (Known)");
                 }
                 else
                 {
                     if (m_plan.IsPlanned(gs, rs.Level))
                     {
                         sb.Append(" (Planned)");
                         m_allSkillsKnown = false;
                     }
                     else
                     {
                         m_allSkillsKnown = false;
                         m_skillsUnplanned = true;
                     }
                 }
                }
            }
            lblSkill.Text = sb.ToString();
        }

        private void GotItemImage(int itemId, Image i)
        {
            if (i == null)
            {
                return;
            }
            if (itemSelectControl1.SelectedObject == null)
            {
                return;
            }
            if (itemId != itemSelectControl1.SelectedObject.Id)
            {
                return;
            }
            pbItemIcon.Image = i;
        }

        private void ItemBrowserControl_Load(object sender, EventArgs e)
        {
            itemSelectControl1_SelectedItemChanged(null, null);
        }

        private void btnItemSkillsAdd_Click(object sender, EventArgs e)
        {
            Item i = itemSelectControl1.SelectedObject as Item;
            if (i == null)
            {
                return;
            }

            string m_note = i.Name;
            List<Pair<string, int>> skillsToAdd = new List<Pair<string, int>>();
            foreach (EntityRequiredSkill irs in i.RequiredSkills)
            {
                skillsToAdd.Add(new Pair<string, int>(irs.Name, irs.Level));
            }
            m_plan.PlanSetTo(skillsToAdd, m_note, true);
            itemSelectControl1_SelectedItemChanged(new Object(), new EventArgs());
        }

        private void btnCompareWith_Click(object sender, EventArgs e)
        {
            // ask user to select an item for comparison
            Item selectedItem = ItemCompareWindow.CompareWithItemInput(itemSelectControl1.SelectedObject as Item,m_plan);
            if (selectedItem != null)
            {
                lvItemProperties.BeginUpdate();
                try
                {
                    // add new column header and values
                    lvItemProperties.Columns.Add(selectedItem.Name);
                    foreach (EntityProperty prop in selectedItem.Properties)
                    {
                        ListViewItem[] items = lvItemProperties.Items.Find(prop.Name, false);
                        if (items.Length != 0)
                        {
                            // existing property
                            ListViewItem oldItem = items[0];
                            oldItem.SubItems.Add(prop.Value);
                        }
                        else
                        {
                            // new property
                            int skipColumns = lvItemProperties.Columns.Count - 2;
                            ListViewItem newItem = lvItemProperties.Items.Add(prop.Name);
                            newItem.Name = prop.Name;
                            while (skipColumns-- > 0)
                                newItem.SubItems.Add("");
                            newItem.SubItems.Add(prop.Value);
                        }
                    }
                    // mark items with changed value in blue
                    foreach (ListViewItem listItem in lvItemProperties.Items)
                    {
                        for (int i = 2; i < listItem.SubItems.Count; i++)
                        {
                            if (listItem.SubItems[i - 1].Text.CompareTo(listItem.SubItems[i].Text) != 0)
                            {
                                listItem.BackColor = Color.LightBlue;
                                break;
                            }
                        }
                    }
                }
                finally
                {
                    lvItemProperties.EndUpdate();
                }
            }
        }
    }
}
