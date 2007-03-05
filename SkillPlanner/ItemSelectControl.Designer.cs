namespace EVEMon.SkillPlanner
{
    partial class ItemSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemSelectControl));
            this.lbNoMatches = new System.Windows.Forms.Label();
            this.lbItemResults = new System.Windows.Forms.ListBox();
            this.lbSearchTextHint = new System.Windows.Forms.Label();
            this.tvItems = new System.Windows.Forms.TreeView();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbDeadspace = new System.Windows.Forms.CheckBox();
            this.cbOfficer = new System.Windows.Forms.CheckBox();
            this.cbFaction = new System.Windows.Forms.CheckBox();
            this.cbTech2 = new System.Windows.Forms.CheckBox();
            this.cbNamed = new System.Windows.Forms.CheckBox();
            this.cbTech1 = new System.Windows.Forms.CheckBox();
            this.cbSlotFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbNoMatches
            // 
            this.lbNoMatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNoMatches.BackColor = System.Drawing.SystemColors.Window;
            this.lbNoMatches.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbNoMatches.Location = new System.Drawing.Point(2, 2);
            this.lbNoMatches.Name = "lbNoMatches";
            this.lbNoMatches.Size = new System.Drawing.Size(182, 53);
            this.lbNoMatches.TabIndex = 30;
            this.lbNoMatches.Text = "No items match your search.";
            this.lbNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbNoMatches.Visible = false;
            // 
            // lbItemResults
            // 
            this.lbItemResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbItemResults.FormattingEnabled = true;
            this.lbItemResults.IntegralHeight = false;
            this.lbItemResults.Location = new System.Drawing.Point(0, 0);
            this.lbItemResults.Name = "lbItemResults";
            this.lbItemResults.Size = new System.Drawing.Size(185, 254);
            this.lbItemResults.TabIndex = 31;
            this.lbItemResults.Visible = false;
            this.lbItemResults.SelectedIndexChanged += new System.EventHandler(this.lbItemResults_SelectedIndexChanged);
            // 
            // lbSearchTextHint
            // 
            this.lbSearchTextHint.BackColor = System.Drawing.SystemColors.Window;
            this.lbSearchTextHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbSearchTextHint.Location = new System.Drawing.Point(30, 133);
            this.lbSearchTextHint.Name = "lbSearchTextHint";
            this.lbSearchTextHint.Size = new System.Drawing.Size(68, 19);
            this.lbSearchTextHint.TabIndex = 29;
            this.lbSearchTextHint.Text = "Search Text";
            this.lbSearchTextHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSearchTextHint.Click += new System.EventHandler(this.lbSearchTextHint_Click);
            // 
            // tvItems
            // 
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Margin = new System.Windows.Forms.Padding(2);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(185, 254);
            this.tvItems.TabIndex = 28;
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItems_AfterSelect);
            // 
            // tbSearchText
            // 
            this.tbSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchText.Location = new System.Drawing.Point(29, 132);
            this.tbSearchText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(154, 21);
            this.tbSearchText.TabIndex = 27;
            this.tbSearchText.Enter += new System.EventHandler(this.tbSearchText_Enter);
            this.tbSearchText.Leave += new System.EventHandler(this.tbSearchText_Leave);
            this.tbSearchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchText_KeyPress);
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearchText_TextChanged);
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(5, 132);
            this.pbSearchImage.Margin = new System.Windows.Forms.Padding(2);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(20, 20);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 26;
            this.pbSearchImage.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbDeadspace);
            this.panel1.Controls.Add(this.cbOfficer);
            this.panel1.Controls.Add(this.cbFaction);
            this.panel1.Controls.Add(this.cbTech2);
            this.panel1.Controls.Add(this.cbNamed);
            this.panel1.Controls.Add(this.cbTech1);
            this.panel1.Controls.Add(this.cbSlotFilter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbSearchTextHint);
            this.panel1.Controls.Add(this.tbSearchText);
            this.panel1.Controls.Add(this.cbSkillFilter);
            this.panel1.Controls.Add(this.pbSearchImage);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 156);
            this.panel1.TabIndex = 32;
            // 
            // cbDeadspace
            // 
            this.cbDeadspace.AutoSize = true;
            this.cbDeadspace.Location = new System.Drawing.Point(90, 106);
            this.cbDeadspace.Name = "cbDeadspace";
            this.cbDeadspace.Size = new System.Drawing.Size(79, 17);
            this.cbDeadspace.TabIndex = 37;
            this.cbDeadspace.Text = "Deadspace";
            this.cbDeadspace.UseVisualStyleBackColor = true;
            this.cbDeadspace.CheckedChanged += new System.EventHandler(this.cbClass_SelectedChanged);
            // 
            // cbOfficer
            // 
            this.cbOfficer.AutoSize = true;
            this.cbOfficer.Location = new System.Drawing.Point(6, 107);
            this.cbOfficer.Name = "cbOfficer";
            this.cbOfficer.Size = new System.Drawing.Size(59, 17);
            this.cbOfficer.TabIndex = 36;
            this.cbOfficer.Text = "Officer";
            this.cbOfficer.UseVisualStyleBackColor = true;
            this.cbOfficer.CheckedChanged += new System.EventHandler(this.cbClass_SelectedChanged);
            // 
            // cbFaction
            // 
            this.cbFaction.AutoSize = true;
            this.cbFaction.Location = new System.Drawing.Point(89, 82);
            this.cbFaction.Name = "cbFaction";
            this.cbFaction.Size = new System.Drawing.Size(61, 17);
            this.cbFaction.TabIndex = 35;
            this.cbFaction.Text = "Faction";
            this.cbFaction.UseVisualStyleBackColor = true;
            this.cbFaction.CheckedChanged += new System.EventHandler(this.cbClass_SelectedChanged);
            // 
            // cbTech2
            // 
            this.cbTech2.AutoSize = true;
            this.cbTech2.Checked = true;
            this.cbTech2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTech2.Location = new System.Drawing.Point(6, 83);
            this.cbTech2.Name = "cbTech2";
            this.cbTech2.Size = new System.Drawing.Size(58, 17);
            this.cbTech2.TabIndex = 34;
            this.cbTech2.Text = "Tech 2";
            this.cbTech2.UseVisualStyleBackColor = true;
            this.cbTech2.CheckedChanged += new System.EventHandler(this.cbClass_SelectedChanged);
            // 
            // cbNamed
            // 
            this.cbNamed.AutoSize = true;
            this.cbNamed.Checked = true;
            this.cbNamed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNamed.Location = new System.Drawing.Point(90, 59);
            this.cbNamed.Name = "cbNamed";
            this.cbNamed.Size = new System.Drawing.Size(59, 17);
            this.cbNamed.TabIndex = 33;
            this.cbNamed.Text = "Named";
            this.cbNamed.UseVisualStyleBackColor = true;
            this.cbNamed.CheckedChanged += new System.EventHandler(this.cbClass_SelectedChanged);
            // 
            // cbTech1
            // 
            this.cbTech1.AutoSize = true;
            this.cbTech1.Checked = true;
            this.cbTech1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTech1.Location = new System.Drawing.Point(6, 59);
            this.cbTech1.Name = "cbTech1";
            this.cbTech1.Size = new System.Drawing.Size(56, 17);
            this.cbTech1.TabIndex = 32;
            this.cbTech1.Text = "Tech I";
            this.cbTech1.UseVisualStyleBackColor = true;
            this.cbTech1.CheckedChanged += new System.EventHandler(this.cbClass_SelectedChanged);
            // 
            // cbSlotFilter
            // 
            this.cbSlotFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSlotFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSlotFilter.FormattingEnabled = true;
            this.cbSlotFilter.Items.AddRange(new object[] {
            "All Items",
            "High Slot",
            "Medium Slot",
            "Low Slot",
            "No Slot"});
            this.cbSlotFilter.Location = new System.Drawing.Point(38, 29);
            this.cbSlotFilter.Name = "cbSlotFilter";
            this.cbSlotFilter.Size = new System.Drawing.Size(147, 21);
            this.cbSlotFilter.TabIndex = 31;
            this.cbSlotFilter.SelectedIndexChanged += new System.EventHandler(this.cbSlotFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "By slot:";
            // 
            // cbSkillFilter
            // 
            this.cbSkillFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSkillFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkillFilter.FormattingEnabled = true;
            this.cbSkillFilter.Items.AddRange(new object[] {
            "All Items",
            "Items I Can Use",
            "Items I Can NOT Use"});
            this.cbSkillFilter.Location = new System.Drawing.Point(38, 3);
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(147, 21);
            this.cbSkillFilter.TabIndex = 1;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "By skill:";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.lbItemResults);
            this.panel2.Controls.Add(this.lbNoMatches);
            this.panel2.Controls.Add(this.tvItems);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 156);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 254);
            this.panel2.TabIndex = 33;
            // 
            // ItemSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ItemSelectControl";
            this.Size = new System.Drawing.Size(185, 410);
            this.Load += new System.EventHandler(this.ItemSelectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNoMatches;
        private System.Windows.Forms.ListBox lbItemResults;
        private System.Windows.Forms.Label lbSearchTextHint;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbSkillFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSlotFilter;
        private System.Windows.Forms.CheckBox cbDeadspace;
        private System.Windows.Forms.CheckBox cbOfficer;
        private System.Windows.Forms.CheckBox cbFaction;
        private System.Windows.Forms.CheckBox cbTech2;
        private System.Windows.Forms.CheckBox cbNamed;
        private System.Windows.Forms.CheckBox cbTech1;
    }
}
