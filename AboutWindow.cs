using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using EVEMon.Common;
using System.Text;

namespace EVEMon
{
    public partial class AboutWindow : EVEMonForm
    {
        private string[] Developers = { "Six Anari", "Anders Chydenius", 
            "Eewec", "Brad Stone", "Labogh", "mrcue", "romanl", "happyslinky", 
            "Stevil Knevil", "Safrax" };

        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = String.Format(lblVersion.Text, currentVersion.ToString());
            AddDevelopersToListBox();
        }

        private void AddDevelopersToListBox()
        {
            foreach (string i in Developers)
            {
                lstDevelopers.Items.Add(i);
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
    }
}
