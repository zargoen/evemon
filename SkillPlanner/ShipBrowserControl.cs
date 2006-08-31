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

        private GrandCharacterInfo m_grandCharacterInfo;
        private Plan m_plan;

        public GrandCharacterInfo GrandCharacterInfo
        {
            get { return m_grandCharacterInfo; }
            set { m_grandCharacterInfo = value; }
        }

        public Plan Plan
        {
            get { return m_plan; }
            set { m_plan = value; }
        }

        public ShipBrowserControl()
        {
            InitializeComponent();
            shipSelectControl_SelectedShipChanged(null, null);
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
                EveSession.GetImageAsync(
                    "http://www.eve-online.com/bitmaps/icons/itemdb/shiptypes/256_256/" +
                    shipId.ToString() + ".png", true, delegate(EveSession ss, Image i)
                                                          {
                                                              GotShipImage(shipId, i);
                                                          });

                lblShipClass.Text = s.Type + " > " + s.Race;
                lblShipName.Text = s.Name;
                lblShipDescription.Text = Regex.Replace(s.Description, "<. +?>", String.Empty, RegexOptions.Singleline);
                // force the label to fit the panel
                pnlShipDescription_Changed(null, null);

                bool allKnown = true;
                allKnown = SetShipSkillLabel(0, lblShipSkill1, s.RequiredSkills) && allKnown;
                allKnown = SetShipSkillLabel(1, lblShipSkill2, s.RequiredSkills) && allKnown;
                allKnown = SetShipSkillLabel(2, lblShipSkill3, s.RequiredSkills) && allKnown;

                if (!allKnown)
                {
                    List<Pair<GrandSkill, int>> reqSkills = new List<Pair<GrandSkill, int>>();
                    foreach (ShipRequiredSkill srs in s.RequiredSkills)
                    {
                        Pair<GrandSkill, int> p = new Pair<GrandSkill, int>();
                        p.A = m_grandCharacterInfo.GetSkill(srs.Name);
                        p.B = srs.Level;
                        reqSkills.Add(p);
                    }
                    TimeSpan trainTime = m_grandCharacterInfo.GetTrainingTimeToMultipleSkills(reqSkills);
                    lblShipTimeRequired.Text = "Training Time: " +
                                               GrandSkill.TimeSpanToDescriptiveText(trainTime,
                                                                                    DescriptiveTextOptions.IncludeCommas |
                                                                                    DescriptiveTextOptions.SpaceText);
                    btnShipSkillsAdd.Enabled = true;
                }
                else
                {
                    lblShipTimeRequired.Text = String.Empty;
                    btnShipSkillsAdd.Enabled = false;
                }

                lbShipProperties.BeginUpdate();
                try
                {
                    lbShipProperties.Items.Clear();
                    foreach (ShipProperty prop in s.Properties)
                    {
                        lbShipProperties.Items.Add(prop);
                    }
                }
                finally
                {
                    lbShipProperties.EndUpdate();
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

        private bool SetShipSkillLabel(int rnum, Label skillLabel, List<ShipRequiredSkill> list)
        {
            if (list.Count > rnum)
            {
                GrandSkill gs = m_grandCharacterInfo.GetSkill(list[rnum].Name);
                string knownText = String.Empty;
                if (gs.Level >= list[rnum].Level)
                {
                    knownText = " (Known)";
                }
                skillLabel.Text = list[rnum].Name + " " +
                                  GrandSkill.GetRomanForInt(list[rnum].Level) + knownText;
                return (knownText.Length > 0);
            }
            else
            {
                skillLabel.Text = String.Empty;
                return true;
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
    }
}
