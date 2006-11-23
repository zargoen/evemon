using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EVEMon
{
    public partial class EVEFolderWindow : Form
    {
        public EVEFolderWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fbdOpenDir.SelectedPath = m_EVEFolder;
            DialogResult dr = fbdOpenDir.ShowDialog();
            if (dr == DialogResult.OK)
            {
                tbFilename.Text = fbdOpenDir.SelectedPath;
                btnOk.Enabled = true;
//                FileOk = true;
//
//                //anders - find the real character.  This probably burns too much time to stay here, but I like it
//                XmlDocument xdoc = new XmlDocument();
//                xdoc.Load(ofdOpenXml.FileName);
//                XmlElement cElement = SerializableCharacterInfo.FindCharacterElement(xdoc);
//                tbFileCharName.Text = cElement.Attributes["name"].Value;
            }

        }

        private string m_EVEFolder;

        public string EVEFolder
        {
            get { return m_EVEFolder; }
            set
            {
                m_EVEFolder = value;
                tbFilename.Text = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_EVEFolder = tbFilename.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
