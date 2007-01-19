using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;

/// <summary>
/// This is the implant groups UI
/// </summary>
namespace EVEMon.ImpGroups
{
    public partial class ImpGroups : EVEMonForm
    {
        public ImpGroups()
        {
            InitializeComponent();
        }

        public ImpGroups(CharacterInfo gci)
            : this()
        {
            MaxJumpClones = gci.GetSkill("Infomorph Psychology").Level;
            Char_name = gci.Name;
            m_implants = Slot.GetImplants();
            // m_input and m_workingSets are NOT the same,
            // if you simply use the = operator to copy them,
            // then you actually end up with two references to the same object,
            // which causes some really daft errors.
            m_input = gci.implantSets;
            foreach (string s in m_input.Keys)
            {
                m_workingSets.Add(s, new ImplantSet(m_input[s].Array));
            }
        }

        // This is the main collection of Implants you can chose from
        private string Char_name;
        private int MaxJumpClones;
        private Slot[] m_implants;

        // Here is the result set you return
        private Dictionary<string, ImplantSet> m_resultSets = null;

        public Dictionary<string, ImplantSet> ResultBonuses
        {
            get { return m_resultSets; }
        }

        private Dictionary<string, ImplantSet> m_input;
        // Here is the local store for the implants you are changing
        private Dictionary<string, ImplantSet> m_workingSets = new Dictionary<string, ImplantSet>();

        private void ImpGroups_Load(object sender, EventArgs e)
        {
            /// Total number of implant sets should be:
            ///    1 for the implants generated in EVEMon from the CCP supplied XML
            ///  + 1 for the current head full according to EVEmon
            ///  + the number equivalent to the level of "Infomorph Psychology"
            JumpCloneTxt.Text = String.Format("{0} has the skill for {1:D} Jump Clones\n(plus 1 for the implants in your active body)",
                          Char_name, MaxJumpClones);
            lbJumpClone.Items.Add("Auto");
            lbJumpClone.Items.Add("Current");
            for (int i = 1; i <= MaxJumpClones; i++)
            {
                lbJumpClone.Items.Add("Clone " + i);
            }

            lbJumpClone.SelectedIndex = 1;

            Buildtxt();
            panel1.Refresh();
        }

        private void Buildtxt()
        {
            for (int i = 1; i <= 10; i++)
            {
                System.Windows.Forms.TextBox x = (System.Windows.Forms.TextBox)this.panel1.Controls["txtImplant" + i];

                if (m_workingSets.ContainsKey(lbJumpClone.SelectedItem.ToString()))
                {
                    if (lbJumpClone.SelectedItem.ToString() == "Current")
                    {
                        if (i <= 5)
                        {
                            UserImplant a = m_workingSets["Current"][i - 1];
                            if (a != null)
                                x.Text = a.Name;
                            else
                            {
                                UserImplant s;
                                if (m_workingSets.ContainsKey("Auto"))
                                    s = m_workingSets["Auto"][i - 1];
                                else
                                    s = null;
                                UserImplant t;
                                if (m_input.ContainsKey("Current"))
                                    t = m_input["Current"][i - 1];
                                else
                                    t = null;
                                if (t == null)
                                {
                                    if (s != null)
                                        x.Text = s.Name;
                                    else
                                        x.Text = "";
                                }
                                else
                                    x.Text = "";
                            }
                        }
                        else
                        {
                            UserImplant a = m_workingSets["Current"][i - 1];
                            if (a != null)
                                x.Text = a.Name;
                            else
                                x.Text = "";
                        }
                    }
                    else
                    {
                        // Set the text to show the selected implants
                        UserImplant a = m_workingSets[lbJumpClone.SelectedItem.ToString()][i - 1];
                        if (a != null)
                            x.Text = a.Name;
                        else
                            x.Text = "";
                    }
                }
                else
                    x.Text = "";

                if (x.Text == "" || x.Text == "<None>")
                    x.Text = "<None>";
                else
                {
                    bool found = false;
                    foreach (Implant z in m_implants[i - 1].ImplantList)
                    {
                        if (z == null)
                            continue;
                        if (z.Name == x.Text)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        x.Text = "<Invalid Implant name, please update>";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_resultSets = new Dictionary<string, ImplantSet>();
            foreach (string x in m_workingSets.Keys)
            {
                m_resultSets.Add(x, new ImplantSet(m_workingSets[x].Array));
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbJumpClone_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSwapWithCurrent.Enabled = ((lbJumpClone.SelectedItem.ToString() != "Auto") && (lbJumpClone.SelectedItem.ToString() != "Current"));
            btnDeleteCurrent.Enabled = (lbJumpClone.SelectedItem.ToString() == "Current");
            Buildtxt();
            panel1.Refresh();
        }

        private void Get_Implant(object sender, EventArgs e)
        {
            int Slot = System.Convert.ToInt32(((System.Windows.Forms.Control)(sender)).Name.Replace("btnSlot", ""));
            // This is where we need to get the details for a new Implant.
            if (!m_workingSets.ContainsKey(lbJumpClone.SelectedItem.ToString()))
            {
                m_workingSets[lbJumpClone.SelectedItem.ToString()] = new ImplantSet();
            }
            UserImplant x = m_workingSets[lbJumpClone.SelectedItem.ToString()][Slot - 1];
            if (x == null)
                x = new UserImplant(Slot, new Implant(), true);
            using (ManualImplantDetailWindow f = new ManualImplantDetailWindow(x))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (lbJumpClone.SelectedItem.ToString() != "Auto")
                    {
                        m_workingSets[lbJumpClone.SelectedItem.ToString()][Slot - 1] = f.ResultBonus;
                    }
                    else
                    { // Give the user a message to tell them that they can't modify the implants dl'd from CCP "Auto", only overload them in a set such as "Current"
                        MessageBox.Show("The \"Auto\" Jump Clone set is the CCP XML implants\nThis set cannot be edited!", "Attempted to edit \"Auto\" Set!", MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                    }
                }
            }

            // if there are no implants in the group then delete the group
            // adding an implant in will re-add the group in the code above.
            bool delete = true;
            foreach (UserImplant temp in m_workingSets[lbJumpClone.SelectedItem.ToString()].Array)
            {
                if (temp != null)
                    delete = false;
            }
            if (delete)
                m_workingSets.Remove(lbJumpClone.SelectedItem.ToString());

            Buildtxt();
            panel1.Refresh();
        }

        private void btnDeleteCurrent_Click(object sender, EventArgs e)
        {
            if (m_workingSets.ContainsKey("Current"))
            {
                m_workingSets.Remove("Current");
            }
            Buildtxt();
            panel1.Refresh();
        }

        private void btnSwapWithCurrent_Click(object sender, EventArgs e)
        {
            ImplantSet cur = null;
            ImplantSet b = null;
            string name_to_swap = lbJumpClone.SelectedItem.ToString();
            if (m_workingSets.ContainsKey("Current"))
                cur = m_workingSets["Current"];
            if (m_workingSets.ContainsKey(name_to_swap))
            {
                b = m_workingSets[name_to_swap];
            }
            if (cur != null)
            {
                if (b != null)
                {
                    m_workingSets["Current"] = new ImplantSet(b.Array);
                    m_workingSets[name_to_swap] = new ImplantSet(cur.Array);
                }
                else
                {
                    m_workingSets.Remove("Current");
                    m_workingSets.Add(name_to_swap, new ImplantSet(cur.Array));
                }
            }
            else
            {
                if (b != null)
                {
                    m_workingSets.Add("Current", new ImplantSet(b.Array));
                    m_workingSets.Remove(name_to_swap);
                }
            }
            Buildtxt();
            panel1.Refresh();
        }

        private void btnSwapWithCurrent_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnSwapWithCurrent, "Weeee, Jumpy Jumpy!");
            toolTip1.IsBalloon = false;
            toolTip1.Active = true;
        }

        private void btnDeleteCurrent_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnDeleteCurrent, "Arrrgg, podded Again! :(");
            toolTip1.IsBalloon = false;
            toolTip1.Active = true;
        }
    }
}