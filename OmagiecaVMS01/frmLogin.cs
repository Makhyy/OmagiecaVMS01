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
                    MessageBox.Show("Both username and password are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                UserAccount userAccount = userAccountBLL.AuthenticateUser(username, password);

                if (userAccount != null)
                {
                    CurrentSession.UserAccountId = userAccount.UserAccountId; // Set the session user ID
                    CurrentSession.Username = userAccount.Username; // Set the session username
                    CurrentSession.UserRole = userAccount.UserRole; // Set the session user role
                    CurrentSession.FirstName = userAccount.FirstName; // Set the session first name
                    CurrentSession.LastName = userAccount.LastName; // Set the session last name
                    MessageBox.Show($"Welcome, {userAccount.FirstName} {userAccount.LastName}! You are logged in as {userAccount.UserRole}.",
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
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while attempting to log in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
