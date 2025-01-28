using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using MODELS;



namespace OmagiecaVMS01
{
    public partial class frmReceptionist : Form
    {
        private ucfrmRFIDMonitor ucRM;
        private ucfrmVisitor     ucV;
        public frmReceptionist()
        {
            InitializeComponent();
            ucRM = new ucfrmRFIDMonitor();
            ucV = new ucfrmVisitor();
            DisplayCurrentUserName();


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


        private void btnVisitor_Click(object sender, EventArgs e)
        {
           
        }

        private void btnReportsAndAnalytic_Click(object sender, EventArgs e)
        {
            try
            {
                var visitorControl = new ucfrmVisitor();
                LoadUserControl(visitorControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Reports and Analytics screen: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                var dashboardControl = new ucfrmADashboard();
                LoadUserControl(dashboardControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Dashboard: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVisitorStatus_Click(object sender, EventArgs e)
        {
            try
            {
                var vstatusControl = new ucfrmVisitorStatus();
                LoadUserControl(vstatusControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Visitor Status screen: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var rsettingsControl = new ucfrmUserGuideReceptionist();
                LoadUserControl(rsettingsControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Settings screen: {ex.Message}",
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

        private void ucfrmADashboard1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

        private void lblCurrentUser_Click(object sender, EventArgs e)
        {

        }
    }
}
