using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ShipBrowserControl : UserControl
    {
        private Plan m_plan;
        public Plan Plan
        {
            get { return m_plan; }
            set { 
                m_plan = value;
                shipSelectControl.Plan = value;
            }
        }

        private bool m_allSkillsKnown;
        private bool m_skillsUnplanned;

        public ShipBrowserControl()
        {
            InitializeComponent();
            this.scShipSelect.RememberDistanceKey = "ShipBrowser";
            shipSelectControl_SelectedShipChanged(null, null);
        }

        private static string m_propName;
        private static bool findShipProperty(ShipProperty p)
        {
            return p.Name.Equals(ShipBrowserControl.m_propName);
        }
        
        private void shipSelectControl_SelectedShipChanged(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(256, 256);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 256, 256));
            }
            pbShipImage.Image = b;

            if (shipSelectControl.SelectedShip != null)
            {
                Ship s = shipSelectControl.SelectedShip;
                int shipId = s.Id;

                if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\Optional\\Ships256_256.resources"))
                {
                    System.Resources.IResourceReader basic;
                    basic = new System.Resources.ResourceReader(System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\Optional\\Ships256_256.resources");
                    System.Collections.IDictionaryEnumerator basicx = basic.GetEnumerator();
                    while (basicx.MoveNext())
                    {
                        if (basicx.Key.ToString() == "_" + s.Id)
                        {
                            pbShipImage.Image = (System.Drawing.Image)basicx.Value;
                        }
                    }
                }
                else
                {
                    EveSession.GetImageAsync(
                        "http://www.eve-online.com/bitmaps/icons/itemdb/shiptypes/256_256/" +
                        shipId.ToString() + ".png", true, delegate(EveSession ss, Image i)
                                                              {
                                                                  GotShipImage(shipId, i);
                                                              });
                }

                lblShipClass.Text = s.Type + " > " + s.Race;
                lblShipName.Text = s.Name;
                lblShipDescription.Text = Regex.Replace(s.Description, "<.+?>", String.Empty, RegexOptions.Singleline);
                // force the label to fit the panel
                pnlShipDescription_Changed(null, null);

                m_allSkillsKnown = true;
                m_skillsUnplanned = false;

                SetShipSkillLabel(0, lblShipSkill1, s.RequiredSkills);
                SetShipSkillLabel(1, lblShipSkill2, s.RequiredSkills);
                SetShipSkillLabel(2, lblShipSkill3, s.RequiredSkills);

                if (! m_allSkillsKnown)
                {
                    List<Pair<Skill, int>> reqSkills = new List<Pair<Skill, int>>();
                    foreach (ShipRequiredSkill srs in s.RequiredSkills)
                    {
                        Pair<Skill, int> p = new Pair<Skill, int>();
                        p.A = m_plan.GrandCharacterInfo.GetSkill(srs.Name);
                        p.B = srs.Level;
                        reqSkills.Add(p);
                    }
                    TimeSpan trainTime = m_plan.GrandCharacterInfo.GetTrainingTimeToMultipleSkills(reqSkills);
                    lblShipTimeRequired.Text = "Training Time: " +
                                               Skill.TimeSpanToDescriptiveText(trainTime,
                                                                                    DescriptiveTextOptions.IncludeCommas |
                                                                                    DescriptiveTextOptions.SpaceText);
                    
                }
                else
                {
                    lblShipTimeRequired.Text = String.Empty;
                }
                if (m_skillsUnplanned)
                {
                    btnShipSkillsAdd.Enabled = true;
                }
                else
                {
                    btnShipSkillsAdd.Enabled = false;
                }

                lvShipProperties.BeginUpdate();
                try
                {
                    // remove excess columns that might have been added by 'compare with' earlier
                    while (lvShipProperties.Columns.Count > 2)
                        lvShipProperties.Columns.RemoveAt(2);
                    // (re)construct ship properties list
                    lvShipProperties.Items.Clear();

                    /* display the properties in a logical sequence
                     
                     If  the starts with =, then it will be displayed as a heading and not
                     a value. 
                     NB: In Compare mode, only the FIRST selected ship will have sorted attributes 
                     so if there is an attribute in this list that the first ship doesn't have
                      but the second does, then it will be listed in the unsorted part of the
                     list for the second ship - such occurences are rare.
                     (e.g. comparing a freighter to a battleship - slot configuration will be shown in 
                      the "other" section) */
                    
                    String[] shipAttributeList = new String[]{
                        // Price
                        "Base price",
                        // Fitting
                        "=Fitting",
                        "CPU Output",
                        "powergrid Output",
                        "Calibration",
                        "Low Slots",
                        "Med Slots",
                        "High Slots",
                        "Launcher hardpoints",
                        "Turret hardpoints",
                        "Rig Slots",
                        // Attributes - structure
                        "=Structure",
                        "hp",
                        "Capacity",
                        "Drone Capacity",
                        "Mass",
                        "Volume",
                        "EM dmg resistance",
                        "Explosive dmg resistance",
                        "Kinetic dmg resistance",
                        "Thermal dmg resistance",
                        // Attributes - Armor
                        "=Armor",
                        "Armor Hitpoints",
                        "Armor Em Damage Resistance",
                        "Armor Explosive Damage Resistance",
                        "Armor Kinetic Damage Resistance",
                        "Armor Thermal Damage Resistance",
                        // Attributes - Shield
                        "=Shield",
                        "Shield Capacity",
                        "Shield recharge time",
                        "Shield Em Damage Resistance",
                        "Shield Explosive Damage Resistance",
                        "Shield Kinetic Damage Resistance",
                        "Shield Thermal Damage Resistance",
                        // Attributes - cap
                        "=Capacitor",
                        "Capacitor Capacity",
                        "Recharge time",
                        // Attributes - Targeting
                        "=Targeting",
                        "Maximum Targeting Range",
                        "Max  Locked Targets", // Yes, it has a double space in the xml!
                        "Max Locked Targets", // And sometimes it doesn't!
                        "Scan Resolution",
                        "Gravimetric Sensor Strength",
                        "LADAR Sensor Strength",
                        "Magnetometric Sensor Strength",
                        "RADAR Sensor Strength",
                        "Signature Radius",
                        // Attributes - Propulsion
                        "=Propulsion",
                        "Max Velocity",
                        "=Other"
                    };


                    ListViewItem listItem = null;
                    foreach (String att in shipAttributeList)
                    {
                        if (att.StartsWith("="))
                        {
                            listItem = new ListViewItem(att.Substring(1));
                            listItem.BackColor = System.Drawing.SystemColors.MenuHighlight;
                            lvShipProperties.Items.Add(listItem);
                        }
                        else
                        {
                            m_propName = att;
                            ShipProperty sp = s.Properties.Find(findShipProperty);
                            if (sp != null)
                            {
                                listItem = new ListViewItem(new string[] { sp.Name, sp.Value });
                                listItem.Name = sp.Name;
                                lvShipProperties.Items.Add(listItem);
                            }
                       }
                    }                    
                    
                    // Display any properties not shown in the sorted list
                    foreach (ShipProperty prop in s.Properties)
                    {
                        // make sure we haven't already displayed this property
                        if (Array.IndexOf(shipAttributeList, prop.Name) < 0)
                        {
                            listItem = new ListViewItem(new string[] { prop.Name, prop.Value });
                            listItem.Name = prop.Name;
                            lvShipProperties.Items.Add(listItem);
                        }
                    }
                }
                finally
                {
                    lvShipProperties.EndUpdate();
                }

                foreach (Control c in scShipSelect.Panel2.Controls)
                {
                    c.Visible = true;
                }
            }
            else
            {
                foreach (Control c in scShipSelect.Panel2.Controls)
                {
                    c.Visible = false;
                }
            }
        }

        private void SetShipSkillLabel(int rnum, Label skillLabel, List<ShipRequiredSkill> list)
        {
            if (list.Count > rnum)
            {
                Skill gs = m_plan.GrandCharacterInfo.GetSkill(list[rnum].Name);
                string addText = String.Empty;
                if (gs.Level >= list[rnum].Level)
                {
                    addText = " (Known)";
                }
                else if (Plan.IsPlanned(gs, list[rnum].Level))
                {
                    addText = " (Planned)";
                    m_allSkillsKnown = false;
                }
                else
                {
                    m_allSkillsKnown = false;
                    m_skillsUnplanned = true;
                }
                skillLabel.Text = list[rnum].Name + " " +
                                  Skill.GetRomanForInt(list[rnum].Level) + addText;
            }
            else
            {
                skillLabel.Text = String.Empty;
            }
        }

        private void GotShipImage(int shipId, Image i)
        {
            if (i == null)
            {
                return;
            }
            if (shipSelectControl.SelectedShip == null)
            {
                return;
            }
            if (shipId != shipSelectControl.SelectedShip.Id)
            {
                return;
            }
            pbShipImage.Image = i;
        }

        
        private void btnShipSkillsAdd_Click(object sender, EventArgs e)
        {
            Ship s = shipSelectControl.SelectedShip;
            if (s == null)
            {
                return;
            }

            string m_note = s.Name;
            List<Pair<string, int>> skillsToAdd = new List<Pair<string, int>>();
            foreach (ShipRequiredSkill srs in s.RequiredSkills)
            {
                skillsToAdd.Add(new Pair<string, int>(srs.Name, srs.Level));
            }
            AddPlanConfirmWindow.AddSkillsWithConfirm(m_plan, skillsToAdd,m_note);
            shipSelectControl_SelectedShipChanged(new Object(), new EventArgs());
        }

        private void pnlShipDescription_Changed(object sender, EventArgs e)
        {
            int w = pnlShipDescription.ClientSize.Width;
            lblShipDescription.MaximumSize = new Size(w, Int32.MaxValue);
            if (lblShipDescription.PreferredHeight > pnlShipDescription.ClientSize.Height)
            {
                pnlShipDescription.Visible = false;
                pnlShipDescription.PerformLayout();
                int xw = pnlShipDescription.ClientSize.Width;
                lblShipDescription.MaximumSize = new Size(xw, Int32.MaxValue);
                pnlShipDescription.Visible = true;
            }
        }

        private void btnCompareWith_Click(object sender, EventArgs e)
        {
            // ask user to select a ship for comparison
            Ship selectedShip = ShipCompareWindow.CompareWithShipInput(shipSelectControl.SelectedShip);
            if (selectedShip != null)
            {
                lvShipProperties.BeginUpdate();
                try
                {
                    // add new column header and values
                    lvShipProperties.Columns.Add(selectedShip.Name);
                    foreach (ShipProperty prop in selectedShip.Properties)
                    {
                        ListViewItem[] items = lvShipProperties.Items.Find(prop.Name, false);
                        if (items.Length != 0)
                        {
                            // existing property
                            ListViewItem oldItem = items[0];
                            oldItem.SubItems.Add(prop.Value);
                        }
                        else
                        {
                            // new property
                            int skipColumns = lvShipProperties.Columns.Count - 2;
                            ListViewItem newItem = lvShipProperties.Items.Add(prop.Name);
                            newItem.Name = prop.Name;
                            while (skipColumns-- > 0)
                                newItem.SubItems.Add("");
                            newItem.SubItems.Add(prop.Value);
                        }
                    }
                    // mark properties with changed value in blue
                    foreach (ListViewItem listItem in lvShipProperties.Items)
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
                    lvShipProperties.EndUpdate();
                }
            }
        }
    }

}
