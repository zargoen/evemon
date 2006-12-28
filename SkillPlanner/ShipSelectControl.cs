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

        private Plan m_plan;
        public Plan Plan
        {
            get { return m_plan; }
            set { m_plan = value; }
        }

        private Ship[] m_ships;

        private void ShipSelectControl_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            m_ships = Ship.GetShips();
            if (m_ships != null)
                BuildTreeView();
        }

        // Filtering code
        private delegate bool ShipFilter(Ship s);
        private ShipFilter sf = delegate { return true; };

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilter.SelectedIndex)
            {
                default:
                case 0: // All Ships
                    sf = delegate
                             {
                                 return true;
                             };
                    break;
                case 1: // Ships I can fly
                    sf = delegate(Ship s)
                             {
                                 Skill gs=null;
                                 for (int i = 0; i < s.RequiredSkills.Count;i++)
                                 {
                                    gs = m_plan.GrandCharacterInfo.GetSkill(s.RequiredSkills[i].Name);
                                    if (gs.Level < s.RequiredSkills[i].Level) return false;
                                 }
                                 return true;
                             };
                    break;
            }
            if (m_ships != null)
                BuildTreeView();
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
            bool showListBox = false;

            if (!String.IsNullOrEmpty(tbSearchText.Text))
            {
                string trimmedSearch = tbSearchText.Text.Trim().ToLower();
                if (!String.IsNullOrEmpty(tbSearchText.Text))
                {
                    showListBox = true;

                    lbShipResults.BeginUpdate();
                    try
                    {
                        lbShipResults.Items.Clear();
                        SortedList<string, Ship> results = new SortedList<string, Ship>();
                        foreach (Ship s in m_ships)
                        {
                            if (s.Name.ToLower().IndexOf(trimmedSearch) != -1)
                            {
                                results[s.Name] = s;
                            }
                        }
                        foreach (string shipName in results.Keys)
                        {
                            lbShipResults.Items.Add(shipName);
                        }
                    }
                    finally
                    {
                        lbShipResults.EndUpdate();
                    }
                }
            }

            tvShips.Visible = !showListBox;
            lbShipResults.Visible = showListBox;
            lbNoMatches.Visible = showListBox && (lbShipResults.Items.Count == 0);
            if (lbNoMatches.Visible)
            {
                lbNoMatches.BringToFront();
            }
        }

        private void tvShips_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;
            if (tn != null)
            {
                if (tn.Tag is Ship)
                {
                    OnSelectedShipChanged((Ship) tn.Tag);
                }
            }
        }

        private void lbShipResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = lbShipResults.SelectedIndex;
            if (idx >= 0)
            {
                string shipName = (string) lbShipResults.Items[idx];
                foreach (Ship s in m_ships)
                {
                    if (s.Name == shipName)
                    {
                        OnSelectedShipChanged(s);
                        return;
                    }
                }
            }
        }

        private Ship m_selectedShip = null;

        public Ship SelectedShip
        {
            get { return m_selectedShip; }
        }

        private void OnSelectedShipChanged(Ship s)
        {
            m_selectedShip = s;
            if (SelectedShipChanged != null)
            {
                SelectedShipChanged(this, new EventArgs());
            }
        }

        public event EventHandler<EventArgs> SelectedShipChanged;
    }
}