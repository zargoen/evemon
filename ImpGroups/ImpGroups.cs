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
/// Still to do:
///  * pass to this class and then load 'm_implantSets' info.
///  * return a 'Dictionary<int, ImplantSet>' value on hitting ok
///     * following on from that last one the passed back value needs applying to the characters stored variable
///     * also, if the user hits cancel, anything done in this window should have no effect on the characters data.
///  * design and implement the UI for entering a new implant into an implant group
///  * finish implementing class ImplantSet so that it contains all the needed info to do this properly
///  * update class 'SerializableImplantSet' so it saves all of the changes to class 'ImplantSet'
/// </summary>

namespace EVEMon.ImpGroups
{
    public partial class ImpGroups : EVEMonForm
    {
        public ImpGroups()
        {
            InitializeComponent();
        }

        public ImpGroups(Settings s, CharacterInfo gci)
            : this()
        {
            m_settings = s;
            m_grandCharacterInfo = gci;
            m_charKey = m_grandCharacterInfo.Name;
        }

        private Settings m_settings;
        private CharacterInfo m_grandCharacterInfo;
        private string m_charKey;

        private void ImpGroups_Load(object sender, EventArgs e)
        {
            int JumpClones = m_grandCharacterInfo.GetSkill("Infomorph Psychology").Level;
            JumpCloneTxt.Text = String.Format("{0} has the skill for {1} Jump Clones\n(plus 1 for the implants in your active body)",
                                      m_charKey, JumpClones.ToString());
            lbJumpClone.Items.Add("Current");
            for (int i = 0; i < JumpClones; i++)
            {
                lbJumpClone.Items.Add("Clone " + i);
            }
            lbJumpClone.SelectedIndex = 0;
        }

        private void Buildtxt()
        {
/*            SortedList<string, Slot> Slots = new SortedList<string, Slot>();
            foreach (Slot i in m_implants)
            {
                if (!Slots.ContainsKey(i.Number.ToString()))
                {
                    Slots.Add(i.Number.ToString(), i);
                }
            }

            foreach (KeyValuePair<string, Slot> kvp in Slots)
            {
                TreeNode tvn = new TreeNode();
                tvn.Text = "Slot " + kvp.Key;
                System.Windows.Forms.ComboBox x = (System.Windows.Forms.ComboBox)this.panel1.Controls["txtImplant" + kvp.Key];
                SortedList<string, Implant> implants = new SortedList<string, Implant>();
                foreach (Implant i in kvp.Value.ImplantList)
                {
                    implants[i.Name] = i;
                }
                x.Items.Add("<None>");
                foreach (Implant i in implants.Values)
                {
                    x.Items.Add(i);
                }
                x.SelectedIndex = 0;
            }*/
            for (int i = 1; i <= 10; i++)
            {
                System.Windows.Forms.TextBox x = (System.Windows.Forms.TextBox)this.panel1.Controls["txtImplant" + i];
                if (m_grandCharacterInfo.implantSets.Count != 0 && m_grandCharacterInfo.implantSets.ContainsKey(lbJumpClone.SelectedIndex))
                {
                    // Set the text to show the selected implants
                    x.Text = m_grandCharacterInfo.implantSets[lbJumpClone.SelectedIndex][i-1];
                }
                else if (m_grandCharacterInfo.implantSets.Count == 0 && lbJumpClone.SelectedIndex == 0 && i <= 5)
                {
                    // if we don't have anything set here, then we need to check the old type
                    switch (i)
                    {
                        case 1:
                            x.Text = m_grandCharacterInfo.getImplantName(EveAttribute.Perception);
                            break;
                        case 2:
                            x.Text = m_grandCharacterInfo.getImplantName(EveAttribute.Memory);
                            break;
                        case 3:
                            x.Text = m_grandCharacterInfo.getImplantName(EveAttribute.Willpower);
                            break;
                        case 4:
                            x.Text = m_grandCharacterInfo.getImplantName(EveAttribute.Intelligence);
                            break;
                        case 5:
                            x.Text = m_grandCharacterInfo.getImplantName(EveAttribute.Charisma);
                            break;
                        default:
                            x.Text = "???";
                            break;
                    }
                    if (x.Text == "")
                        x.Text = "<None>";
                }
                else
                    x.Text = "<None>";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbJumpClone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Buildtxt();
            panel1.Refresh();
        }

        private void btnSlot1_Click(object sender, EventArgs e)
        {
            Get_Implant(1);
        }

        private void btnSlot2_Click(object sender, EventArgs e)
        {
            Get_Implant(2);
        }

        private void btnSlot3_Click(object sender, EventArgs e)
        {
            Get_Implant(3);
        }

        private void btnSlot4_Click(object sender, EventArgs e)
        {
            Get_Implant(4);
        }

        private void btnSlot5_Click(object sender, EventArgs e)
        {
            Get_Implant(5);
        }

        private void btnSlot6_Click(object sender, EventArgs e)
        {
            Get_Implant(6);
        }

        private void btnSlot7_Click(object sender, EventArgs e)
        {
            Get_Implant(7);
        }

        private void btnSlot8_Click(object sender, EventArgs e)
        {
            Get_Implant(8);
        }

        private void btnSlot9_Click(object sender, EventArgs e)
        {
            Get_Implant(9);
        }

        private void btnSlot10_Click(object sender, EventArgs e)
        {
            Get_Implant(10);
        }

        private void Get_Implant(int Slot)
        {
            // This is where we need to get the details for a new Implant.
            /*DialogResult dr = DialogResult.No;
            ListViewItem lvi = lvImplants.SelectedItems[0];
            using (ManualImplantDetailWindow f = new ManualImplantDetailWindow(lvi.Tag as GrandEveAttributeBonus))
            {
                DialogResult dr2 = f.ShowDialog();
                if (dr2 == DialogResult.OK)
                {
                    ListViewItem rlvi = CreateItemForBonus(f.ResultBonus);
                    // change the manual implant details
                    lvImplants.Items[lvImplants.Items.IndexOf(lvi)] = rlvi;
                    if (((lvi.Tag as GrandEveAttributeBonus).EveAttribute) != ((rlvi.Tag as GrandEveAttributeBonus).EveAttribute))
                    {
                        Where = Searching.XML;
                        int i = 0;
                        // add XML sourced implant with (lvi.Tag as GrandEveAttributeBonus).EveAttribute
                        GrandEveAttributeBonus add_temp = (lvi.Tag as GrandEveAttributeBonus);
                        toSearchForAttrib = add_temp.EveAttribute;
                        if (workingList.Exists(isSameAttribute))
                        {
                            i = workingList.FindIndex(isSameAttribute);
                            lvImplants.Items.Add(CreateItemForBonus(workingList[i]));
                        }
                        i = -1;
                        // remove any XML sourced implant with (rlvi.Tag as GrandEveAttributeBonus).EveAttribute
                        GrandEveAttributeBonus remove_temp = (rlvi.Tag as GrandEveAttributeBonus);
                        toSearchForAttrib = remove_temp.EveAttribute;
                        i = workingList.FindIndex(isSameAttribute);
                        if (i != -1)
                        {
                            List<ListViewItem> removeItems = new List<ListViewItem>();
                            foreach (ListViewItem Lvi in lvImplants.Items)
                            {
                                if ((Lvi.Tag as GrandEveAttributeBonus).EveAttribute == remove_temp.EveAttribute && (Lvi.Tag as GrandEveAttributeBonus).Manual == false)
                                    removeItems.Add(Lvi);
                            }
                            lvImplants.BeginUpdate();
                            try
                            {
                                foreach (ListViewItem Lvi in removeItems)
                                {
                                    lvImplants.Items.Remove(Lvi);
                                }
                            }
                            finally
                            {
                                lvImplants.EndUpdate();
                            }
                        }

                    }
                }
            }*/
        }
    }
}