namespace OmagiecaVMS01
{
    partial class ucfrmRFIDMonitorExit
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
            this.groupBoxRFID = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxRFID.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRFID
            // 
            this.groupBoxRFID.Controls.Add(this.labelStatus);
            this.groupBoxRFID.Controls.Add(this.buttonStart);
            this.groupBoxRFID.Controls.Add(this.buttonStop);
            this.groupBoxRFID.ForeColor = System.Drawing.Color.White;
            this.groupBoxRFID.Location = new System.Drawing.Point(46, 38);
            this.groupBoxRFID.Name = "groupBoxRFID";
            this.groupBoxRFID.Size = new System.Drawing.Size(347, 175);
            this.groupBoxRFID.TabIndex = 16;
            this.groupBoxRFID.TabStop = false;
            this.groupBoxRFID.Text = "RFID Reader";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.SystemColors.Info;
            this.labelStatus.Location = new System.Drawing.Point(123, 26);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(97, 20);
            this.labelStatus.TabIndex = 12;
            this.labelStatus.Text = "Status: Ready";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(98, 58);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(149, 41);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start Reading";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.Crimson;
            this.buttonStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Location = new System.Drawing.Point(98, 115);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(149, 41);
            this.buttonStop.TabIndex = 11;
            this.buttonStop.Text = "Stop Reading";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(153, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 30);
            this.label1.TabIndex = 17;
            this.label1.Text = "EXIT READER";
            // 
            // ucfrmRFIDMonitorExit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.Controls.Add(this.groupBoxRFID);
            this.Controls.Add(this.label1);
            this.Name = "ucfrmRFIDMonitorExit";
            this.Size = new System.Drawing.Size(439, 235);
            this.groupBoxRFID.ResumeLayout(false);
            this.groupBoxRFID.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRFID;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label1;
    }
}
