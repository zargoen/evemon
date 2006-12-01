namespace EVEMon
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbPlaySoundOnSkillComplete = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpEmailSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMailServer = new System.Windows.Forms.TextBox();
            this.tbFromAddress = new System.Windows.Forms.TextBox();
            this.tbToAddress = new System.Windows.Forms.TextBox();
            this.cbEmailServerRequireSsl = new System.Windows.Forms.CheckBox();
            this.cbEmailAuthRequired = new System.Windows.Forms.CheckBox();
            this.tlpEmailAuthTable = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbEmailUsername = new System.Windows.Forms.TextBox();
            this.tbEmailPassword = new System.Windows.Forms.TextBox();
            this.tbPortNumber = new System.Windows.Forms.TextBox();
            this.lblPortNumber = new System.Windows.Forms.Label();
            this.btnTestEmail = new System.Windows.Forms.Button();
            this.cbSendEmail = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.verticalFlowPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.flowLayoutPanel24 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbSystemTrayOptionsNever = new System.Windows.Forms.RadioButton();
            this.rbSystemTrayOptionsMinimized = new System.Windows.Forms.RadioButton();
            this.rbSystemTrayOptionsAlways = new System.Windows.Forms.RadioButton();
            this.cbCloseToTray = new System.Windows.Forms.CheckBox();
            this.cbRunAtStartup = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbRelocateEveWindow = new System.Windows.Forms.CheckBox();
            this.flpScreenSelect = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.cbScreenList = new System.Windows.Forms.ComboBox();
            this.btnIdentifyScreens = new System.Windows.Forms.Button();
            this.gboxTooltipOptions = new System.Windows.Forms.GroupBox();
            this.cbTooltipOptionName = new System.Windows.Forms.CheckBox();
            this.cbTooltipOptionSkill = new System.Windows.Forms.CheckBox();
            this.cbTooltipOptionDate = new System.Windows.Forms.CheckBox();
            this.cbTooltipOptionETA = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel16 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel15 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbWorksafeMode = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel21 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel22 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbTitleToTime = new System.Windows.Forms.CheckBox();
            this.cbWindowsTitleList = new System.Windows.Forms.ComboBox();
            this.gbSkillPlannerHighlighting = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel13 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbHighlightPlannedSkills = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel14 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbHighlightPrerequisites = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tvlist = new System.Windows.Forms.TreeView();
            this.cbSkillIconSet = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.verticalFlowPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.verticalFlowPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbDefaultProxy = new System.Windows.Forms.RadioButton();
            this.rbCustomProxy = new System.Windows.Forms.RadioButton();
            this.vfpCustomProxySettings = new System.Windows.Forms.FlowLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.tbProxyHttpHost = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnProxyHttpAuth = new System.Windows.Forms.Button();
            this.tbProxyHttpPort = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.verticalFlowPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbShowBalloonTips = new System.Windows.Forms.CheckBox();
            this.cbShowCompletedSkillsDialog = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbEmailUseShortFormat = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbAutomaticallySearchForNewVersions = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel20 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbKeepCharacterPlans = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel19 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel18 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbDeleteCharactersSilently = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel17 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbAutomaticEOSkillUpdate = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cbCheckTranquilityStatus = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numericStatusInterval = new System.Windows.Forms.NumericUpDown();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel23 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.nmUpdateSpd = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.chkG15Enabled = new System.Windows.Forms.CheckBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.nmSCycle = new System.Windows.Forms.NumericUpDown();
            this.chkSDisplay = new System.Windows.Forms.CheckBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.nmCCycle = new System.Windows.Forms.NumericUpDown();
            this.chkCDisplay = new System.Windows.Forms.CheckBox();
            this.g15Preview = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel26 = new System.Windows.Forms.FlowLayoutPanel();
            this.pbg15Preview = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel25 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.tmrG15Update = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbRunIGBServer = new System.Windows.Forms.CheckBox();
            this.cbIGBPublic = new System.Windows.Forms.CheckBox();
            this.tb_IgbPort = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.flowLayoutPanel27 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpEmailSettings.SuspendLayout();
            this.tlpEmailAuthTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.verticalFlowPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel24.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flpScreenSelect.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.gboxTooltipOptions.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.flowLayoutPanel16.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.flowLayoutPanel15.SuspendLayout();
            this.flowLayoutPanel22.SuspendLayout();
            this.gbSkillPlannerHighlighting.SuspendLayout();
            this.flowLayoutPanel13.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.verticalFlowPanel3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.verticalFlowPanel4.SuspendLayout();
            this.vfpCustomProxySettings.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.verticalFlowPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.flowLayoutPanel11.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.flowLayoutPanel10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.flowLayoutPanel20.SuspendLayout();
            this.flowLayoutPanel18.SuspendLayout();
            this.flowLayoutPanel12.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStatusInterval)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.flowLayoutPanel23.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmUpdateSpd)).BeginInit();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmSCycle)).BeginInit();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCCycle)).BeginInit();
            this.g15Preview.SuspendLayout();
            this.flowLayoutPanel26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbg15Preview)).BeginInit();
            this.flowLayoutPanel25.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel27.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(84, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbPlaySoundOnSkillComplete
            // 
            this.cbPlaySoundOnSkillComplete.AutoSize = true;
            this.cbPlaySoundOnSkillComplete.Location = new System.Drawing.Point(12, 49);
            this.cbPlaySoundOnSkillComplete.Name = "cbPlaySoundOnSkillComplete";
            this.cbPlaySoundOnSkillComplete.Size = new System.Drawing.Size(216, 17);
            this.cbPlaySoundOnSkillComplete.TabIndex = 3;
            this.cbPlaySoundOnSkillComplete.Text = "Play sound when skill training completes";
            this.cbPlaySoundOnSkillComplete.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tlpEmailSettings, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 49);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 227F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 227F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(338, 227);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tlpEmailSettings
            // 
            this.tlpEmailSettings.AutoSize = true;
            this.tlpEmailSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEmailSettings.ColumnCount = 2;
            this.tlpEmailSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailSettings.Controls.Add(this.label1, 0, 0);
            this.tlpEmailSettings.Controls.Add(this.label2, 0, 5);
            this.tlpEmailSettings.Controls.Add(this.label3, 0, 6);
            this.tlpEmailSettings.Controls.Add(this.tbMailServer, 1, 0);
            this.tlpEmailSettings.Controls.Add(this.tbFromAddress, 1, 5);
            this.tlpEmailSettings.Controls.Add(this.tbToAddress, 1, 6);
            this.tlpEmailSettings.Controls.Add(this.cbEmailServerRequireSsl, 1, 2);
            this.tlpEmailSettings.Controls.Add(this.cbEmailAuthRequired, 1, 3);
            this.tlpEmailSettings.Controls.Add(this.tlpEmailAuthTable, 1, 4);
            this.tlpEmailSettings.Controls.Add(this.tbPortNumber, 1, 1);
            this.tlpEmailSettings.Controls.Add(this.lblPortNumber, 0, 1);
            this.tlpEmailSettings.Location = new System.Drawing.Point(22, 3);
            this.tlpEmailSettings.Name = "tlpEmailSettings";
            this.tlpEmailSettings.RowCount = 7;
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailSettings.Size = new System.Drawing.Size(294, 216);
            this.tlpEmailSettings.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email Server:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "From address:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "To address:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMailServer
            // 
            this.tbMailServer.Location = new System.Drawing.Point(85, 3);
            this.tbMailServer.Name = "tbMailServer";
            this.tbMailServer.Size = new System.Drawing.Size(152, 21);
            this.tbMailServer.TabIndex = 1;
            // 
            // tbFromAddress
            // 
            this.tbFromAddress.Location = new System.Drawing.Point(85, 165);
            this.tbFromAddress.Name = "tbFromAddress";
            this.tbFromAddress.Size = new System.Drawing.Size(206, 21);
            this.tbFromAddress.TabIndex = 1;
            // 
            // tbToAddress
            // 
            this.tbToAddress.Location = new System.Drawing.Point(85, 192);
            this.tbToAddress.Name = "tbToAddress";
            this.tbToAddress.Size = new System.Drawing.Size(206, 21);
            this.tbToAddress.TabIndex = 1;
            // 
            // cbEmailServerRequireSsl
            // 
            this.cbEmailServerRequireSsl.AutoSize = true;
            this.cbEmailServerRequireSsl.Location = new System.Drawing.Point(85, 57);
            this.cbEmailServerRequireSsl.Name = "cbEmailServerRequireSsl";
            this.cbEmailServerRequireSsl.Size = new System.Drawing.Size(114, 17);
            this.cbEmailServerRequireSsl.TabIndex = 1;
            this.cbEmailServerRequireSsl.Text = "Connect using SSL";
            this.cbEmailServerRequireSsl.UseVisualStyleBackColor = true;
            // 
            // cbEmailAuthRequired
            // 
            this.cbEmailAuthRequired.AutoSize = true;
            this.cbEmailAuthRequired.Location = new System.Drawing.Point(85, 80);
            this.cbEmailAuthRequired.Name = "cbEmailAuthRequired";
            this.cbEmailAuthRequired.Size = new System.Drawing.Size(125, 17);
            this.cbEmailAuthRequired.TabIndex = 1;
            this.cbEmailAuthRequired.Text = "Server requires login";
            this.cbEmailAuthRequired.UseVisualStyleBackColor = true;
            this.cbEmailAuthRequired.CheckedChanged += new System.EventHandler(this.cbEmailAuthRequired_CheckedChanged);
            // 
            // tlpEmailAuthTable
            // 
            this.tlpEmailAuthTable.AutoSize = true;
            this.tlpEmailAuthTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEmailAuthTable.ColumnCount = 2;
            this.tlpEmailAuthTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailAuthTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpEmailAuthTable.Controls.Add(this.label5, 0, 1);
            this.tlpEmailAuthTable.Controls.Add(this.label4, 0, 0);
            this.tlpEmailAuthTable.Controls.Add(this.tbEmailUsername, 1, 0);
            this.tlpEmailAuthTable.Controls.Add(this.tbEmailPassword, 1, 1);
            this.tlpEmailAuthTable.Location = new System.Drawing.Point(85, 103);
            this.tlpEmailAuthTable.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.tlpEmailAuthTable.Name = "tlpEmailAuthTable";
            this.tlpEmailAuthTable.RowCount = 2;
            this.tlpEmailAuthTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailAuthTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEmailAuthTable.Size = new System.Drawing.Size(200, 54);
            this.tlpEmailAuthTable.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "Password:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 27);
            this.label4.TabIndex = 7;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbEmailUsername
            // 
            this.tbEmailUsername.Location = new System.Drawing.Point(68, 3);
            this.tbEmailUsername.Name = "tbEmailUsername";
            this.tbEmailUsername.Size = new System.Drawing.Size(129, 21);
            this.tbEmailUsername.TabIndex = 1;
            // 
            // tbEmailPassword
            // 
            this.tbEmailPassword.Location = new System.Drawing.Point(68, 30);
            this.tbEmailPassword.Name = "tbEmailPassword";
            this.tbEmailPassword.PasswordChar = '*';
            this.tbEmailPassword.Size = new System.Drawing.Size(129, 21);
            this.tbEmailPassword.TabIndex = 1;
            // 
            // tbPortNumber
            // 
            this.tbPortNumber.Location = new System.Drawing.Point(85, 30);
            this.tbPortNumber.Name = "tbPortNumber";
            this.tbPortNumber.Size = new System.Drawing.Size(152, 21);
            this.tbPortNumber.TabIndex = 1;
            this.tbPortNumber.Text = "25";
            // 
            // lblPortNumber
            // 
            this.lblPortNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPortNumber.AutoSize = true;
            this.lblPortNumber.Location = new System.Drawing.Point(8, 27);
            this.lblPortNumber.Name = "lblPortNumber";
            this.lblPortNumber.Size = new System.Drawing.Size(71, 27);
            this.lblPortNumber.TabIndex = 10;
            this.lblPortNumber.Text = "Port Number:";
            this.lblPortNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTestEmail
            // 
            this.btnTestEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestEmail.Location = new System.Drawing.Point(240, 282);
            this.btnTestEmail.Name = "btnTestEmail";
            this.btnTestEmail.Size = new System.Drawing.Size(110, 23);
            this.btnTestEmail.TabIndex = 10;
            this.btnTestEmail.Text = "Send Test Email";
            this.btnTestEmail.UseVisualStyleBackColor = true;
            this.btnTestEmail.Click += new System.EventHandler(this.btnTestEmail_Click);
            // 
            // cbSendEmail
            // 
            this.cbSendEmail.AutoSize = true;
            this.cbSendEmail.Location = new System.Drawing.Point(12, 3);
            this.cbSendEmail.Name = "cbSendEmail";
            this.cbSendEmail.Size = new System.Drawing.Size(215, 17);
            this.cbSendEmail.TabIndex = 1;
            this.cbSendEmail.Text = "Send email when skill training completes";
            this.cbSendEmail.UseVisualStyleBackColor = true;
            this.cbSendEmail.CheckedChanged += new System.EventHandler(this.cbSendEmail_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 40);
            this.label6.TabIndex = 8;
            this.label6.Text = "Server Password:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Email Server:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 26);
            this.label8.TabIndex = 1;
            this.label8.Text = "From address:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(396, 494);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.verticalFlowPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(388, 468);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // verticalFlowPanel1
            // 
            this.verticalFlowPanel1.AutoSize = true;
            this.verticalFlowPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.verticalFlowPanel1.Controls.Add(this.groupBox1);
            this.verticalFlowPanel1.Controls.Add(this.groupBox5);
            this.verticalFlowPanel1.Controls.Add(this.gboxTooltipOptions);
            this.verticalFlowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel1.Location = new System.Drawing.Point(3, 3);
            this.verticalFlowPanel1.Name = "verticalFlowPanel1";
            this.verticalFlowPanel1.Size = new System.Drawing.Size(382, 462);
            this.verticalFlowPanel1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 91);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Settings";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.panel1);
            this.flowLayoutPanel2.Controls.Add(this.cbCloseToTray);
            this.flowLayoutPanel2.Controls.Add(this.cbRunAtStartup);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(356, 71);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.flowLayoutPanel24);
            this.panel1.Location = new System.Drawing.Point(12, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 25);
            this.panel1.TabIndex = 8;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(-1, 5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Show in Tray:";
            // 
            // flowLayoutPanel24
            // 
            this.flowLayoutPanel24.Controls.Add(this.rbSystemTrayOptionsNever);
            this.flowLayoutPanel24.Controls.Add(this.rbSystemTrayOptionsMinimized);
            this.flowLayoutPanel24.Controls.Add(this.rbSystemTrayOptionsAlways);
            this.flowLayoutPanel24.Location = new System.Drawing.Point(78, 0);
            this.flowLayoutPanel24.Name = "flowLayoutPanel24";
            this.flowLayoutPanel24.Size = new System.Drawing.Size(250, 25);
            this.flowLayoutPanel24.TabIndex = 5;
            // 
            // rbSystemTrayOptionsNever
            // 
            this.rbSystemTrayOptionsNever.AutoSize = true;
            this.rbSystemTrayOptionsNever.Location = new System.Drawing.Point(3, 3);
            this.rbSystemTrayOptionsNever.Name = "rbSystemTrayOptionsNever";
            this.rbSystemTrayOptionsNever.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.rbSystemTrayOptionsNever.Size = new System.Drawing.Size(57, 17);
            this.rbSystemTrayOptionsNever.TabIndex = 1;
            this.rbSystemTrayOptionsNever.TabStop = true;
            this.rbSystemTrayOptionsNever.Tag = "";
            this.rbSystemTrayOptionsNever.Text = "Never";
            this.rbSystemTrayOptionsNever.UseVisualStyleBackColor = true;
            this.rbSystemTrayOptionsNever.CheckedChanged += new System.EventHandler(this.rbSystemTrayOptionsNever_CheckedChanged);
            // 
            // rbSystemTrayOptionsMinimized
            // 
            this.rbSystemTrayOptionsMinimized.AutoSize = true;
            this.rbSystemTrayOptionsMinimized.Location = new System.Drawing.Point(66, 3);
            this.rbSystemTrayOptionsMinimized.Name = "rbSystemTrayOptionsMinimized";
            this.rbSystemTrayOptionsMinimized.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.rbSystemTrayOptionsMinimized.Size = new System.Drawing.Size(104, 17);
            this.rbSystemTrayOptionsMinimized.TabIndex = 2;
            this.rbSystemTrayOptionsMinimized.TabStop = true;
            this.rbSystemTrayOptionsMinimized.Tag = "";
            this.rbSystemTrayOptionsMinimized.Text = "When Minimized";
            this.rbSystemTrayOptionsMinimized.UseVisualStyleBackColor = true;
            // 
            // rbSystemTrayOptionsAlways
            // 
            this.rbSystemTrayOptionsAlways.AutoSize = true;
            this.rbSystemTrayOptionsAlways.Location = new System.Drawing.Point(176, 3);
            this.rbSystemTrayOptionsAlways.Name = "rbSystemTrayOptionsAlways";
            this.rbSystemTrayOptionsAlways.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.rbSystemTrayOptionsAlways.Size = new System.Drawing.Size(62, 17);
            this.rbSystemTrayOptionsAlways.TabIndex = 3;
            this.rbSystemTrayOptionsAlways.TabStop = true;
            this.rbSystemTrayOptionsAlways.Tag = "";
            this.rbSystemTrayOptionsAlways.Text = "Always";
            this.rbSystemTrayOptionsAlways.UseVisualStyleBackColor = true;
            // 
            // cbCloseToTray
            // 
            this.cbCloseToTray.AutoSize = true;
            this.cbCloseToTray.Location = new System.Drawing.Point(12, 28);
            this.cbCloseToTray.Name = "cbCloseToTray";
            this.cbCloseToTray.Size = new System.Drawing.Size(90, 17);
            this.cbCloseToTray.TabIndex = 3;
            this.cbCloseToTray.Text = "Close to Tray";
            this.cbCloseToTray.UseVisualStyleBackColor = true;
            // 
            // cbRunAtStartup
            // 
            this.cbRunAtStartup.AutoSize = true;
            this.cbRunAtStartup.Location = new System.Drawing.Point(12, 51);
            this.cbRunAtStartup.Name = "cbRunAtStartup";
            this.cbRunAtStartup.Size = new System.Drawing.Size(138, 17);
            this.cbRunAtStartup.TabIndex = 4;
            this.cbRunAtStartup.Text = "Run EVEMon at Startup";
            this.cbRunAtStartup.UseVisualStyleBackColor = true;
            this.cbRunAtStartup.CheckedChanged += new System.EventHandler(this.cbRunAtStartup_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.AutoSize = true;
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.Controls.Add(this.flowLayoutPanel1);
            this.groupBox5.Location = new System.Drawing.Point(3, 100);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(362, 72);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "EVE Window Relocation";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.cbRelocateEveWindow);
            this.flowLayoutPanel1.Controls.Add(this.flpScreenSelect);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(356, 52);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // cbRelocateEveWindow
            // 
            this.cbRelocateEveWindow.AutoSize = true;
            this.cbRelocateEveWindow.Location = new System.Drawing.Point(12, 3);
            this.cbRelocateEveWindow.Name = "cbRelocateEveWindow";
            this.cbRelocateEveWindow.Size = new System.Drawing.Size(214, 17);
            this.cbRelocateEveWindow.TabIndex = 0;
            this.cbRelocateEveWindow.Text = "Relocate windowed EVE to fill a monitor";
            this.cbRelocateEveWindow.UseVisualStyleBackColor = true;
            this.cbRelocateEveWindow.CheckedChanged += new System.EventHandler(this.cbRelocateEveWindow_CheckedChanged);
            // 
            // flpScreenSelect
            // 
            this.flpScreenSelect.AutoSize = true;
            this.flpScreenSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpScreenSelect.Controls.Add(this.flowLayoutPanel4);
            this.flpScreenSelect.Controls.Add(this.btnIdentifyScreens);
            this.flpScreenSelect.Enabled = false;
            this.flpScreenSelect.Location = new System.Drawing.Point(34, 23);
            this.flpScreenSelect.Margin = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.flpScreenSelect.Name = "flpScreenSelect";
            this.flpScreenSelect.Size = new System.Drawing.Size(288, 29);
            this.flpScreenSelect.TabIndex = 7;
            this.flpScreenSelect.WrapContents = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.label9);
            this.flowLayoutPanel4.Controls.Add(this.cbScreenList);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(180, 27);
            this.flowLayoutPanel4.TabIndex = 2;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 27);
            this.label9.TabIndex = 2;
            this.label9.Text = "Monitor:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbScreenList
            // 
            this.cbScreenList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenList.FormattingEnabled = true;
            this.cbScreenList.Location = new System.Drawing.Point(56, 3);
            this.cbScreenList.Name = "cbScreenList";
            this.cbScreenList.Size = new System.Drawing.Size(121, 21);
            this.cbScreenList.TabIndex = 1;
            // 
            // btnIdentifyScreens
            // 
            this.btnIdentifyScreens.AutoSize = true;
            this.btnIdentifyScreens.Location = new System.Drawing.Point(183, 3);
            this.btnIdentifyScreens.Name = "btnIdentifyScreens";
            this.btnIdentifyScreens.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnIdentifyScreens.Size = new System.Drawing.Size(102, 23);
            this.btnIdentifyScreens.TabIndex = 3;
            this.btnIdentifyScreens.Text = "Identify Screens";
            this.btnIdentifyScreens.UseVisualStyleBackColor = true;
            this.btnIdentifyScreens.Click += new System.EventHandler(this.btnIdentifyScreens_Click);
            // 
            // gboxTooltipOptions
            // 
            this.gboxTooltipOptions.Controls.Add(this.cbTooltipOptionName);
            this.gboxTooltipOptions.Controls.Add(this.cbTooltipOptionSkill);
            this.gboxTooltipOptions.Controls.Add(this.cbTooltipOptionDate);
            this.gboxTooltipOptions.Controls.Add(this.cbTooltipOptionETA);
            this.gboxTooltipOptions.Location = new System.Drawing.Point(3, 178);
            this.gboxTooltipOptions.Name = "gboxTooltipOptions";
            this.gboxTooltipOptions.Size = new System.Drawing.Size(359, 113);
            this.gboxTooltipOptions.TabIndex = 7;
            this.gboxTooltipOptions.TabStop = false;
            this.gboxTooltipOptions.Text = "Tray Icon Tooltip";
            // 
            // cbTooltipOptionName
            // 
            this.cbTooltipOptionName.AutoSize = true;
            this.cbTooltipOptionName.Location = new System.Drawing.Point(14, 19);
            this.cbTooltipOptionName.Name = "cbTooltipOptionName";
            this.cbTooltipOptionName.Size = new System.Drawing.Size(104, 17);
            this.cbTooltipOptionName.TabIndex = 5;
            this.cbTooltipOptionName.Text = "Character Name";
            this.cbTooltipOptionName.UseVisualStyleBackColor = true;
            // 
            // cbTooltipOptionSkill
            // 
            this.cbTooltipOptionSkill.AutoSize = true;
            this.cbTooltipOptionSkill.Location = new System.Drawing.Point(14, 42);
            this.cbTooltipOptionSkill.Name = "cbTooltipOptionSkill";
            this.cbTooltipOptionSkill.Size = new System.Drawing.Size(102, 17);
            this.cbTooltipOptionSkill.TabIndex = 4;
            this.cbTooltipOptionSkill.Text = "Skill Name/Level";
            this.cbTooltipOptionSkill.UseVisualStyleBackColor = true;
            // 
            // cbTooltipOptionDate
            // 
            this.cbTooltipOptionDate.AutoSize = true;
            this.cbTooltipOptionDate.Location = new System.Drawing.Point(14, 88);
            this.cbTooltipOptionDate.Name = "cbTooltipOptionDate";
            this.cbTooltipOptionDate.Size = new System.Drawing.Size(118, 17);
            this.cbTooltipOptionDate.TabIndex = 3;
            this.cbTooltipOptionDate.Text = "Date of Completion";
            this.cbTooltipOptionDate.UseVisualStyleBackColor = true;
            // 
            // cbTooltipOptionETA
            // 
            this.cbTooltipOptionETA.AutoSize = true;
            this.cbTooltipOptionETA.Checked = true;
            this.cbTooltipOptionETA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTooltipOptionETA.Location = new System.Drawing.Point(14, 65);
            this.cbTooltipOptionETA.Name = "cbTooltipOptionETA";
            this.cbTooltipOptionETA.Size = new System.Drawing.Size(117, 17);
            this.cbTooltipOptionETA.TabIndex = 2;
            this.cbTooltipOptionETA.Text = "Time to Completion";
            this.cbTooltipOptionETA.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.flowLayoutPanel16);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(388, 468);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Look And Feel";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel16
            // 
            this.flowLayoutPanel16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel16.Controls.Add(this.groupBox11);
            this.flowLayoutPanel16.Controls.Add(this.gbSkillPlannerHighlighting);
            this.flowLayoutPanel16.Controls.Add(this.groupBox10);
            this.flowLayoutPanel16.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel16.Location = new System.Drawing.Point(6, 6);
            this.flowLayoutPanel16.Name = "flowLayoutPanel16";
            this.flowLayoutPanel16.Size = new System.Drawing.Size(376, 456);
            this.flowLayoutPanel16.TabIndex = 10;
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.AutoSize = true;
            this.groupBox11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox11.Controls.Add(this.flowLayoutPanel15);
            this.groupBox11.Location = new System.Drawing.Point(3, 3);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(362, 70);
            this.groupBox11.TabIndex = 10;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "General";
            // 
            // flowLayoutPanel15
            // 
            this.flowLayoutPanel15.AutoSize = true;
            this.flowLayoutPanel15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel15.Controls.Add(this.cbWorksafeMode);
            this.flowLayoutPanel15.Controls.Add(this.flowLayoutPanel21);
            this.flowLayoutPanel15.Controls.Add(this.flowLayoutPanel22);
            this.flowLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel15.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel15.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel15.Name = "flowLayoutPanel15";
            this.flowLayoutPanel15.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel15.Size = new System.Drawing.Size(356, 50);
            this.flowLayoutPanel15.TabIndex = 3;
            // 
            // cbWorksafeMode
            // 
            this.cbWorksafeMode.AutoSize = true;
            this.cbWorksafeMode.Location = new System.Drawing.Point(12, 3);
            this.cbWorksafeMode.Name = "cbWorksafeMode";
            this.cbWorksafeMode.Size = new System.Drawing.Size(271, 17);
            this.cbWorksafeMode.TabIndex = 6;
            this.cbWorksafeMode.Text = "Run in \"safe for work\" mode (no portraits or colors)";
            this.cbWorksafeMode.UseVisualStyleBackColor = true;
            this.cbWorksafeMode.CheckedChanged += new System.EventHandler(this.cbWorksafeMode_CheckedChanged);
            // 
            // flowLayoutPanel21
            // 
            this.flowLayoutPanel21.AutoSize = true;
            this.flowLayoutPanel21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel21.Enabled = false;
            this.flowLayoutPanel21.Location = new System.Drawing.Point(34, 23);
            this.flowLayoutPanel21.Margin = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.flowLayoutPanel21.Name = "flowLayoutPanel21";
            this.flowLayoutPanel21.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel21.TabIndex = 8;
            this.flowLayoutPanel21.WrapContents = false;
            // 
            // flowLayoutPanel22
            // 
            this.flowLayoutPanel22.AutoSize = true;
            this.flowLayoutPanel22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel22.Controls.Add(this.cbTitleToTime);
            this.flowLayoutPanel22.Controls.Add(this.cbWindowsTitleList);
            this.flowLayoutPanel22.Location = new System.Drawing.Point(9, 23);
            this.flowLayoutPanel22.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel22.Name = "flowLayoutPanel22";
            this.flowLayoutPanel22.Size = new System.Drawing.Size(338, 27);
            this.flowLayoutPanel22.TabIndex = 2;
            this.flowLayoutPanel22.WrapContents = false;
            // 
            // cbTitleToTime
            // 
            this.cbTitleToTime.AutoSize = true;
            this.cbTitleToTime.Location = new System.Drawing.Point(3, 3);
            this.cbTitleToTime.Name = "cbTitleToTime";
            this.cbTitleToTime.Size = new System.Drawing.Size(102, 17);
            this.cbTitleToTime.TabIndex = 6;
            this.cbTitleToTime.Text = "Set titlebar to : ";
            this.cbTitleToTime.UseVisualStyleBackColor = true;
            // 
            // cbWindowsTitleList
            // 
            this.cbWindowsTitleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWindowsTitleList.FormattingEnabled = true;
            this.cbWindowsTitleList.Items.AddRange(new object[] {
            "single character - finishing skill next",
            "single character - selected character",
            "multi character - finishing skill next first",
            "multi character - selected character first "});
            this.cbWindowsTitleList.Location = new System.Drawing.Point(111, 3);
            this.cbWindowsTitleList.Name = "cbWindowsTitleList";
            this.cbWindowsTitleList.Size = new System.Drawing.Size(224, 21);
            this.cbWindowsTitleList.TabIndex = 1;
            // 
            // gbSkillPlannerHighlighting
            // 
            this.gbSkillPlannerHighlighting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSkillPlannerHighlighting.AutoSize = true;
            this.gbSkillPlannerHighlighting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbSkillPlannerHighlighting.Controls.Add(this.flowLayoutPanel13);
            this.gbSkillPlannerHighlighting.Location = new System.Drawing.Point(3, 79);
            this.gbSkillPlannerHighlighting.Name = "gbSkillPlannerHighlighting";
            this.gbSkillPlannerHighlighting.Size = new System.Drawing.Size(362, 66);
            this.gbSkillPlannerHighlighting.TabIndex = 11;
            this.gbSkillPlannerHighlighting.TabStop = false;
            this.gbSkillPlannerHighlighting.Text = "Skill Planner";
            // 
            // flowLayoutPanel13
            // 
            this.flowLayoutPanel13.AutoSize = true;
            this.flowLayoutPanel13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel13.Controls.Add(this.cbHighlightPlannedSkills);
            this.flowLayoutPanel13.Controls.Add(this.flowLayoutPanel14);
            this.flowLayoutPanel13.Controls.Add(this.cbHighlightPrerequisites);
            this.flowLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel13.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel13.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel13.Name = "flowLayoutPanel13";
            this.flowLayoutPanel13.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel13.Size = new System.Drawing.Size(356, 46);
            this.flowLayoutPanel13.TabIndex = 1;
            this.flowLayoutPanel13.WrapContents = false;
            // 
            // cbHighlightPlannedSkills
            // 
            this.cbHighlightPlannedSkills.AutoSize = true;
            this.cbHighlightPlannedSkills.Location = new System.Drawing.Point(12, 3);
            this.cbHighlightPlannedSkills.Name = "cbHighlightPlannedSkills";
            this.cbHighlightPlannedSkills.Size = new System.Drawing.Size(142, 17);
            this.cbHighlightPlannedSkills.TabIndex = 0;
            this.cbHighlightPlannedSkills.Text = "Emphasize Planned Skills";
            this.cbHighlightPlannedSkills.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel14
            // 
            this.flowLayoutPanel14.AutoSize = true;
            this.flowLayoutPanel14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel14.Enabled = false;
            this.flowLayoutPanel14.Location = new System.Drawing.Point(34, 23);
            this.flowLayoutPanel14.Margin = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.flowLayoutPanel14.Name = "flowLayoutPanel14";
            this.flowLayoutPanel14.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel14.TabIndex = 7;
            this.flowLayoutPanel14.WrapContents = false;
            // 
            // cbHighlightPrerequisites
            // 
            this.cbHighlightPrerequisites.AutoSize = true;
            this.cbHighlightPrerequisites.Location = new System.Drawing.Point(12, 26);
            this.cbHighlightPrerequisites.Name = "cbHighlightPrerequisites";
            this.cbHighlightPrerequisites.Size = new System.Drawing.Size(136, 17);
            this.cbHighlightPrerequisites.TabIndex = 8;
            this.cbHighlightPrerequisites.Text = "Highlight Pre-requisites";
            this.cbHighlightPrerequisites.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.AutoSize = true;
            this.groupBox10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox10.Controls.Add(this.tableLayoutPanel4);
            this.groupBox10.Location = new System.Drawing.Point(3, 151);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(362, 203);
            this.groupBox10.TabIndex = 13;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Skill Browser Icon Set";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tvlist, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.cbSkillIconSet, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(356, 183);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // tvlist
            // 
            this.tvlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvlist.Location = new System.Drawing.Point(3, 30);
            this.tvlist.Name = "tvlist";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Node2";
            treeNode3.Name = "Node3";
            treeNode3.Text = "Node3";
            treeNode4.Name = "Node4";
            treeNode4.Text = "Node4";
            treeNode5.Name = "Node5";
            treeNode5.Text = "Node5";
            treeNode6.Name = "Node6";
            treeNode6.Text = "Node6";
            treeNode7.Name = "Node7";
            treeNode7.Text = "Node7";
            treeNode8.Name = "Node8";
            treeNode8.Text = "Node8";
            treeNode9.Name = "Node0";
            treeNode9.Text = "Node0";
            this.tvlist.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9});
            this.tvlist.Size = new System.Drawing.Size(350, 150);
            this.tvlist.TabIndex = 9;
            // 
            // cbSkillIconSet
            // 
            this.cbSkillIconSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSkillIconSet.FormattingEnabled = true;
            this.cbSkillIconSet.Location = new System.Drawing.Point(3, 3);
            this.cbSkillIconSet.Name = "cbSkillIconSet";
            this.cbSkillIconSet.Size = new System.Drawing.Size(350, 21);
            this.cbSkillIconSet.TabIndex = 3;
            this.cbSkillIconSet.SelectedIndexChanged += new System.EventHandler(this.cbSkillIconSet_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.verticalFlowPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(388, 468);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Network";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // verticalFlowPanel3
            // 
            this.verticalFlowPanel3.Controls.Add(this.groupBox6);
            this.verticalFlowPanel3.Controls.Add(this.groupBox2);
            this.verticalFlowPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel3.Location = new System.Drawing.Point(3, 3);
            this.verticalFlowPanel3.Name = "verticalFlowPanel3";
            this.verticalFlowPanel3.Size = new System.Drawing.Size(382, 462);
            this.verticalFlowPanel3.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.AutoSize = true;
            this.groupBox6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox6.Controls.Add(this.verticalFlowPanel4);
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(365, 141);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Proxy Server Settings";
            // 
            // verticalFlowPanel4
            // 
            this.verticalFlowPanel4.AutoSize = true;
            this.verticalFlowPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.verticalFlowPanel4.Controls.Add(this.rbDefaultProxy);
            this.verticalFlowPanel4.Controls.Add(this.rbCustomProxy);
            this.verticalFlowPanel4.Controls.Add(this.vfpCustomProxySettings);
            this.verticalFlowPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel4.Location = new System.Drawing.Point(3, 17);
            this.verticalFlowPanel4.Name = "verticalFlowPanel4";
            this.verticalFlowPanel4.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.verticalFlowPanel4.Size = new System.Drawing.Size(359, 121);
            this.verticalFlowPanel4.TabIndex = 1;
            // 
            // rbDefaultProxy
            // 
            this.rbDefaultProxy.AutoSize = true;
            this.rbDefaultProxy.Location = new System.Drawing.Point(13, 3);
            this.rbDefaultProxy.Name = "rbDefaultProxy";
            this.rbDefaultProxy.Size = new System.Drawing.Size(248, 17);
            this.rbDefaultProxy.TabIndex = 2;
            this.rbDefaultProxy.TabStop = true;
            this.rbDefaultProxy.Text = "Use system default proxy (from Control Panel)";
            this.rbDefaultProxy.UseVisualStyleBackColor = true;
            // 
            // rbCustomProxy
            // 
            this.rbCustomProxy.AutoSize = true;
            this.rbCustomProxy.Location = new System.Drawing.Point(13, 26);
            this.rbCustomProxy.Name = "rbCustomProxy";
            this.rbCustomProxy.Size = new System.Drawing.Size(156, 17);
            this.rbCustomProxy.TabIndex = 3;
            this.rbCustomProxy.TabStop = true;
            this.rbCustomProxy.Text = "Use custom proxy settings:";
            this.rbCustomProxy.UseVisualStyleBackColor = true;
            this.rbCustomProxy.CheckedChanged += new System.EventHandler(this.rbCustomProxy_CheckedChanged);
            // 
            // vfpCustomProxySettings
            // 
            this.vfpCustomProxySettings.AutoSize = true;
            this.vfpCustomProxySettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.vfpCustomProxySettings.Controls.Add(this.label13);
            this.vfpCustomProxySettings.Controls.Add(this.tableLayoutPanel3);
            this.vfpCustomProxySettings.Enabled = false;
            this.vfpCustomProxySettings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.vfpCustomProxySettings.Location = new System.Drawing.Point(35, 49);
            this.vfpCustomProxySettings.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.vfpCustomProxySettings.Name = "vfpCustomProxySettings";
            this.vfpCustomProxySettings.Size = new System.Drawing.Size(311, 69);
            this.vfpCustomProxySettings.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(305, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Please enter the server and port to use as your proxy server:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label10, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbProxyHttpHost, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label11, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label12, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnProxyHttpAuth, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbProxyHttpPort, 3, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(304, 50);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 27);
            this.label10.TabIndex = 0;
            this.label10.Text = "HTTP:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbProxyHttpHost
            // 
            this.tbProxyHttpHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tbProxyHttpHost.Location = new System.Drawing.Point(45, 26);
            this.tbProxyHttpHost.Name = "tbProxyHttpHost";
            this.tbProxyHttpHost.Size = new System.Drawing.Size(107, 21);
            this.tbProxyHttpHost.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(158, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Port";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(45, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Host/IP Address";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnProxyHttpAuth
            // 
            this.btnProxyHttpAuth.AutoSize = true;
            this.btnProxyHttpAuth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProxyHttpAuth.Location = new System.Drawing.Point(202, 26);
            this.btnProxyHttpAuth.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnProxyHttpAuth.Name = "btnProxyHttpAuth";
            this.btnProxyHttpAuth.Size = new System.Drawing.Size(99, 23);
            this.btnProxyHttpAuth.TabIndex = 5;
            this.btnProxyHttpAuth.Text = "Authentication...";
            this.btnProxyHttpAuth.UseVisualStyleBackColor = true;
            this.btnProxyHttpAuth.Click += new System.EventHandler(this.btnProxyHttpAuth_Click);
            // 
            // tbProxyHttpPort
            // 
            this.tbProxyHttpPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tbProxyHttpPort.Location = new System.Drawing.Point(158, 26);
            this.tbProxyHttpPort.Name = "tbProxyHttpPort";
            this.tbProxyHttpPort.Size = new System.Drawing.Size(38, 21);
            this.tbProxyHttpPort.TabIndex = 2;
            this.tbProxyHttpPort.TextChanged += new System.EventHandler(this.tbProxyHttpPort_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.verticalFlowPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(388, 468);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Alerts";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // verticalFlowPanel2
            // 
            this.verticalFlowPanel2.AutoSize = true;
            this.verticalFlowPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.verticalFlowPanel2.Controls.Add(this.groupBox4);
            this.verticalFlowPanel2.Controls.Add(this.groupBox3);
            this.verticalFlowPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalFlowPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.verticalFlowPanel2.Location = new System.Drawing.Point(3, 3);
            this.verticalFlowPanel2.Name = "verticalFlowPanel2";
            this.verticalFlowPanel2.Size = new System.Drawing.Size(382, 462);
            this.verticalFlowPanel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.AutoSize = true;
            this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(this.flowLayoutPanel5);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(368, 89);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Alerts";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.cbShowBalloonTips);
            this.flowLayoutPanel5.Controls.Add(this.cbShowCompletedSkillsDialog);
            this.flowLayoutPanel5.Controls.Add(this.cbPlaySoundOnSkillComplete);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel5.Size = new System.Drawing.Size(362, 69);
            this.flowLayoutPanel5.TabIndex = 1;
            // 
            // cbShowBalloonTips
            // 
            this.cbShowBalloonTips.AutoSize = true;
            this.cbShowBalloonTips.Location = new System.Drawing.Point(12, 3);
            this.cbShowBalloonTips.Name = "cbShowBalloonTips";
            this.cbShowBalloonTips.Size = new System.Drawing.Size(242, 17);
            this.cbShowBalloonTips.TabIndex = 1;
            this.cbShowBalloonTips.Text = "Show balloon tip when skill training completes";
            this.cbShowBalloonTips.UseVisualStyleBackColor = true;
            // 
            // cbShowCompletedSkillsDialog
            // 
            this.cbShowCompletedSkillsDialog.AutoSize = true;
            this.cbShowCompletedSkillsDialog.Location = new System.Drawing.Point(12, 26);
            this.cbShowCompletedSkillsDialog.Name = "cbShowCompletedSkillsDialog";
            this.cbShowCompletedSkillsDialog.Size = new System.Drawing.Size(159, 17);
            this.cbShowCompletedSkillsDialog.TabIndex = 2;
            this.cbShowCompletedSkillsDialog.Text = "Show completed skills dialog";
            this.cbShowCompletedSkillsDialog.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.AutoSize = true;
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.flowLayoutPanel6);
            this.groupBox3.Location = new System.Drawing.Point(3, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(368, 328);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Email Alert";
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.cbSendEmail);
            this.flowLayoutPanel6.Controls.Add(this.cbEmailUseShortFormat);
            this.flowLayoutPanel6.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel6.Controls.Add(this.btnTestEmail);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel6.Size = new System.Drawing.Size(362, 308);
            this.flowLayoutPanel6.TabIndex = 1;
            this.flowLayoutPanel6.WrapContents = false;
            // 
            // cbEmailUseShortFormat
            // 
            this.cbEmailUseShortFormat.AutoSize = true;
            this.cbEmailUseShortFormat.Location = new System.Drawing.Point(12, 26);
            this.cbEmailUseShortFormat.Name = "cbEmailUseShortFormat";
            this.cbEmailUseShortFormat.Size = new System.Drawing.Size(186, 17);
            this.cbEmailUseShortFormat.TabIndex = 2;
            this.cbEmailUseShortFormat.Text = " Use Short Format (SMS-Friendly)";
            this.cbEmailUseShortFormat.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.flowLayoutPanel11);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(388, 468);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Updates";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel11
            // 
            this.flowLayoutPanel11.AutoSize = true;
            this.flowLayoutPanel11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel11.Controls.Add(this.groupBox8);
            this.flowLayoutPanel11.Controls.Add(this.groupBox9);
            this.flowLayoutPanel11.Controls.Add(this.groupBox7);
            this.flowLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel11.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel11.Name = "flowLayoutPanel11";
            this.flowLayoutPanel11.Size = new System.Drawing.Size(382, 462);
            this.flowLayoutPanel11.TabIndex = 9;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.AutoSize = true;
            this.groupBox8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox8.Controls.Add(this.flowLayoutPanel10);
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(222, 60);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Version Checking";
            // 
            // flowLayoutPanel10
            // 
            this.flowLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel10.AutoSize = true;
            this.flowLayoutPanel10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel10.Controls.Add(this.cbAutomaticallySearchForNewVersions);
            this.flowLayoutPanel10.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel10.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.flowLayoutPanel10.Size = new System.Drawing.Size(199, 23);
            this.flowLayoutPanel10.TabIndex = 0;
            // 
            // cbAutomaticallySearchForNewVersions
            // 
            this.cbAutomaticallySearchForNewVersions.AutoSize = true;
            this.cbAutomaticallySearchForNewVersions.Location = new System.Drawing.Point(3, 3);
            this.cbAutomaticallySearchForNewVersions.Name = "cbAutomaticallySearchForNewVersions";
            this.cbAutomaticallySearchForNewVersions.Size = new System.Drawing.Size(193, 17);
            this.cbAutomaticallySearchForNewVersions.TabIndex = 0;
            this.cbAutomaticallySearchForNewVersions.Text = "Disable automatic EVEMon updates";
            this.cbAutomaticallySearchForNewVersions.UseVisualStyleBackColor = true;
            this.cbAutomaticallySearchForNewVersions.CheckedChanged += new System.EventHandler(this.cbAutomaticallySearchForNewVersions_CheckedChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.AutoSize = true;
            this.groupBox9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox9.Controls.Add(this.flowLayoutPanel20);
            this.groupBox9.Controls.Add(this.flowLayoutPanel19);
            this.groupBox9.Controls.Add(this.flowLayoutPanel18);
            this.groupBox9.Controls.Add(this.flowLayoutPanel17);
            this.groupBox9.Controls.Add(this.flowLayoutPanel12);
            this.groupBox9.Location = new System.Drawing.Point(3, 69);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(222, 118);
            this.groupBox9.TabIndex = 9;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "XML Update";
            // 
            // flowLayoutPanel20
            // 
            this.flowLayoutPanel20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel20.AutoSize = true;
            this.flowLayoutPanel20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel20.Controls.Add(this.cbKeepCharacterPlans);
            this.flowLayoutPanel20.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel20.Location = new System.Drawing.Point(3, 75);
            this.flowLayoutPanel20.Name = "flowLayoutPanel20";
            this.flowLayoutPanel20.Size = new System.Drawing.Size(133, 23);
            this.flowLayoutPanel20.TabIndex = 4;
            // 
            // cbKeepCharacterPlans
            // 
            this.cbKeepCharacterPlans.AutoSize = true;
            this.cbKeepCharacterPlans.Location = new System.Drawing.Point(3, 3);
            this.cbKeepCharacterPlans.Name = "cbKeepCharacterPlans";
            this.cbKeepCharacterPlans.Size = new System.Drawing.Size(127, 17);
            this.cbKeepCharacterPlans.TabIndex = 1;
            this.cbKeepCharacterPlans.Text = "Keep character plans";
            this.cbKeepCharacterPlans.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel19
            // 
            this.flowLayoutPanel19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel19.AutoSize = true;
            this.flowLayoutPanel19.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel19.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel19.Location = new System.Drawing.Point(45, 33);
            this.flowLayoutPanel19.Name = "flowLayoutPanel19";
            this.flowLayoutPanel19.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel19.TabIndex = 3;
            // 
            // flowLayoutPanel18
            // 
            this.flowLayoutPanel18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel18.AutoSize = true;
            this.flowLayoutPanel18.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel18.Controls.Add(this.cbDeleteCharactersSilently);
            this.flowLayoutPanel18.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel18.Location = new System.Drawing.Point(3, 46);
            this.flowLayoutPanel18.Name = "flowLayoutPanel18";
            this.flowLayoutPanel18.Size = new System.Drawing.Size(153, 23);
            this.flowLayoutPanel18.TabIndex = 2;
            // 
            // cbDeleteCharactersSilently
            // 
            this.cbDeleteCharactersSilently.AutoSize = true;
            this.cbDeleteCharactersSilently.Location = new System.Drawing.Point(3, 3);
            this.cbDeleteCharactersSilently.Name = "cbDeleteCharactersSilently";
            this.cbDeleteCharactersSilently.Size = new System.Drawing.Size(147, 17);
            this.cbDeleteCharactersSilently.TabIndex = 1;
            this.cbDeleteCharactersSilently.Text = "Delete characters silently";
            this.cbDeleteCharactersSilently.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel17
            // 
            this.flowLayoutPanel17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel17.AutoSize = true;
            this.flowLayoutPanel17.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel17.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel17.Location = new System.Drawing.Point(14, 19);
            this.flowLayoutPanel17.Name = "flowLayoutPanel17";
            this.flowLayoutPanel17.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel17.TabIndex = 1;
            // 
            // flowLayoutPanel12
            // 
            this.flowLayoutPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel12.AutoSize = true;
            this.flowLayoutPanel12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel12.Controls.Add(this.cbAutomaticEOSkillUpdate);
            this.flowLayoutPanel12.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel12.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel12.Name = "flowLayoutPanel12";
            this.flowLayoutPanel12.Size = new System.Drawing.Size(194, 23);
            this.flowLayoutPanel12.TabIndex = 0;
            // 
            // cbAutomaticEOSkillUpdate
            // 
            this.cbAutomaticEOSkillUpdate.AutoSize = true;
            this.cbAutomaticEOSkillUpdate.Location = new System.Drawing.Point(3, 3);
            this.cbAutomaticEOSkillUpdate.Name = "cbAutomaticEOSkillUpdate";
            this.cbAutomaticEOSkillUpdate.Size = new System.Drawing.Size(188, 17);
            this.cbAutomaticEOSkillUpdate.TabIndex = 1;
            this.cbAutomaticEOSkillUpdate.Text = "Disable automatic skill XML update";
            this.cbAutomaticEOSkillUpdate.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.AutoSize = true;
            this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox7.Controls.Add(this.cbCheckTranquilityStatus);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.numericStatusInterval);
            this.groupBox7.Location = new System.Drawing.Point(3, 193);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(222, 79);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Tranquility Status";
            // 
            // cbCheckTranquilityStatus
            // 
            this.cbCheckTranquilityStatus.AutoSize = true;
            this.cbCheckTranquilityStatus.Location = new System.Drawing.Point(6, 20);
            this.cbCheckTranquilityStatus.Name = "cbCheckTranquilityStatus";
            this.cbCheckTranquilityStatus.Size = new System.Drawing.Size(159, 17);
            this.cbCheckTranquilityStatus.TabIndex = 3;
            this.cbCheckTranquilityStatus.Text = "Check for Tranquility Status";
            this.cbCheckTranquilityStatus.UseVisualStyleBackColor = true;
            this.cbCheckTranquilityStatus.CheckedChanged += new System.EventHandler(this.cbCheckTranquilityStatus_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Update Status Every";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(172, 40);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "minutes";
            // 
            // numericStatusInterval
            // 
            this.numericStatusInterval.Location = new System.Drawing.Point(119, 38);
            this.numericStatusInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericStatusInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericStatusInterval.Name = "numericStatusInterval";
            this.numericStatusInterval.Size = new System.Drawing.Size(47, 21);
            this.numericStatusInterval.TabIndex = 0;
            this.numericStatusInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.flowLayoutPanel23);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(388, 468);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Logitech G15";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel23
            // 
            this.flowLayoutPanel23.Controls.Add(this.groupBox12);
            this.flowLayoutPanel23.Controls.Add(this.groupBox13);
            this.flowLayoutPanel23.Controls.Add(this.groupBox14);
            this.flowLayoutPanel23.Controls.Add(this.g15Preview);
            this.flowLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel23.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel23.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel23.Name = "flowLayoutPanel23";
            this.flowLayoutPanel23.Size = new System.Drawing.Size(382, 462);
            this.flowLayoutPanel23.TabIndex = 0;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label21);
            this.groupBox12.Controls.Add(this.nmUpdateSpd);
            this.groupBox12.Controls.Add(this.label22);
            this.groupBox12.Controls.Add(this.chkG15Enabled);
            this.groupBox12.Location = new System.Drawing.Point(3, 3);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(363, 70);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "G15 Status";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(202, 45);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Milliseconds";
            // 
            // nmUpdateSpd
            // 
            this.nmUpdateSpd.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nmUpdateSpd.Location = new System.Drawing.Point(132, 43);
            this.nmUpdateSpd.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmUpdateSpd.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nmUpdateSpd.Name = "nmUpdateSpd";
            this.nmUpdateSpd.Size = new System.Drawing.Size(64, 21);
            this.nmUpdateSpd.TabIndex = 2;
            this.nmUpdateSpd.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 45);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(133, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Advanced: Update speed:";
            // 
            // chkG15Enabled
            // 
            this.chkG15Enabled.AutoSize = true;
            this.chkG15Enabled.Location = new System.Drawing.Point(6, 20);
            this.chkG15Enabled.Name = "chkG15Enabled";
            this.chkG15Enabled.Size = new System.Drawing.Size(164, 17);
            this.chkG15Enabled.TabIndex = 0;
            this.chkG15Enabled.Text = "Enable Logitech G15 Support";
            this.chkG15Enabled.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label17);
            this.groupBox13.Controls.Add(this.label16);
            this.groupBox13.Controls.Add(this.nmSCycle);
            this.groupBox13.Controls.Add(this.chkSDisplay);
            this.groupBox13.Location = new System.Drawing.Point(3, 79);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(363, 71);
            this.groupBox13.TabIndex = 1;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Skill Display";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(140, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Seconds";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 40);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Cycle every";
            // 
            // nmSCycle
            // 
            this.nmSCycle.DecimalPlaces = 2;
            this.nmSCycle.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nmSCycle.Location = new System.Drawing.Point(76, 38);
            this.nmSCycle.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nmSCycle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmSCycle.Name = "nmSCycle";
            this.nmSCycle.Size = new System.Drawing.Size(58, 21);
            this.nmSCycle.TabIndex = 1;
            this.nmSCycle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkSDisplay
            // 
            this.chkSDisplay.AutoSize = true;
            this.chkSDisplay.Location = new System.Drawing.Point(6, 20);
            this.chkSDisplay.Name = "chkSDisplay";
            this.chkSDisplay.Size = new System.Drawing.Size(107, 17);
            this.chkSDisplay.TabIndex = 0;
            this.chkSDisplay.Text = "Cycle skill display";
            this.chkSDisplay.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label20);
            this.groupBox14.Controls.Add(this.label19);
            this.groupBox14.Controls.Add(this.nmCCycle);
            this.groupBox14.Controls.Add(this.chkCDisplay);
            this.groupBox14.Location = new System.Drawing.Point(3, 156);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(363, 71);
            this.groupBox14.TabIndex = 1;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Character Display";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(140, 40);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Seconds";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 40);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "Cycle every";
            // 
            // nmCCycle
            // 
            this.nmCCycle.DecimalPlaces = 2;
            this.nmCCycle.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nmCCycle.Location = new System.Drawing.Point(76, 38);
            this.nmCCycle.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nmCCycle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmCCycle.Name = "nmCCycle";
            this.nmCCycle.Size = new System.Drawing.Size(58, 21);
            this.nmCCycle.TabIndex = 1;
            this.nmCCycle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkCDisplay
            // 
            this.chkCDisplay.AutoSize = true;
            this.chkCDisplay.Location = new System.Drawing.Point(6, 20);
            this.chkCDisplay.Name = "chkCDisplay";
            this.chkCDisplay.Size = new System.Drawing.Size(137, 17);
            this.chkCDisplay.TabIndex = 0;
            this.chkCDisplay.Text = "Cycle character display";
            this.chkCDisplay.UseVisualStyleBackColor = true;
            // 
            // g15Preview
            // 
            this.g15Preview.Controls.Add(this.flowLayoutPanel26);
            this.g15Preview.Location = new System.Drawing.Point(3, 233);
            this.g15Preview.Name = "g15Preview";
            this.g15Preview.Size = new System.Drawing.Size(363, 141);
            this.g15Preview.TabIndex = 2;
            this.g15Preview.TabStop = false;
            this.g15Preview.Text = "G15 Preview";
            // 
            // flowLayoutPanel26
            // 
            this.flowLayoutPanel26.Controls.Add(this.pbg15Preview);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel25);
            this.flowLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel26.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel26.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel26.Name = "flowLayoutPanel26";
            this.flowLayoutPanel26.Size = new System.Drawing.Size(357, 121);
            this.flowLayoutPanel26.TabIndex = 0;
            // 
            // pbg15Preview
            // 
            this.pbg15Preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbg15Preview.Location = new System.Drawing.Point(3, 3);
            this.pbg15Preview.Name = "pbg15Preview";
            this.pbg15Preview.Size = new System.Drawing.Size(320, 86);
            this.pbg15Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbg15Preview.TabIndex = 0;
            this.pbg15Preview.TabStop = false;
            // 
            // flowLayoutPanel25
            // 
            this.flowLayoutPanel25.Controls.Add(this.button1);
            this.flowLayoutPanel25.Controls.Add(this.button2);
            this.flowLayoutPanel25.Controls.Add(this.button3);
            this.flowLayoutPanel25.Controls.Add(this.button4);
            this.flowLayoutPanel25.Location = new System.Drawing.Point(3, 95);
            this.flowLayoutPanel25.Name = "flowLayoutPanel25";
            this.flowLayoutPanel25.Size = new System.Drawing.Size(320, 22);
            this.flowLayoutPanel25.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 15);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(83, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 15);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(163, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 15);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(243, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 15);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.AutoSize = true;
            this.flowLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel7.Controls.Add(this.tabControl1);
            this.flowLayoutPanel7.Controls.Add(this.flowLayoutPanel8);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(404, 574);
            this.flowLayoutPanel7.TabIndex = 1;
            this.flowLayoutPanel7.WrapContents = false;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel8.AutoSize = true;
            this.flowLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel8.Controls.Add(this.btnOk);
            this.flowLayoutPanel8.Controls.Add(this.btnCancel);
            this.flowLayoutPanel8.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(237, 500);
            this.flowLayoutPanel8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(162, 29);
            this.flowLayoutPanel8.TabIndex = 1;
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.AutoSize = true;
            this.flowLayoutPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel9.Controls.Add(this.checkBox1);
            this.flowLayoutPanel9.Controls.Add(this.checkBox2);
            this.flowLayoutPanel9.Controls.Add(this.checkBox3);
            this.flowLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel9.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel9.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel9.Size = new System.Drawing.Size(356, 70);
            this.flowLayoutPanel9.TabIndex = 3;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Minimize to Tray";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 26);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(171, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Set window title to training time";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(12, 49);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(264, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Run in \"safe for work\" mode (no portraits or colors)";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // chName
            // 
            this.chName.Text = "Sample";
            // 
            // tmrG15Update
            // 
            this.tmrG15Update.Enabled = true;
            this.tmrG15Update.Interval = 666;
            this.tmrG15Update.Tick += new System.EventHandler(this.tmrG15Update_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.flowLayoutPanel3);
            this.groupBox2.Location = new System.Drawing.Point(3, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IGB Mini-server";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.cbRunIGBServer);
            this.flowLayoutPanel3.Controls.Add(this.cbIGBPublic);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel27);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.flowLayoutPanel3.Size = new System.Drawing.Size(359, 80);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // cbRunIGBServer
            // 
            this.cbRunIGBServer.AutoSize = true;
            this.cbRunIGBServer.Location = new System.Drawing.Point(12, 3);
            this.cbRunIGBServer.Name = "cbRunIGBServer";
            this.cbRunIGBServer.Size = new System.Drawing.Size(217, 17);
            this.cbRunIGBServer.TabIndex = 3;
            this.cbRunIGBServer.Text = "Run IGB Mini-server on http://localhost/";
            this.cbRunIGBServer.UseVisualStyleBackColor = true;
            // 
            // cbIGBPublic
            // 
            this.cbIGBPublic.AutoSize = true;
            this.cbIGBPublic.Location = new System.Drawing.Point(12, 26);
            this.cbIGBPublic.Name = "cbIGBPublic";
            this.cbIGBPublic.Size = new System.Drawing.Size(162, 17);
            this.cbIGBPublic.TabIndex = 4;
            this.cbIGBPublic.Text = "Make IGB Mini-server public?";
            this.cbIGBPublic.UseVisualStyleBackColor = true;
            // 
            // tb_IgbPort
            // 
            this.tb_IgbPort.Location = new System.Drawing.Point(71, 3);
            this.tb_IgbPort.Name = "tb_IgbPort";
            this.tb_IgbPort.Size = new System.Drawing.Size(48, 21);
            this.tb_IgbPort.TabIndex = 5;
            this.tb_IgbPort.Text = "80";
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(3, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(62, 24);
            this.label23.TabIndex = 6;
            this.label23.Text = "IGB Port";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel27
            // 
            this.flowLayoutPanel27.Controls.Add(this.label23);
            this.flowLayoutPanel27.Controls.Add(this.tb_IgbPort);
            this.flowLayoutPanel27.Location = new System.Drawing.Point(12, 49);
            this.flowLayoutPanel27.Name = "flowLayoutPanel27";
            this.flowLayoutPanel27.Size = new System.Drawing.Size(132, 28);
            this.flowLayoutPanel27.TabIndex = 7;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(404, 574);
            this.Controls.Add(this.flowLayoutPanel7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVEMon Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlpEmailSettings.ResumeLayout(false);
            this.tlpEmailSettings.PerformLayout();
            this.tlpEmailAuthTable.ResumeLayout(false);
            this.tlpEmailAuthTable.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.verticalFlowPanel1.ResumeLayout(false);
            this.verticalFlowPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel24.ResumeLayout(false);
            this.flowLayoutPanel24.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flpScreenSelect.ResumeLayout(false);
            this.flpScreenSelect.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.gboxTooltipOptions.ResumeLayout(false);
            this.gboxTooltipOptions.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.flowLayoutPanel16.ResumeLayout(false);
            this.flowLayoutPanel16.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.flowLayoutPanel15.ResumeLayout(false);
            this.flowLayoutPanel15.PerformLayout();
            this.flowLayoutPanel22.ResumeLayout(false);
            this.flowLayoutPanel22.PerformLayout();
            this.gbSkillPlannerHighlighting.ResumeLayout(false);
            this.gbSkillPlannerHighlighting.PerformLayout();
            this.flowLayoutPanel13.ResumeLayout(false);
            this.flowLayoutPanel13.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.verticalFlowPanel3.ResumeLayout(false);
            this.verticalFlowPanel3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.verticalFlowPanel4.ResumeLayout(false);
            this.verticalFlowPanel4.PerformLayout();
            this.vfpCustomProxySettings.ResumeLayout(false);
            this.vfpCustomProxySettings.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.verticalFlowPanel2.ResumeLayout(false);
            this.verticalFlowPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.flowLayoutPanel11.ResumeLayout(false);
            this.flowLayoutPanel11.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.flowLayoutPanel10.ResumeLayout(false);
            this.flowLayoutPanel10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.flowLayoutPanel20.ResumeLayout(false);
            this.flowLayoutPanel20.PerformLayout();
            this.flowLayoutPanel18.ResumeLayout(false);
            this.flowLayoutPanel18.PerformLayout();
            this.flowLayoutPanel12.ResumeLayout(false);
            this.flowLayoutPanel12.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStatusInterval)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.flowLayoutPanel23.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmUpdateSpd)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmSCycle)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCCycle)).EndInit();
            this.g15Preview.ResumeLayout(false);
            this.flowLayoutPanel26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbg15Preview)).EndInit();
            this.flowLayoutPanel25.ResumeLayout(false);
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel27.ResumeLayout(false);
            this.flowLayoutPanel27.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbSendEmail;
        private System.Windows.Forms.TableLayoutPanel tlpEmailSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMailServer;
        private System.Windows.Forms.TextBox tbFromAddress;
        private System.Windows.Forms.TextBox tbToAddress;
        private System.Windows.Forms.Button btnTestEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbEmailServerRequireSsl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbEmailPassword;
        private System.Windows.Forms.TextBox tbEmailUsername;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox cbEmailAuthRequired;
        private System.Windows.Forms.TableLayoutPanel tlpEmailAuthTable;
        private System.Windows.Forms.CheckBox cbPlaySoundOnSkillComplete;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel1;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbRelocateEveWindow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbScreenList;
        private System.Windows.Forms.Button btnIdentifyScreens;
        private System.Windows.Forms.FlowLayoutPanel flpScreenSelect;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.FlowLayoutPanel verticalFlowPanel4;
        private System.Windows.Forms.FlowLayoutPanel vfpCustomProxySettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbProxyHttpPort;
        private System.Windows.Forms.TextBox tbProxyHttpHost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnProxyHttpAuth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton rbCustomProxy;
        private System.Windows.Forms.RadioButton rbDefaultProxy;
        private System.Windows.Forms.CheckBox cbShowBalloonTips;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox cbAutomaticEOSkillUpdate;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.CheckBox cbAutomaticallySearchForNewVersions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel12;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox cbCheckTranquilityStatus;
        private System.Windows.Forms.NumericUpDown numericStatusInterval;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbShowCompletedSkillsDialog;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel16;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel15;
        private System.Windows.Forms.CheckBox cbWorksafeMode;
        private System.Windows.Forms.GroupBox gbSkillPlannerHighlighting;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel13;
        private System.Windows.Forms.CheckBox cbHighlightPlannedSkills;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel14;
        private System.Windows.Forms.CheckBox cbHighlightPrerequisites;
        private System.Windows.Forms.GroupBox gboxTooltipOptions;
        private System.Windows.Forms.CheckBox cbTooltipOptionDate;
        private System.Windows.Forms.CheckBox cbTooltipOptionETA;
        private System.Windows.Forms.CheckBox cbTooltipOptionName;
        private System.Windows.Forms.CheckBox cbTooltipOptionSkill;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TreeView tvlist;
        private System.Windows.Forms.ComboBox cbSkillIconSet;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel17;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel18;
        private System.Windows.Forms.CheckBox cbDeleteCharactersSilently;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel20;
        private System.Windows.Forms.CheckBox cbKeepCharacterPlans;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel19;
        private System.Windows.Forms.TextBox tbPortNumber;
        private System.Windows.Forms.Label lblPortNumber;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox cbCloseToTray;
        private System.Windows.Forms.CheckBox cbRunAtStartup;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel21;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel22;
        private System.Windows.Forms.CheckBox cbTitleToTime;
        private System.Windows.Forms.ComboBox cbWindowsTitleList;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel24;
        private System.Windows.Forms.RadioButton rbSystemTrayOptionsNever;
        private System.Windows.Forms.RadioButton rbSystemTrayOptionsMinimized;
        private System.Windows.Forms.RadioButton rbSystemTrayOptionsAlways;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel23;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.CheckBox chkG15Enabled;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown nmSCycle;
        private System.Windows.Forms.CheckBox chkSDisplay;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown nmCCycle;
        private System.Windows.Forms.CheckBox chkCDisplay;
        private System.Windows.Forms.GroupBox g15Preview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel26;
        private System.Windows.Forms.PictureBox pbg15Preview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel25;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer tmrG15Update;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nmUpdateSpd;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox cbEmailUseShortFormat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox cbRunIGBServer;
        private System.Windows.Forms.CheckBox cbIGBPublic;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel27;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tb_IgbPort;
    }
}
