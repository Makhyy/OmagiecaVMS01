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

                // Assign the username and password from the text boxes
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Authenticate user
                UserAccount userAccount = userAccountBLL.AuthenticateUser(username, password);

                if (userAccount != null)
                {
                    MessageBox.Show($"Welcome, {userAccount.FirstName} {userAccount.LastName}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UserRole = userAccount.UserRole; // Assuming you have a public property UserRole defined in this form
                    this.DialogResult = DialogResult.OK; // Indicate success
                    this.Close(); // Close the login form
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while attempting to log in: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
