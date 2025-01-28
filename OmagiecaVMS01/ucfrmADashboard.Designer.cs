namespace OmagiecaVMS01
{
    partial class ucfrmADashboard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucfrmADashboard));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.dgvVisitorsReport = new System.Windows.Forms.DataGridView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDailyVisitorsEntered = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblDailyRemainingVisitors = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.labelTotalRecords = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblDailyVisitorsExited = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlRFIDMonitorExit = new System.Windows.Forms.Panel();
            this.pnlRFIDMonitor = new System.Windows.Forms.Panel();
            this.ClosingTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitorsReport)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Calligraphy", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OliveDrab;
            this.label1.Location = new System.Drawing.Point(159, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 124);
            this.label1.TabIndex = 1;
            this.label1.Text = ".";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Calligraphy", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OliveDrab;
            this.label2.Location = new System.Drawing.Point(419, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 83);
            this.label2.TabIndex = 36;
            this.label2.Text = ".";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 300;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // dgvVisitorsReport
            // 
            this.dgvVisitorsReport.AllowUserToAddRows = false;
            this.dgvVisitorsReport.AllowUserToDeleteRows = false;
            this.dgvVisitorsReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVisitorsReport.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvVisitorsReport.BackgroundColor = System.Drawing.Color.OldLace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVisitorsReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVisitorsReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LavenderBlush;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVisitorsReport.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVisitorsReport.EnableHeadersVisualStyles = false;
            this.dgvVisitorsReport.GridColor = System.Drawing.Color.OldLace;
            this.dgvVisitorsReport.Location = new System.Drawing.Point(549, 10);
            this.dgvVisitorsReport.Name = "dgvVisitorsReport";
            this.dgvVisitorsReport.ReadOnly = true;
            this.dgvVisitorsReport.Size = new System.Drawing.Size(41, 44);
            this.dgvVisitorsReport.TabIndex = 41;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.OldLace;
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1261, 260);
            this.panel7.TabIndex = 287;
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Location = new System.Drawing.Point(3, 263);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1255, 173);
            this.panel6.TabIndex = 288;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.BackColor = System.Drawing.Color.Blue;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.lblDailyVisitorsEntered);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(29, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 142);
            this.panel3.TabIndex = 37;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(76, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblDailyVisitorsEntered
            // 
            this.lblDailyVisitorsEntered.AutoSize = true;
            this.lblDailyVisitorsEntered.BackColor = System.Drawing.Color.Transparent;
            this.lblDailyVisitorsEntered.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDailyVisitorsEntered.ForeColor = System.Drawing.Color.White;
            this.lblDailyVisitorsEntered.Location = new System.Drawing.Point(90, 83);
            this.lblDailyVisitorsEntered.Name = "lblDailyVisitorsEntered";
            this.lblDailyVisitorsEntered.Size = new System.Drawing.Size(33, 37);
            this.lblDailyVisitorsEntered.TabIndex = 1;
            this.lblDailyVisitorsEntered.Text = "0";
            this.lblDailyVisitorsEntered.Click += new System.EventHandler(this.lblDailyVisitorsEntered_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(43, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Visitor Entered";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Purple;
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.lblDailyRemainingVisitors);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(671, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 142);
            this.panel1.TabIndex = 38;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(88, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(63, 50);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // lblDailyRemainingVisitors
            // 
            this.lblDailyRemainingVisitors.AutoSize = true;
            this.lblDailyRemainingVisitors.BackColor = System.Drawing.Color.Transparent;
            this.lblDailyRemainingVisitors.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDailyRemainingVisitors.ForeColor = System.Drawing.Color.White;
            this.lblDailyRemainingVisitors.Location = new System.Drawing.Point(98, 82);
            this.lblDailyRemainingVisitors.Name = "lblDailyRemainingVisitors";
            this.lblDailyRemainingVisitors.Size = new System.Drawing.Size(33, 37);
            this.lblDailyRemainingVisitors.TabIndex = 1;
            this.lblDailyRemainingVisitors.Text = "0";
            this.lblDailyRemainingVisitors.Click += new System.EventHandler(this.lblDailyRemainingVisitors_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(37, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 25);
            this.label10.TabIndex = 0;
            this.label10.Text = "Remaining Visitor";
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel5.BackColor = System.Drawing.Color.Green;
            this.panel5.Controls.Add(this.pictureBox4);
            this.panel5.Controls.Add(this.labelTotalRecords);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Location = new System.Drawing.Point(996, 18);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(227, 142);
            this.panel5.TabIndex = 40;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(78, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(63, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // labelTotalRecords
            // 
            this.labelTotalRecords.AutoSize = true;
            this.labelTotalRecords.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalRecords.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalRecords.ForeColor = System.Drawing.Color.White;
            this.labelTotalRecords.Location = new System.Drawing.Point(96, 82);
            this.labelTotalRecords.Name = "labelTotalRecords";
            this.labelTotalRecords.Size = new System.Drawing.Size(33, 37);
            this.labelTotalRecords.TabIndex = 1;
            this.labelTotalRecords.Text = "0";
            this.labelTotalRecords.Click += new System.EventHandler(this.labelTotalRecords_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(48, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Total Visitors";
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel4.BackColor = System.Drawing.Color.Crimson;
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Controls.Add(this.lblDailyVisitorsExited);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(341, 18);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(227, 142);
            this.panel4.TabIndex = 39;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(81, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(63, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // lblDailyVisitorsExited
            // 
            this.lblDailyVisitorsExited.AutoSize = true;
            this.lblDailyVisitorsExited.BackColor = System.Drawing.Color.Transparent;
            this.lblDailyVisitorsExited.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDailyVisitorsExited.ForeColor = System.Drawing.Color.White;
            this.lblDailyVisitorsExited.Location = new System.Drawing.Point(91, 83);
            this.lblDailyVisitorsExited.Name = "lblDailyVisitorsExited";
            this.lblDailyVisitorsExited.Size = new System.Drawing.Size(33, 37);
            this.lblDailyVisitorsExited.TabIndex = 1;
            this.lblDailyVisitorsExited.Text = "0";
            this.lblDailyVisitorsExited.Click += new System.EventHandler(this.lblDailyVisitorsExited_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(53, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Visitor Exited";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pnlRFIDMonitorExit);
            this.panel2.Controls.Add(this.pnlRFIDMonitor);
            this.panel2.Controls.Add(this.dgvVisitorsReport);
            this.panel2.Location = new System.Drawing.Point(52, 442);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1157, 254);
            this.panel2.TabIndex = 289;
            // 
            // pnlRFIDMonitorExit
            // 
            this.pnlRFIDMonitorExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlRFIDMonitorExit.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlRFIDMonitorExit.Location = new System.Drawing.Point(622, 10);
            this.pnlRFIDMonitorExit.Name = "pnlRFIDMonitorExit";
            this.pnlRFIDMonitorExit.Size = new System.Drawing.Size(439, 235);
            this.pnlRFIDMonitorExit.TabIndex = 287;
            // 
            // pnlRFIDMonitor
            // 
            this.pnlRFIDMonitor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlRFIDMonitor.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlRFIDMonitor.Location = new System.Drawing.Point(80, 10);
            this.pnlRFIDMonitor.Name = "pnlRFIDMonitor";
            this.pnlRFIDMonitor.Size = new System.Drawing.Size(439, 235);
            this.pnlRFIDMonitor.TabIndex = 286;
            // 
            // ClosingTimer
            // 
            this.ClosingTimer.Interval = 60000;
            this.ClosingTimer.Tick += new System.EventHandler(this.ClosingTimer_Tick);
            // 
            // ucfrmADashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel7);
            this.Name = "ucfrmADashboard";
            this.Size = new System.Drawing.Size(1261, 699);
            this.Load += new System.EventHandler(this.ucfrmADashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitorsReport)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.DataGridView dgvVisitorsReport;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDailyVisitorsEntered;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblDailyVisitorsExited;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblDailyRemainingVisitors;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label labelTotalRecords;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlRFIDMonitorExit;
        private System.Windows.Forms.Panel pnlRFIDMonitor;
        private System.Windows.Forms.Timer ClosingTimer;
    }
}
