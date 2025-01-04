namespace OmagiecaVMS01
{
    partial class frmRFID
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxRFID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(347, 255);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(156, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Reading";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // textBoxRFID
            // 
            this.textBoxRFID.Location = new System.Drawing.Point(347, 171);
            this.textBoxRFID.Name = "textBoxRFID";
            this.textBoxRFID.ReadOnly = true;
            this.textBoxRFID.Size = new System.Drawing.Size(181, 20);
            this.textBoxRFID.TabIndex = 1;
            // 
            // frmRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 469);
            this.Controls.Add(this.textBoxRFID);
            this.Controls.Add(this.buttonStart);
            this.Name = "frmRFID";
            this.Text = "frmRFID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxRFID;
    }
}