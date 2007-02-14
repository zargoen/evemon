namespace EVEMon.SkillPlanner
{
    partial class SkillSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillSelectControl));
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.tvSkillList = new System.Windows.Forms.TreeView();
            this.lbSearchList = new System.Windows.Forms.ListBox();
            this.lblSearchTip = new System.Windows.Forms.Label();
            this.lblNoMatches = new System.Windows.Forms.Label();
            this.cbShowNonPublic = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSorting = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvSortedSkillList = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chSortKey = new System.Windows.Forms.ColumnHeader();
            this.ilSkillIcons1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSkillFilter
            // 
            this.cbSkillFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSkillFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkillFilter.FormattingEnabled = true;
            this.cbSkillFilter.Items.AddRange(new object[] {
            "All",
            "Known",
            "Level I Ready",
            "Not Known",
            "Not Known - Owned",
            "Not Known - Unowned",
            "Not Planned",
            "Not Planned - Trainable",
            "Partially Trained",
            "Planned",
            "Trainable"});
            this.cbSkillFilter.Location = new System.Drawing.Point(31, 3);
            this.cbSkillFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSkillFilter.MaxDropDownItems = 12;
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(231, 21);
            this.cbSkillFilter.Sorted = true;
            this.cbSkillFilter.TabIndex = 0;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(31, 54);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(231, 21);
            this.tbSearch.TabIndex = 1;
            this.tbSearch.Enter += new System.EventHandler(this.tbSearch_Enter);
            this.tbSearch.Leave += new System.EventHandler(this.tbSearch_Leave);
            this.tbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearch_KeyPress);
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(7, 54);
            this.pbSearchImage.Margin = new System.Windows.Forms.Padding(2);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(20, 20);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 19;
            this.pbSearchImage.TabStop = false;
            // 
            // tvSkillList
            // 
            this.tvSkillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSkillList.Location = new System.Drawing.Point(0, 0);
            this.tvSkillList.Margin = new System.Windows.Forms.Padding(2);
            this.tvSkillList.Name = "tvSkillList";
            this.tvSkillList.Size = new System.Drawing.Size(262, 313);
            this.tvSkillList.TabIndex = 20;
            this.tvSkillList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSkillList_AfterSelect);
            // 
            // lbSearchList
            // 
            this.lbSearchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSearchList.FormattingEnabled = true;
            this.lbSearchList.IntegralHeight = false;
            this.lbSearchList.Location = new System.Drawing.Point(2, 197);
            this.lbSearchList.Margin = new System.Windows.Forms.Padding(2);
            this.lbSearchList.Name = "lbSearchList";
            this.lbSearchList.Size = new System.Drawing.Size(260, 108);
            this.lbSearchList.TabIndex = 21;
            this.lbSearchList.Visible = false;
            this.lbSearchList.SelectedIndexChanged += new System.EventHandler(this.lbSearchList_SelectedIndexChanged);
            // 
            // lblSearchTip
            // 
            this.lblSearchTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSearchTip.BackColor = System.Drawing.SystemColors.Window;
            this.lblSearchTip.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSearchTip.Location = new System.Drawing.Point(32, 56);
            this.lblSearchTip.Name = "lblSearchTip";
            this.lblSearchTip.Size = new System.Drawing.Size(68, 14);
            this.lblSearchTip.TabIndex = 22;
            this.lblSearchTip.Text = "Search Text";
            this.lblSearchTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearchTip.Click += new System.EventHandler(this.lblSearchTip_Click);
            // 
            // lblNoMatches
            // 
            this.lblNoMatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoMatches.BackColor = System.Drawing.SystemColors.Window;
            this.lblNoMatches.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNoMatches.Location = new System.Drawing.Point(4, 23);
            this.lblNoMatches.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNoMatches.Name = "lblNoMatches";
            this.lblNoMatches.Padding = new System.Windows.Forms.Padding(4);
            this.lblNoMatches.Size = new System.Drawing.Size(256, 22);
            this.lblNoMatches.TabIndex = 23;
            this.lblNoMatches.Text = "No skills match your search.";
            this.lblNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblNoMatches.Visible = false;
            // 
            // cbShowNonPublic
            // 
            this.cbShowNonPublic.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbShowNonPublic.Location = new System.Drawing.Point(0, 391);
            this.cbShowNonPublic.Margin = new System.Windows.Forms.Padding(2);
            this.cbShowNonPublic.Name = "cbShowNonPublic";
            this.cbShowNonPublic.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.cbShowNonPublic.Size = new System.Drawing.Size(262, 22);
            this.cbShowNonPublic.TabIndex = 24;
            this.cbShowNonPublic.Text = "Show Non-Public Skills";
            this.cbShowNonPublic.UseVisualStyleBackColor = true;
            this.cbShowNonPublic.CheckedChanged += new System.EventHandler(this.cbShowNonPublic_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Filter:";
            // 
            // cbSorting
            // 
            this.cbSorting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSorting.FormattingEnabled = true;
            this.cbSorting.Items.AddRange(new object[] {
            "No Sorting",
            "Time to Next Level",
            "Time to Level V"});
            this.cbSorting.Location = new System.Drawing.Point(31, 29);
            this.cbSorting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSorting.Name = "cbSorting";
            this.cbSorting.Size = new System.Drawing.Size(231, 21);
            this.cbSorting.TabIndex = 26;
            this.cbSorting.SelectedIndexChanged += new System.EventHandler(this.cbSorting_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Sort:";
            // 
            // lvSortedSkillList
            // 
            this.lvSortedSkillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSortedSkillList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chSortKey});
            this.lvSortedSkillList.FullRowSelect = true;
            this.lvSortedSkillList.Location = new System.Drawing.Point(2, 62);
            this.lvSortedSkillList.Margin = new System.Windows.Forms.Padding(2);
            this.lvSortedSkillList.Name = "lvSortedSkillList";
            this.lvSortedSkillList.Size = new System.Drawing.Size(258, 107);
            this.lvSortedSkillList.TabIndex = 28;
            this.lvSortedSkillList.TileSize = new System.Drawing.Size(16, 16);
            this.lvSortedSkillList.UseCompatibleStateImageBehavior = false;
            this.lvSortedSkillList.View = System.Windows.Forms.View.Details;
            this.lvSortedSkillList.Visible = false;
            this.lvSortedSkillList.SelectedIndexChanged += new System.EventHandler(this.lvSortedSkillList_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            // 
            // chSortKey
            // 
            this.chSortKey.Text = "Sort";
            // 
            // ilSkillIcons1
            // 
            this.ilSkillIcons1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSkillIcons1.ImageStream")));
            this.ilSkillIcons1.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSkillIcons1.Images.SetKeyName(0, "book");
            this.ilSkillIcons1.Images.SetKeyName(1, "PrereqsNOTMet");
            this.ilSkillIcons1.Images.SetKeyName(2, "PrereqsMet");
            this.ilSkillIcons1.Images.SetKeyName(3, "lvl0");
            this.ilSkillIcons1.Images.SetKeyName(4, "lvl1");
            this.ilSkillIcons1.Images.SetKeyName(5, "lvl2");
            this.ilSkillIcons1.Images.SetKeyName(6, "lvl3");
            this.ilSkillIcons1.Images.SetKeyName(7, "lvl4");
            this.ilSkillIcons1.Images.SetKeyName(8, "lvl5");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbSorting);
            this.panel1.Controls.Add(this.cbSkillFilter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pbSearchImage);
            this.panel1.Controls.Add(this.lblSearchTip);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 78);
            this.panel1.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.lvSortedSkillList);
            this.panel2.Controls.Add(this.lblNoMatches);
            this.panel2.Controls.Add(this.lbSearchList);
            this.panel2.Controls.Add(this.tvSkillList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 78);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 313);
            this.panel2.TabIndex = 30;
            // 
            // SkillSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbShowNonPublic);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SkillSelectControl";
            this.Size = new System.Drawing.Size(262, 413);
            this.Load += new System.EventHandler(this.SkillSelectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.ListBox lbSearchList;
        private System.Windows.Forms.Label lblSearchTip;
        private System.Windows.Forms.Label lblNoMatches;
        private System.Windows.Forms.CheckBox cbShowNonPublic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSorting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvSortedSkillList;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chSortKey;
        private System.Windows.Forms.ImageList ilSkillIcons1;
        public System.Windows.Forms.TreeView tvSkillList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbSkillFilter;
    }
}
