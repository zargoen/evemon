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

        private void itemSelectControl1_SelectedItemChanged(object sender, EventArgs e)
        {
            Item i = itemSelectControl1.SelectedItem;
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
                lbItemProperties.BeginUpdate();
                try
                {
                    lbItemProperties.Items.Clear();
                    foreach (ItemProperty ip in i.Properties)
                    {
                        decimal DecimalValue;
                        string  ItemValue = ip.Value;
                        ItemValue = ItemValue.Replace("%", "");
                        ItemValue = ItemValue.Replace(",", ".");
                        try
                        {
                            DecimalValue = System.Convert.ToDecimal(ItemValue);
                            if ((ip.Name.Contains("bonus") || ip.Name.Contains("multiplier")) && ip.Value.Contains("%") &&
                               (2 > DecimalValue))
                            {
                                ItemValue = ItemPropertyBonusToPercent(DecimalValue);
                            }
                            else
                            {
                                ItemValue = ip.Value;
                            }
                        }
                        catch(FormatException)
                        {
                            ItemValue = ip.Value;
                        }

                        lbItemProperties.Items.Add(ip.Name + ": " + ItemValue);
                    }
                }
                finally
                {
                    lbItemProperties.EndUpdate();
                }

                m_allSkillsKnown = true;
                m_skillsUnplanned = false;

                SetSkillLabel(0, lblItemSkill1, i);
                SetSkillLabel(1, lblItemSkill2, i);
                SetSkillLabel(2, lblItemSkill3, i);

                if (!m_allSkillsKnown)
                {
                    List<Pair<GrandSkill, int>> reqSkills = new List<Pair<GrandSkill, int>>();
                    foreach (ItemRequiredSkill irs in i.RequiredSkills)
                    {
                        Pair<GrandSkill, int> p = new Pair<GrandSkill, int>();
                        p.A = m_plan.GrandCharacterInfo.GetSkill(irs.Name);
                        p.B = irs.Level;
                        reqSkills.Add(p);
                    }
                    TimeSpan trainTime = m_plan.GrandCharacterInfo.GetTrainingTimeToMultipleSkills(reqSkills);
                    lblItemTimeRequired.Text = "Training Time: " +
                                               GrandSkill.TimeSpanToDescriptiveText(trainTime,
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
                        if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "//Resources//Optional//Drones64_64.resources"))
                        {
                            basic = new System.Resources.ResourceReader(System.AppDomain.CurrentDomain.BaseDirectory + "//Resources//Optional//Drones64_64.resources");
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
                        if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "//Resources//Optional//Items64_64.resources"))
                        {
                            basic = new System.Resources.ResourceReader(System.AppDomain.CurrentDomain.BaseDirectory + "//Resources//Optional//Items64_64.resources");
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
                                                         itemSelectControl1.
                                                             SelectedItem == i)
                                                     {
                                                         GotItemImage(i.Id, img);
                                                     }
                                                 });
                    }
                }
            }
        }

        private static string ItemPropertyBonusToPercent(Decimal Value)
        {
            if (Value > 0)
            {
                Value = (1 - Value) * 100;
            }
            else
            {
                Value = Value * -1;
            }
            return System.Convert.ToString(Math.Round(Value, 2)) + " %";
        }

        private void SetSkillLabel(int skillNum, Label lblSkill, Item i)
        {
            if (i.RequiredSkills.Count <= skillNum)
            {
                lblSkill.Text = String.Empty;
                return;
            }

            ItemRequiredSkill rs = i.RequiredSkills[skillNum];
            StringBuilder sb = new StringBuilder();
            sb.Append(rs.Name);
            sb.Append(' ');
            sb.Append(GrandSkill.GetRomanForInt(rs.Level));
            if (m_plan != null)
            {
                GrandCharacterInfo gci = m_plan.GrandCharacterInfo;
                GrandSkill gs = gci.GetSkill(rs.Name);
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
            if (itemSelectControl1.SelectedItem == null)
            {
                return;
            }
            if (itemId != itemSelectControl1.SelectedItem.Id)
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
            Item i = itemSelectControl1.SelectedItem;
            if (i == null)
            {
                return;
            }

            string m_note = i.Name;
            List<Pair<string, int>> skillsToAdd = new List<Pair<string, int>>();
            foreach (ItemRequiredSkill irs in i.RequiredSkills)
            {
                skillsToAdd.Add(new Pair<string, int>(irs.Name, irs.Level));
            }
            AddPlanConfirmWindow.AddSkillsWithConfirm(m_plan, skillsToAdd,m_note);
            itemSelectControl1_SelectedItemChanged(new Object(), new EventArgs());
        }
    }
}
