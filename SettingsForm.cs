using System;
using System.Drawing;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.WindowRelocator;
using Microsoft.Win32;

namespace EVEMon
{
    public partial class SettingsForm : EVEMonForm
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(Settings s)
            : this()
        {
            m_settings = s;
        }

        private Settings m_settings;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ApplyToSettings(Settings s)
        {
            // Look and feel options
            s.SkillPlannerHighlightPrerequisites = cbHighlightPrerequisites.Checked;
            s.SkillPlannerHighlightPlannedSkills = cbHighlightPlannedSkills.Checked;
            s.MinimizeToTray = cbMinimizeToTray.Checked;
            s.CloseToTray = cbCloseToTray.Checked;
            s.TitleToTime = cbTitleToTime.Checked;
            s.TitleToTimeLayout = cbWindowsTitleList.SelectedIndex + 1;
            s.WorksafeMode = cbWorksafeMode.Checked;
            s.RunIGBServer = cbRunIGBServer.Checked;
            s.RelocateEveWindow = cbRelocateEveWindow.Checked;
            s.RelocateTargetScreen = cbScreenList.SelectedIndex;
            s.SkillIconGroup = cbSkillIconSet.SelectedIndex + 1;
            s.EnableBalloonTips = cbShowBalloonTips.Checked;
            s.PlaySoundOnSkillComplete = cbPlaySoundOnSkillComplete.Checked;
            s.EnableSkillCompleteDialog = cbShowCompletedSkillsDialog.Checked;

            // Email Options
            s.EnableEmailAlert = cbSendEmail.Checked;
            s.EmailServer = tbMailServer.Text;
            try
            {
                s.PortNumber = Convert.ToInt16(tbPortNumber.Text);
            }
            catch (FormatException e)
            {
                ExceptionHandler.LogException(e, true);
                tbPortNumber.Text = "25";
                s.PortNumber = 25;
            }
            s.EmailServerRequiresSsl = cbEmailServerRequireSsl.Checked;
            s.EmailAuthRequired = cbEmailAuthRequired.Checked;
            s.EmailAuthUsername = tbEmailUsername.Text;
            s.EmailAuthPassword = tbEmailPassword.Text;
            s.EmailFromAddress = tbFromAddress.Text;
            s.EmailToAddress = tbToAddress.Text;

            // Tooltips
            ToolTipDisplayOptions tipOptions = ToolTipDisplayOptions.Blank;

            if (cbTooltipOptionDate.Checked)
                tipOptions = tipOptions | ToolTipDisplayOptions.TimeFinished;

            if (cbTooltipOptionETA.Checked)
                tipOptions = tipOptions | ToolTipDisplayOptions.TimeRemaining;
            
            if (cbTooltipOptionName.Checked)
                tipOptions = tipOptions | ToolTipDisplayOptions.Name;
            
            if (cbTooltipOptionSkill.Checked)
                tipOptions = tipOptions | ToolTipDisplayOptions.Skill;

            s.TooltipOptions = tipOptions;

            s.UseCustomProxySettings = rbCustomProxy.Checked;
            ProxySetting httpSetting = ((ProxySetting)btnProxyHttpAuth.Tag).Clone();
            httpSetting.Host = tbProxyHttpHost.Text;
            try
            {
                httpSetting.Port = Convert.ToInt32(tbProxyHttpPort.Text);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, true);
                httpSetting.Port = 0;
            }
            s.HttpProxy = httpSetting;

            m_settings.CheckTranquilityStatus = cbCheckTranquilityStatus.Checked;
            m_settings.StatusUpdateInterval = (int)numericStatusInterval.Value;

            m_settings.DisableXMLAutoUpdate = cbAutomaticEOSkillUpdate.Checked;

            m_settings.DeleteCharacterSilently = cbDeleteCharactersSilently.Checked;
            m_settings.KeepCharacterPlans = cbKeepCharacterPlans.Checked;

            m_settings.DisableEVEMonVersionCheck = cbAutomaticallySearchForNewVersions.Checked;

            m_settings.EnableSkillCompleteDialog = cbShowCompletedSkillsDialog.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ApplyToSettings(m_settings);
            m_settings.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cbScreenList.Items.Clear();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                cbScreenList.Items.Add(String.Format("Screen {0}", i + 1));
            }

            cbSkillIconSet.Items.Clear();
            for (int i = 1; i < EVEMon.Resources.icons.Skill_Select.IconSettings.Default.Properties.Count; i++)
            {
                cbSkillIconSet.Items.Add(EVEMon.Resources.icons.Skill_Select.IconSettings.Default.Properties["Group" + i].DefaultValue.ToString());
            }

            int maxWidth = 0;
            int maxHeight = 0;
            foreach (TabPage tp in tabControl1.TabPages)
            {
                tabControl1.SelectedTab = tp;
                Size prefSize = tp.Controls[0].GetPreferredSize(new Size(Int32.MaxValue, Int32.MaxValue));
                prefSize.Width += tp.Padding.Left + tp.Padding.Right;
                prefSize.Height += tp.Padding.Top + tp.Padding.Bottom;
                maxWidth = Math.Max(maxWidth, prefSize.Width);
                maxHeight = Math.Max(maxHeight, prefSize.Height);
            }
            int extraHeight = tabControl1.ClientSize.Height - tabControl1.TabPages[0].Height;
            int extraWidth = tabControl1.ClientSize.Width - tabControl1.TabPages[0].Width;
            tabControl1.ClientSize = new Size(maxWidth + extraWidth, maxHeight + extraHeight);
            tabControl1.SelectedTab = tabControl1.TabPages[0];

            // Look and feel options
            cbMinimizeToTray.Checked = m_settings.MinimizeToTray;
            cbCloseToTray.Checked = m_settings.CloseToTray;
            cbTitleToTime.Checked = m_settings.TitleToTime;
            cbWindowsTitleList.SelectedIndex = m_settings.TitleToTimeLayout -1;
            cbWorksafeMode.Checked = m_settings.WorksafeMode;
            gbSkillPlannerHighlighting.Enabled = !cbWorksafeMode.Checked;
            cbRunIGBServer.Checked = m_settings.RunIGBServer;
            cbRelocateEveWindow.Checked = m_settings.RelocateEveWindow;
            if (m_settings.RelocateTargetScreen < cbScreenList.Items.Count)
                cbScreenList.SelectedIndex = m_settings.RelocateTargetScreen;
            else
                cbScreenList.SelectedIndex = 0;

            // Skill Icon Set
            if (m_settings.SkillIconGroup <= cbSkillIconSet.Items.Count && m_settings.SkillIconGroup > 0)
                cbSkillIconSet.SelectedIndex = m_settings.SkillIconGroup - 1;
            else
                cbSkillIconSet.SelectedIndex = 0;

            cbShowBalloonTips.Checked = m_settings.EnableBalloonTips;
            cbPlaySoundOnSkillComplete.Checked = m_settings.PlaySoundOnSkillComplete;
            cbShowCompletedSkillsDialog.Checked = m_settings.EnableSkillCompleteDialog;

            cbSendEmail.Checked = m_settings.EnableEmailAlert;
            tbMailServer.Text = m_settings.EmailServer;
            tbPortNumber.Text = m_settings.PortNumber.ToString();
            cbEmailServerRequireSsl.Checked = m_settings.EmailServerRequiresSsl;
            cbEmailAuthRequired.Checked = m_settings.EmailAuthRequired;
            tbEmailUsername.Text = m_settings.EmailAuthUsername;
            tbEmailPassword.Text = m_settings.EmailAuthPassword;
            tbFromAddress.Text = m_settings.EmailFromAddress;
            tbToAddress.Text = m_settings.EmailToAddress;

            rbDefaultProxy.Checked = (m_settings.UseCustomProxySettings == false);
            rbCustomProxy.Checked = (m_settings.UseCustomProxySettings);
            tbProxyHttpHost.Text = m_settings.HttpProxy.Host;
            tbProxyHttpPort.Text = m_settings.HttpProxy.Port.ToString();
            btnProxyHttpAuth.Tag = m_settings.HttpProxy.Clone();

            cbCheckTranquilityStatus.Checked = m_settings.CheckTranquilityStatus;
            numericStatusInterval.Value = m_settings.StatusUpdateInterval;

            cbAutomaticallySearchForNewVersions.Checked = m_settings.DisableEVEMonVersionCheck;
            cbAutomaticEOSkillUpdate.Checked = m_settings.DisableXMLAutoUpdate;

            cbDeleteCharactersSilently.Checked = m_settings.DeleteCharacterSilently;
            cbKeepCharacterPlans.Checked = m_settings.KeepCharacterPlans;

            cbHighlightPlannedSkills.Checked = m_settings.SkillPlannerHighlightPlannedSkills;
            cbHighlightPrerequisites.Checked = m_settings.SkillPlannerHighlightPrerequisites;

            cbTooltipOptionDate.Checked = ((m_settings.TooltipOptions & ToolTipDisplayOptions.TimeFinished) == ToolTipDisplayOptions.TimeFinished);
            cbTooltipOptionETA.Checked = ((m_settings.TooltipOptions & ToolTipDisplayOptions.TimeRemaining) == ToolTipDisplayOptions.TimeRemaining);
            cbTooltipOptionSkill.Checked = ((m_settings.TooltipOptions & ToolTipDisplayOptions.Skill) == ToolTipDisplayOptions.Skill);
            cbTooltipOptionName.Checked = ((m_settings.TooltipOptions & ToolTipDisplayOptions.Name) == ToolTipDisplayOptions.Name);


            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            if (rk != null && rk.GetValue("EVEMon") == null)
            {
                cbRunAtStartup.Checked = false;
            }
            else
            {
                cbRunAtStartup.Checked = true;
            }
            UpdateDisables();
        }

        private void cbRunAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if ((sender as CheckBox).Checked)
            {
                rk.SetValue("EVEMon", String.Format("{0} {1}", Application.ExecutablePath.ToString(), "-startMinimized"));
            }
            else
            {
                rk.DeleteValue("EVEMon", false);
            }
        }

        private void cbWorksafeMode_CheckedChanged(object sender, EventArgs e)
        {
            gbSkillPlannerHighlighting.Enabled = !cbWorksafeMode.Checked;
        }

        private void cbSendEmail_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }

        private void UpdateDisables()
        {
            tlpEmailSettings.Enabled = cbSendEmail.Checked;
            btnTestEmail.Enabled = cbSendEmail.Checked;
            tlpEmailAuthTable.Enabled = cbEmailAuthRequired.Checked;

            bool isValid = true;
            isValid = isValid && ValidateProxySetting(tbProxyHttpHost.Text, tbProxyHttpPort.Text);

            btnOk.Enabled = isValid;
            numericStatusInterval.Enabled = cbCheckTranquilityStatus.Checked;
        }

        private bool ValidateProxySetting(string host, string port)
        {
            if (rbCustomProxy.Checked)
            {
                if (String.IsNullOrEmpty(host))
                {
                    return false;
                }

                int junk;
                if (!Int32.TryParse(port, out junk) || junk < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnTestEmail_Click(object sender, EventArgs e)
        {
            Settings ts = new Settings();
            ts.NeverSave();
            ApplyToSettings(ts);
            if (!Emailer.SendTestMail(ts))
            {
                MessageBox.Show("The message failed to send.", "Mail Failure", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("The message sent successfully. Please verify that the message was received.",
                                "Mail Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbEmailAuthRequired_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }

        private void btnIdentifyScreens_Click(object sender, EventArgs e)
        {
            IdentifyScreenForm.Display();
        }

        private void cbRelocateEveWindow_CheckedChanged(object sender, EventArgs e)
        {
            flpScreenSelect.Enabled = cbRelocateEveWindow.Checked;
        }

        private void rbCustomProxy_CheckedChanged(object sender, EventArgs e)
        {
            vfpCustomProxySettings.Enabled = rbCustomProxy.Checked;
            UpdateDisables();
        }

        private void tbProxyHttpPort_TextChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }

        private void btnProxyHttpAuth_Click(object sender, EventArgs e)
        {
            ProxySetting ps = ((ProxySetting)btnProxyHttpAuth.Tag).Clone();
            using (ProxyAuthenticationWindow f = new ProxyAuthenticationWindow())
            {
                f.ProxySetting = ps;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    btnProxyHttpAuth.Tag = f.ProxySetting.Clone();
                }
            }
        }

        private void cbCheckTranquilityStatus_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisables();
        }

        private void cbAutomaticallySearchForNewVersions_CheckedChanged(object sender, EventArgs e)
        {
            UpdateManager um = UpdateManager.GetInstance();
            if (cbAutomaticallySearchForNewVersions.Checked == false)
            {
                um.UpdateAvailable += new UpdateAvailableHandler(um_UpdateAvailable);
                um.Start();
            }
            else
            {
                try
                {
                    um.UpdateAvailable -= new UpdateAvailableHandler(um_UpdateAvailable);
                    um.Stop();
                }
                catch
                {
                    //We ignore this since the form may be starting up and thus 
                    //UpdateManager has no running start method.
                }
            }
            UpdateDisables();
        }

        private bool m_updateShowing = false;

        private void um_UpdateAvailable(object sender, UpdateAvailableEventArgs e)
        {
            if (e.NewestVersion <= new Version(m_settings.IgnoreUpdateVersion))
                return;

            this.Invoke(new MethodInvoker(delegate
            {
                if (!m_updateShowing)
                {
                    m_updateShowing = true;
                    using (UpdateNotifyForm f = new UpdateNotifyForm(m_settings, e))
                    {
                        f.ShowDialog();
                        if (f.DialogResult == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    m_updateShowing = false;
                }
            }));
        }

        private void cbSkillIconSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageList def = new ImageList();
            def.ColorDepth = ColorDepth.Depth32Bit;
            string groupname = null;
            if (cbSkillIconSet.SelectedIndex >= 0 && cbSkillIconSet.SelectedIndex < EVEMon.Resources.icons.Skill_Select.IconSettings.Default.Properties.Count - 1)
                groupname = EVEMon.Resources.icons.Skill_Select.IconSettings.Default.Properties["Group" + (cbSkillIconSet.SelectedIndex + 1)].DefaultValue.ToString();
            if (groupname != null)
            {
                System.Resources.IResourceReader basic = new System.Resources.ResourceReader(System.AppDomain.CurrentDomain.BaseDirectory + "//Resources//icons//Skill_Select//Group0//Default.resources");
                System.Collections.IDictionaryEnumerator basicx = basic.GetEnumerator();
                while (basicx.MoveNext())
                {
                    def.Images.Add(basicx.Key.ToString(), (System.Drawing.Icon)basicx.Value);
                }
                basic.Close();
                basic = new System.Resources.ResourceReader(System.AppDomain.CurrentDomain.BaseDirectory + "//Resources//icons//Skill_Select//Group" + (cbSkillIconSet.SelectedIndex + 1) + "//" + groupname + ".resources");
                basicx = basic.GetEnumerator();
                while (basicx.MoveNext())
                {
                    if (def.Images.ContainsKey(basicx.Key.ToString()))
                    {
                        def.Images.RemoveByKey(basicx.Key.ToString());
                    }
                    def.Images.Add(basicx.Key.ToString(), (System.Drawing.Icon)basicx.Value);
                }
                basic.Close();
            }
            tvlist.Nodes.Clear();
            tvlist.ImageList = def;
            tvlist.ImageList.ColorDepth = ColorDepth.Depth32Bit;
            TreeNode gtn = new TreeNode("Book", tvlist.ImageList.Images.IndexOfKey("book"), tvlist.ImageList.Images.IndexOfKey("book"));
            gtn.Nodes.Add(new TreeNode("Pre-Reqs NOT met (Rank)", tvlist.ImageList.Images.IndexOfKey("PrereqsNOTMet"), tvlist.ImageList.Images.IndexOfKey("PrereqsNOTMet")));
            gtn.Nodes.Add(new TreeNode("Pre-Reqs met (Rank)", tvlist.ImageList.Images.IndexOfKey("PrereqsMet"), tvlist.ImageList.Images.IndexOfKey("PrereqsMet")));
            for (int i = 0; i < 6; i++)
            {
                gtn.Nodes.Add(new TreeNode("Level " + i + " (Rank)", tvlist.ImageList.Images.IndexOfKey("lvl" + i), tvlist.ImageList.Images.IndexOfKey("lvl" + i)));
            }
            gtn.Expand();
            tvlist.Nodes.Add(gtn);
        }

        private void cbTitleToTime_CheckedChanged(object sender, EventArgs e)
        {

        }

        
    }
}
