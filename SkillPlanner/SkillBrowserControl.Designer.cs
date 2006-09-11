namespace EVEMon.SkillPlanner
{
    partial class SkillBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer2 = new EVEMon.SkillPlanner.PersistentSplitContainer();
            this.skillSelectControl = new EVEMon.SkillPlanner.SkillSelectControl();
            this.pnlPlanControl = new System.Windows.Forms.Panel();
            this.lblSkillClass = new System.Windows.Forms.Label();
            this.textboxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPlanSelect = new System.Windows.Forms.ComboBox();
            this.lblAttributes = new System.Windows.Forms.Label();
            this.lblLevel5Time = new System.Windows.Forms.Label();
            this.lblLevel4Time = new System.Windows.Forms.Label();
            this.lblLevel3Time = new System.Windows.Forms.Label();
            this.lblLevel2Time = new System.Windows.Forms.Label();
            this.lblLevel1Time = new System.Windows.Forms.Label();
            this.lblSkillName = new System.Windows.Forms.Label();
            this.skillTreeDisplay = new EVEMon.SkillPlanner.SkillTreeDisplay();
            this.cmsSkillContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPlanTo1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miCancelPlanMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.miCancelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miCancelThis = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrSkillTick = new System.Windows.Forms.Timer(this.components);
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnlPlanControl.SuspendLayout();
            this.cmsSkillContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.skillSelectControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pnlPlanControl);
            this.splitContainer2.Panel2.Controls.Add(this.skillTreeDisplay);
            this.splitContainer2.RememberDistanceKey = null;
            this.splitContainer2.Size = new System.Drawing.Size(867, 508);
            this.splitContainer2.SplitterDistance = 163;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 1;
            // 
            // skillSelectControl
            // 
            this.skillSelectControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.skillSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillSelectControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skillSelectControl.GrandCharacterInfo = null;
            this.skillSelectControl.Location = new System.Drawing.Point(0, 0);
            this.skillSelectControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.skillSelectControl.Name = "skillSelectControl";
            this.skillSelectControl.Plan = null;
            this.skillSelectControl.Size = new System.Drawing.Size(163, 508);
            this.skillSelectControl.TabIndex = 0;
            this.skillSelectControl.SelectedSkillChanged += new System.EventHandler<System.EventArgs>(this.skillSelectControl_SelectedSkillChanged);
            // 
            // pnlPlanControl
            // 
            this.pnlPlanControl.Controls.Add(this.lblSkillClass);
            this.pnlPlanControl.Controls.Add(this.textboxDescription);
            this.pnlPlanControl.Controls.Add(this.label1);
            this.pnlPlanControl.Controls.Add(this.cbPlanSelect);
            this.pnlPlanControl.Controls.Add(this.lblAttributes);
            this.pnlPlanControl.Controls.Add(this.lblLevel5Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel4Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel3Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel2Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel1Time);
            this.pnlPlanControl.Controls.Add(this.lblSkillName);
            this.pnlPlanControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPlanControl.Location = new System.Drawing.Point(0, 0);
            this.pnlPlanControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPlanControl.Name = "pnlPlanControl";
            this.pnlPlanControl.Size = new System.Drawing.Size(699, 130);
            this.pnlPlanControl.TabIndex = 3;
            this.pnlPlanControl.Visible = false;
            // 
            // lblSkillClass
            // 
            this.lblSkillClass.AutoSize = true;
            this.lblSkillClass.Location = new System.Drawing.Point(4, 5);
            this.lblSkillClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSkillClass.Name = "lblSkillClass";
            this.lblSkillClass.Size = new System.Drawing.Size(94, 17);
            this.lblSkillClass.TabIndex = 18;
            this.lblSkillClass.Text = "Skill Category";
            // 
            // textboxDescription
            // 
            this.textboxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textboxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxDescription.Location = new System.Drawing.Point(385, 23);
            this.textboxDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textboxDescription.Multiline = true;
            this.textboxDescription.Name = "textboxDescription";
            this.textboxDescription.ReadOnly = true;
            this.textboxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxDescription.Size = new System.Drawing.Size(310, 70);
            this.textboxDescription.TabIndex = 17;
            this.textboxDescription.Text = "textbox1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(500, 101);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Plan:";
            // 
            // cbPlanSelect
            // 
            this.cbPlanSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlanSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanSelect.FormattingEnabled = true;
            this.cbPlanSelect.Items.AddRange(new object[] {
            "Not Planned",
            "Level I",
            "Level II",
            "Level III",
            "Level IV",
            "Level V"});
            this.cbPlanSelect.Location = new System.Drawing.Point(550, 97);
            this.cbPlanSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbPlanSelect.Name = "cbPlanSelect";
            this.cbPlanSelect.Size = new System.Drawing.Size(132, 24);
            this.cbPlanSelect.TabIndex = 15;
            // 
            // lblAttributes
            // 
            this.lblAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAttributes.Location = new System.Drawing.Point(382, 5);
            this.lblAttributes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttributes.Name = "lblAttributes";
            this.lblAttributes.Size = new System.Drawing.Size(313, 16);
            this.lblAttributes.TabIndex = 14;
            this.lblAttributes.Text = "Primary: Intelligence, Secondary: Willpower";
            this.lblAttributes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLevel5Time
            // 
            this.lblLevel5Time.AutoSize = true;
            this.lblLevel5Time.Location = new System.Drawing.Point(4, 107);
            this.lblLevel5Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevel5Time.Name = "lblLevel5Time";
            this.lblLevel5Time.Size = new System.Drawing.Size(139, 17);
            this.lblLevel5Time.TabIndex = 5;
            this.lblLevel5Time.Text = "Level V: ..... (plus ...)";
            // 
            // lblLevel4Time
            // 
            this.lblLevel4Time.AutoSize = true;
            this.lblLevel4Time.Location = new System.Drawing.Point(4, 91);
            this.lblLevel4Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevel4Time.Name = "lblLevel4Time";
            this.lblLevel4Time.Size = new System.Drawing.Size(142, 17);
            this.lblLevel4Time.TabIndex = 4;
            this.lblLevel4Time.Text = "Level IV: ..... (plus ...)";
            // 
            // lblLevel3Time
            // 
            this.lblLevel3Time.AutoSize = true;
            this.lblLevel3Time.Location = new System.Drawing.Point(4, 75);
            this.lblLevel3Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevel3Time.Name = "lblLevel3Time";
            this.lblLevel3Time.Size = new System.Drawing.Size(139, 17);
            this.lblLevel3Time.TabIndex = 3;
            this.lblLevel3Time.Text = "Level III: ..... (plus ...)";
            // 
            // lblLevel2Time
            // 
            this.lblLevel2Time.AutoSize = true;
            this.lblLevel2Time.Location = new System.Drawing.Point(4, 59);
            this.lblLevel2Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevel2Time.Name = "lblLevel2Time";
            this.lblLevel2Time.Size = new System.Drawing.Size(136, 17);
            this.lblLevel2Time.TabIndex = 2;
            this.lblLevel2Time.Text = "Level II: ..... (plus ...)";
            // 
            // lblLevel1Time
            // 
            this.lblLevel1Time.AutoSize = true;
            this.lblLevel1Time.Location = new System.Drawing.Point(4, 43);
            this.lblLevel1Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevel1Time.Name = "lblLevel1Time";
            this.lblLevel1Time.Size = new System.Drawing.Size(133, 17);
            this.lblLevel1Time.TabIndex = 1;
            this.lblLevel1Time.Text = "Level I: ..... (plus ...)";
            // 
            // lblSkillName
            // 
            this.lblSkillName.AutoSize = true;
            this.lblSkillName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillName.Location = new System.Drawing.Point(4, 23);
            this.lblSkillName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSkillName.Name = "lblSkillName";
            this.lblSkillName.Size = new System.Drawing.Size(78, 17);
            this.lblSkillName.TabIndex = 0;
            this.lblSkillName.Text = "Skill Name";
            // 
            // skillTreeDisplay
            // 
            this.skillTreeDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.skillTreeDisplay.AutoScroll = true;
            this.skillTreeDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.skillTreeDisplay.Location = new System.Drawing.Point(8, 138);
            this.skillTreeDisplay.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.skillTreeDisplay.Name = "skillTreeDisplay";
            this.skillTreeDisplay.Plan = null;
            this.skillTreeDisplay.RootSkill = null;
            this.skillTreeDisplay.Size = new System.Drawing.Size(687, 367);
            this.skillTreeDisplay.TabIndex = 0;
            this.skillTreeDisplay.Visible = false;
            this.skillTreeDisplay.WorksafeMode = false;
            // 
            // cmsSkillContext
            // 
            this.cmsSkillContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPlanTo1,
            this.miPlanTo2,
            this.miPlanTo3,
            this.miPlanTo4,
            this.miPlanTo5,
            this.toolStripMenuItem1,
            this.miCancelPlanMenu});
            this.cmsSkillContext.Name = "cmsSkillContext";
            this.cmsSkillContext.Size = new System.Drawing.Size(203, 142);
            // 
            // miPlanTo1
            // 
            this.miPlanTo1.Name = "miPlanTo1";
            this.miPlanTo1.Size = new System.Drawing.Size(202, 22);
            this.miPlanTo1.Text = "Plan to Level I";
            // 
            // miPlanTo2
            // 
            this.miPlanTo2.Name = "miPlanTo2";
            this.miPlanTo2.Size = new System.Drawing.Size(202, 22);
            this.miPlanTo2.Text = "Plan to Level II";
            // 
            // miPlanTo3
            // 
            this.miPlanTo3.Name = "miPlanTo3";
            this.miPlanTo3.Size = new System.Drawing.Size(202, 22);
            this.miPlanTo3.Text = "Plan to Level III";
            // 
            // miPlanTo4
            // 
            this.miPlanTo4.Name = "miPlanTo4";
            this.miPlanTo4.Size = new System.Drawing.Size(202, 22);
            this.miPlanTo4.Text = "Plan to Level IV";
            // 
            // miPlanTo5
            // 
            this.miPlanTo5.Name = "miPlanTo5";
            this.miPlanTo5.Size = new System.Drawing.Size(202, 22);
            this.miPlanTo5.Text = "Plan to Level V";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
            // 
            // miCancelPlanMenu
            // 
            this.miCancelPlanMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCancelAll,
            this.miCancelThis});
            this.miCancelPlanMenu.Name = "miCancelPlanMenu";
            this.miCancelPlanMenu.Size = new System.Drawing.Size(202, 22);
            this.miCancelPlanMenu.Text = "Cancel Current Plan";
            // 
            // miCancelAll
            // 
            this.miCancelAll.Name = "miCancelAll";
            this.miCancelAll.Size = new System.Drawing.Size(264, 22);
            this.miCancelAll.Text = "Cancel Plan and Prerequisites";
            // 
            // miCancelThis
            // 
            this.miCancelThis.Name = "miCancelThis";
            this.miCancelThis.Size = new System.Drawing.Size(264, 22);
            this.miCancelThis.Text = "Cancel Plan for This Skill Only";
            // 
            // tmrSkillTick
            // 
            this.tmrSkillTick.Enabled = true;
            this.tmrSkillTick.Interval = 1000;
            // 
            // SkillBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SkillBrowser";
            this.Size = new System.Drawing.Size(867, 508);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.pnlPlanControl.ResumeLayout(false);
            this.pnlPlanControl.PerformLayout();
            this.cmsSkillContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EVEMon.SkillPlanner.PersistentSplitContainer splitContainer2;
        private EVEMon.SkillPlanner.SkillSelectControl skillSelectControl;
        private System.Windows.Forms.ContextMenuStrip cmsSkillContext;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo1;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo2;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo3;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo4;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miCancelPlanMenu;
        private System.Windows.Forms.ToolStripMenuItem miCancelAll;
        private System.Windows.Forms.ToolStripMenuItem miCancelThis;
        private System.Windows.Forms.Timer tmrSkillTick;
        private System.Windows.Forms.Panel pnlPlanControl;
        private System.Windows.Forms.Label lblSkillClass;
        private System.Windows.Forms.TextBox textboxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPlanSelect;
        private System.Windows.Forms.Label lblAttributes;
        private System.Windows.Forms.Label lblLevel5Time;
        private System.Windows.Forms.Label lblLevel4Time;
        private System.Windows.Forms.Label lblLevel3Time;
        private System.Windows.Forms.Label lblLevel2Time;
        private System.Windows.Forms.Label lblLevel1Time;
        private System.Windows.Forms.Label lblSkillName;
        private SkillTreeDisplay skillTreeDisplay;

    }
}
