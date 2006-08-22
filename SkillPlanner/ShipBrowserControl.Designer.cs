namespace EVEMon.SkillPlanner
{
    partial class ShipBrowserControl
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
            this.scShipSelect = new System.Windows.Forms.SplitContainer();
            this.shipSelectControl = new EVEMon.SkillPlanner.ShipSelectControl();
            this.pnlShipDescription = new System.Windows.Forms.Panel();
            this.lblShipDescription = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShipSkillsAdd = new System.Windows.Forms.Button();
            this.lblShipTimeRequired = new System.Windows.Forms.Label();
            this.lblShipSkill3 = new System.Windows.Forms.Label();
            this.lblShipSkill2 = new System.Windows.Forms.Label();
            this.lblShipSkill1 = new System.Windows.Forms.Label();
            this.lbShipProperties = new System.Windows.Forms.ListBox();
            this.lblShipName = new System.Windows.Forms.Label();
            this.lblShipClass = new System.Windows.Forms.Label();
            this.pbShipImage = new System.Windows.Forms.PictureBox();
            this.scShipSelect.Panel1.SuspendLayout();
            this.scShipSelect.Panel2.SuspendLayout();
            this.scShipSelect.SuspendLayout();
            this.pnlShipDescription.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShipImage)).BeginInit();
            this.SuspendLayout();
            // 
            // scShipSelect
            // 
            this.scShipSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scShipSelect.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scShipSelect.Location = new System.Drawing.Point(0, 0);
            this.scShipSelect.Name = "scShipSelect";
            // 
            // scShipSelect.Panel1
            // 
            this.scShipSelect.Panel1.Controls.Add(this.shipSelectControl);
            // 
            // scShipSelect.Panel2
            // 
            this.scShipSelect.Panel2.Controls.Add(this.pnlShipDescription);
            this.scShipSelect.Panel2.Controls.Add(this.groupBox1);
            this.scShipSelect.Panel2.Controls.Add(this.lbShipProperties);
            this.scShipSelect.Panel2.Controls.Add(this.lblShipName);
            this.scShipSelect.Panel2.Controls.Add(this.lblShipClass);
            this.scShipSelect.Panel2.Controls.Add(this.pbShipImage);
            //this.scShipSelect.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.scShipSelect_Panel2_Paint);
            this.scShipSelect.Size = new System.Drawing.Size(761, 423);
            this.scShipSelect.SplitterDistance = 193;
            this.scShipSelect.TabIndex = 1;
            // 
            // shipSelectControl
            // 
            this.shipSelectControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shipSelectControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shipSelectControl.Location = new System.Drawing.Point(3, 3);
            this.shipSelectControl.Name = "shipSelectControl";
            this.shipSelectControl.Size = new System.Drawing.Size(187, 417);
            this.shipSelectControl.TabIndex = 0;
            this.shipSelectControl.SelectedShipChanged += new System.EventHandler<System.EventArgs>(this.shipSelectControl_SelectedShipChanged);
            // 
            // pnlShipDescription
            // 
            this.pnlShipDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlShipDescription.AutoScroll = true;
            this.pnlShipDescription.Controls.Add(this.lblShipDescription);
            this.pnlShipDescription.Location = new System.Drawing.Point(305, 265);
            this.pnlShipDescription.Name = "pnlShipDescription";
            this.pnlShipDescription.Size = new System.Drawing.Size(256, 38);
            this.pnlShipDescription.TabIndex = 8;
            this.pnlShipDescription.ClientSizeChanged += new System.EventHandler(pnlShipDescription_ClientSizeChanged);
            // 
            // lblShipDescription
            // 
            this.lblShipDescription.AutoSize = true;
            this.lblShipDescription.Location = new System.Drawing.Point(-3, 0);
            this.lblShipDescription.Name = "lblShipDescription";
            this.lblShipDescription.Size = new System.Drawing.Size(35, 13);
            this.lblShipDescription.TabIndex = 4;
            this.lblShipDescription.Text = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShipSkillsAdd);
            this.groupBox1.Controls.Add(this.lblShipTimeRequired);
            this.groupBox1.Controls.Add(this.lblShipSkill3);
            this.groupBox1.Controls.Add(this.lblShipSkill2);
            this.groupBox1.Controls.Add(this.lblShipSkill1);
            this.groupBox1.Location = new System.Drawing.Point(305, 309);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Required Skills";
            // 
            // btnShipSkillsAdd
            // 
            this.btnShipSkillsAdd.Location = new System.Drawing.Point(121, 82);
            this.btnShipSkillsAdd.Name = "btnShipSkillsAdd";
            this.btnShipSkillsAdd.Size = new System.Drawing.Size(129, 23);
            this.btnShipSkillsAdd.TabIndex = 4;
            this.btnShipSkillsAdd.Text = "Add Skills to Plan";
            this.btnShipSkillsAdd.UseVisualStyleBackColor = true;
            this.btnShipSkillsAdd.Click += new System.EventHandler(btnShipSkillsAdd_Click);
            // 
            // lblShipTimeRequired
            // 
            this.lblShipTimeRequired.AutoSize = true;
            this.lblShipTimeRequired.Location = new System.Drawing.Point(6, 65);
            this.lblShipTimeRequired.Name = "lblShipTimeRequired";
            this.lblShipTimeRequired.Size = new System.Drawing.Size(79, 13);
            this.lblShipTimeRequired.TabIndex = 3;
            this.lblShipTimeRequired.Text = "Time Required:";
            // 
            // lblShipSkill3
            // 
            this.lblShipSkill3.AutoSize = true;
            this.lblShipSkill3.Location = new System.Drawing.Point(6, 43);
            this.lblShipSkill3.Name = "lblShipSkill3";
            this.lblShipSkill3.Size = new System.Drawing.Size(35, 13);
            this.lblShipSkill3.TabIndex = 2;
            this.lblShipSkill3.Text = "label4";
            // 
            // lblShipSkill2
            // 
            this.lblShipSkill2.AutoSize = true;
            this.lblShipSkill2.Location = new System.Drawing.Point(6, 30);
            this.lblShipSkill2.Name = "lblShipSkill2";
            this.lblShipSkill2.Size = new System.Drawing.Size(35, 13);
            this.lblShipSkill2.TabIndex = 1;
            this.lblShipSkill2.Text = "label3";
            // 
            // lblShipSkill1
            // 
            this.lblShipSkill1.AutoSize = true;
            this.lblShipSkill1.Location = new System.Drawing.Point(6, 17);
            this.lblShipSkill1.Name = "lblShipSkill1";
            this.lblShipSkill1.Size = new System.Drawing.Size(35, 13);
            this.lblShipSkill1.TabIndex = 0;
            this.lblShipSkill1.Text = "label2";
            // 
            // lbShipProperties
            // 
            this.lbShipProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbShipProperties.FormattingEnabled = true;
            this.lbShipProperties.IntegralHeight = false;
            this.lbShipProperties.Location = new System.Drawing.Point(3, 37);
            this.lbShipProperties.Name = "lbShipProperties";
            this.lbShipProperties.Size = new System.Drawing.Size(296, 383);
            this.lbShipProperties.TabIndex = 3;
            // 
            // lblShipName
            // 
            this.lblShipName.AutoSize = true;
            this.lblShipName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipName.Location = new System.Drawing.Point(3, 16);
            this.lblShipName.Name = "lblShipName";
            this.lblShipName.Size = new System.Drawing.Size(55, 18);
            this.lblShipName.TabIndex = 2;
            this.lblShipName.Text = "Raven";
            // 
            // lblShipClass
            // 
            this.lblShipClass.AutoSize = true;
            this.lblShipClass.Location = new System.Drawing.Point(3, 3);
            this.lblShipClass.Name = "lblShipClass";
            this.lblShipClass.Size = new System.Drawing.Size(102, 13);
            this.lblShipClass.TabIndex = 1;
            this.lblShipClass.Text = "Battleships > Caldari";
            // 
            // pbShipImage
            // 
            this.pbShipImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbShipImage.Location = new System.Drawing.Point(305, 3);
            this.pbShipImage.Name = "pbShipImage";
            this.pbShipImage.Size = new System.Drawing.Size(256, 256);
            this.pbShipImage.TabIndex = 0;
            this.pbShipImage.TabStop = false;
            // 
            // ShipBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scShipSelect);
            this.Name = "ShipBrowserControl";
            this.Size = new System.Drawing.Size(761, 423);
            this.scShipSelect.Panel1.ResumeLayout(false);
            this.scShipSelect.Panel2.ResumeLayout(false);
            this.scShipSelect.Panel2.PerformLayout();
            this.scShipSelect.ResumeLayout(false);
            this.pnlShipDescription.ResumeLayout(false);
            this.pnlShipDescription.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Load += new System.EventHandler(this.ShipBrowserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbShipImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scShipSelect;
        private ShipSelectControl shipSelectControl;
        private System.Windows.Forms.Panel pnlShipDescription;
        private System.Windows.Forms.Label lblShipDescription;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShipSkillsAdd;
        private System.Windows.Forms.Label lblShipTimeRequired;
        private System.Windows.Forms.Label lblShipSkill3;
        private System.Windows.Forms.Label lblShipSkill2;
        private System.Windows.Forms.Label lblShipSkill1;
        private System.Windows.Forms.ListBox lbShipProperties;
        private System.Windows.Forms.Label lblShipName;
        private System.Windows.Forms.Label lblShipClass;
        private System.Windows.Forms.PictureBox pbShipImage;


    }
}
