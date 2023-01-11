namespace Loan_issue.Report
{
    partial class rptPayMethode
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
            this.crystViwerPaymethode = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPayMethod = new System.Windows.Forms.ComboBox();
            this.rbtByPayMethod = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtAllPayMethod = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LgDisplayName = new System.Windows.Forms.Label();
            this.LgUser = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // crystViwerPaymethode
            // 
            this.crystViwerPaymethode.ActiveViewIndex = -1;
            this.crystViwerPaymethode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystViwerPaymethode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystViwerPaymethode.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystViwerPaymethode.Location = new System.Drawing.Point(310, 104);
            this.crystViwerPaymethode.Name = "crystViwerPaymethode";
            this.crystViwerPaymethode.Size = new System.Drawing.Size(699, 405);
            this.crystViwerPaymethode.TabIndex = 0;
            this.crystViwerPaymethode.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(188, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load The Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbPayMethod
            // 
            this.cmbPayMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayMethod.Enabled = false;
            this.cmbPayMethod.FormattingEnabled = true;
            this.cmbPayMethod.Items.AddRange(new object[] {
            "Day",
            "Week",
            "Months"});
            this.cmbPayMethod.Location = new System.Drawing.Point(120, 53);
            this.cmbPayMethod.Name = "cmbPayMethod";
            this.cmbPayMethod.Size = new System.Drawing.Size(159, 23);
            this.cmbPayMethod.TabIndex = 2;
            // 
            // rbtByPayMethod
            // 
            this.rbtByPayMethod.AutoSize = true;
            this.rbtByPayMethod.Location = new System.Drawing.Point(6, 54);
            this.rbtByPayMethod.Name = "rbtByPayMethod";
            this.rbtByPayMethod.Size = new System.Drawing.Size(108, 19);
            this.rbtByPayMethod.TabIndex = 3;
            this.rbtByPayMethod.TabStop = true;
            this.rbtByPayMethod.Text = "By Pay Method";
            this.rbtByPayMethod.UseVisualStyleBackColor = true;
            this.rbtByPayMethod.CheckedChanged += new System.EventHandler(this.rbtByPayMethod_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(7, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 133);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter By......";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.rbtByPayMethod);
            this.groupBox2.Controls.Add(this.rbtAllPayMethod);
            this.groupBox2.Controls.Add(this.cmbPayMethod);
            this.groupBox2.Location = new System.Drawing.Point(6, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 91);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pay Method Wise....";
            // 
            // rbtAllPayMethod
            // 
            this.rbtAllPayMethod.AutoSize = true;
            this.rbtAllPayMethod.Location = new System.Drawing.Point(6, 29);
            this.rbtAllPayMethod.Name = "rbtAllPayMethod";
            this.rbtAllPayMethod.Size = new System.Drawing.Size(39, 19);
            this.rbtAllPayMethod.TabIndex = 4;
            this.rbtAllPayMethod.TabStop = true;
            this.rbtAllPayMethod.Text = "All";
            this.rbtAllPayMethod.UseVisualStyleBackColor = true;
            this.rbtAllPayMethod.CheckedChanged += new System.EventHandler(this.rbtAllPayMethod_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.LgDisplayName);
            this.panel1.Controls.Add(this.LgUser);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Location = new System.Drawing.Point(-2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1025, 75);
            this.panel1.TabIndex = 85;
            // 
            // LgDisplayName
            // 
            this.LgDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgDisplayName.AutoSize = true;
            this.LgDisplayName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgDisplayName.ForeColor = System.Drawing.Color.Black;
            this.LgDisplayName.Location = new System.Drawing.Point(895, 37);
            this.LgDisplayName.Name = "LgDisplayName";
            this.LgDisplayName.Size = new System.Drawing.Size(87, 17);
            this.LgDisplayName.TabIndex = 36;
            this.LgDisplayName.Text = "DispalyName";
            // 
            // LgUser
            // 
            this.LgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgUser.AutoSize = true;
            this.LgUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgUser.ForeColor = System.Drawing.Color.Black;
            this.LgUser.Location = new System.Drawing.Point(926, 14);
            this.LgUser.Name = "LgUser";
            this.LgUser.Size = new System.Drawing.Size(76, 17);
            this.LgUser.TabIndex = 35;
            this.LgUser.Text = "HideUserID";
            this.LgUser.Visible = false;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(821, 37);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(79, 17);
            this.label28.TabIndex = 34;
            this.label28.Text = "Login User :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Loan_issue.Properties.Resources.payed;
            this.pictureBox1.Location = new System.Drawing.Point(14, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(11, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(0, 24);
            this.label17.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(88, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(171, 25);
            this.label15.TabIndex = 0;
            this.label15.Text = "Pay Methode Wise";
            // 
            // rptPayMethode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 521);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.crystViwerPaymethode);
            this.Name = "rptPayMethode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pay Methode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystViwerPaymethode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbPayMethod;
        private System.Windows.Forms.RadioButton rbtByPayMethod;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label LgDisplayName;
        public System.Windows.Forms.Label LgUser;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rbtAllPayMethod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
    }
}