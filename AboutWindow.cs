using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using EVEMon.Common;
using System.Text;
using System.Collections;
using System.Drawing;

namespace EVEMon
{
    public partial class AboutWindow : EVEMonForm
    {
        SortedList slPriority;
        SortedList slDevelopers;
        SortedList slOutput;
        public Font myFont;
        public Font myFontbold;

        public AboutWindow()
        {
            InitializeComponent();

            myFont = new System.Drawing.Font("Tahoma", 8);
            myFontbold = new System.Drawing.Font("Tahoma", 8, FontStyle.Bold);

            slPriority = new SortedList();
            slPriority.Add("01", "EVEMon Legend");
            slPriority.Add("02", "EVEMon Guru");
            slPriority.Add("03", "EVEMon Developer");
            slPriority.Add("04", "EVEMon Noob");
            slPriority.Add("05", "Past Contributor");

            slDevelopers = new SortedList();
            slDevelopers.Add("Six Anari","01");
            slDevelopers.Add("Anders Chydenius","02");
            slDevelopers.Add("Eewec","03");
            slDevelopers.Add("Brad Stone", "03");
            slDevelopers.Add("mrcue","03");
            slDevelopers.Add("Labogh","03");
            slDevelopers.Add("Stevil Knevil","03");
            slDevelopers.Add("Safrax","03");
            slDevelopers.Add("Jalon Mevek", "03");
            slDevelopers.Add("romanl","03");
            slDevelopers.Add("TheBelgarion", "04");
            slDevelopers.Add("happyslinky", "04");
            slDevelopers.Add("Nascent Nimbus","04");
            slDevelopers.Add("Lexiica", "04");
 
            slOutput = new SortedList();

        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = String.Format(lblVersion.Text, currentVersion.ToString());
            AddDevelopersToListBox();
        }

        private void AddDevelopersToListBox()
        {
            // Set up the list of developers.
            int iOrder = 0;
            bool bAddGroup;
            for (int i = 0; i < slPriority.Count; i++)
            {
                bAddGroup = true;
                for (int j = 0; j < slDevelopers.Count; j++)
                {
                    if (slPriority.GetKey(i) == slDevelopers.GetByIndex(j))
                    {
                        if (bAddGroup)
                        {
                            slOutput.Add(iOrder++, slPriority.GetByIndex(i));
                            bAddGroup = false;
                        }
                        slOutput.Add(iOrder++, slDevelopers.GetKey(j));
                    }
                }
            }
            for (int i = 0; i < slOutput.Count; i++)
            {
                lstDevelopers.Items.Add(slOutput.GetByIndex(i));
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://evemon.battleclinic.com/");
        }

        private void lstDevelopers_DrawItem(object sender,
        System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                string sLine = slOutput.GetByIndex(e.Index).ToString();
                if (sLine.Contains("EVEMon")) 
                {
                    sLine = sLine + ":";
                    e.Graphics.DrawString(sLine, myFontbold, Brushes.Black, new Point(e.Bounds.X, e.Bounds.Y));
                }
                else
                {
                    sLine = "  "+sLine;
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Point(e.Bounds.X, e.Bounds.Y));
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lstDevelopers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
