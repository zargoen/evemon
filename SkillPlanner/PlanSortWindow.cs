using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class PlanSortWindow : EVEMonForm
    {
        public PlanSortWindow()
        {
            InitializeComponent();
        }

        private void PlanSortWindow_Load(object sender, EventArgs e)
        {
            cbSortType.BeginUpdate();
            try
            {
                cbSortType.Items.Clear();
                int count = Enum.GetValues(typeof (PlanSortType)).Length;
                for (int i = 0; i < count; i++)
                {
                    string txt = String.Empty;
                    PlanSortDescriptionAttribute descAttr =
                        EnumAttributeReader<PlanSortType, PlanSortDescriptionAttribute>.GetAttribute((PlanSortType) i);
                    if (descAttr != null)
                    {
                        txt = descAttr.DisplayText;
                    }
                    else
                    {
                        txt = ((PlanSortType) i).ToString();
                    }
                    cbSortType.Items.Add(txt);
                }
            }
            finally
            {
                cbSortType.EndUpdate();
            }
            cbSortType.SelectedIndex = (int) PlanSortType.FastestFirst;
        }

        private void cbSortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlanSortDescriptionAttribute descAttr =
                EnumAttributeReader<PlanSortType, PlanSortDescriptionAttribute>.GetAttribute(
                    (PlanSortType) cbSortType.SelectedIndex);
            if (descAttr != null)
            {
                lblDescription.Text = descAttr.Description;
            }
            else
            {
                lblDescription.Text = String.Empty;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private PlanSortType m_resultSortType;
        private bool m_resultLearningFirst;

        public PlanSortType SortType
        {
            get { return m_resultSortType; }
        }

        public bool LearningFirst
        {
            get { return m_resultLearningFirst; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_resultSortType = (PlanSortType) cbSortType.SelectedIndex;
            m_resultLearningFirst = cbArrangeLearning.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }


}