using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class EveObjectSelectControl : UserControl
    {
        public EveObjectSelectControl()
        {
            InitializeComponent();
        }

        protected Settings m_settings;
        protected Plan m_plan;
        public Plan Plan
        {
            get { return m_plan; }
            set { m_plan = value; }
        }

        protected virtual void EveObjectSelectControl_Load(object sender, EventArgs e)
        {
            m_filterDelegate = SelectAll;
            if (this.DesignMode)
            {
                return;
            }

            try
            {
                m_settings = Settings.GetInstance();
                //cbSkillFilter.SelectedIndex = 0;
                lbSearchTextHint.Visible = String.IsNullOrEmpty(tbSearchText.Text);
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
        protected delegate bool EveObjectFilter(EveObject eo);
        protected EveObjectFilter m_filterDelegate;
        #endregion

        #region delegate functions

        /// <summary>
        /// Filter for all items
        /// </summary>
        /// <param name="eo"></param>
        /// <returns></returns>
        protected bool SelectAll(EveObject eo) { return true; }
        
        /// <summary>
        /// Filter for items that can be used (prereqs met)
        /// </summary>
        /// <param name="eo"></param>
        /// <returns></returns>
        protected bool CanUse(EveObject eo)
        {
             Skill skill = null;
             for (int i = 0; i < eo.RequiredSkills.Count; i++)
             {
                 try
                 {
                     skill = m_plan.GrandCharacterInfo.GetSkill(eo.RequiredSkills[i].Name);
                     if (skill.Level < eo.RequiredSkills[i].Level) return false;
                 }
                 catch
                 {
                     // unknown or no skill - assume we can use it
                     return true;
                 }
             }
             return true;
         }

        protected bool CannotUse(EveObject eo)
        {
            return !CanUse(eo);
        }
        #endregion

        #region Search
        protected void lbSearchTextHint_Click(object sender, EventArgs e)
        {
            tbSearchText.Focus();
        }

        protected void tbSearchText_Enter(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = false;
        }

        protected void tbSearchText_Leave(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = String.IsNullOrEmpty(tbSearchText.Text);
        }

        protected virtual void tbSearchText_TextChanged(object sender, EventArgs e)
        {
            SearchTextChanged();
        }

        protected void SearchTextChanged()
        {
            string searchText = tbSearchText.Text.Trim().ToLower();
           
            if (String.IsNullOrEmpty(searchText))
            {
                tvItems.Visible = true;
                lbSearchList.Visible = false;
                lbNoMatches.Visible = false;
                return;
            }
            // find everything in the current tree that matches the search string
            SortedList<string, EveObject> filteredItems = new SortedList<string, EveObject>();
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
                    foreach (EveObject eo in filteredItems.Values)
                    {   
                        lbSearchList.Items.Add(eo);
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

        private void SearchNode(TreeNode tn, string searchText, SortedList<string, EveObject> filteredItems)
        {
            EveObject itm = tn.Tag as EveObject;
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
                SetSelectedObject(tvItems.SelectedNode.Tag as EveObject);
            }
            else
            {
                SetSelectedObject(null);
            }
        }

        protected EveObject m_selectedObject = null;

        public EveObject SelectedObject
        {
            get { return m_selectedObject; }
            set { m_selectedObject = value; }
        }

        public event EventHandler<EventArgs> SelectedObjectChanged;

        protected void SetSelectedObject(EveObject s)
        {
            m_selectedObject = s;
            if (SelectedObjectChanged != null)
            {
                SelectedObjectChanged(this, new EventArgs());
            }
        }
        
        private void lbSearchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedObject(lbSearchList.SelectedItem as EveObject);
        }
        #endregion

    }
}
