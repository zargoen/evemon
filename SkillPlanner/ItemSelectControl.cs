using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ItemSelectControl : UserControl
    {
        private Settings m_settings;

        public ItemSelectControl()
        {
            InitializeComponent();
        }

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
 //           cbSkillFilter.SelectedIndex = 0;
 //           cbSlotFilter.SelectedIndex = 0;

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

        // Filtering code
        private delegate bool ItemFilter(Item i);
        private static ItemFilter showAll = delegate { return true; };

        private ItemFilter skillFilter = showAll;
        
        private ItemFilter slotFilter = showAll;

        private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_settings.ItemSkillFilter=cbSkillFilter.SelectedIndex;
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
            }
            if (m_rootCategory != null)
                BuildTreeView();
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
            if (m_rootCategory != null)
                BuildTreeView();
        }

        private void cbClass_SelectedChanged(object sender, EventArgs e)
        {
            if (sender == cbTech1) m_settings.ShowT1Items = cbTech1.Checked;
            if (sender == cbNamed) m_settings.ShowNamedItems = cbNamed.Checked;
            if (sender == cbTech2) m_settings.ShowT2Items = cbTech2.Checked;
            if (sender == cbOfficer) m_settings.ShowOfficerItems = cbOfficer.Checked;
            if (sender == cbFaction) m_settings.ShowFactionItems = cbFaction.Checked;
            if (sender == cbDeadspace) m_settings.ShowDeadspaceItems = cbDeadspace.Checked;

            if (m_rootCategory != null)
                BuildTreeView();
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
            if (m_rootCategory == null) return;
            m_settings.ItemBrowserSearch = tbSearchText.Text;
            if (String.IsNullOrEmpty(tbSearchText.Text) ||
                String.IsNullOrEmpty(tbSearchText.Text.Trim()))
            {
                tvItems.Visible = true;
                lbItemResults.Visible = false;
                lbNoMatches.Visible = false;
                return;
            }

            string searchText = tbSearchText.Text.Trim().ToLower();
            SortedList<string, Item> searchedItems = new SortedList<string, Item>();
            SearchCategory(m_rootCategory, searchText, searchedItems);

            lbItemResults.BeginUpdate();
            try
            {
                lbItemResults.Items.Clear();
                if (searchedItems.Count > 0)
                {
                    foreach (Item i in searchedItems.Values)
                    {
                        lbItemResults.Items.Add(i);
                    }
                }
                else
                {
                    lbNoMatches.Visible = true;
                }
            }
            finally
            {
                lbItemResults.EndUpdate();
            }

            lbItemResults.Visible = true;
            tvItems.Visible = false;
        }

        private void SearchCategory(ItemCategory cat, string searchText, SortedList<string, Item> searchedItems)
        {
            foreach (ItemCategory tcat in cat.Subcategories)
            {
                SearchCategory(tcat, searchText, searchedItems);
            }
            foreach (Item titem in cat.Items)
            {
                if (titem.Name.ToLower().Contains(searchText))
                {
                    searchedItems.Add(titem.Name, titem);
                }
            }
        }

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
            SetSelectedItem(lbItemResults.SelectedItem as Item);
        }
    }
}
