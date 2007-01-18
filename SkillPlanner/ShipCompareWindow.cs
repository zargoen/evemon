using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ShipCompareWindow : Form
    {
        public ShipCompareWindow()
        {
            InitializeComponent();
        }

        public Ship SelectedShip
        {
            get { return shipSelectControl.SelectedShip; }
            set { shipSelectControl.SelectedShip = value; }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public static Ship CompareWithShipInput(Ship selectedShip,Plan plan)
        {
            using (ShipCompareWindow f = new ShipCompareWindow())
            {
                f.shipSelectControl.Plan = plan;
                f.SelectedShip = selectedShip;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                    return f.SelectedShip;
                else
                    return null;
            }
        }

    }
}
