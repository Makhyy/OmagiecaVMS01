using BLL;
using MODELS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class frmForgotPassword : Form

    {
        private readonly UserAccountBLL userAccountBLL;
        private string username; // To store the verified username
        private string securityQuestion; // To store the user's security question
        public frmForgotPassword()
        {
            InitializeComponent();
            userAccountBLL = new UserAccountBLL();
            InitializeControls(); // Initialize visibility of controls
        }
        private void InitializeControls()
        {
            lblSecurityQuestion.Visible = false;
            txtSecurityAnswer.Visible = false;
            btnSubmitAnswer.Visible = false;
            txtNewPassword.Visible = false;
            btnResetPassword.Visible = false;
        }

        private void btnVerifyUsername_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate username input
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                username = txtUsername.Text.Trim();

                // Get user details based on username
                UserAccount userAccount = userAccountBLL.GetUserByUsername(username);
                if (userAccount != null)
                {
                    // Show security question
                    securityQuestion = userAccount.SecurityQuestion;
                    lblSecurityQuestion.Text = $"Security Question: {securityQuestion}";
                    lblSecurityQuestion.Visible = true;
                    txtSecurityAnswer.Visible = true;
                    btnSubmitAnswer.Visible = true;
                }
                else
                {
                    MessageBox.Show("Username not found. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while verifying the username: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate security answer input
                if (string.IsNullOrWhiteSpace(txtSecurityAnswer.Text))
                {
                    MessageBox.Show("Please enter your security answer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSecurityAnswer.Focus();
                    return;
                }

                string securityAnswer = txtSecurityAnswer.Text.Trim();

                // Validate security answer
                if (userAccountBLL.ValidateSecurityQuestion(username, securityQuestion, securityAnswer))
                {
                    MessageBox.Show("Security answer is correct. Please enter your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Show new password fields
                    txtNewPassword.Visible = true;
                    btnResetPassword.Visible = true;
                }
                else
                {
                    MessageBox.Show("Incorrect security answer. Please try again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating the security answer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate new password input
                if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || txtNewPassword.Text.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    return;
                }

                string newPassword = txtNewPassword.Text.Trim();

                // Update the password in the database
                userAccountBLL.UpdatePassword(username, newPassword);
                MessageBox.Show("Your password has been reset successfully! You can now log in with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (Form form in Application.OpenForms)
                {
                    if (form is frmLogin)
                    {
                        form.Show(); // Bring the login form to the front
                        this.Close(); // Close the forgot password form
                        return;
                    }
                }

                // If no login form is open, create a new one
                var loginForm = new frmLogin();
                loginForm.Show();
                this.Close();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while resetting the password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }
    
    }
}
