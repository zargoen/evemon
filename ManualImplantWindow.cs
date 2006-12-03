using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon
{
    public partial class ManualImplantWindow : EVEMonForm
    {
        public ManualImplantWindow()
        {
            InitializeComponent();
        }

        public ManualImplantWindow(IEnumerable<GrandEveAttributeBonus> bonuses)
            : this()
        {
            foreach (GrandEveAttributeBonus b in bonuses)
            {
                workingList.Add(b);
            }
        }

        static EveAttribute toSearchForAttrib = EveAttribute.None;
        enum Searching { ALL, XML, USER };
        static Searching Where = Searching.ALL;

        private static bool isSameAttribute(GrandEveAttributeBonus x)
        {
            if (x.EveAttribute == toSearchForAttrib && (Where == Searching.ALL || (x.Manual && Where == Searching.USER) || (!x.Manual && Where == Searching.XML)))
                return true;
            else
                return false;
        }

        private List<GrandEveAttributeBonus> workingList = new List<GrandEveAttributeBonus>();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_resultList = new List<GrandEveAttributeBonus>();
            foreach (ListViewItem lvi in lvImplants.Items)
            {
                m_resultList.Add(lvi.Tag as GrandEveAttributeBonus);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private List<GrandEveAttributeBonus> m_resultList = null;

        public IEnumerable<GrandEveAttributeBonus> ResultBonuses
        {
            get { return m_resultList; }
        }

        private void cmsImplantCommands_Opening(object sender, CancelEventArgs e)
        {
            miModify.Enabled = (lvImplants.SelectedItems.Count == 1);
            miModify.Visible = (lvImplants.SelectedItems.Count > 0);
            miDelete.Enabled = (lvImplants.SelectedItems.Count > 0);
            miDelete.Visible = (lvImplants.SelectedItems.Count > 0);
            msEditSep.Visible = (lvImplants.SelectedItems.Count > 0);
        }

        private void miAddNew_Click(object sender, EventArgs e)
        {
            using (ManualImplantDetailWindow f = new ManualImplantDetailWindow())
            {
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    // remove CCP XML item for same attribute if it exists
                    GrandEveAttributeBonus temp = f.ResultBonus;
                    toSearchForAttrib = temp.EveAttribute;
                    Where = Searching.XML;
                    int i = workingList.FindIndex(isSameAttribute);
                    if (i != -1)
                    {
                        List<ListViewItem> removeItems = new List<ListViewItem>();
                        foreach (ListViewItem Lvi in lvImplants.Items)
                        {
                            if ((Lvi.Tag as GrandEveAttributeBonus).EveAttribute == temp.EveAttribute)
                                removeItems.Add(Lvi);
                        }
                        lvImplants.BeginUpdate();
                        try
                        {
                            foreach (ListViewItem lvi in removeItems)
                            {
                                lvImplants.Items.Remove(lvi);
                            }
                        }
                        finally
                        {
                            lvImplants.EndUpdate();
                        }
                    }
                    // add new User generated implant
                    lvImplants.Items.Add(CreateItemForBonus(temp));
                }
            }
        }

        private ListViewItem CreateItemForBonus(GrandEveAttributeBonus b)
        {
            ListViewItem lvi = new ListViewItem(b.EveAttribute.ToString());
            lvi.Tag = b;
            if (b.Manual)
                lvi.SubItems.Add("User");
            else
                lvi.SubItems.Add("CCP XML");
            lvi.SubItems.Add(b.Amount.ToString());
            lvi.SubItems.Add(b.Name);
            return lvi;
        }

        private void ManualImplantWindow_Load(object sender, EventArgs e)
        {
            lvImplants.BeginUpdate();
            try
            {
                foreach (GrandEveAttributeBonus b in workingList)
                {
                    if (!b.Manual)
                    {
                        toSearchForAttrib = b.EveAttribute;
                        Where = Searching.USER;
                        if (!workingList.Exists(isSameAttribute))
                        {
                            ListViewItem lvi = CreateItemForBonus(b);
                            lvImplants.Items.Add(lvi);
                        }
                    }
                    else
                    {
                        ListViewItem lvi = CreateItemForBonus(b);
                        lvImplants.Items.Add(lvi);
                    }
                }
                chName.Width = -2;
            }
            finally
            {
                lvImplants.EndUpdate();
            }
        }

        private void miModify_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.No;
            if (lvImplants.SelectedItems.Count != 1)
            {
                dr = MessageBox.Show("Please try again with one implant selected.",
                                     "Wrong number of Implants selected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if ((lvImplants.SelectedItems[0].Tag as GrandEveAttributeBonus).Manual == false)
            {
                dr = MessageBox.Show("Please add an implant to overwrite an automatically included implant.",
                                     "Trying to modify an XML implant!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem lvi = lvImplants.SelectedItems[0];
            using (ManualImplantDetailWindow f = new ManualImplantDetailWindow(lvi.Tag as GrandEveAttributeBonus))
            {
                DialogResult dr2 = f.ShowDialog();
                if (dr2 == DialogResult.OK)
                {
                    ListViewItem rlvi = CreateItemForBonus(f.ResultBonus);
                    // change the manual implant details
                    lvImplants.Items[lvImplants.Items.IndexOf(lvi)] = rlvi;
                    if (((lvi.Tag as GrandEveAttributeBonus).EveAttribute) != ((rlvi.Tag as GrandEveAttributeBonus).EveAttribute))
                    {
                        Where = Searching.XML;
                        int i = 0;
                        // add XML sourced implant with (lvi.Tag as GrandEveAttributeBonus).EveAttribute
                        GrandEveAttributeBonus add_temp = (lvi.Tag as GrandEveAttributeBonus);
                        toSearchForAttrib = add_temp.EveAttribute;
                        if (workingList.Exists(isSameAttribute))
                        {
                            i = workingList.FindIndex(isSameAttribute);
                            lvImplants.Items.Add(CreateItemForBonus(workingList[i]));
                        }
                        i = -1;
                        // remove any XML sourced implant with (rlvi.Tag as GrandEveAttributeBonus).EveAttribute
                        GrandEveAttributeBonus remove_temp = (rlvi.Tag as GrandEveAttributeBonus);
                        toSearchForAttrib = remove_temp.EveAttribute;
                        i = workingList.FindIndex(isSameAttribute);
                        if (i != -1)
                        {
                            List<ListViewItem> removeItems = new List<ListViewItem>();
                            foreach (ListViewItem Lvi in lvImplants.Items)
                            {
                                if ((Lvi.Tag as GrandEveAttributeBonus).EveAttribute == remove_temp.EveAttribute && (Lvi.Tag as GrandEveAttributeBonus).Manual == false)
                                    removeItems.Add(Lvi);
                            }
                            lvImplants.BeginUpdate();
                            try
                            {
                                foreach (ListViewItem Lvi in removeItems)
                                {
                                    lvImplants.Items.Remove(Lvi);
                                }
                            }
                            finally
                            {
                                lvImplants.EndUpdate();
                            }
                        }

                    }
                }
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.No;
            if (lvImplants.SelectedItems.Count == 0)
            {
                dr = MessageBox.Show("Please try again with at least one implant selected.",
                                     "No Implants selected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                foreach (ListViewItem lvi in lvImplants.SelectedItems)
                {
                    if ((lvi.Tag as GrandEveAttributeBonus).Manual == false)
                    {
                        dr = MessageBox.Show("Please add an implant to overwrite an automatically included implant.\nYou can not delete an XML implant.\nAdd a zero value implant to negate the XML implant.",
                                             "Trying to delete an XML implant!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            List<ListViewItem> removeItems = new List<ListViewItem>();
            if (lvImplants.SelectedItems.Count == 1)
            {
                ListViewItem lvi = lvImplants.SelectedItems[0];
                GrandEveAttributeBonus b = lvi.Tag as GrandEveAttributeBonus;
                dr = MessageBox.Show("Are you sure you want to delete \"" + b.Name + "\"?",
                                     "Delete Implant?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                     MessageBoxDefaultButton.Button2);
                removeItems.Add(lvi);
            }
            else
            {
                dr = MessageBox.Show("Are you sure you want to delete these implants?",
                                     "Delete Implants?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                     MessageBoxDefaultButton.Button2);
                foreach (ListViewItem lvi in lvImplants.SelectedItems)
                {
                    removeItems.Add(lvi);
                }
            }

            lvImplants.BeginUpdate();
            try
            {
                if (dr == DialogResult.Yes)
                {
                    foreach (ListViewItem lvi in removeItems)
                    {
                        GrandEveAttributeBonus temp = (lvi.Tag as GrandEveAttributeBonus);
                        toSearchForAttrib = temp.EveAttribute;
                        Where = Searching.XML;
                        if (workingList.Exists(isSameAttribute))
                        {
                            int i = workingList.FindIndex(isSameAttribute);
                            lvImplants.Items.Add(CreateItemForBonus(workingList[i]));
                        }
                        lvImplants.Items.Remove(lvi);
                    }
                }
            }
            finally
            {
                lvImplants.EndUpdate();
            }
        }

        private void lvImplants_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void lvImplants_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                miDelete_Click(this, new EventArgs());
            }
        }

        private void lvImplants_DoubleClick(object sender, EventArgs e)
        {
            if (lvImplants.SelectedItems.Count > 0)
            {
                miModify_Click(this, new EventArgs());
            }
            else
            {
                miAddNew_Click(this, new EventArgs());
            }
        }
    }
}