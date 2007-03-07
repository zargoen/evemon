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
            this.cbDeadspace = new System.Windows.Forms.CheckBox();
            this.cbOfficer = new System.Windows.Forms.CheckBox();
            this.cbFaction = new System.Windows.Forms.CheckBox();
            this.cbTech2 = new System.Windows.Forms.CheckBox();
            this.cbNamed = new System.Windows.Forms.CheckBox();
            this.cbTech1 = new System.Windows.Forms.CheckBox();
            this.cbSlotFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSkillFilter
            // 
            this.cbSkillFilter.Location = new System.Drawing.Point(38, 3);
            this.cbSkillFilter.Size = new System.Drawing.Size(147, 21);
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // tbSearchText
            // 
            this.tbSearchText.Location = new System.Drawing.Point(29, 132);
            this.tbSearchText.Size = new System.Drawing.Size(156, 21);
            // 
            // tvItems
            // 
            this.tvItems.LineColor = System.Drawing.Color.Black;
            this.tvItems.Size = new System.Drawing.Size(185, 254);
            // 
            // lbNoMatches
            // 
            this.lbNoMatches.Location = new System.Drawing.Point(2, 2);
            this.lbNoMatches.Text = "No items match your search.";
            // 
            // lbSearchList
            // 
            this.lbSearchList.Size = new System.Drawing.Size(185, 254);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbSlotFilter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbDeadspace);
            this.panel1.Controls.Add(this.cbOfficer);
            this.panel1.Controls.Add(this.cbTech2);
            this.panel1.Controls.Add(this.cbTech1);
            this.panel1.Controls.Add(this.cbFaction);
            this.panel1.Controls.Add(this.cbNamed);
            this.panel1.Size = new System.Drawing.Size(185, 156);
            this.panel1.Controls.SetChildIndex(this.tbSearchText, 0);
            this.panel1.Controls.SetChildIndex(this.label1, 0);
            this.panel1.Controls.SetChildIndex(this.cbNamed, 0);
            this.panel1.Controls.SetChildIndex(this.cbFaction, 0);
            this.panel1.Controls.SetChildIndex(this.cbTech1, 0);
            this.panel1.Controls.SetChildIndex(this.cbTech2, 0);
            this.panel1.Controls.SetChildIndex(this.cbOfficer, 0);
            this.panel1.Controls.SetChildIndex(this.cbDeadspace, 0);
            this.panel1.Controls.SetChildIndex(this.pbSearchImage, 0);
            this.panel1.Controls.SetChildIndex(this.lbSearchTextHint, 0);
            this.panel1.Controls.SetChildIndex(this.cbSkillFilter, 0);
            this.panel1.Controls.SetChildIndex(this.label2, 0);
            this.panel1.Controls.SetChildIndex(this.cbSlotFilter, 0);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 156);
            this.panel2.Size = new System.Drawing.Size(185, 254);
            // 
            // lbSearchTextHint
            // 
            this.lbSearchTextHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSearchTextHint.Location = new System.Drawing.Point(30, 133);
            this.lbSearchTextHint.Size = new System.Drawing.Size(68, 19);
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Location = new System.Drawing.Point(5, 132);
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.Text = "By Skill:";
            // 
            // cbDeadspace
            // 
            this.cbDeadspace.AutoSize = true;
            this.cbDeadspace.Location = new System.Drawing.Point(90, 107);
            this.cbDeadspace.Name = "cbDeadspace";
            this.cbDeadspace.Size = new System.Drawing.Size(79, 17);
            this.cbDeadspace.TabIndex = 45;
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
            this.cbOfficer.TabIndex = 44;
            this.cbOfficer.Text = "Officer";
            this.cbOfficer.UseVisualStyleBackColor = true;
            this.cbOfficer.CheckedChanged += new System.EventHandler(this.cbClass_SelectedChanged);
            // 
            // cbFaction
            // 
            this.cbFaction.AutoSize = true;
            this.cbFaction.Location = new System.Drawing.Point(90, 83);
            this.cbFaction.Name = "cbFaction";
            this.cbFaction.Size = new System.Drawing.Size(61, 17);
            this.cbFaction.TabIndex = 43;
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
            this.cbTech2.TabIndex = 42;
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
            this.cbNamed.TabIndex = 41;
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
            this.cbTech1.TabIndex = 40;
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
            this.cbSlotFilter.TabIndex = 39;
            this.cbSlotFilter.SelectedIndexChanged += new System.EventHandler(this.cbSlotFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "By slot:";
            // 
            // ItemSelectControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Name = "ItemSelectControl1";
            this.Size = new System.Drawing.Size(185, 410);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbDeadspace;
        private System.Windows.Forms.CheckBox cbOfficer;
        private System.Windows.Forms.CheckBox cbFaction;
        private System.Windows.Forms.CheckBox cbTech2;
        private System.Windows.Forms.CheckBox cbNamed;
        private System.Windows.Forms.CheckBox cbTech1;
        private System.Windows.Forms.ComboBox cbSlotFilter;
        private System.Windows.Forms.Label label2;
    }
}
