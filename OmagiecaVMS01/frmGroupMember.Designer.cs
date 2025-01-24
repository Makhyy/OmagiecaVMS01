namespace OmagiecaVMS01
{
    partial class frmGroupMember
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
            this.dgvGroupMembers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupMembers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGroupMembers
            // 
            this.dgvGroupMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroupMembers.Location = new System.Drawing.Point(113, 65);
            this.dgvGroupMembers.Name = "dgvGroupMembers";
            this.dgvGroupMembers.Size = new System.Drawing.Size(753, 399);
            this.dgvGroupMembers.TabIndex = 0;
            this.dgvGroupMembers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGroupMembers_CellContentClick);
            // 
            // frmGroupMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 501);
            this.Controls.Add(this.dgvGroupMembers);
            this.Name = "frmGroupMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGroupMember";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupMembers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGroupMembers;
    }
}