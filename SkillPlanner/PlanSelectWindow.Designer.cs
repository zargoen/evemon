namespace EVEMon.SkillPlanner
{
    partial class PlanSelectWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanSelectWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ofdOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoadPlan = new System.Windows.Forms.ToolStripButton();
            this.tsbRenamePlan = new System.Windows.Forms.ToolStripButton();
            this.tsbDeletePlan = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbPlanList = new System.Windows.Forms.ListView();
            this.PlanName = new System.Windows.Forms.ColumnHeader();
            this.PlanDate = new System.Windows.Forms.ColumnHeader();
            this.PlanSkills = new System.Windows.Forms.ColumnHeader();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a plan to open, multiple plans to merge, or choose \"New Plan\":";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(84, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ofdOpenDialog
            // 
            this.ofdOpenDialog.Filter = "Plan Files (*.emp, *.xml)|*.xml;*.emp|All Files (*.*)|*.*";
            this.ofdOpenDialog.Title = "Open Plan File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadPlan,
            this.tsbRenamePlan,
            this.tsbDeletePlan});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(484, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLoadPlan
            // 
            this.tsbLoadPlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadPlan.Image")));
            this.tsbLoadPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadPlan.Name = "tsbLoadPlan";
            this.tsbLoadPlan.Size = new System.Drawing.Size(73, 22);
            this.tsbLoadPlan.Text = "Load Plan";
            this.tsbLoadPlan.Click += new System.EventHandler(this.tsbLoadPlan_Click);
            // 
            // tsbRenamePlan
            // 
            this.tsbRenamePlan.Enabled = false;
            this.tsbRenamePlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbRenamePlan.Image")));
            this.tsbRenamePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRenamePlan.Name = "tsbRenamePlan";
            this.tsbRenamePlan.Size = new System.Drawing.Size(89, 22);
            this.tsbRenamePlan.Text = "Rename Plan";
            this.tsbRenamePlan.Click += new System.EventHandler(this.tsbRenamePlan_Click);
            // 
            // tsbDeletePlan
            // 
            this.tsbDeletePlan.Enabled = false;
            this.tsbDeletePlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeletePlan.Image")));
            this.tsbDeletePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeletePlan.Name = "tsbDeletePlan";
            this.tsbDeletePlan.Size = new System.Drawing.Size(81, 22);
            this.tsbDeletePlan.Text = "Delete Plan";
            this.tsbDeletePlan.Click += new System.EventHandler(this.tsbDeletePlan_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lbPlanList);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Location = new System.Drawing.Point(9, 22);
            this.panel1.MinimumSize = new System.Drawing.Size(305, 155);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 326);
            this.panel1.TabIndex = 6;
            // 
            // lbPlanList
            // 
            this.lbPlanList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlanName,
            this.PlanDate,
            this.PlanSkills});
            this.lbPlanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlanList.FullRowSelect = true;
            this.lbPlanList.Location = new System.Drawing.Point(0, 0);
            this.lbPlanList.Name = "lbPlanList";
            this.lbPlanList.Size = new System.Drawing.Size(435, 326);
            this.lbPlanList.TabIndex = 1;
            this.lbPlanList.UseCompatibleStateImageBehavior = false;
            this.lbPlanList.View = System.Windows.Forms.View.Details;
            this.lbPlanList.DoubleClick += new System.EventHandler(this.lbPlanList_DoubleClick);
            this.lbPlanList.SelectedIndexChanged += new System.EventHandler(this.lbPlanList_SelectedIndexChanged);
            // 
            // PlanName
            // 
            this.PlanName.Text = "Plan Name";
            this.PlanName.Width = 176;
            // 
            // PlanDate
            // 
            this.PlanDate.Text = "Completion Time";
            this.PlanDate.Width = 197;
            // 
            // PlanSkills
            // 
            this.PlanSkills.Text = "Skills";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsbMoveUp,
            this.tsbMoveDown});
            this.toolStrip2.Location = new System.Drawing.Point(435, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(38, 326);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 13);
            this.toolStripLabel1.Text = "Move:";
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveUp.Enabled = false;
            this.tsbMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveUp.Image")));
            this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new System.Drawing.Size(35, 20);
            this.tsbMoveUp.Text = "Move Up";
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbMoveDown
            // 
            this.tsbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveDown.Enabled = false;
            this.tsbMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveDown.Image")));
            this.tsbMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Size = new System.Drawing.Size(35, 20);
            this.tsbMoveDown.Text = "Move Down";
            this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(6, 6, 0, 12);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 400);
            this.tableLayoutPanel1.TabIndex = 8;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOpen);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(317, 359);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 8, 6, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(162, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(3, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // PlanSelectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 425);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(366, 333);
            this.Name = "PlanSelectWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Plan";
            this.Load += new System.EventHandler(this.PlanSelectWindow_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog ofdOpenDialog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbLoadPlan;
        private System.Windows.Forms.ToolStripButton tsbRenamePlan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ToolStripButton tsbMoveDown;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsbDeletePlan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.ListView lbPlanList;
		private System.Windows.Forms.ColumnHeader PlanName;
		private System.Windows.Forms.ColumnHeader PlanDate;
		private System.Windows.Forms.ColumnHeader PlanSkills;
        private System.Windows.Forms.Button btnOpen;
    }
}
