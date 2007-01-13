using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using EVEMon.Common;
using System.Drawing;

namespace EVEMon.SkillPlanner
{
    public partial class PlanOrderEditorControl : UserControl
    {
        private System.Drawing.Font m_plannedSkillFont;
        private System.Drawing.Font m_prerequisiteSkillFont;
        private System.Drawing.Color m_trainablePlanEntryColor;

        public PlanOrderEditorControl()
        {
            InitializeComponent();
            m_plannedSkillFont = new System.Drawing.Font(lvSkills.Font, System.Drawing.FontStyle.Bold);
            m_prerequisiteSkillFont = new System.Drawing.Font(lvSkills.Font, System.Drawing.FontStyle.Regular);
            m_trainablePlanEntryColor = SystemColors.GrayText;
        }

        private NewPlannerWindow m_plannerWindow;
        public NewPlannerWindow PlannerWindow
        {
            set { m_plannerWindow = value; }
        }

        private Plan m_plan;
        public Plan Plan
        {
            get { return m_plan; }
            set
            {
                if (m_plan != null)
                {
                    m_plan.Changed -= new EventHandler<EventArgs>(OnPlanChanged);
                    m_plan.GrandCharacterInfo.SkillChanged -= new SkillChangedHandler(OnSkillChanged);
                }
                m_plan = value;
                if (m_plan != null)
                {
                    m_plan.Changed += new EventHandler<EventArgs>(OnPlanChanged);
                    m_plan.GrandCharacterInfo.SkillChanged += new SkillChangedHandler(OnSkillChanged);
                }
                UpdateListColumns();
                OnPlanChanged(null, null);
            }
        }

        private void OnPlanChanged(object sender, EventArgs e)
        {
            tmrAutoRefresh.Enabled = false;
            if (m_plan == null)
                lvSkills.Items.Clear();
            else
                UpdateSkillList();
        }

        private void OnSkillChanged(object sender, SkillChangedEventArgs e)
        {
            UpdateListViewItems();
        }

        private void tmrAutoRefresh_Tick(object sender, EventArgs e)
        {
            UpdateListViewItems();
        }

        private void llSuggestionLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ColumnPreference pref = m_plan.ColumnPreference;
            using (PlanOrderEditorColumnSelectWindow f = new PlanOrderEditorColumnSelectWindow(pref))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    UpdateListColumns();
                    Program.Settings.Save();
                }
            }
        }

        #region Look and Feel
        private bool m_HighlightPrerequisites = false;
        public bool HighlightPrerequisites
        {
            get { return m_HighlightPrerequisites; }
            set { m_HighlightPrerequisites = value; UpdateSkillList(); }
        }

        private bool m_HighlightPlannedSkills = false;
        public bool HighlightPlannedSkills
        {
            get { return m_HighlightPlannedSkills; }
            set { m_HighlightPlannedSkills = value; UpdateSkillList(); }
        }

        private bool m_DimUntrainable = false;
        public bool DimUntrainable
        {
            get { return m_DimUntrainable; }
            set { m_DimUntrainable = value; UpdateSkillList(); }
        }

        private bool m_WorksafeMode = false;
        public bool WorksafeMode
        {
            get { return m_WorksafeMode; }
            set { m_WorksafeMode = value; UpdateSkillList(); }
        }
        #endregion Look and Feel

        #region Skill List View
        private void UpdateSkillList()
        {            
            lvSkills.BeginUpdate();
            try
            {
                lvSkills.Items.Clear();
                if (m_plan != null)
                {
                    foreach (Plan.Entry pe in m_plan.Entries)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Tag = pe;

                        // Highlight entries that were planned, if option enabled.
                        if (!m_WorksafeMode &&
                            m_HighlightPlannedSkills &&
                            pe.EntryType == Plan.Entry.Type.Planned)
                        {
                            lvi.Font = m_plannedSkillFont;
                        }
                        else
                        {
                            lvi.Font = m_prerequisiteSkillFont;
                        }

                        // Gray out entries that can not be trained immediately.
                        if (!pe.CanTrainNow && m_DimUntrainable)
                        {
                            lvi.ForeColor = m_trainablePlanEntryColor;
                        }

                        lvSkills.Items.Add(lvi);

                        Skill gs = pe.Skill;
                        if (gs.InTraining)
                        {
                            // This skill is currently in training so (re)start the auto refresh timer
                            tmrAutoRefresh.Enabled = true;
                        }
                    }
                    UpdateListViewItems();
                }
            }
            finally
            {
                lvSkills.EndUpdate();
            }
        }

        private const int MAX_NOTES_PREVIEW_CHARS = 60;
        private void UpdateListViewItems()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                                                  {
                                                      UpdateListViewItems();
                                                  }));
                return;
            }

            EveAttributeScratchpad scratchpad = new EveAttributeScratchpad();

            lvSkills.BeginUpdate();
            try
            {
                DateTime start = DateTime.Now;
                int skillPointTotal = m_plan.GrandCharacterInfo.SkillPointTotal;
                NumberFormatInfo nfi = NumberFormatInfo.CurrentInfo;

                for (int i = 0; i < lvSkills.Items.Count; i++)
                {
                    ListViewItem lvi = lvSkills.Items[i];
                    Plan.Entry pe = (Plan.Entry)lvi.Tag;
                    Skill gs = pe.Skill;

                    while (lvi.SubItems.Count < lvSkills.Columns.Count + 1)
                    {
                        lvi.SubItems.Add(String.Empty);
                    }

                    TimeSpan trainTime = gs.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad);
                    TimeSpan trainTimeNatural = gs.GetTrainingTimeOfLevelOnly(pe.Level, true, scratchpad,false);
                    int currentSP = gs.CurrentSkillPoints;
                    int reqBeforeThisLevel = gs.GetPointsRequiredForLevel(pe.Level - 1);
                    int reqToThisLevel = gs.GetPointsRequiredForLevel(pe.Level);
                    int pointsInThisLevel = currentSP - reqBeforeThisLevel;
                    if (pointsInThisLevel < 0)
                    {
                        pointsInThisLevel = 0;
                    }
                    double deltaPointsOfLevel = Convert.ToDouble(reqToThisLevel - reqBeforeThisLevel);
                    double pctComplete = pointsInThisLevel / deltaPointsOfLevel;

                    DateTime thisStart = start;
                    start += trainTime;
                    DateTime thisEnd = start;

                    skillPointTotal += (reqToThisLevel - reqBeforeThisLevel - pointsInThisLevel);

                    string planGroups;
                    if (pe.PlanGroups.Count == 0)
                    {
                        planGroups = "None";
                    }
                    else if (pe.PlanGroups.Count == 1)
                    {
                        planGroups = (string)pe.PlanGroups[0];
                    }
                    else
                    {
                        planGroups = "Multiple (" + pe.PlanGroups.Count + ")";
                    }

                    for (int x = 0; x < lvSkills.Columns.Count; x++)
                    {
                        ColumnPreference.ColumnType ct = (ColumnPreference.ColumnType)lvSkills.Columns[x].Tag;
                        string res = String.Empty;
                        switch (ct)
                        {
                            case ColumnPreference.ColumnType.SkillName:
                                res = gs.Name + " " + Skill.GetRomanForInt(pe.Level);
                                break;
                            case ColumnPreference.ColumnType.PlanGroup:
                                res = planGroups;
                                break;
                            case ColumnPreference.ColumnType.TrainingTime:
                                res =
                                    Skill.TimeSpanToDescriptiveText(trainTime, DescriptiveTextOptions.IncludeCommas);
                                break;
                            case ColumnPreference.ColumnType.TrainingTimeNatural:
                                res =
                                    Skill.TimeSpanToDescriptiveText(trainTimeNatural, DescriptiveTextOptions.IncludeCommas);
                                break;
                            case ColumnPreference.ColumnType.EarliestStart:
                                res = thisStart.ToString();
                                break;
                            case ColumnPreference.ColumnType.EarliestEnd:
                                res = thisEnd.ToString();
                                break;
                            case ColumnPreference.ColumnType.PercentComplete:
                                res = pctComplete.ToString("0%");
                                break;
                            case ColumnPreference.ColumnType.SkillRank:
                                res = gs.Rank.ToString();
                                break;
                            case ColumnPreference.ColumnType.PrimaryAttribute:
                                res = gs.PrimaryAttribute.ToString();
                                break;
                            case ColumnPreference.ColumnType.SecondaryAttribute:
                                res = gs.SecondaryAttribute.ToString();
                                break;
                            case ColumnPreference.ColumnType.SkillGroup:
                                res = gs.SkillGroup.Name;
                                break;
                            case ColumnPreference.ColumnType.Notes:
                                string xx;
                                if (String.IsNullOrEmpty(pe.Notes))
                                {
                                    res = String.Empty;
                                }
                                else
                                {
                                    xx = Regex.Replace(pe.Notes, @"(\r|\n)+", " ", RegexOptions.None);
                                    if (xx.Length <= MAX_NOTES_PREVIEW_CHARS)
                                    {
                                        res = xx;
                                    }
                                    else
                                    {
                                        res = xx.Substring(0, MAX_NOTES_PREVIEW_CHARS) + "...";
                                    }
                                }
                                break;
                            case ColumnPreference.ColumnType.PlanType:
                                res = pe.EntryType.ToString();
                                break;
                            case ColumnPreference.ColumnType.SPTotal:
                                res = skillPointTotal.ToString("N00", nfi);
                                break;
                        }
                        lvi.SubItems[x].Text = res;
                    }

                    lvi.SubItems[lvSkills.Columns.Count].Text = pe.EntryType.ToString();

                    scratchpad.ApplyALevelOf(gs);
                }
            }
            finally
            {
                lvSkills.EndUpdate();
            }
        }

        private void lvSkills_ListViewItemsDragged(object sender, EventArgs e)
        {
            RebuildPlanFromListViewOrder();
        }

        private void lvSkills_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            // Because this occurs before the reordering happens, we have to delay the
            // Order update a bit...
            ThreadPool.QueueUserWorkItem(delegate
                                             {
                                                 this.Invoke(new MethodInvoker(delegate
                                                                                   {
                                                                                       StringBuilder sb =
                                                                                           new StringBuilder();
                                                                                       SortedDictionary
                                                                                           <int,
                                                                                               ColumnPreference.
                                                                                                   ColumnType> order =
                                                                                                       new
                                                                                                           SortedDictionary
                                                                                                               <int,
                                                                                                                   ColumnPreference
                                                                                                                       .
                                                                                                                       ColumnType
                                                                                                                   >();
                                                                                       for (int i = 0;
                                                                                            i < lvSkills.Columns.Count;
                                                                                            i++)
                                                                                       {
                                                                                           ColumnHeader ch =
                                                                                               lvSkills.Columns[i];
                                                                                           order.Add(ch.DisplayIndex,
                                                                                                     (
                                                                                                     ColumnPreference.
                                                                                                         ColumnType)
                                                                                                     ch.Tag);
                                                                                       }
                                                                                       foreach (
                                                                                           KeyValuePair
                                                                                               <int,
                                                                                                   ColumnPreference.
                                                                                                       ColumnType> pair
                                                                                               in order)
                                                                                       {
                                                                                           if (sb.Length != 0)
                                                                                           {
                                                                                               sb.Append(',');
                                                                                           }
                                                                                           sb.Append(
                                                                                               pair.Value.ToString());
                                                                                       }

                                                                                       m_plan.ColumnPreference.Order =
                                                                                           sb.ToString();
                                                                                       Program.Settings.Save();
                                                                                   }));
                                             });
        }

        private void lvSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSkills.SelectedIndices.Count == 0)
            {
                tsbMoveUp.Enabled = false;
                tsbMoveDown.Enabled = false;
            }
            else
            {
                tsbMoveUp.Enabled = (lvSkills.SelectedIndices[0] != 0);
                tsbMoveDown.Enabled = (lvSkills.SelectedIndices[lvSkills.SelectedIndices.Count - 1] !=
                                       lvSkills.Items.Count - 1);
            }

            // Update the colour of all items

            foreach (ListViewItem current in lvSkills.Items)
            {
                bool isSameSkill = false;
                bool isPreRequisite = false;
                bool isPostRequisite = false;


                if (!m_WorksafeMode &&
                    m_HighlightPrerequisites &&
                    lvSkills.SelectedItems.Count == 1)
                {
                    Plan.Entry currentSkill = (Plan.Entry)current.Tag;
                    Plan.Entry selectedSkill = (Plan.Entry)lvSkills.SelectedItems[0].Tag;
                    // Single select so check for pre-requisite highlighting
                    int neededLevel;
                    if (currentSkill.Skill.HasAsPrerequisite(selectedSkill.Skill, out neededLevel, false))
                    {
                        if (currentSkill.Level == 1 && neededLevel >= selectedSkill.Level)
                            isPostRequisite = true;
                    }
                    if (selectedSkill.Skill.HasAsPrerequisite(currentSkill.Skill, out neededLevel, false))
                    {
                        if (currentSkill.Level == neededLevel)
                            isPreRequisite = true;
                    }
                    if (currentSkill.SkillName == selectedSkill.SkillName)
                    {
                        isSameSkill = true;
                    }
                }

                if (isSameSkill)
                {
                    //current.BackColor = Color.LightYellow;
                    current.StateImageIndex = 1;
                }
                else if (isPreRequisite)
                {
                    //current.BackColor = Color.LightPink;
                    current.StateImageIndex = 2;
                }
                else if (isPostRequisite)
                {
                    //current.BackColor = Color.LightGreen;
                    current.StateImageIndex = 0;
                }
                else
                {
                    //current.BackColor = lvSkills.BackColor;
                    current.StateImageIndex = -1;
                }
            }
            if (lvSkills.SelectedItems.Count > 1)
            {
                TimeSpan selectedTrainTime = TimeSpan.Zero;
                foreach (ListViewItem current in lvSkills.SelectedItems)
                {
                    Plan.Entry pe = (Plan.Entry)current.Tag;
                    Skill gs = pe.Skill;
                    selectedTrainTime += gs.GetTrainingTimeOfLevelOnly(pe.Level, true, null);
                }

                m_plannerWindow.UpdateStatusBarSelected(String.Format("{0} Skills selected, Training time: {1}",
                                            lvSkills.SelectedItems.Count,
                                            Skill.TimeSpanToDescriptiveText(selectedTrainTime,
                                                                                     DescriptiveTextOptions.FullText |
                                                                                     DescriptiveTextOptions.
                                                                                         IncludeCommas |
                                                                                     DescriptiveTextOptions.SpaceText)));
            }
            else
            {
                // reset the status bar to normal in case multiple items were previously selected
                m_plannerWindow.UpdateStatusBar();
            }
        }

        private void lvSkills_ItemHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            ListViewItem lvi = e.Item;
            if (null != lvi)
            {
                lvi.ToolTipText = GetPlanEntryForListViewItem(lvi).Skill.Description;
            }
        }

        private void RebuildPlanFromListViewOrder()
        {
            m_plan.SuppressEvents();
            try
            {
                m_plan.Entries.Clear();
                foreach (ListViewItem lvi in lvSkills.Items)
                {
                    Plan.Entry newPe = ((Plan.Entry)lvi.Tag).Clone() as Plan.Entry;
                    m_plan.Entries.Add(newPe);
                }
                // Enforces proper ordering too!
                m_plan.CheckForMissingPrerequisites();
            }
            finally
            {
                m_plan.ResumeEvents();
            }
        }

        public void UpdateListColumns()
        {
            if (m_plan != null)
            {
                lvSkills.BeginUpdate();
                try
                {
                    lvSkills.Columns.Clear();
                    List<ColumnPreference.ColumnType> alreadyAdded = new List<ColumnPreference.ColumnType>();

                    foreach (string ts in m_plan.ColumnPreference.Order.Split(','))
                    {
                        try
                        {
                            ColumnPreference.ColumnType ct = (ColumnPreference.ColumnType)Enum.Parse(
                                                                                               typeof(
                                                                                                   ColumnPreference.
                                                                                                   ColumnType), ts, true);
                            if (m_plan.ColumnPreference[ct] && !alreadyAdded.Contains(ct))
                            {
                                ColumnHeader ch = new ColumnHeader();
                                ColumnPreference.ColumnDisplayAttribute cda = ColumnPreference.GetAttribute(ct);
                                ch.Text = cda.Header;
                                ch.Tag = ct;
                                lvSkills.Columns.Add(ch);
                                alreadyAdded.Add(ct);
                            }
                        }
                        catch (Exception e)
                        {
                            ExceptionHandler.LogException(e, false);
                        }
                    }

                    for (int i = 0; i < ColumnPreference.ColumnCount; i++)
                    {
                        ColumnPreference.ColumnType ct = (ColumnPreference.ColumnType)i;
                        if (m_plan.ColumnPreference[i] && !alreadyAdded.Contains(ct))
                        {
                            ColumnHeader ch = new ColumnHeader();
                            ColumnPreference.ColumnDisplayAttribute cda = ColumnPreference.GetAttribute(ct);
                            ch.Text = cda.Header;
                            ch.Tag = ct;
                            lvSkills.Columns.Add(ch);
                            alreadyAdded.Add(ct);
                        }
                    }
                    //}
                    //finally
                    //{
                    //    lvSkills.EndUpdate();
                    //}
                    UpdateListViewItems();
                    //lvSkills.BeginUpdate();
                    //try
                    //{
                    for (int i = 0; i < lvSkills.Columns.Count; i++)
                    {
                        ColumnHeader ch = lvSkills.Columns[i];
                        ColumnPreference.ColumnDisplayAttribute cda =
                            ColumnPreference.GetAttribute((ColumnPreference.ColumnType)ch.Tag);
                        ch.Width = cda.Width;
                    }
                }
                finally
                {
                    lvSkills.EndUpdate();
                }
            }
        }
        #endregion Skill List View

        #region Context Menu
        private void cmsContextMenu_Opening(object sender, CancelEventArgs e)
        {
            miRemoveFromPlan.Enabled = (lvSkills.SelectedItems.Count == 1);
            miChangeNote.Enabled = (lvSkills.SelectedItems.Count == 1);
            miShowInSkillBrowser.Enabled = (lvSkills.SelectedItems.Count == 1);
            if (lvSkills.SelectedItems.Count == 1 &&
                GetPlanEntryForListViewItem(lvSkills.SelectedItems[0]).PlanGroups.Count > 0)
            {
                miPlanGroups.Enabled = true;
                miPlanGroups.DropDownItems.Clear();
                List<string> planGroups = new List<string>();
                foreach (string pg in GetPlanEntryForListViewItem(lvSkills.SelectedItems[0]).PlanGroups)
                {
                    planGroups.Add(pg);
                }
                planGroups.Sort();
                foreach (string pg in planGroups)
                {
                    ToolStripButton tsb = new ToolStripButton(pg);
                    tsb.Click += new EventHandler(tsb_Click);
                    miPlanGroups.DropDownItems.Add(tsb);
                }
            }
            else
            {
                miPlanGroups.Enabled = false;
            }
        }

        private void tsb_Click(object sender, EventArgs e)
        {
            string planGroup = ((ToolStripButton)sender).Text;
            foreach (ListViewItem lvi in lvSkills.Items)
            {
                lvi.Selected = GetPlanEntryForListViewItem(lvi).PlanGroups.Contains(planGroup);
            }
        }

        private void miShowInSkillBrowser_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvSkills.SelectedItems[0];
            Plan.Entry pe = (Plan.Entry)lvi.Tag;
            Skill gs = pe.Skill;
            m_plannerWindow.ShowSkillInTree(gs);
        }

        private void miRemoveFromPlan_Click(object sender, EventArgs e)
        {
            if (lvSkills.SelectedItems.Count != 1)
            {
                return;
            }

            using (CancelChoiceWindow f = new CancelChoiceWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                if (dr == DialogResult.Yes)
                {
                    RemoveFromPlan(GetPlanEntryForListViewItem(lvSkills.SelectedItems[0]), true);
                }
                if (dr == DialogResult.No)
                {
                    RemoveFromPlan(GetPlanEntryForListViewItem(lvSkills.SelectedItems[0]), false);
                }
            }
        }

        private void miChangeNote_Click(object sender, EventArgs e)
        {
            if (lvSkills.SelectedItems.Count < 0)
            {
                return;
            }
            ListViewItem lvi = lvSkills.SelectedItems[0];
            if (lvi == null)
            {
                return;
            }
            Plan.Entry pe = lvi.Tag as Plan.Entry;
            if (pe == null)
            {
                return;
            }
            string sn = pe.SkillName + " " + Skill.GetRomanForInt(pe.Level);
            using (EditEntryNoteWindow f = new EditEntryNoteWindow(sn))
            {
                f.NoteText = pe.Notes;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                pe.Notes = f.NoteText;
                UpdateListViewItems();
                Program.Settings.Save();
            }
        }
        #endregion Context Menu

        #region Plan Re-Ordering
        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            Dictionary<string, bool> seld = new Dictionary<string, bool>();
            lvSkills.BeginUpdate();
            try
            {
                // Store the current selection so that we can restore it later 
                // (must be done by skill name as PlanChanged() removes and Re-adds the skills)
                List<int> sel = new List<int>();
                foreach (int si in lvSkills.SelectedIndices)
                {
                    ListViewItem lvi = lvSkills.Items[si];
                    Plan.Entry pe = (Plan.Entry)lvi.Tag;
                    sel.Add(si);
                    seld[pe.SkillName + " " + pe.Level.ToString()] = true;
                }
                for (int i = 0; i < lvSkills.Items.Count; i++)
                {
                    if (sel.Contains(i + 1))
                    {
                        ListViewItem lvix = lvSkills.Items[i + 1];
                        // Must remove any icon or we get an exception when we remove it
                        lvix.StateImageIndex = -1;
                        lvSkills.Items.RemoveAt(i + 1);
                        lvSkills.Items.Insert(i, lvix);
                    }
                }
                RebuildPlanFromListViewOrder();
            }
            finally
            {
                // Now reselect the skills that we had selected before
                // the selection is lost in the call to PlanChanged()
                foreach (ListViewItem lvi in lvSkills.Items)
                {
                    Plan.Entry pe = (Plan.Entry)lvi.Tag;
                    string k = pe.SkillName + " " + pe.Level.ToString();
                    if (seld.ContainsKey(k))
                    {
                        lvi.Selected = true;
                    }
                    else
                    {
                        lvi.Selected = false;
                    }
                }
                lvSkills.EndUpdate();
            }
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            Dictionary<string, bool> seld = new Dictionary<string, bool>();
            lvSkills.BeginUpdate();
            try
            {
                // Store the current selection so that we can restore it later 
                // (must be done by skill name as PlanChanged() removes and Re-adds the skills)
                List<int> sel = new List<int>();
                foreach (int si in lvSkills.SelectedIndices)
                {
                    ListViewItem lvi = lvSkills.Items[si];
                    Plan.Entry pe = (Plan.Entry)lvi.Tag;
                    sel.Add(si);
                    seld[pe.SkillName + " " + pe.Level.ToString()] = true;
                }
                for (int i = lvSkills.Items.Count - 1; i >= 0; i--)
                {
                    if (sel.Contains(i - 1))
                    {
                        ListViewItem lvix = lvSkills.Items[i - 1];
                        // Must remove any icon or we get an exception when we remove it
                        lvix.StateImageIndex = -1;
                        lvSkills.Items.RemoveAt(i - 1);
                        lvSkills.Items.Insert(i, lvix);
                    }
                }
                RebuildPlanFromListViewOrder();
            }
            finally
            {
                // Now reselect the skills that we had selected before
                // the selection is lost in the call to PlanChanged()
                foreach (ListViewItem lvi in lvSkills.Items)
                {
                    Plan.Entry pe = (Plan.Entry)lvi.Tag;
                    string k = pe.SkillName + " " + pe.Level.ToString();
                    if (seld.ContainsKey(k))
                    {
                        lvi.Selected = true;
                    }
                    else
                    {
                        lvi.Selected = false;
                    }
                }
                lvSkills.EndUpdate();
            }
        }

        private void tsbSort_Click(object sender, EventArgs e)
        {
            using (PlanSortWindow f = new PlanSortWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    PlanSorter.SortPlan(m_plan, f.SortType, f.LearningFirst);
                    tsbMoveDown.Enabled = false;
                    tsbMoveUp.Enabled = false;
                }
            }
        }
        #endregion Plan Re-Ordering

        private Plan.Entry GetPlanEntryForListViewItem(ListViewItem lvi)
        {
            if (lvi == null)
            {
                return null;
            }
            return lvi.Tag as Plan.Entry;
        }

        private void RemoveFromPlan(Plan.Entry pe, bool includePrerequisites)
        {
            bool result = m_plan.RemoveEntry(pe.Skill, includePrerequisites, false);
            if (!result)
            {
                MessageBox.Show(this,
                                "The plan for this skill could not be cancelled because this skill is " +
                                "required for another skill you have planned.",
                                "Skill Needed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
