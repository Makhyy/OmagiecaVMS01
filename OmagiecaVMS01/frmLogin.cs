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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OmagiecaVMS01
{
    public partial class frmLogin : Form
    {
        private readonly UserAccountBLL userAccountBLL;
        public string UserRole { get; private set; }

        public frmLogin()
        {
            InitializeComponent();
            userAccountBLL = new UserAccountBLL();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Username and Password are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                UserAccount userAccount = userAccountBLL.AuthenticateUser(username, password);

                if (userAccount != null)
                {
                    // Set session variables
                    CurrentSession.UserAccountId = userAccount.UserAccountId;
                    CurrentSession.Username = userAccount.Username;
                    CurrentSession.UserRole = userAccount.UserRole;
                    CurrentSession.FirstName = userAccount.FirstName;
                    CurrentSession.LastName = userAccount.LastName;

                    // Show success message
                    MessageBox.Show($"Welcome, {userAccount.FirstName} {userAccount.LastName}! ",
                        "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide(); // Hide the login form

                    // Redirect based on user role
                    switch (userAccount.UserRole)
                    {
                        case "Admin":
                            frmAdmin admin = new frmAdmin();
                            admin.ShowDialog();
                            break;
                        case "Receptionist":
                            frmReceptionist receptionistDashboard = new frmReceptionist();
                            receptionistDashboard.ShowDialog();
                            break;
                        default:
                            MessageBox.Show("Unauthorized role", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    this.Close(); // Close the login form after the dashboard is closed
                }
                else
                {
                    // Show error message for invalid login
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    

                    // Reset the placeholders (if using watermarks)
                    txtUsername.Text = "Username";
                    txtPassword.Text = "Password";

                    // Set focus back to the username field
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while attempting to log in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearTextBoxes();
            }
        }

        private void ClearTextBoxes()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }



        private void loginShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = loginShowPassword.Checked ? '\0' : '*';
        }

        private void fogotPassword_Click(object sender, EventArgs e)
        {
            using (frmForgotPassword frmverifypassword = new frmForgotPassword())
            {
               
                frmverifypassword.ShowDialog(); // Show the Forgot Password form as a modal dialog
                this.Show(); // Re-show the login form after the Forgot Password form is closed
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
