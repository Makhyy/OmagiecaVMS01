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
using static OmagiecaVMS01.OmagiecaVMS01DBDataSet2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OmagiecaVMS01
{

    public partial class ucfrmVisitor : UserControl
    {
        private PaymentBLL paymentBLL;
        private VisitorBLL visitorBLL;


        private List<VisitorType> VisitorTypes;
        public ucfrmVisitor()
        {
            InitializeComponent();
            LoadVisitors();

            LoadRFIDTags();
            RefreshRFIDTags();
            ClearInputs();
            txtCityMunicipality.TextChanged += TextBox_TextChanged;
            txtForeignCountry.TextChanged += TextBox_TextChanged;

            dgvVisitors.Columns["UserAccountId"].Visible = false;

            ucfrmRFIDMonitor rfidMonitor = new ucfrmRFIDMonitor();
            rfidMonitor.Dock = DockStyle.Fill;
            pnlRFIDMonitor.Controls.Add(rfidMonitor);
            dgvVisitors.Columns["EntryTime"].Visible = false;
            dgvVisitors.Columns["ExitTime"].Visible = false;
            dgvVisitors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVisitors.MultiSelect = true;
        }

        private void visitorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.visitorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.omagiecaVMS01DBDataSet2);

        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Check if textBox1 has text and adjust textBox2's enabled state
            txtForeignCountry.Enabled = string.IsNullOrEmpty(txtCityMunicipality.Text);

            // Check if textBox2 has text and adjust textBox1's enabled state
            txtCityMunicipality.Enabled = string.IsNullOrEmpty(txtForeignCountry.Text);
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
                dgvVisitors.Columns["VisitorType"].DisplayIndex = 4;
                dgvVisitors.Columns["IsPWD"].DisplayIndex = 5;
                dgvVisitors.Columns["Gender"].DisplayIndex = 6;
                dgvVisitors.Columns["CityMunicipality"].DisplayIndex = 7;
                dgvVisitors.Columns["ForeignCountry"].DisplayIndex = 8;
                dgvVisitors.Columns["PaymentAmount"].DisplayIndex = 9;

                dgvVisitors.Columns["DateRegistered"].DisplayIndex = 10;


                // Customize column headers (if needed)
                dgvVisitors.Columns["VisitorId"].HeaderText = "Visitor ID";
                dgvVisitors.Columns["FirstName"].HeaderText = "First Name";
                dgvVisitors.Columns["LastName"].HeaderText = "Last Name";
                dgvVisitors.Columns["Age"].HeaderText = "Age";
                dgvVisitors.Columns["VisitorType"].HeaderText = "Visitor Type";
                dgvVisitors.Columns["IsPWD"].HeaderText = "PWD";
                dgvVisitors.Columns["Gender"].HeaderText = "Gender";
                dgvVisitors.Columns["CityMunicipality"].HeaderText = "City/Municipality";
                dgvVisitors.Columns["ForeignCountry"].HeaderText = "Foreign Country";
                dgvVisitors.Columns["PaymentAmount"].HeaderText = "Payment Amount";
                dgvVisitors.Columns["DateRegistered"].HeaderText = "Date Registered";

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Age = int.Parse(txtAge.Text),
                    VisitorType = cboVisitorType.SelectedItem.ToString(),
                    IsPWD = chkIsPWD.Checked,
                    Gender = cboGender.SelectedItem.ToString(),
                    CityMunicipality = string.IsNullOrWhiteSpace(txtCityMunicipality.Text) ? null : txtCityMunicipality.Text.Trim(),
                    ForeignCountry = string.IsNullOrWhiteSpace(txtForeignCountry.Text) ? null : txtForeignCountry.Text.Trim(),
                    PaymentAmount = decimal.Parse(txtPaymentAmount.Text),
                    RfidTagNumberId = cboRFIDTag.SelectedValue != null ? Convert.ToInt32(cboRFIDTag.SelectedValue) : 0,
                    DateRegistered = DateTime.Now
                };

                // Assume payment amount and RFID tag number are entered directly in the form
                decimal paymentAmount = decimal.Parse(txtPaymentAmount.Text);
                int rfidTagNumber = Convert.ToInt32(cboRFIDTag.SelectedValue);

                // Use the BLL to handle complete registration including validation, payment, and RFID assignment
                VisitorBLL visitorBLL = new VisitorBLL();
                visitorBLL.RegisterVisitor(visitor, rfidTagNumber);

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
                    VisitorType = cboVisitorType.SelectedItem.ToString(),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Age = int.Parse(txtAge.Text),
                    IsPWD = chkIsPWD.Checked,
                    Gender = cboGender.SelectedItem.ToString(),
                    CityMunicipality = txtCityMunicipality.Text,
                    ForeignCountry = txtForeignCountry.Text,
                    PaymentAmount = decimal.Parse(txtPaymentAmount.Text),
                    RfidTagNumberId = Convert.ToInt32(txtPaymentAmount.SelectedValue),
                    DateRegistered = DateTime.Now
                };

                VisitorBLL visitorBLL = new VisitorBLL();
                visitorBLL.UpdateVisitor(visitor);

                MessageBox.Show("Visitor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadVisitors(); // Refresh visitors
                ClearInputs();
                LoadRFIDTags();
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
                    ClearInputs();
                    LoadRFIDTags();
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
                var results = visitorBLL.SearchVisitors(keyword); // Ensure this method exists and is implemented correctly

                dgvVisitors.DataSource = results;
                if (results == null || results.Count == 0)
                {
                    MessageBox.Show("No records found.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopulateVisitorTypeAndPayment()
        {
            try
            {
                PaymentBLL paymentBLL = new PaymentBLL();
                DataTable payments = paymentBLL.GetAllPayments(); // Fetch all payments

                if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                {
                    cboVisitorType.SelectedIndex = -1;
                    txtPaymentAmount.Text = string.Empty;
                    return;
                }

                string visitorType;
                decimal paymentAmount = 0;

                // Determine visitor type based on age
                if (age <= 12)
                {
                    visitorType = "Child";
                }
                else if (age >= 13 && age <= 59)
                {
                    visitorType = "Adult";
                }
                else
                {
                    visitorType = "Senior Citizen";
                }

                // Fetch payment amount for the visitor type
                var paymentRecord = payments.AsEnumerable()
                                            .FirstOrDefault(row => row["VisitorType"].ToString() == visitorType);

                if (paymentRecord != null)
                {
                    paymentAmount = Convert.ToDecimal(paymentRecord["PaymentAmount"]);
                }
                else
                {
                    throw new Exception($"Payment record not found for visitor type: {visitorType}");
                }

                // Update the UI
                cboVisitorType.Text = visitorType;
                txtPaymentAmount.Text = paymentAmount.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating visitor type and payment amount: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                if (int.TryParse(txtAge.Text, out int age) && age > 0)
                {
                    PaymentBLL paymentBLL = new PaymentBLL();
                    DataTable payments = paymentBLL.GetAllPayments();

                    string visitorType;
                    decimal basePaymentAmount = 0;

                    // Determine visitor type
                    if (age <= 12)
                    {
                        visitorType = "Child";
                    }
                    else if (age >= 13 && age <= 59)
                    {
                        visitorType = "Adult";
                    }
                    else
                    {
                        visitorType = "Senior Citizen";
                    }

                    // Fetch payment amount for the visitor type
                    DataRow paymentRecord = payments.AsEnumerable()
                                                    .FirstOrDefault(row => row["VisitorType"].ToString() == visitorType);
                    if (paymentRecord != null)
                    {
                        basePaymentAmount = Convert.ToDecimal(paymentRecord["PaymentAmount"]);
                    }

                    // Update UI
                    cboVisitorType.Text = visitorType;
                    txtPaymentAmount.Text = basePaymentAmount.ToString("F2");
                }
                else
                {
                    cboVisitorType.SelectedIndex = -1;
                    txtPaymentAmount.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating payment amount: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            txtPaymentAmount.Text = "0.00"; // Default value for Payment Amount

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

                if (dgvVisitors.Columns.Contains("VisitorType") && selectedRow.Cells["VisitorType"].Value != null)
                {
                    cboVisitorType.SelectedValue = selectedRow.Cells["VisitorType"].Value;
                }


                txtPaymentAmount.Text = selectedRow.Cells["PaymentAmount"].Value?.ToString() ?? string.Empty;

                if (dgvVisitors.Columns.Contains("RfidTagNumberId") && selectedRow.Cells["RfidTagNumberId"].Value != null)
                {
                    cboRFIDTag.SelectedValue = selectedRow.Cells["RfidTagNumberId"].Value;
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

        private void dgvVisitors_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void chkIsPWD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.TryParse(txtPaymentAmount.Text, out decimal originalAmount))
                {
                    string visitorType = cboVisitorType.SelectedItem?.ToString();

                    if (string.IsNullOrEmpty(visitorType))
                    {
                        MessageBox.Show("Please select a visitor type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Call the BLL to get the PWD discount for the selected visitor type
                    PaymentBLL paymentBLL = new PaymentBLL();
                    decimal pwdDiscount = paymentBLL.GetPWDDiscount(visitorType);



                    if (chkIsPWD.Checked)
                    {
                        decimal discountedAmount = originalAmount - pwdDiscount;
                        if (discountedAmount < 0) discountedAmount = 0;
                        txtPaymentAmount.Text = discountedAmount.ToString("F2");
                    }
                    else
                    {
                        PopulateVisitorTypeAndPayment(); // Restore original amount if unchecked
                    }
                }
                else
                {
                    MessageBox.Show("Invalid payment amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while applying the PWD discount: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void txtPaymentAmount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadVisitors(); // Load all visitors again
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}