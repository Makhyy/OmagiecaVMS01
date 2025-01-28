using System;
using BLL;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MODELS;

namespace OmagiecaVMS01
{
    public partial class frmAdmin : Form
    {
        private VisitorBLL visitorBLL;
        private ucfrmADashboard ucfrmADashboard;
        public frmAdmin()
        {
            InitializeComponent();
            DisplayCurrentUserName();

          //  InitializeDashboard();


        }
        private void DisplayCurrentUserName()
        {
            // Assuming CurrentSession stores the current user's first name and last name
            if (!string.IsNullOrEmpty(CurrentSession.FirstName) && !string.IsNullOrEmpty(CurrentSession.LastName))
            {
                lblCurrentUser.Text = $"Welcome, {CurrentSession.FirstName} {CurrentSession.LastName}!";
            }
            else
            {
                lblCurrentUser.Text = "Welcome, guest!";
            }
        }
        public void LoadUserControl(UserControl userControl)
        {
            try
            {
                // Dispose of existing controls to free resources
                if (mainPanel.Controls.Count > 0)
                {
                    mainPanel.Controls[0]?.Dispose();
                }

                // Clear existing controls in the panel
                mainPanel.Controls.Clear();

                // Dock the UserControl to fill the panel
                userControl.Dock = DockStyle.Fill;

                // Add the UserControl to the panel
                mainPanel.Controls.Add(userControl);
            }
            catch (Exception ex)
            {
                // Handle any errors during UserControl loading
                MessageBox.Show($"An error occurred while loading the control: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRFIDTags_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the RFID Tag Management UserControl
                var rfidTagControl = new ucfrmRFIDTagManagement();

                // Load the UserControl into the main form's panel
                LoadUserControl(rfidTagControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the RFID Tags screen: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            try
            {
                var reportsControl = new ucfrmReports();
                LoadUserControl(reportsControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Reports screen: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                var paymentsControl = new ucfrmPayments();
                LoadUserControl(paymentsControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Payments screen: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            try
            {
                var usersControl = new ucfrmUsers();
                LoadUserControl(usersControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Users screen: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
                
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var settingsControl = new ucfrmUserGuideAdmin();
                LoadUserControl(settingsControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Settings screen: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnADashboard_Click(object sender, EventArgs e)
        {

            try
            {
                var adashboardControl = new ucfrmADashboard();
                LoadUserControl(adashboardControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Admin Dashboard: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Prompt the user to confirm they want to log out
            if (MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CurrentSession.Logout(); // Clear the current session
                this.DialogResult = DialogResult.OK; // Set the DialogResult to OK to indicate a logout
                this.Hide(); // Close the current main form (receptionist/admin form)
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();
                this.Close();
            }
        }

        private void btnVisitorManagement_Click(object sender, EventArgs e)
        {

            try
            {
                var visitorManagement = new ucfrmVisitor();
                LoadUserControl(visitorManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Admin Dashboard: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void lblCurrentUser_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            // Ensure there is a valid user logged in
            if (CurrentSession.UserAccountId > 0)
            {
                // Initialize the profile editing form with the current user's ID
                frmUserProfile userProfileForm = new frmUserProfile(CurrentSession.UserAccountId);
                userProfileForm.ShowDialog(); // Show form as a modal dialog box
            }
            else
            {
                MessageBox.Show("No user is currently logged in. Please log in to edit a profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ucfrmADashboard2_Load(object sender, EventArgs e)
        {

        }

        /*  public void InitializeDashboard()
          {
              if (CurrentSession.UserAccountId > 0)
              {
                  // Assume CurrentSession.UserRole contains the role of the logged-in user
                  pictureBox3.Enabled = CurrentSession.UserRole == "Admin" || CurrentSession.UserRole == "User";
              }
              else
              {
                //  pictureBox3.Enabled = false; // Disable if no user is logged in
              }
          }*/




    }
}
