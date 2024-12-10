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
    public partial class frmLogin : Form
    {
        private readonly UserAccountBLL userAccountBLL;
        public frmLogin()
        {
            InitializeComponent();
            userAccountBLL = new UserAccountBLL();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // Authenticate user
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                UserAccount userAccount = userAccountBLL.AuthenticateUser(username, password);

                if (userAccount != null)
                {
                    // Successful login
                    MessageBox.Show($"Welcome, {userAccount.FirstName} {userAccount.LastName}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Redirect based on role
                    if (userAccount.UserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        var adminForm = new frmAdmin();
                        adminForm.Show();
                    }
                    else if (userAccount.UserRole.Equals("Receptionist", StringComparison.OrdinalIgnoreCase))
                    {
                        var receptionistForm = new frmReceptionist();
                        receptionistForm.Show();
                    }

                    this.Hide(); // Hide login form
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show("An error occurred while attempting to log in: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = loginShowPassword.Checked ? '\0' : '*';
        }

        private void fogotPassword_Click(object sender, EventArgs e)
        {
            
            frmForgotPassword frmverifypassword = new frmForgotPassword();
            frmverifypassword.Show();
            this.Hide();
        }
    }
}
