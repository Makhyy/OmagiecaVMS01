namespace OmagiecaVMS01
{
    partial class ucfrmVisitorReportOptions
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
            this.cmbVisitorType = new System.Windows.Forms.ComboBox();
            this.btnGenerateRebtnApplyFiltersport = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbVisitorType
            // 
            this.cmbVisitorType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVisitorType.FormattingEnabled = true;
            this.cmbVisitorType.Items.AddRange(new object[] {
            "Child",
            "Adult",
            "Senior Cetizen",
            "PWD"});
            this.cmbVisitorType.Location = new System.Drawing.Point(805, 11);
            this.cmbVisitorType.Name = "cmbVisitorType";
            this.cmbVisitorType.Size = new System.Drawing.Size(153, 28);
            this.cmbVisitorType.TabIndex = 281;
            // 
            // btnGenerateRebtnApplyFiltersport
            // 
            this.btnGenerateRebtnApplyFiltersport.BackColor = System.Drawing.Color.Green;
            this.btnGenerateRebtnApplyFiltersport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerateRebtnApplyFiltersport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateRebtnApplyFiltersport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateRebtnApplyFiltersport.Location = new System.Drawing.Point(1015, 6);
            this.btnGenerateRebtnApplyFiltersport.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateRebtnApplyFiltersport.Name = "btnGenerateRebtnApplyFiltersport";
            this.btnGenerateRebtnApplyFiltersport.Size = new System.Drawing.Size(166, 36);
            this.btnGenerateRebtnApplyFiltersport.TabIndex = 284;
            this.btnGenerateRebtnApplyFiltersport.Text = "Apply Filter";
            this.btnGenerateRebtnApplyFiltersport.UseVisualStyleBackColor = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Location = new System.Drawing.Point(375, 14);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(278, 26);
            this.dtpEndDate.TabIndex = 286;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Location = new System.Drawing.Point(16, 14);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(277, 26);
            this.dtpStartDate.TabIndex = 285;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(710, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 287;
            this.label1.Text = "Visitor Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(320, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 25);
            this.label2.TabIndex = 288;
            this.label2.Text = "-";
            // 
            // ucfrmVisitorReportOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.btnGenerateRebtnApplyFiltersport);
            this.Controls.Add(this.cmbVisitorType);
            this.Name = "ucfrmVisitorReportOptions";
            this.Size = new System.Drawing.Size(1195, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVisitorType;
        private System.Windows.Forms.Button btnGenerateRebtnApplyFiltersport;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
