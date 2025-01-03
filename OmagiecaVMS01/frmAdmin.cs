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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var settingsControl = new ucfrmSettings();
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
            this.DialogResult = DialogResult.OK; // Set the DialogResult to OK to indicate a logout
            this.Close(); // Close the receptionist form
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
    }
}
