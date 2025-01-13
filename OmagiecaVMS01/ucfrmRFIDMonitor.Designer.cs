namespace OmagiecaVMS01
{
    partial class ucfrmRFIDMonitor
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (mySerialPort != null)
                {
                    if (mySerialPort.IsOpen)
                    {
                        mySerialPort.Close();
                    }
                    mySerialPort.Dispose();
                }
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
            this.textBoxRFID = new System.Windows.Forms.TextBox();
            this.groupBoxRFID.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRFID
            // 
            this.groupBoxRFID.Controls.Add(this.labelStatus);
            this.groupBoxRFID.Controls.Add(this.buttonStart);
            this.groupBoxRFID.Controls.Add(this.buttonStop);
            this.groupBoxRFID.Controls.Add(this.textBoxRFID);
            this.groupBoxRFID.ForeColor = System.Drawing.Color.White;
            this.groupBoxRFID.Location = new System.Drawing.Point(38, 3);
            this.groupBoxRFID.Name = "groupBoxRFID";
            this.groupBoxRFID.Size = new System.Drawing.Size(347, 210);
            this.groupBoxRFID.TabIndex = 14;
            this.groupBoxRFID.TabStop = false;
            this.groupBoxRFID.Text = "RFID Reader";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelStatus.Location = new System.Drawing.Point(106, 16);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(147, 25);
            this.labelStatus.TabIndex = 12;
            this.labelStatus.Text = "Status: Ready";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(111, 94);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(124, 41);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start Reading";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.Crimson;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Location = new System.Drawing.Point(99, 151);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(149, 41);
            this.buttonStop.TabIndex = 11;
            this.buttonStop.Text = "Stop Reading";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxRFID
            // 
            this.textBoxRFID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRFID.Location = new System.Drawing.Point(89, 49);
            this.textBoxRFID.Name = "textBoxRFID";
            this.textBoxRFID.ReadOnly = true;
            this.textBoxRFID.Size = new System.Drawing.Size(165, 31);
            this.textBoxRFID.TabIndex = 10;
            // 
            // ucfrmRFIDMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Controls.Add(this.groupBoxRFID);
            this.Name = "ucfrmRFIDMonitor";
            this.Size = new System.Drawing.Size(415, 235);
            this.Load += new System.EventHandler(this.ucfrmRFIDMonitor_Load);
            this.groupBoxRFID.ResumeLayout(false);
            this.groupBoxRFID.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRFID;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxRFID;
    }
}
