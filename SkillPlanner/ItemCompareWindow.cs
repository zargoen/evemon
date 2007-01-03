using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ItemCompareWindow : Form
    {
        public ItemCompareWindow()
        {
            InitializeComponent();
        }

        public Item SelectedItem
        {
            get { return itemSelectControl1.SelectedItem; }
            set { itemSelectControl1.SelectedItem = value; }
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

        public static Item CompareWithItemInput(Item selectedItem)
        {
            using (ItemCompareWindow f = new ItemCompareWindow())
            {
                f.SelectedItem = selectedItem;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                    return f.SelectedItem;
                else
                    return null;
            }
        }

    }
}
