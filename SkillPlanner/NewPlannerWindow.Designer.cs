namespace EVEMon.SkillPlanner
{
    partial class NewPlannerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPlannerWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslSuggestion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsddbPlans = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyForum = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImplantCalculator = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDeletePlan = new System.Windows.Forms.ToolStripButton();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpPlanQueue = new System.Windows.Forms.TabPage();
            this.tpSkillBrowser = new System.Windows.Forms.TabPage();
            this.tpShipBrowser = new System.Windows.Forms.TabPage();
            this.tpItemBrowser = new System.Windows.Forms.TabPage();
            this.planEditor = new EVEMon.SkillPlanner.PlanOrderEditorControl();
            this.skillBrowser = new EVEMon.SkillPlanner.SkillBrowser();
            this.shipBrowser = new EVEMon.SkillPlanner.ShipBrowserControl();
            this.itemBrowser = new EVEMon.SkillPlanner.ItemBrowserControl();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpPlanQueue.SuspendLayout();
            this.tpSkillBrowser.SuspendLayout();
            this.tpShipBrowser.SuspendLayout();
            this.tpItemBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblStatusText,
            this.tslSuggestion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(658, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slblStatusText
            // 
            this.slblStatusText.Name = "slblStatusText";
            this.slblStatusText.Size = new System.Drawing.Size(79, 17);
            this.slblStatusText.Text = "0 Skills Planned";
            // 
            // tslSuggestion
            // 
            this.tslSuggestion.Image = ((System.Drawing.Image)(resources.GetObject("tslSuggestion.Image")));
            this.tslSuggestion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tslSuggestion.IsLink = true;
            this.tslSuggestion.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.tslSuggestion.Name = "tslSuggestion";
            this.tslSuggestion.Size = new System.Drawing.Size(88, 17);
            this.tslSuggestion.Text = "Suggestion...";
            this.tslSuggestion.Visible = false;
            this.tslSuggestion.Click += new System.EventHandler(this.tslSuggestion_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbPlans,
            this.tsbSaveAs,
            this.tsbCopyForum,
            this.toolStripSeparator2,
            this.tsbImplantCalculator,
            this.toolStripSeparator1,
            this.tsbDeletePlan});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(658, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsddbPlans
            // 
            this.tsddbPlans.Image = ((System.Drawing.Image)(resources.GetObject("tsddbPlans.Image")));
            this.tsddbPlans.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsddbPlans.Name = "tsddbPlans";
            this.tsddbPlans.Size = new System.Drawing.Size(88, 22);
            this.tsddbPlans.Text = "Select Plan";
            this.tsddbPlans.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbPlans_DropDownItemClicked);
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAs.Image")));
            this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(95, 22);
            this.tsbSaveAs.Text = "Save to File...";
            this.tsbSaveAs.Click += new System.EventHandler(this.tsbSaveAs_Click);
            // 
            // tsbCopyForum
            // 
            this.tsbCopyForum.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyForum.Image")));
            this.tsbCopyForum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyForum.Name = "tsbCopyForum";
            this.tsbCopyForum.Size = new System.Drawing.Size(102, 22);
            this.tsbCopyForum.Text = "Copy for Forum";
            this.tsbCopyForum.Click += new System.EventHandler(this.tsbCopyForum_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImplantCalculator
            // 
            this.tsbImplantCalculator.Image = ((System.Drawing.Image)(resources.GetObject("tsbImplantCalculator.Image")));
            this.tsbImplantCalculator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImplantCalculator.Name = "tsbImplantCalculator";
            this.tsbImplantCalculator.Size = new System.Drawing.Size(114, 22);
            this.tsbImplantCalculator.Text = "Implant Calculator";
            this.tsbImplantCalculator.Click += new System.EventHandler(this.tsbImplantCalculator_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDeletePlan
            // 
            this.tsbDeletePlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeletePlan.Image")));
            this.tsbDeletePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeletePlan.Name = "tsbDeletePlan";
            this.tsbDeletePlan.Size = new System.Drawing.Size(81, 22);
            this.tsbDeletePlan.Text = "Delete Plan";
            this.tsbDeletePlan.Click += new System.EventHandler(this.tsbDeletePlan_Click);
            // 
            // ttToolTip
            // 
            this.ttToolTip.AutoPopDelay = 5000000;
            this.ttToolTip.InitialDelay = 500;
            this.ttToolTip.ReshowDelay = 100;
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "EVEMon Plan Format (*.emp)|*.emp|XML Format (*.xml)|*.xml|Text Format (*.txt)|*.t" +
                "xt";
            this.sfdSave.Title = "Save to File";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpPlanQueue);
            this.tabControl.Controls.Add(this.tpSkillBrowser);
            this.tabControl.Controls.Add(this.tpShipBrowser);
            this.tabControl.Controls.Add(this.tpItemBrowser);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(658, 439);
            this.tabControl.TabIndex = 4;
            // 
            // tpPlanQueue
            // 
            this.tpPlanQueue.Controls.Add(this.planEditor);
            this.tpPlanQueue.Location = new System.Drawing.Point(4, 22);
            this.tpPlanQueue.Name = "tpPlanQueue";
            this.tpPlanQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlanQueue.Size = new System.Drawing.Size(650, 413);
            this.tpPlanQueue.TabIndex = 1;
            this.tpPlanQueue.Text = "Plan Queue";
            this.tpPlanQueue.UseVisualStyleBackColor = true;
            // 
            // tpSkillBrowser
            // 
            this.tpSkillBrowser.Controls.Add(this.skillBrowser);
            this.tpSkillBrowser.Location = new System.Drawing.Point(4, 22);
            this.tpSkillBrowser.Name = "tpSkillBrowser";
            this.tpSkillBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tpSkillBrowser.Size = new System.Drawing.Size(650, 413);
            this.tpSkillBrowser.TabIndex = 0;
            this.tpSkillBrowser.Text = "Skill Browser";
            this.tpSkillBrowser.UseVisualStyleBackColor = true;
            // 
            // tpShipBrowser
            // 
            this.tpShipBrowser.Controls.Add(this.shipBrowser);
            this.tpShipBrowser.Location = new System.Drawing.Point(4, 22);
            this.tpShipBrowser.Name = "tpShipBrowser";
            this.tpShipBrowser.Size = new System.Drawing.Size(650, 413);
            this.tpShipBrowser.TabIndex = 2;
            this.tpShipBrowser.Text = "Ship Browser";
            this.tpShipBrowser.UseVisualStyleBackColor = true;
            // 
            // tpItemBrowser
            // 
            this.tpItemBrowser.Controls.Add(this.itemBrowser);
            this.tpItemBrowser.Location = new System.Drawing.Point(4, 22);
            this.tpItemBrowser.Name = "tpItemBrowser";
            this.tpItemBrowser.Size = new System.Drawing.Size(650, 413);
            this.tpItemBrowser.TabIndex = 3;
            this.tpItemBrowser.Text = "Item Browser";
            this.tpItemBrowser.UseVisualStyleBackColor = true;
            // 
            // planEditor
            // 
            this.planEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planEditor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.planEditor.HighlightPlannedSkills = false;
            this.planEditor.HighlightPrerequisites = false;
            this.planEditor.Location = new System.Drawing.Point(3, 3);
            this.planEditor.Name = "planEditor";
            this.planEditor.Plan = null;
            this.planEditor.Size = new System.Drawing.Size(644, 407);
            this.planEditor.TabIndex = 2;
            this.planEditor.WorksafeMode = false;
            // 
            // skillBrowser
            // 
            this.skillBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillBrowser.Location = new System.Drawing.Point(3, 3);
            this.skillBrowser.Name = "skillBrowser";
            this.skillBrowser.Plan = null;
            this.skillBrowser.SelectedSkill = null;
            this.skillBrowser.Size = new System.Drawing.Size(644, 407);
            this.skillBrowser.TabIndex = 0;
            // 
            // shipBrowser
            // 
            this.shipBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shipBrowser.Location = new System.Drawing.Point(0, 0);
            this.shipBrowser.Name = "shipBrowser";
            this.shipBrowser.Plan = null;
            this.shipBrowser.Size = new System.Drawing.Size(650, 413);
            this.shipBrowser.TabIndex = 0;
            // 
            // itemBrowser
            // 
            this.itemBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemBrowser.Location = new System.Drawing.Point(0, 0);
            this.itemBrowser.Name = "itemBrowser";
            this.itemBrowser.Plan = null;
            this.itemBrowser.Size = new System.Drawing.Size(650, 413);
            this.itemBrowser.TabIndex = 0;
            // 
            // NewPlannerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 486);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(666, 353);
            this.Name = "NewPlannerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVEMon Skill Planner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewPlannerWindow_FormClosed);
            this.Shown += new System.EventHandler(this.NewPlannerWindow_Shown);
            this.Load += new System.EventHandler(this.NewPlannerWindow_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpPlanQueue.ResumeLayout(false);
            this.tpSkillBrowser.ResumeLayout(false);
            this.tpShipBrowser.ResumeLayout(false);
            this.tpItemBrowser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel slblStatusText;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbDeletePlan;
        private System.Windows.Forms.ToolStripStatusLabel tslSuggestion;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
        private System.Windows.Forms.ToolStripButton tsbCopyForum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbImplantCalculator;
        private System.Windows.Forms.TabPage tpItemBrowser;
        private ItemBrowserControl itemBrowser;
        private System.Windows.Forms.TabPage tpPlanQueue;
        private PlanOrderEditorControl planEditor;
        private System.Windows.Forms.TabPage tpShipBrowser;
        private ShipBrowserControl shipBrowser;
        private System.Windows.Forms.TabPage tpSkillBrowser;
        private SkillBrowser skillBrowser;
        private System.Windows.Forms.ToolStripDropDownButton tsddbPlans;
    }
}
