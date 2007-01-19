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
            m_grandCharacterInfo = gci;
            m_charKey = m_grandCharacterInfo.Name;
            m_implants = Slot.GetImplants();
            // m_input and m_workingSets are NOT the same,
            // if you simply use the = operator to copy them,
            // then you actually end up with two references to the same object,
            // which causes some really daft errors.
            m_input = m_grandCharacterInfo.implantSets;
            foreach (string s in m_input.Keys)
            {
                m_workingSets.Add(s, new ImplantSet(m_input[s].Array));
            }
        }

        private CharacterInfo m_grandCharacterInfo;
        private string m_charKey;

        // This is the main collection of Implants you can chose from
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
            int JumpClones = m_grandCharacterInfo.GetSkill("Infomorph Psychology").Level;
            /// Total number of implant sets should be:
            ///    1 for the implants generated in EVEMon from the CCP supplied XML
            ///  + 1 for the current head full according to EVEmon
            ///  + the number equivalent to the level of "Infomorph Psychology"
            JumpCloneTxt.Text = String.Format("{0} has the skill for {1} Jump Clones\n(plus 1 for the implants in your active body)",
                                      m_charKey, JumpClones.ToString());
            lbJumpClone.Items.Add("Auto");
            lbJumpClone.Items.Add("Current");
            for (int i = 1; i <= JumpClones; i++)
            {
                lbJumpClone.Items.Add("Clone " + i);
            }

            lbJumpClone.SelectedIndex = 1;

            Buildtxt();
            panel1.Refresh();
        }

        private void Buildtxt()
        {
            /*if (m_implants != null && tvlist.Nodes.Count == 0)
            {
                SortedList<int, Slot> Slots = new SortedList<int, Slot>();
                foreach (Slot i in m_implants)
                {
                    if (!Slots.ContainsKey(i.Number))
                    {
                        Slots.Add(i.Number, i);
                    }
                }
                TreeNode root = new TreeNode("Implants");
                foreach (KeyValuePair<int, Slot> kvp in Slots)
                {
                    TreeNode tvn = new TreeNode();
                    tvn.Text = "Slot " + kvp.Key.ToString();
                    //System.Windows.Forms.ComboBox x = (System.Windows.Forms.ComboBox)this.panel1.Controls["txtImplant" + kvp.Key];
                    SortedList<string, Implant> implants = new SortedList<string, Implant>();
                    foreach (Implant i in kvp.Value.ImplantList)
                    {
                        implants[i.Name] = i;
                    }
                    //x.Items.Add("<None>");
                    foreach (Implant i in implants.Values)
                    {
                        tvn.Nodes.Add(i.Name);
                        //x.Items.Add(i);
                    }
                    //x.SelectedIndex = 0;
                    root.Nodes.Add(tvn);
                }
                tvlist.Nodes.Add(root);
            }*/

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
    }
}