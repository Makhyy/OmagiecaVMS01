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



namespace OmagiecaVMS01
{
    public partial class frmReceptionist : Form
    {
        public frmReceptionist()
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
                var rsettingsControl = new ucfrmRSettings();
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
            this.DialogResult = DialogResult.OK; // Set the DialogResult to OK to indicate a logout
            this.Close(); // Close the receptionist form
        }
    }
}
