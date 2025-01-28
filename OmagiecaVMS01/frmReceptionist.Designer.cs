namespace OmagiecaVMS01
{
    partial class frmReceptionist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceptionist));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnVisitorStatus = new System.Windows.Forms.Button();
            this.btnRDashboard = new System.Windows.Forms.Button();
            this.btnReportsAndAnalytic = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.ucfrmADashboard1 = new OmagiecaVMS01.ucfrmADashboard();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnSettings);
            this.panel1.Controls.Add(this.btnVisitorStatus);
            this.panel1.Controls.Add(this.btnRDashboard);
            this.panel1.Controls.Add(this.btnReportsAndAnalytic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 806);
            this.panel1.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.BackColor = System.Drawing.Color.Firebrick;
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLogout.FlatAppearance.BorderSize = 2;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(4, 754);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(188, 39);
            this.btnLogout.TabIndex = 15;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(186, 164);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // btnSettings
            // 
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSettings.FlatAppearance.BorderSize = 2;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(13, 418);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(186, 49);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "User Guide";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnVisitorStatus
            // 
            this.btnVisitorStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVisitorStatus.FlatAppearance.BorderSize = 2;
            this.btnVisitorStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnVisitorStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisitorStatus.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisitorStatus.ForeColor = System.Drawing.Color.White;
            this.btnVisitorStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnVisitorStatus.Image")));
            this.btnVisitorStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisitorStatus.Location = new System.Drawing.Point(13, 349);
            this.btnVisitorStatus.Margin = new System.Windows.Forms.Padding(4);
            this.btnVisitorStatus.Name = "btnVisitorStatus";
            this.btnVisitorStatus.Size = new System.Drawing.Size(186, 49);
            this.btnVisitorStatus.TabIndex = 7;
            this.btnVisitorStatus.Text = "Visitor Status";
            this.btnVisitorStatus.UseVisualStyleBackColor = true;
            this.btnVisitorStatus.Click += new System.EventHandler(this.btnVisitorStatus_Click);
            // 
            // btnRDashboard
            // 
            this.btnRDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRDashboard.FlatAppearance.BorderSize = 2;
            this.btnRDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnRDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRDashboard.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRDashboard.ForeColor = System.Drawing.Color.White;
            this.btnRDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnRDashboard.Image")));
            this.btnRDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRDashboard.Location = new System.Drawing.Point(13, 204);
            this.btnRDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.btnRDashboard.Name = "btnRDashboard";
            this.btnRDashboard.Size = new System.Drawing.Size(186, 49);
            this.btnRDashboard.TabIndex = 6;
            this.btnRDashboard.Text = "Dashboard";
            this.btnRDashboard.UseVisualStyleBackColor = true;
            this.btnRDashboard.Click += new System.EventHandler(this.btnRDashboard_Click);
            // 
            // btnReportsAndAnalytic
            // 
            this.btnReportsAndAnalytic.BackColor = System.Drawing.Color.ForestGreen;
            this.btnReportsAndAnalytic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReportsAndAnalytic.FlatAppearance.BorderSize = 2;
            this.btnReportsAndAnalytic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnReportsAndAnalytic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportsAndAnalytic.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportsAndAnalytic.ForeColor = System.Drawing.Color.White;
            this.btnReportsAndAnalytic.Image = ((System.Drawing.Image)(resources.GetObject("btnReportsAndAnalytic.Image")));
            this.btnReportsAndAnalytic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportsAndAnalytic.Location = new System.Drawing.Point(16, 275);
            this.btnReportsAndAnalytic.Margin = new System.Windows.Forms.Padding(4);
            this.btnReportsAndAnalytic.Name = "btnReportsAndAnalytic";
            this.btnReportsAndAnalytic.Size = new System.Drawing.Size(183, 51);
            this.btnReportsAndAnalytic.TabIndex = 5;
            this.btnReportsAndAnalytic.Text = "   Register Visitor";
            this.btnReportsAndAnalytic.UseVisualStyleBackColor = false;
            this.btnReportsAndAnalytic.Click += new System.EventHandler(this.btnReportsAndAnalytic_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.OldLace;
            this.mainPanel.Controls.Add(this.ucfrmADashboard1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(209, 84);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mainPanel.Size = new System.Drawing.Size(1261, 722);
            this.mainPanel.TabIndex = 1;
            // 
            // ucfrmADashboard1
            // 
            this.ucfrmADashboard1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ucfrmADashboard1.BackColor = System.Drawing.Color.OldLace;
            this.ucfrmADashboard1.Location = new System.Drawing.Point(0, 6);
            this.ucfrmADashboard1.Name = "ucfrmADashboard1";
            this.ucfrmADashboard1.Size = new System.Drawing.Size(1261, 699);
            this.ucfrmADashboard1.TabIndex = 0;
            this.ucfrmADashboard1.Load += new System.EventHandler(this.ucfrmADashboard1_Load);
            // 
            // panel3
            // 
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BackColor = System.Drawing.Color.ForestGreen;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(209, 84);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1261, 10);
            this.panel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panel2.Controls.Add(this.lblCurrentUser);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(209, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1261, 84);
            this.panel2.TabIndex = 46;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new System.Drawing.Font("Segoe UI Semilight", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentUser.Location = new System.Drawing.Point(7, 59);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(83, 20);
            this.lblCurrentUser.TabIndex = 51;
            this.lblCurrentUser.Text = "Username";
            this.lblCurrentUser.Click += new System.EventHandler(this.lblCurrentUser_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(17, 11);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(55, 48);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 50;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(206, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(55, 56);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 48;
            this.pictureBox2.TabStop = false;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label8.Location = new System.Drawing.Point(315, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(753, 23);
            this.label8.TabIndex = 47;
            this.label8.Text = "(Oboob Mangrove Garden Integrated Ecotourism and Conservation Association)\r\n";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label1.Location = new System.Drawing.Point(209, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(954, 56);
            this.label1.TabIndex = 46;
            this.label1.Text = "OMAGIECA Visitor Management System";
            // 
            // frmReceptionist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1470, 806);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmReceptionist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receptionist";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnReportsAndAnalytic;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnVisitorStatus;
        private System.Windows.Forms.Button btnRDashboard;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnLogout;
        private ucfrmADashboard ucfrmADashboard1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblCurrentUser;
    }
}

