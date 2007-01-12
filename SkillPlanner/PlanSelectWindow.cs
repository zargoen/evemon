using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml.Serialization;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class PlanSelectWindow : EVEMonForm
    {
        public PlanSelectWindow()
        {
            InitializeComponent();
        }

        public PlanSelectWindow(Settings s, CharacterInfo gci)
            : this()
        {
            m_settings = s;
            m_grandCharacterInfo = gci;
            m_charKey = m_grandCharacterInfo.Name;
        }

        private Settings m_settings;
        private CharacterInfo m_grandCharacterInfo;
        private string m_charKey;

        public string CharKey
        {
            get { return m_charKey; }
            set { m_charKey = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PlanSelectWindow_Load(object sender, EventArgs e)
        {
            PopulatePlanList();
        }

        private void PopulatePlanList()
        {
            lbPlanList.BeginUpdate();
            try
            {
                lbPlanList.Items.Clear();
                lbPlanList.Items.Add("<New Plan>");

                foreach (string planName in m_settings.GetPlansForCharacter(m_charKey))
                {
					Plan tmp_plan = m_settings.GetPlanByName(m_charKey, planName);
                    if (tmp_plan.GrandCharacterInfo == null)
                    {
                        tmp_plan.GrandCharacterInfo = m_grandCharacterInfo;
                    }
					TimeSpan ts_plan = tmp_plan.GetTotalTime(null);
					ListViewItem lvi = new ListViewItem(planName);
					lvi.Text = planName;
					lvi.SubItems.Add(Skill.TimeSpanToDescriptiveText(	ts_plan,
                                                                            DescriptiveTextOptions.FullText |
                                                                            DescriptiveTextOptions.IncludeCommas |
                                                                            DescriptiveTextOptions.SpaceText));
					lvi.SubItems.Add(tmp_plan.UniqueSkillCount.ToString());
					lbPlanList.Items.Add(lvi);
                }
            }
            finally
            {
                lbPlanList.EndUpdate();
            }
        }

        private void lbPlanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If we have selected the <New Plan> (index 0) and any others then make it so we've
            // selected only <New Plan>
            if (lbPlanList.SelectedIndices.Contains(0) && lbPlanList.SelectedIndices.Count > 1)
            {
                lbPlanList.SelectedIndices.Clear();
                lbPlanList.Items[0].Selected = true;
            }

            // If we have 1 and only 1 plan selected, and it isn't the <New Plan> we 
            // can move it, rename it or delete it.
            if (lbPlanList.SelectedItems.Count == 1 && !lbPlanList.SelectedIndices.Contains(0))
            {
                int idx = lbPlanList.SelectedIndices[0];
                tsbMoveUp.Enabled = (idx > 1);
                tsbMoveDown.Enabled = (idx < lbPlanList.Items.Count - 1);
                tsbRenamePlan.Enabled = true;
				tsbDeletePlan.Enabled = true;
            } else {
                tsbMoveUp.Enabled = false;
                tsbMoveDown.Enabled = false;
                tsbRenamePlan.Enabled = false;
                tsbDeletePlan.Enabled = false;
            }

            // The Open button in enabled if there is at least 1 selected item
            // and the Open button is called "Merge" if more than 1 plan is selected.
            btnOpen.Enabled = (lbPlanList.SelectedItems.Count > 0);
            btnOpen.Text = (lbPlanList.SelectedItems.Count > 1 ? "Merge" : "Open");
        }


        private Plan m_result;

        public Plan ResultPlan
        {
            get { return m_result; }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (lbPlanList.SelectedItems.Count > 1)
            {
                m_result = new Plan();

                foreach (ListViewItem plan in lbPlanList.SelectedItems)
                {
                    string s = (string) plan.Text;
                    Plan p = m_settings.GetPlanByName(m_charKey, s);
                    foreach (Plan.Entry entry in p.Entries)
                    {
                        Plan.Entry entryInMergedPlan = m_result.GetEntry(entry.SkillName, entry.Level);
                        if (m_result.GetEntry(entry.SkillName, entry.Level) == null)
                        {
                            Plan.Entry entryClone = (Plan.Entry)entry.Clone();
                            entryClone.PlanGroups.Add(p.Name);
                            m_result.Entries.Add(entryClone);
                        }
                        else
                        {
                            entryInMergedPlan.PlanGroups.Add(p.Name);
                            foreach (string subplanName in entry.PlanGroups)
                            {
                                if (!entryInMergedPlan.PlanGroups.Contains(subplanName))
                                {
                                    entryInMergedPlan.PlanGroups.Add(subplanName);
                                }
                            }
                        }
                    }
                }
            }
            else if (lbPlanList.SelectedIndices[0] == 0)
            {
                m_result = null;
            }
            else
            {
                string s = (string) lbPlanList.SelectedItems[0].Text;
                m_result = m_settings.GetPlanByName(m_charKey, s);
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbPlanList_DoubleClick(object sender, EventArgs e)
        {
            if (lbPlanList.SelectedItems.Count > 0)
            {
                btnOpen_Click(this, new EventArgs());
            }
        }

        private void LoadPlan()
        {
            DialogResult dr = ofdOpenDialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                Plan loadedPlan = null;
                using (Stream s = new FileStream(ofdOpenDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xs = new XmlSerializer(typeof (Plan));
                    try
                    {
                        loadedPlan = (Plan) xs.Deserialize(s);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.LogException(ex, true);

                        s.Seek(0, SeekOrigin.Begin);
                        using (GZipStream gzs = new GZipStream(s, CompressionMode.Decompress))
                        {
                            try
                            {
                                loadedPlan = (Plan) xs.Deserialize(gzs);
                            }
                            catch (Exception e)
                            {
                                ExceptionHandler.LogException(e, true);
                                throw new ApplicationException("Could not determine input file format.");
                            }
                        }
                    }
                }

                using (NewPlanWindow npw = new NewPlanWindow())
                {
                    string oldPlanName = "";
                    string newPlanName = "";
                    while (newPlanName == "")
                    {
                        npw.Text = "Load Plan";
                        npw.Result = Path.GetFileNameWithoutExtension(ofdOpenDialog.FileName);
                        DialogResult xdr = npw.ShowDialog();
                        if (xdr == DialogResult.Cancel)
                        {
                            return;
                        }
                        string planName = npw.Result;

                        Plan oldPlan = m_settings.GetPlanByName(m_charKey, planName);
                        if (oldPlan == null)
                        {
                            // No plan of the same name, so no replacement necessary
                            oldPlanName = "";
                            newPlanName = planName;
                        }
                        else
                        {
                            // Should we try replacing the original plan?
                            string message = "Plan with name '" + planName + "' already exists. Replace plan '" +
                                             planName + "'?";
                            string caption = "Replace Plan '" + planName + "'";
                            DialogResult result;

                            // Display the MessageBox.
                            result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                // Rename the original plan for removal once we've loaded the new one
                                oldPlanName = planName + "_BACKUP";
                                m_settings.RenamePlanFor(m_charKey, planName, oldPlanName);
                                newPlanName = planName;
                            }
                            else
                            {
                                // User does not want to replace original plan, so get them to choose a new name
                                oldPlanName = "";
                                newPlanName = "";
                            }
                        }
                    }

                    // Now we have a valid name for the new plan, and potentially an old plan to be removed.
                    try
                    {
                        m_settings.AddPlanFor(m_charKey, loadedPlan, newPlanName);
                        
                    }
                    catch (ApplicationException err)
                    {
                        ExceptionHandler.LogException(err, true);
                        // Rename the old plan to it's original name
                        if (oldPlanName != "")
                        {
                            m_settings.RenamePlanFor(m_charKey, oldPlanName, newPlanName);
                        }

                        MessageBox.Show("Could not add the plan:\n" + err.Message,
                                        "Could Not Add Plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // We have successfully loaded the plan so remove the old one for good if needed
                    if (oldPlanName != "")
                    {
                        m_settings.RemovePlanFor(m_charKey, oldPlanName);
                    }
                }
            }
            catch (Exception err)
            {
                ExceptionHandler.LogException(err, true);
                MessageBox.Show("There was an error loading the saved plan:\n" + err.Message,
                                "Could Not Load Plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopulatePlanList();
        }

        private void tsbLoadPlan_Click(object sender, EventArgs e)
        {
            LoadPlan();
        }

        private void tsbRenamePlan_Click(object sender, EventArgs e)
        {
            using (NewPlanWindow f = new NewPlanWindow())
            {
                f.Text = "Rename Plan";
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                string oldName = (string) lbPlanList.SelectedItems[0].Text;
                string newName = f.Result;
                if (oldName == newName)
                {
                    return;
                }
                if (m_settings.GetPlanByName(m_charKey, newName) != null)
                {
                    MessageBox.Show("A plan with that name already exists.",
                                    "Duplicate Plan Name", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                m_settings.RenamePlanFor(m_charKey,
                                         oldName, f.Result);
                lbPlanList.SelectedItems.Clear();
                PopulatePlanList();
            }
        }

        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            int idx = lbPlanList.SelectedIndices[0];
            List<string> newOrder = new List<string>();
            for (int i = 1; i < lbPlanList.Items.Count; i++)
            {
                if (i == idx - 1)
                {
                    newOrder.Add((string) lbPlanList.SelectedItems[0].Text);
                }
                if (i != idx)
                {
                    newOrder.Add((string) lbPlanList.Items[i].Text);
                }
            }
            FinalizePlanReorder(idx - 1, newOrder);
        }

        private void FinalizePlanReorder(int idx, List<string> newOrder)
        {
            m_settings.RearrangePlansFor(m_charKey, newOrder);

            lbPlanList.BeginUpdate();
            try
            {
                PopulatePlanList();
                lbPlanList.Items[idx].Selected = true;
            }
            finally
            {
                lbPlanList.EndUpdate();
            }
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            int idx = lbPlanList.SelectedIndices[0];
            List<string> newOrder = new List<string>();
            for (int i = 1; i < lbPlanList.Items.Count; i++)
            {
                if (i != idx)
                {
                    newOrder.Add((string) lbPlanList.Items[i].Text);
                }
                if (i == idx + 1)
                {
                    newOrder.Add((string) lbPlanList.SelectedItems[0].Text);
                }
            }
            FinalizePlanReorder(idx + 1, newOrder);
        }

        private void tsbDeletePlan_Click(object sender, EventArgs e)
        {
            string planName = (string) lbPlanList.SelectedItems[0].Text;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete \"" + planName +
                                              "\"?", "Delete Plan", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            m_settings.RemovePlanFor(m_charKey, planName);
            lbPlanList.SelectedItems.Clear();
            PopulatePlanList();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}
    }
}
