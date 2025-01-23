using BLL;
using MODELS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MODELS.CurrentSession;

namespace OmagiecaVMS01
{
    public partial class frmUserProfileEdit : Form
    {
       

        private UserAccountBLL userAccountBLL;
        private int userId;
        public frmUserProfileEdit(int userId)
        {
            InitializeComponent();
          
            

            this.userId = userId;
            userAccountBLL = new UserAccountBLL(); // Assuming the BLL handles connection strings internally
            LoadUserData();
            
        }
      

        private void LoadUserData()
        {
            var user = userAccountBLL.GetUserAccount(userId);
            if (user != null)
            {

                txtUserAccountId.Text = user.UserAccountId.ToString();
                txtFirstName.Text = user.FirstName ?? "";
                txtLastName.Text = user.LastName ?? "";
                txtAge.Text = user.Age?.ToString() ?? "";
                cboGender.SelectedIndex = cboGender.Items.IndexOf(user.Gender);
                txtAddress.Text = user.Address?.ToString() ?? "";
                cboUserRole.SelectedIndex = cboUserRole.Items.IndexOf(user.UserRole);





                // Populate other fields accordingly
            }
            else
            {
                MessageBox.Show("Failed to load user data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
       
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (Application.OpenForms["Form2"] is frmUserProfile form2)
            {
                form2.LoadUserData();  // Calls a method on Form2 to refresh it
            }
        }
        private void frmUserProfileEdit_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Use int.TryParse to safely convert UserAccountId and Age from string to int.
            if (!int.TryParse(txtUserAccountId.Text, out int UserAccountId))
            {
                MessageBox.Show("Invalid User Account ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop further execution if parsing fails
            }

            string FirstName = txtFirstName.Text;
            string LastName = txtLastName.Text;

            if (!int.TryParse(txtAge.Text, out int Age) || Age <= 0)
            {
                MessageBox.Show("Please enter a valid age.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop further execution if parsing fails
            }

            // Ensure that a gender and user role are selected
            if (cboGender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string Gender = cboGender.SelectedItem.ToString();

            if (cboUserRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a user role.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string UserRole = cboUserRole.SelectedItem.ToString();

            string Address = txtAddress.Text;

            // Call the BLL to update the user account
            var useraccount = new UserAccountBLL();
            if (useraccount.UpdateUserAccountFromEdit(UserAccountId, FirstName, LastName, Age, Gender, Address, UserRole))
            {
                MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
