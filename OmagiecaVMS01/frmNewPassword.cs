using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OmagiecaVMS01
{
    public partial class frmNewPassword : Form
    {
        private string username;
        public frmNewPassword(string username)
        {
            InitializeComponent();
            this.username = username;
        }

       

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string newPassword = txtNewPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    MessageBox.Show("Please fill both password fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UserAccountBLL userAccountBLL = new UserAccountBLL();
                userAccountBLL.UpdatePassword(username, newPassword);

                MessageBox.Show("Password reset successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close Reset Password Form
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while resetting the password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
