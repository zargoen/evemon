using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ItemSelectControl : UserControl
    {
        public ItemSelectControl()
        {
            InitializeComponent();
        }

        private Settings m_settings;
        private Plan m_plan;
        public Plan Plan
        {
            get { return m_plan; }
            set { m_plan = value; }
        }


        private ItemCategory m_rootCategory;

        private void ItemSelectControl_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            try
            {
                m_settings = Settings.GetInstance();
                cbSkillFilter.SelectedIndex = m_settings.ItemSkillFilter;
                cbSlotFilter.SelectedIndex = m_settings.ItemSlotFilter;

                cbTech1.Checked = m_settings.ShowT1Items;
                cbNamed.Checked = m_settings.ShowNamedItems;
                cbTech2.Checked = m_settings.ShowT2Items;
                cbOfficer.Checked = m_settings.ShowOfficerItems;
                cbFaction.Checked = m_settings.ShowFactionItems;
                cbDeadspace.Checked = m_settings.ShowDeadspaceItems;

                m_rootCategory = ItemCategory.GetRootCategory();

                // needs to be after we set the root category.
                if (m_settings.StoreBrowserFilters)
                    tbSearchText.Text = m_settings.ItemBrowserSearch;
                lbSearchTextHint.Visible = String.IsNullOrEmpty(tbSearchText.Text);
                if (m_plan != null)
                    BuildTreeView();
            }
            catch (Exception err)
            {
                // This occurs when we're in the designer. DesignMode doesn't get set
                // when the control is a subcontrol of a user control, so we should handle
                // this here :(
                ExceptionHandler.LogException(err, true);
                return;
            }
        }

        #region Filters
        private delegate bool ItemFilter(Item i);
        private static ItemFilter showAll = delegate { return true; };

        private ItemFilter skillFilter = showAll;
        
        private ItemFilter slotFilter = showAll;

        private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_settings.ItemSkillFilter=cbSkillFilter.SelectedIndex;
            UpdateDisplay();
        }

        private void UpdateSkillFilter()
        {
            switch (cbSkillFilter.SelectedIndex)
            {
                default:
                case 0: // All Items
                    skillFilter = showAll;
                    break;
                case 1: // Items I can use
                    skillFilter = delegate(Item i)
                             {
                                 Skill gs = null;
                                 for (int x = 0; x < i.RequiredSkills.Count; x++)
                                 {
                                     try
                                     {
                                         gs = m_plan.GrandCharacterInfo.GetSkill(i.RequiredSkills[x].Name);
                                         if (gs.Level < i.RequiredSkills[x].Level) return false;
                                     }
                                     catch
                                     {
                                         // unknown or no skill - assume we can use it
                                         return true;
                                     }
                                 }
                                 return true;
                             };
                    break;
                case 2: // Items I Can NOT Use
                    skillFilter = delegate(Item i)
                             {
                                 Skill gs = null;
                                 for (int x = 0; x < i.RequiredSkills.Count; x++)
                                 {
                                     try
                                     {
                                         gs = m_plan.GrandCharacterInfo.GetSkill(i.RequiredSkills[x].Name);
                                         if (gs.Level < i.RequiredSkills[x].Level) return true;
                                     }
                                     catch
                                     {
                                         // unknown or no skill - assume we can use it
                                         return false;
                                     }
                                 }
                                 return false;
                             };
                    break;
            }
        }

        private bool ClassFilter(Item i)
        {
            switch (i.Metagroup)
            {
                case "Tech I":
                   return cbTech1.Checked;
                case "Named":
                    return cbNamed.Checked;
                case "Tech II":
                    return cbTech2.Checked;
                case "Officer":
                case "Storyline":
                    return cbOfficer.Checked;
                case "Faction":
                    return cbFaction.Checked;
                case "Deadspace":
                    return cbDeadspace.Checked;
                default:
                    return false;
             }
        }

        private void cbSlotFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_settings.ItemSlotFilter=cbSlotFilter.SelectedIndex;
            UpdateDisplay();
        }

        private void UpdateSlotFilter()
        {
            switch (cbSlotFilter.SelectedIndex)
            {
                default:
                case 0: // All items
                    slotFilter = showAll;
                    break;
                case 1: // High slot
                case 2: // Mid slot
                case 3: // Low slot
                    slotFilter = delegate(Item i) { return i.SlotIndex == cbSlotFilter.SelectedIndex; };
                    break;
                case 4: // No-slot items
                    slotFilter = delegate(Item i) { return i.SlotIndex == 0; };
                    break;
            }
        }

        private void cbClass_SelectedChanged(object sender, EventArgs e)
        {
            if (sender == cbTech1) m_settings.ShowT1Items = cbTech1.Checked;
            if (sender == cbNamed) m_settings.ShowNamedItems = cbNamed.Checked;
            if (sender == cbTech2) m_settings.ShowT2Items = cbTech2.Checked;
            if (sender == cbOfficer) m_settings.ShowOfficerItems = cbOfficer.Checked;
            if (sender == cbFaction) m_settings.ShowFactionItems = cbFaction.Checked;
            if (sender == cbDeadspace) m_settings.ShowDeadspaceItems = cbDeadspace.Checked;
            UpdateDisplay();
        }
        
        #endregion

        #region Display

        private void UpdateDisplay()
        {
            UpdateSkillFilter();
            UpdateSlotFilter();
            if (m_rootCategory != null)
                BuildTreeView();
            SearchTextChanged();
        }

        private void BuildTreeView()
        {
            tvItems.Nodes.Clear();
            tvItems.BeginUpdate();
            try
            {
                if (m_rootCategory != null)
                {
                    BuildSubtree(m_rootCategory, tvItems.Nodes);
                }
                // expand the top level nodes so we see Ship Equipment/Ship Upgrades etc.
                foreach (TreeNode n in tvItems.Nodes)
                {
                    n.Expand();
                }
                
            }
            finally
            {
                tvItems.EndUpdate();
            }
        }

        private void BuildSubtree(ItemCategory cat, TreeNodeCollection nodeCollection)
        {
            SortedDictionary<string, ItemCategory> sortedSubcats = new SortedDictionary<string, ItemCategory>();
            foreach (ItemCategory tcat in cat.Subcategories)
            {
                sortedSubcats.Add(tcat.Name, tcat);
            }
            foreach (ItemCategory tcat in sortedSubcats.Values)
            {
                TreeNode tn = new TreeNode();
                tn.Text = tcat.Name;
                BuildSubtree(tcat, tn.Nodes);
                if (tn.GetNodeCount(true) > 0)
                    nodeCollection.Add(tn);
            }

            SortedDictionary<string, Item> sortedItems = new SortedDictionary<string, Item>();
            foreach (Item titem in cat.Items)
            {
                if (skillFilter(titem) && slotFilter(titem) && ClassFilter(titem))
                    sortedItems.Add(titem.Name, titem);
            }
            foreach (Item titem in sortedItems.Values)
            {
                TreeNode tn = new TreeNode();
                tn.Text = titem.Name;
                tn.Tag = titem;
                nodeCollection.Add(tn);
            }
        }
        #endregion

        #region Search
        private void lbSearchTextHint_Click(object sender, EventArgs e)
        {
            tbSearchText.Focus();
        }

        private void tbSearchText_Enter(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = false;
        }

        private void tbSearchText_Leave(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = String.IsNullOrEmpty(tbSearchText.Text);
        }

        private void tbSearchText_TextChanged(object sender, EventArgs e)
        {

            if (m_settings.StoreBrowserFilters)
                m_settings.ItemBrowserSearch = tbSearchText.Text;
            SearchTextChanged();
        }

        private void SearchTextChanged()
        {
            if (m_rootCategory == null) return;

            string searchText = tbSearchText.Text.Trim().ToLower();

            if (String.IsNullOrEmpty(searchText))
            {
                tvItems.Visible = true;
                lbSearchList.Visible = false;
                lbNoMatches.Visible = false;
                return;
            }

            // first pass - find everything in the current tree matches the search string
            SortedList<string, Item> filteredItems = new SortedList<string, Item>();
            foreach (TreeNode n in tvItems.Nodes)
            {
                SearchNode(n,searchText,filteredItems);
            }
            lbSearchList.BeginUpdate();
            try
            {
                lbSearchList.Items.Clear();
                if (filteredItems.Count > 0)
                {
                    foreach (Item i in filteredItems.Values)
                    {
                        lbSearchList.Items.Add(i);
                    }
                }
            }
            finally
            {
                lbSearchList.EndUpdate();
            }

            lbSearchList.Visible = true;
            tvItems.Visible = false;
            lbNoMatches.Visible = (filteredItems.Count == 0);
        }

        private void SearchNode(TreeNode tn, string searchText, SortedList<string, Item> filteredItems)
        {
            Item itm = tn.Tag as Item;
            if (itm == null)
            {
                foreach (TreeNode subNode in tn.Nodes)
                {
                    SearchNode(subNode, searchText, filteredItems);
                }
                return;
            }
            if (itm.Name.ToLower().Contains(searchText) || itm.Description.ToLower().Contains(searchText))
            {
                filteredItems.Add(itm.Name, itm);
            }
        }

        private void tbSearchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x01)
            {
                tbSearchText.SelectAll();
                e.Handled = true;
            }

        }

        #endregion 

        #region Events
        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvItems.SelectedNode != null)
            {
                SetSelectedItem(tvItems.SelectedNode.Tag as Item);
            }
            else
            {
                SetSelectedItem(null);
            }
        }

        private Item m_selectedItem = null;

        public Item SelectedItem
        {
            get { return m_selectedItem; }
            set { SetSelectedItem(value); }
        }

        public event EventHandler<EventArgs> SelectedItemChanged;

        private void SetSelectedItem(Item i)
        {
            m_selectedItem = i;
            if (SelectedItemChanged != null)
            {
                SelectedItemChanged(this, new EventArgs());
            }
        }

        private void lbItemResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedItem(lbSearchList.SelectedItem as Item);
        }
        #endregion
    }
}
