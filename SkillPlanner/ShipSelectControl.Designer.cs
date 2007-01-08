namespace EVEMon.SkillPlanner
{
    partial class ShipSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipSelectControl));
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.tvShips = new System.Windows.Forms.TreeView();
            this.lbSearchTextHint = new System.Windows.Forms.Label();
            this.lbNoMatches = new System.Windows.Forms.Label();
            this.lbShipResults = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(7, 29);
            this.pbSearchImage.Margin = new System.Windows.Forms.Padding(2);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(20, 20);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 20;
            this.pbSearchImage.TabStop = false;
            // 
            // tbSearchText
            // 
            this.tbSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchText.Location = new System.Drawing.Point(31, 29);
            this.tbSearchText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(154, 21);
            this.tbSearchText.TabIndex = 21;
            this.tbSearchText.Enter += new System.EventHandler(this.tbSearchText_Enter);
            this.tbSearchText.Leave += new System.EventHandler(this.tbSearchText_Leave);
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearchText_TextChanged);
            // 
            // tvShips
            // 
            this.tvShips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvShips.Location = new System.Drawing.Point(0, 0);
            this.tvShips.Margin = new System.Windows.Forms.Padding(2);
            this.tvShips.Name = "tvShips";
            this.tvShips.Size = new System.Drawing.Size(185, 344);
            this.tvShips.TabIndex = 22;
            this.tvShips.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvShips_AfterSelect);
            // 
            // lbSearchTextHint
            // 
            this.lbSearchTextHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSearchTextHint.BackColor = System.Drawing.SystemColors.Window;
            this.lbSearchTextHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbSearchTextHint.Location = new System.Drawing.Point(32, 31);
            this.lbSearchTextHint.Name = "lbSearchTextHint";
            this.lbSearchTextHint.Size = new System.Drawing.Size(68, 14);
            this.lbSearchTextHint.TabIndex = 23;
            this.lbSearchTextHint.Text = "Search Text";
            this.lbSearchTextHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSearchTextHint.Click += new System.EventHandler(this.lbSearchTextHint_Click);
            // 
            // lbNoMatches
            // 
            this.lbNoMatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNoMatches.BackColor = System.Drawing.SystemColors.Window;
            this.lbNoMatches.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbNoMatches.Location = new System.Drawing.Point(2, 18);
            this.lbNoMatches.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNoMatches.Name = "lbNoMatches";
            this.lbNoMatches.Size = new System.Drawing.Size(168, 40);
            this.lbNoMatches.TabIndex = 24;
            this.lbNoMatches.Text = "No ships match your search.";
            this.lbNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbNoMatches.Visible = false;
            // 
            // lbShipResults
            // 
            this.lbShipResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbShipResults.FormattingEnabled = true;
            this.lbShipResults.IntegralHeight = false;
            this.lbShipResults.Location = new System.Drawing.Point(0, 0);
            this.lbShipResults.Margin = new System.Windows.Forms.Padding(2);
            this.lbShipResults.Name = "lbShipResults";
            this.lbShipResults.Size = new System.Drawing.Size(185, 344);
            this.lbShipResults.TabIndex = 25;
            this.lbShipResults.Visible = false;
            this.lbShipResults.SelectedIndexChanged += new System.EventHandler(this.lbShipResults_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbSkillFilter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pbSearchImage);
            this.panel1.Controls.Add(this.lbSearchTextHint);
            this.panel1.Controls.Add(this.tbSearchText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 54);
            this.panel1.TabIndex = 26;
            // 
            // cbSkillFilter
            // 
            this.cbSkillFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSkillFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkillFilter.FormattingEnabled = true;
            this.cbSkillFilter.Items.AddRange(new object[] {
            "All Ships",
            "Ships I Can Fly"});
            this.cbSkillFilter.Location = new System.Drawing.Point(31, 3);
            this.cbSkillFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(154, 21);
            this.cbSkillFilter.TabIndex = 0;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Filter:";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.lbShipResults);
            this.panel2.Controls.Add(this.lbNoMatches);
            this.panel2.Controls.Add(this.tvShips);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 344);
            this.panel2.TabIndex = 27;
            // 
            // ShipSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ShipSelectControl";
            this.Size = new System.Drawing.Size(185, 398);
            this.Load += new System.EventHandler(this.ShipSelectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.TreeView tvShips;
        private System.Windows.Forms.Label lbSearchTextHint;
        private System.Windows.Forms.Label lbNoMatches;
        private System.Windows.Forms.ListBox lbShipResults;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbSkillFilter;
        private System.Windows.Forms.Label label1;
    }
}
