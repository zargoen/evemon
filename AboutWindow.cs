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
        SortedList slColourKey;
        SortedList slDevelopers;
        public Font myFont;

        public AboutWindow()
        {
            InitializeComponent();

            myFont = new System.Drawing.Font("Tahoma", 8);

            slColourKey = new SortedList();
            slColourKey.Add("EVEMon Legend", Brushes.Red);
            slColourKey.Add("EVEMon Guru", Brushes.DarkBlue);
            slColourKey.Add("EVEMon Developer", Brushes.Plum);
            slColourKey.Add("EVEMon Part-timer", Brushes.Khaki);
            slColourKey.Add("EVEMon Noob", Brushes.LightSeaGreen);

            slDevelopers = new SortedList();
            slDevelopers.Add("Six Anari", "EVEMon Legend");
            slDevelopers.Add("Anders Chydenius", "EVEMon Guru");
            slDevelopers.Add("Eewec", "EVEMon Developer");
            slDevelopers.Add("Brad Stone", "EVEMon Developer");
            slDevelopers.Add("mrcue", "EVEMon Developer");
            slDevelopers.Add("Labogh", "EVEMon Developer");
            slDevelopers.Add("romanl", "EVEMon Developer");
            slDevelopers.Add("happyslinky", "EVEMon Developer");
            slDevelopers.Add("Stevil Knevil", "EVEMon Developer");
            slDevelopers.Add("Safrax", "EVEMon Developer");
            slDevelopers.Add("Jalon Mevek", "EVEMon Developer");
            slDevelopers.Add("Nascent Nimbus", "EVEMon Noob");
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = String.Format(lblVersion.Text, currentVersion.ToString());
            AddDevelopersToListBox();
        }

        private void AddDevelopersToListBox()
        {
            // Set up the colour key list box.
            for (int i = 0; i < slColourKey.Count; i++)
            {
                lbColourKey.Items.Add(slColourKey.GetByIndex(i));
            }

            // Set up the list of developers.
            for (int i = 0; i < slDevelopers.Count; i++)
            {
                lstDevelopers.Items.Add(slDevelopers.GetByIndex(i));
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
                Brush b = Brushes.Black;

                b = (Brush)slColourKey.GetByIndex(slColourKey.IndexOfKey(slDevelopers.GetByIndex(e.Index)));
                e.Graphics.FillRectangle(b, e.Bounds);
                e.Graphics.DrawString(slDevelopers.GetKey(e.Index).ToString(), myFont, Brushes.White,
                new Point(e.Bounds.X, e.Bounds.Y));
            }
        }

        private void lbColourKey_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Fill in the colour keys.
            if (e.Index > -1)
            {
                Brush b = Brushes.Black;

                b = (Brush)slColourKey.GetByIndex(e.Index);
                e.Graphics.FillRectangle(b, e.Bounds);
                e.Graphics.DrawString(slColourKey.GetKey(e.Index).ToString(), myFont, Brushes.White,
                new Point(e.Bounds.X, e.Bounds.Y));
            }
        }
    }
}
