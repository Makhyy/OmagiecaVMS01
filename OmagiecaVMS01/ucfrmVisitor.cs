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
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class ucfrmVisitor : UserControl
    {

        
        private List<VisitorType> VisitorTypes;
        public ucfrmVisitor()
        {
            InitializeComponent();
            LoadVisitors();
            LoadVisitorTypes();
            LoadRFIDTags();
            RefreshRFIDTags();
            ClearInputs();
        }

        private void visitorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.visitorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.omagiecaVMS01DBDataSet2);

        }
        private void LoadVisitors()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                dgvVisitors.DataSource = visitorBLL.GetVisitors();

                // Reorder columns
                dgvVisitors.Columns["VisitorId"].DisplayIndex = 0;
                dgvVisitors.Columns["FirstName"].DisplayIndex = 1;
                dgvVisitors.Columns["LastName"].DisplayIndex = 2;
                dgvVisitors.Columns["Age"].DisplayIndex = 3;
                dgvVisitors.Columns["VisitorTypeName"].DisplayIndex = 4;
                dgvVisitors.Columns["IsPWD"].DisplayIndex = 5;
                dgvVisitors.Columns["Gender"].DisplayIndex = 6;
                dgvVisitors.Columns["CityMunicipality"].DisplayIndex = 7;
                dgvVisitors.Columns["ForeignCountry"].DisplayIndex = 8;
                dgvVisitors.Columns["PaymentAmount"].DisplayIndex = 9;
                dgvVisitors.Columns["RfidTagNumber"].DisplayIndex = 10;
                dgvVisitors.Columns["DateRegistered"].DisplayIndex = 11;

                // Customize column headers (if needed)
                dgvVisitors.Columns["VisitorId"].HeaderText = "Visitor ID";
                dgvVisitors.Columns["FirstName"].HeaderText = "First Name";
                dgvVisitors.Columns["LastName"].HeaderText = "Last Name";
                dgvVisitors.Columns["Age"].HeaderText = "Age";
                dgvVisitors.Columns["VisitorTypeName"].HeaderText = "Visitor Type";
                dgvVisitors.Columns["IsPWD"].HeaderText = "PWD";
                dgvVisitors.Columns["Gender"].HeaderText = "Gender";
                dgvVisitors.Columns["CityMunicipality"].HeaderText = "City/Municipality";
                dgvVisitors.Columns["ForeignCountry"].HeaderText = "Foreign Country";
                dgvVisitors.Columns["PaymentAmount"].HeaderText = "Payment Amount";
                dgvVisitors.Columns["RfidTagNumber"].HeaderText = "RFID Tag Number";
                dgvVisitors.Columns["DateRegistered"].HeaderText = "Date Registered";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadVisitorTypes()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                VisitorTypes = visitorBLL.GetVisitorTypes();
                cboVisitorType.DataSource = VisitorTypes;
                cboVisitorType.DisplayMember = "VisitorTypeName";
                cboVisitorType.ValueMember = "VisitorTypeId";
                cboVisitorType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitor types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadRFIDTags()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                var rfidTags = visitorBLL.GetRFIDTags(); // Fetch RFID tags from BLL

                if (rfidTags == null || rfidTags.Count == 0)
                {
                    MessageBox.Show("No RFID tags found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cboRFIDTag.DataSource = rfidTags;
                cboRFIDTag.DisplayMember = "RfidTagNumber"; // Column to display
                cboRFIDTag.ValueMember = "RfidTagNumberId"; // Column for SelectedValue
                cboRFIDTag.SelectedIndex = -1; // Reset selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading RFID tags: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnRegisterVisitor_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs (you can use a helper method for this)
                if (!ValidateInputs())
                    return;

                // Create a new Visitor object
                Visitor visitor = new Visitor
                {
                    VisitorTypeId = Convert.ToInt32(cboVisitorType.SelectedValue),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Age = int.Parse(txtAge.Text),
                    IsPWD = chkIsPWD.Checked,
                    Gender = cboGender.SelectedItem.ToString(),
                    CityMunicipality = string.IsNullOrWhiteSpace(txtCityMunicipality.Text) ? null : txtCityMunicipality.Text.Trim(),
                    ForeignCountry = string.IsNullOrWhiteSpace(txtForeignCountry.Text) ? null : txtForeignCountry.Text.Trim(),
                    PaymentAmount = decimal.Parse(txtPaymentAmount.Text),
                    RfidTagNumber = cboRFIDTag.SelectedValue != null ? Convert.ToInt32(cboRFIDTag.SelectedValue) : 0,
                    DateRegistered = DateTime.Now
                };

                // Add the visitor using the BLL
                VisitorBLL visitorBLL = new VisitorBLL();
                int visitorId = visitorBLL.AddVisitor(visitor);

                // Success message
                MessageBox.Show("Visitor registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh visitors and clear inputs
                LoadVisitors();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while registering the visitor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVisitors.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a visitor to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Visitor visitor = new Visitor
                {
                    VisitorId = (int)dgvVisitors.SelectedRows[0].Cells["VisitorId"].Value,
                    VisitorTypeId = Convert.ToInt32(cboVisitorType.SelectedValue),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Age = int.Parse(txtAge.Text),
                    IsPWD = chkIsPWD.Checked,
                    Gender = cboGender.SelectedItem.ToString(),
                    CityMunicipality = txtCityMunicipality.Text,
                    ForeignCountry = txtForeignCountry.Text,
                    PaymentAmount = decimal.Parse(txtPaymentAmount.Text),
                    RfidTagNumber = Convert.ToInt32(txtPaymentAmount.SelectedValue),
                    DateRegistered = DateTime.Now
                };

                VisitorBLL visitorBLL = new VisitorBLL();
                visitorBLL.UpdateVisitor(visitor);

                MessageBox.Show("Visitor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadVisitors(); // Refresh visitors
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the visitor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVisitors.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a visitor to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int visitorId = (int)dgvVisitors.SelectedRows[0].Cells["VisitorId"].Value;

                DialogResult result = MessageBox.Show("Are you sure you want to delete this visitor?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    VisitorBLL visitorBLL = new VisitorBLL();
                    visitorBLL.DeleteVisitor(visitorId);

                    MessageBox.Show("Visitor deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVisitors(); // Reload visitor list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the visitor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                VisitorBLL visitorBLL = new VisitorBLL();
                dgvVisitors.DataSource = visitorBLL.SearchVisitors(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching for visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopulateVisitorTypeAndPayment()
        {
            try
            {
                if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                {
                    // Invalid age, reset the fields
                    cboVisitorType.SelectedIndex = -1;
                    txtPaymentAmount.SelectedIndex = -1;
                }

                string visitorType;
                decimal paymentAmount;

                // Define visitor type and payment amount logic
                if (age <= 12)
                {
                    visitorType = "Child";
                    paymentAmount = 30.00m; // Example payment for a child
                }
                else if (age >= 13 && age <= 59)
                {
                    visitorType = "Adult";
                    paymentAmount = 75.00m; // Example payment for an adult
                }
                else
                {
                    visitorType = "Senior Citizen";
                    paymentAmount = 50.00m; // Example payment for a senior citizen
                }

                // Set Visitor Type and Payment Amount
                cboVisitorType.Text = visitorType;
                txtPaymentAmount.Text = paymentAmount.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating visitor type and payment amount: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cboVisitorType.SelectedIndex = -1;
            cboGender.SelectedIndex = -1;
            txtCityMunicipality.Clear();
            txtForeignCountry.Clear();
            txtPaymentAmount.SelectedIndex = -1;
            cboRFIDTag.SelectedIndex = -1;
            chkIsPWD.Checked = false;
            txtSearch.Clear();
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            PopulateVisitorTypeAndPayment();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return false;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Valid Age is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAge.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboVisitorType.Text))
            {
                MessageBox.Show("Visitor Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboVisitorType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPaymentAmount.Text) || !decimal.TryParse(txtPaymentAmount.Text, out _))
            {
                MessageBox.Show("Valid Payment Amount is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPaymentAmount.Focus();
                return false;
            }

            return true;
        }

        private void dgvVisitors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVisitors == null)
            {
                throw new Exception("The DataGridView control (dgvVisitors) is not initialized.");
            }

        }

        private void ucfrmVisitor_Load(object sender, EventArgs e)
        {
            LoadVisitors();
            ClearInputs();
            
        }
        private void RefreshRFIDTags()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                List<RFIDTag> availableTags = visitorBLL.GetAvailableRFIDTags();
                cboRFIDTag.DataSource = availableTags;
                cboRFIDTag.DisplayMember = "RfidTagNumber";
                cboRFIDTag.ValueMember = "RfidTagNumberId";
                cboRFIDTag.SelectedIndex = -1; // Reset selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing RFID tags: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVisitors_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVisitors.CurrentRow == null)
                {
                    ClearInputs();
                    return;
                }

                DataGridViewRow selectedRow = dgvVisitors.CurrentRow;

                txtFirstName.Text = selectedRow.Cells["FirstName"].Value?.ToString() ?? string.Empty;
                txtLastName.Text = selectedRow.Cells["LastName"].Value?.ToString() ?? string.Empty;
                txtAge.Text = selectedRow.Cells["Age"].Value?.ToString() ?? string.Empty;
                chkIsPWD.Checked = selectedRow.Cells["IsPWD"].Value != null && (bool)selectedRow.Cells["IsPWD"].Value;

                if (cboGender.Items.Contains(selectedRow.Cells["Gender"].Value?.ToString()))
                {
                    cboGender.SelectedItem = selectedRow.Cells["Gender"].Value?.ToString();
                }
               

                txtCityMunicipality.Text = selectedRow.Cells["CityMunicipality"].Value?.ToString() ?? string.Empty;
                txtForeignCountry.Text = selectedRow.Cells["ForeignCountry"].Value?.ToString() ?? string.Empty;

                if (dgvVisitors.Columns.Contains("VisitorTypeId") && selectedRow.Cells["VisitorTypeId"].Value != null)
                {
                    cboVisitorType.SelectedValue = selectedRow.Cells["VisitorType"].Value;
                }
                

                txtPaymentAmount.Text = selectedRow.Cells["PaymentAmount"].Value?.ToString() ?? string.Empty;

                if (dgvVisitors.Columns.Contains("RfidTagNumber") && selectedRow.Cells["RfidTagNumber"].Value != null)
                {
                    cboRFIDTag.SelectedValue = selectedRow.Cells["RfidTagNumber"].Value;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating the controls: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnGroupRegister_Click(object sender, EventArgs e)
        {
            frmGroupRegister groupRegister = new frmGroupRegister();
            groupRegister.ShowDialog();
        }
    }
}
