using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.PieChart;

namespace EVEMon
{
    public partial class SkillsPieChart : Form
    {
        private Settings m_settings;
        public string active_character;

        public SkillsPieChart()
        {
            InitializeComponent();
            m_settings = Settings.GetInstance();
            skillPieChartControl.LeftMargin = 20F;
            skillPieChartControl.TopMargin = 15F;
            skillPieChartControl.RightMargin = 20F;
            skillPieChartControl.BottomMargin = 15F;
            skillPieChartControl.FitChart = true;
            skillPieChartControl.SliceRelativeHeight = m_settings.SkillPieChartSliceRelativeHeight;
            skillPieChartControl.InitialAngle = m_settings.SkillPieChartInitialAngle;
            pieHeight.Value = (decimal)m_settings.SkillPieChartSliceRelativeHeight;
            pieAngle.Value = (decimal)m_settings.SkillPieChartInitialAngle;
            skillPieChartControl.ShadowStyle = ShadowStyle.GradualShadow;
            skillPieChartControl.EdgeColorType = EVEMon.PieChart.EdgeColorType.DarkerThanSurface;
            skillPieChartControl.EdgeLineWidth = 1F;
            if (m_settings.SkillPieChartColors.Count < 15)
            {
                int alpha = 125;
                skillPieChartControl.Colors = new Color[] {
                    Color.FromArgb(alpha, Color.Red),
                    Color.FromArgb(alpha, Color.Green),
                    Color.FromArgb(alpha, Color.Blue),
                    Color.FromArgb(alpha, Color.Red),
                    Color.FromArgb(alpha, Color.Green),
                    Color.FromArgb(alpha, Color.Blue),
                    Color.FromArgb(alpha, Color.Red),
                    Color.FromArgb(alpha, Color.Green),
                    Color.FromArgb(alpha, Color.Blue),
                    Color.FromArgb(alpha, Color.Red),
                    Color.FromArgb(alpha, Color.Green),
                    Color.FromArgb(alpha, Color.Blue),
                    Color.FromArgb(alpha, Color.Red),
                    Color.FromArgb(alpha, Color.Green),
                    Color.FromArgb(alpha, Color.Blue)
                };
            }
            else
            {
                List<Color> loadedColors = new List<Color>();
                foreach (SerializableColor c in m_settings.SkillPieChartColors)
                {
                    loadedColors.Add(c.ToColor());
                }
                skillPieChartControl.Colors = loadedColors.ToArray();
            }
        }

        private void SkillsPieChart_Load(object sender, EventArgs e)
        {
            SerializableCharacterInfo c_info = m_settings.GetCharacterInfo(active_character);
            decimal[] newValues = new decimal[c_info.SkillGroups.Count];
            string[] newTexts = new string[c_info.SkillGroups.Count];
            string[] newToolTips = new string[c_info.SkillGroups.Count];
            float[] newSliceRelativeDisplacements = new float[c_info.SkillGroups.Count];

            decimal[] n_newValues = new decimal[c_info.SkillGroups.Count];
            string[] n_newTexts = new string[c_info.SkillGroups.Count];
            string[] n_newToolTips = new string[c_info.SkillGroups.Count];
            float[] n_newSliceRelativeDisplacements = new float[c_info.SkillGroups.Count];

            int tinyGroups = 1;
            for (int i = 0; i < c_info.SkillGroups.Count; i++)
            {
                SerializableSkillGroup sg = c_info.SkillGroups[i];
                newValues[i] = 0;
                newTexts[i] = sg.Name;
                foreach (SerializableSkill s in sg.Skills)
                {
                    newValues[i] += s.SkillPoints;
                }
                newSliceRelativeDisplacements[i] = (newValues[i] < 100000) ? 0.06F + (0.008F * ++tinyGroups) : 0.05F;
                newToolTips[i] = sg.Name + " (" + sg.Skills.Count + " skills, " + String.Format("{0:#,###}", newValues[i]) + " skillpoints)";
            }

            // reordening the slices
            for (int num = 0; num < c_info.SkillGroups.Count; num++)
            {
                decimal tempsp = decimal.MinValue;
                int biggest = -1;
                for (int y = 0; y < newValues.Length; y++)
                {
                    if (tempsp == -1)
                    {
                        tempsp = newValues[y];
                        biggest = y;
                    }
                    if (newValues[y] > tempsp)
                    {
                        tempsp = newValues[y];
                        biggest = y;
                    }

                }
                
                n_newValues[num] = tempsp;
                n_newTexts[num] = newTexts[biggest];
                n_newSliceRelativeDisplacements[num] = newSliceRelativeDisplacements[biggest];
                n_newToolTips[num] = newToolTips[biggest];
                newValues[biggest] = 0;
            }

            skillPieChartControl.Values = n_newValues;
            skillPieChartControl.Texts = n_newTexts;
            skillPieChartControl.ToolTips = n_newToolTips;
            skillPieChartControl.SliceRelativeDisplacements = n_newSliceRelativeDisplacements;
        }

        private void SkillsPieChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_settings.SkillPieChartColors.Clear();
            foreach (Color c in skillPieChartControl.Colors)
            {
                m_settings.SkillPieChartColors.Add(new SerializableColor(c));
            }
            m_settings.SkillPieChartSliceRelativeHeight = (float)pieHeight.Value;
            m_settings.SkillPieChartInitialAngle = (float)pieAngle.Value;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pieHeight_ValueChanged(object sender, EventArgs e)
        {
            skillPieChartControl.SliceRelativeHeight = (float)pieHeight.Value;
        }

        private void pieAngle_ValueChanged(object sender, EventArgs e)
        {
            skillPieChartControl.InitialAngle = (float)pieAngle.Value;
        }

        private void skillPieChartControl_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs ev = (MouseEventArgs)e;
            PieChart3D m_pieChart = skillPieChartControl.PieChart;
            int index = m_pieChart.FindPieSliceUnderPoint(new PointF(ev.X, ev.Y));
            if (index != -1 && m_colorDialog.ShowDialog() == DialogResult.OK)
            {
                skillPieChartControl.Colors[index] = Color.FromArgb(125, m_colorDialog.Color);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Bitmap pie = new Bitmap(skillPieChartControl.Width, skillPieChartControl.Height);
            Rectangle bounds = new Rectangle(0, 0, skillPieChartControl.Width, skillPieChartControl.Height);
            skillPieChartControl.DrawToBitmap(pie, bounds);
            DialogResult savePieResult = savePieDialog.ShowDialog();
            if (savePieResult == DialogResult.OK)
            {
                pie.Save(savePieDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
