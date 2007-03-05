using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ShipSelectControl : UserControl
    {
        public ShipSelectControl()
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

        private Ship[] m_ships;

        private void ShipSelectControl_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            try
            {
                m_settings = Settings.GetInstance();
                //cbSkillFilter.SelectedIndex = 0;
                cbSkillFilter.SelectedIndex = m_settings.ShipBrowserFilter;
                m_ships = Ship.GetShips();
                if (m_settings.StoreBrowserFilters)
                    tbSearchText.Text = m_settings.ShipBrowserSearch;
                lbSearchTextHint.Visible = String.IsNullOrEmpty(tbSearchText.Text);
                if (m_ships != null)
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
        private delegate bool ShipFilter(Ship s);
        private ShipFilter sf = delegate { return true; };

        private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_settings.ShipBrowserFilter = cbSkillFilter.SelectedIndex;
            UpdateDisplay();
        }

        private void UpdateSkillFilter()
        {
            switch (cbSkillFilter.SelectedIndex)
            {
                default:
                case 0: // All Ships
                    sf = delegate
                             {
                                 return true;
                             };
                    break;
                case 1: // Ships I Can fly
                    sf = delegate(Ship s)
                             {
                                 Skill gs = null;
                                 for (int i = 0; i < s.RequiredSkills.Count; i++)
                                 {
                                     try
                                     {
                                         gs = m_plan.GrandCharacterInfo.GetSkill(s.RequiredSkills[i].Name);
                                         if (gs.Level < s.RequiredSkills[i].Level) return false;
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
                case 2: // Ships I Can NOT fly
                    sf = delegate(Ship s)
                             {
                                 Skill gs = null;
                                 for (int i = 0; i < s.RequiredSkills.Count; i++)
                                 {
                                     try
                                     {
                                         gs = m_plan.GrandCharacterInfo.GetSkill(s.RequiredSkills[i].Name);
                                         if (gs.Level < s.RequiredSkills[i].Level) return true;
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
        #endregion

        #region Display
        private void UpdateDisplay()
        {
            UpdateSkillFilter();
            if (m_ships != null)
                BuildTreeView();
            SearchTextChanged();
        }

        private void BuildTreeView()
        {
            tvShips.Nodes.Clear();
            tvShips.BeginUpdate();
            try
            {
                SortedList<string, List<Ship>> types = new SortedList<string, List<Ship>>();
                foreach (Ship s in m_ships)
                {
                    if (sf(s))
                    {
                        // check with filter if this ship is to be ad
                        if (!types.ContainsKey(s.Type))
                        {
                            List<Ship> nl = new List<Ship>();
                            nl.Add(s);
                            types.Add(s.Type, nl);
                        }
                        else
                        {
                            types[s.Type].Add(s);
                        }
                    }
                }

                foreach (KeyValuePair<string, List<Ship>> kvp in types)
                {
                    TreeNode tvn = new TreeNode();
                    tvn.Text = kvp.Key;

                    SortedList<string, List<Ship>> races = new SortedList<string, List<Ship>>();
                    foreach (Ship s in kvp.Value)
                    {
                        if (!races.ContainsKey(s.Race))
                        {
                            List<Ship> nl = new List<Ship>();
                            nl.Add(s);
                            races.Add(s.Race, nl);
                        }
                        else
                        {
                            races[s.Race].Add(s);
                        }
                    }
                    foreach (KeyValuePair<string, List<Ship>> ssvp in races)
                    {
                        TreeNode racenode = new TreeNode();
                        racenode.Text = ssvp.Key;

                        SortedList<string, Ship> ships = new SortedList<string, Ship>();
                        foreach (Ship s in ssvp.Value)
                        {
                            ships[s.Name] = s;
                        }
                        foreach (Ship s in ships.Values)
                        {
                            TreeNode stn = new TreeNode();
                            stn.Text = s.Name;
                            stn.Tag = s;
                            racenode.Nodes.Add(stn);
                        }

                        tvn.Nodes.Add(racenode);
                    }

                    tvShips.Nodes.Add(tvn);
                }
            }
            finally
            {
                tvShips.EndUpdate();
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
                m_settings.ShipBrowserSearch = tbSearchText.Text;
            SearchTextChanged();
        }

        private void SearchTextChanged()
        {
            string searchText = tbSearchText.Text.Trim().ToLower();
           
            if (String.IsNullOrEmpty(searchText))
            {
                tvShips.Visible = true;
                lbSearchList.Visible = false;
                lbNoMatches.Visible = false;
                return;
            }
            // find everything in the current tree that matches the search string
            SortedList<string, Ship> filteredItems = new SortedList<string, Ship>();
            foreach (TreeNode n in tvShips.Nodes)
            {
                SearchNode(n,searchText,filteredItems);
            }

            lbSearchList.BeginUpdate();
            try
            {
                lbSearchList.Items.Clear();
                if (filteredItems.Count > 0)
                {
                    foreach (Ship s in filteredItems.Values)
                    {   
                        lbSearchList.Items.Add(s);
                    }
                }
            }
            finally
            {
                lbSearchList.EndUpdate();
            }
            lbSearchList.Visible = true;
            tvShips.Visible = false;
            lbNoMatches.Visible = (filteredItems.Count == 0);
        }

        private void SearchNode(TreeNode tn, string searchText, SortedList<string, Ship> filteredItems)
        {
            Ship itm = tn.Tag as Ship;
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
        private void tvShips_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvShips.SelectedNode != null)
            {
                SetSelectedShip(tvShips.SelectedNode.Tag as Ship);
            }
            else
            {
                SetSelectedShip(null);
            }
        }

        private Ship m_selectedShip = null;

        public Ship SelectedShip
        {
            get { return m_selectedShip; }
            set { m_selectedShip = value; }
        }

        public event EventHandler<EventArgs> SelectedShipChanged;

        private void SetSelectedShip(Ship s)
        {
            m_selectedShip = s;
            if (SelectedShipChanged != null)
            {
                SelectedShipChanged(this, new EventArgs());
            }
        }
        
        private void lbShipResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedShip(lbSearchList.SelectedItem as Ship);
        }
        #endregion
    }
}
