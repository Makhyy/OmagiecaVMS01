﻿namespace OmagiecaVMS01
{
    partial class frmAddmember
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
            this.txtAge = new System.Windows.Forms.TextBox();
            this.cboVisitorType = new System.Windows.Forms.ComboBox();
            this.txtPaymentAmount = new System.Windows.Forms.ComboBox();
            this.cboRFIDTag = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkIsPWD = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAge
            // 
            this.txtAge.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.Location = new System.Drawing.Point(276, 87);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(91, 31);
            this.txtAge.TabIndex = 18;
            this.txtAge.TextChanged += new System.EventHandler(this.txtAge_TextChanged);
            // 
            // cboVisitorType
            // 
            this.cboVisitorType.Enabled = false;
            this.cboVisitorType.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVisitorType.FormattingEnabled = true;
            this.cboVisitorType.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cboVisitorType.Location = new System.Drawing.Point(276, 140);
            this.cboVisitorType.Name = "cboVisitorType";
            this.cboVisitorType.Size = new System.Drawing.Size(158, 29);
            this.cboVisitorType.TabIndex = 299;
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.Enabled = false;
            this.txtPaymentAmount.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentAmount.FormattingEnabled = true;
            this.txtPaymentAmount.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.txtPaymentAmount.Location = new System.Drawing.Point(276, 220);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.Size = new System.Drawing.Size(158, 29);
            this.txtPaymentAmount.TabIndex = 300;
            // 
            // cboRFIDTag
            // 
            this.cboRFIDTag.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRFIDTag.FormattingEnabled = true;
            this.cboRFIDTag.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cboRFIDTag.Location = new System.Drawing.Point(276, 274);
            this.cboRFIDTag.Name = "cboRFIDTag";
            this.cboRFIDTag.Size = new System.Drawing.Size(158, 29);
            this.cboRFIDTag.TabIndex = 301;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(210, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(188, 36);
            this.label8.TabIndex = 302;
            this.label8.Text = "ADD VISITOR";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(212, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 23);
            this.label1.TabIndex = 303;
            this.label1.Text = "Age";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(156, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 23);
            this.label2.TabIndex = 304;
            this.label2.Text = "Visitor Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(101, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 23);
            this.label3.TabIndex = 305;
            this.label3.Text = "Payment Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(104, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 23);
            this.label4.TabIndex = 306;
            this.label4.Text = "RFID Tag Number";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.FlatAppearance.BorderSize = 2;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(133, 358);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 44);
            this.btnSave.TabIndex = 307;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(311, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 44);
            this.btnCancel.TabIndex = 308;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkIsPWD
            // 
            this.chkIsPWD.AutoSize = true;
            this.chkIsPWD.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsPWD.ForeColor = System.Drawing.Color.White;
            this.chkIsPWD.Location = new System.Drawing.Point(276, 175);
            this.chkIsPWD.Name = "chkIsPWD";
            this.chkIsPWD.Size = new System.Drawing.Size(83, 26);
            this.chkIsPWD.TabIndex = 309;
            this.chkIsPWD.Text = "IsPWD";
            this.chkIsPWD.UseVisualStyleBackColor = true;
            this.chkIsPWD.CheckedChanged += new System.EventHandler(this.chkIsPWD_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 63);
            this.panel1.TabIndex = 310;
            // 
            // frmAddmember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(609, 458);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkIsPWD);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboRFIDTag);
            this.Controls.Add(this.txtPaymentAmount);
            this.Controls.Add(this.cboVisitorType);
            this.Controls.Add(this.txtAge);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddmember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAddmember_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.ComboBox cboVisitorType;
        private System.Windows.Forms.ComboBox txtPaymentAmount;
        private System.Windows.Forms.ComboBox cboRFIDTag;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkIsPWD;
        private System.Windows.Forms.Panel panel1;
    }
}