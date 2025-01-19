namespace OmagiecaVMS01
{
    partial class frmVisit
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
            this.btnRegisterVisitor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvVisitorStatus = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitorStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRegisterVisitor
            // 
            this.btnRegisterVisitor.Location = new System.Drawing.Point(325, 508);
            this.btnRegisterVisitor.Name = "btnRegisterVisitor";
            this.btnRegisterVisitor.Size = new System.Drawing.Size(97, 36);
            this.btnRegisterVisitor.TabIndex = 0;
            this.btnRegisterVisitor.Text = "Register";
            this.btnRegisterVisitor.UseVisualStyleBackColor = true;
            this.btnRegisterVisitor.Click += new System.EventHandler(this.btnRegisterVisitor_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(612, 508);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dgvVisitorStatus
            // 
            this.dgvVisitorStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisitorStatus.Location = new System.Drawing.Point(89, 60);
            this.dgvVisitorStatus.Name = "dgvVisitorStatus";
            this.dgvVisitorStatus.Size = new System.Drawing.Size(899, 380);
            this.dgvVisitorStatus.TabIndex = 2;
            // 
            // frmVisit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 607);
            this.Controls.Add(this.dgvVisitorStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRegisterVisitor);
            this.Name = "frmVisit";
            this.Text = "frmVisit";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitorStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegisterVisitor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvVisitorStatus;
    }
}