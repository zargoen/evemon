using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class ShipSelectControl : EveObjectSelectControl
    {
        public ShipSelectControl()
        {
            InitializeComponent();
        }

        private Ship[] m_ships;

        protected override void EveObjectSelectControl_Load(object sender, EventArgs e)
        {
            base.EveObjectSelectControl_Load(sender,e);
            try
            {
                cbSkillFilter.SelectedIndex = m_settings.ShipBrowserFilter;
                m_ships = Ship.GetShips();
                if (m_settings.StoreBrowserFilters)
                    tbSearchText.Text = m_settings.ShipBrowserSearch;
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
            this.cbSkillFilter.Items[0] = "All Ships";
            this.cbSkillFilter.Items[1] = "Ships I can fly";
            this.cbSkillFilter.Items[2] = "Ships I cannot fly";
        }

        #region Filters
        
        protected void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_settings.ShipBrowserFilter = cbSkillFilter.SelectedIndex;
            UpdateDisplay();
        }

        protected void UpdateSkillFilter()
        {
            switch (cbSkillFilter.SelectedIndex)
            {
                default:
                case 0: m_filterDelegate = SelectAll;
                    break;
                case 1: // Ships I Can fly
                    m_filterDelegate = CanUse;
                    break;
                case 2: // Ships I Can NOT fly
                    m_filterDelegate = CannotUse;
                    break;
            }
        }
        #endregion

        #region Display
        protected void UpdateDisplay()
        {
            UpdateSkillFilter();
            if (m_ships != null)
                BuildTreeView();
            SearchTextChanged();
        }

        protected void BuildTreeView()
        {
            tvItems.Nodes.Clear();
            tvItems.BeginUpdate();
            try
            {
                SortedList<string, List<Ship>> types = new SortedList<string, List<Ship>>();
                foreach (Ship s in m_ships)
                {
                    if (m_filterDelegate(s))
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

                    tvItems.Nodes.Add(tvn);
                }
            }
            finally
            {
                tvItems.EndUpdate();
            }
        }
        #endregion

        #region Search
        protected override void tbSearchText_TextChanged(object sender, EventArgs e)
        {
            if (m_settings.StoreBrowserFilters)
                m_settings.ShipBrowserSearch = tbSearchText.Text;
            base.tbSearchText_TextChanged(sender,e);
        }

        #endregion 

        protected void InitializeComponent()
        {
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSkillFilter
            // 
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // tvItems
            // 
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.LineColor = System.Drawing.Color.Black;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Size = new System.Drawing.Size(185, 344);
            // 
            // lbNoMatches
            // 
            this.lbNoMatches.Location = new System.Drawing.Point(4, 2);
            // 
            // lbSearchList
            // 
            this.lbSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSearchList.Location = new System.Drawing.Point(0, 0);
            this.lbSearchList.Size = new System.Drawing.Size(185, 344);
            // 
            // ShipSelectControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Name = "ShipSelectControl1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Events
        
        #endregion
    }
}
