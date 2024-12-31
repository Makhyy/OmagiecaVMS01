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
using static OmagiecaVMS01.OmagiecaVMS01DBDataSet2;

namespace OmagiecaVMS01
{
    public partial class frmGroupRegister : Form
    {
        GroupRegistrationBLL groupRegistrationBLL = new GroupRegistrationBLL();
        public frmGroupRegister()
        {
            InitializeComponent();
            LoadRFIDTags();
        }

        private void ucfrmGroupRegister_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmitRegistration_Click(object sender, EventArgs e)
        {
            
        }
        private List<GroupMember> GetGroupMembersFromGrid()
        {
            var members = new List<GroupMember>();

            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Parse Age
                    if (!int.TryParse(row.Cells["Age"].Value.ToString(), out int age))
                    {
                        MessageBox.Show("Invalid age format. Please check the data.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    // Parse IsPWD
                    bool isPWD = false;
                    string isPWDValue = row.Cells["IsPWD"].Value?.ToString();
                    if (!bool.TryParse(isPWDValue, out isPWD))
                    {
                        if (int.TryParse(isPWDValue, out int isPWDInt))
                        {
                            isPWD = isPWDInt == 1;
                        }
                    }

                    // Parse PaymentAmount
                    if (!decimal.TryParse(row.Cells["PaymentAmount"].Value?.ToString(), out decimal paymentAmount))
                    {
                        MessageBox.Show("Invalid payment format. Please check the data.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;  // Skip this row or handle the error appropriately
                    }
                    // Parse RfidTagNumber
                    if (!int.TryParse(row.Cells["RfidTagNumberId"].Value?.ToString(), out int rfidTagNumber))
                    {
                        MessageBox.Show("Invalid RFID tag format. Please check the data.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;  // Skip this row or handle the error appropriately
                    }


                    // Add validated member to the list
                    members.Add(new GroupMember
                    {
                        Age = age,
                        VisitorType = row.Cells["VisitorType"].Value?.ToString(),
                        IsPWD = isPWD,
                        PaymentAmount = paymentAmount,
                        RfidTagNumberId = rfidTagNumber
                    });
                }
            }

            return members;
        }

        private async void btnSubmitRegistration_Click_1(object sender, EventArgs e)
        {
            // Aggregate payments and validate total
            UpdateTotalPayment();
            if (decimal.Parse(txtTotalPayment.Text) <= 0)
            {
                MessageBox.Show("Total payment amount must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate other inputs
            if (!ValidateInputs(out string validationErrors))
            {
                MessageBox.Show(validationErrors, "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Build GroupRegistration object
                var group = CreateGroupRegistration();

                // Save group registration asynchronously
                await Task.Run(() => groupRegistrationBLL.AddGroupRegistration(group));

                // Success feedback
                MessageBox.Show("Group registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting group: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            frmAddmember addMemberForm = new frmAddmember();
            if (addMemberForm.ShowDialog() == DialogResult.OK)
            {
                int rfidTag = addMemberForm.RfidTagNumberId;  

                if (!decimal.TryParse(addMemberForm.PaymentAmount.ToString(), out decimal paymentAmount))
                {
                    MessageBox.Show("Invalid payment amount format. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Add the validated data to DataGridView
                dgvMembers.Rows.Add(
                    addMemberForm.Age,
                    addMemberForm.VisitorType,
                    addMemberForm.IsPWD,
                    paymentAmount,
                    rfidTag  


                );
                UpdateTotalPaymentAndMembers();
            }
        }

        private GroupRegistration CreateGroupRegistration()
        {
            var registrationDate = DateTime.Now;  // Use this to ensure all date fields are synchronized
            string groupName = string.IsNullOrWhiteSpace(txtGroupName.Text) ? $"{txtFirstName.Text.Trim()} Group" : txtGroupName.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First Name and Last Name must not be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;  // Or handle it more appropriately depending on your application's flow
            }
            // Ensure you handle potential exceptions here too
            decimal representativePayment = GetSafeDecimal(txtPaymentAmount.Text);

            // Retrieve and calculate member payments
            var members = GetGroupMembersFromGrid();
            decimal membersTotalPayment = members.Sum(member => member.PaymentAmount);

            // Total payment is the sum of representative's payment and all members' payments
            decimal totalPaymentAmount = representativePayment + membersTotalPayment;

            return new GroupRegistration
            {
                GroupName = groupName,
                RepresentativeVisitor = new Visitor
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Age = int.TryParse(txtAge.Text, out int age) ? age : 0, // Safely parse age with a fallback
                    VisitorType = cboVisitorType.SelectedItem.ToString(),
                    IsPWD = chkIsPWD.Checked,
                    Gender = cboGender.SelectedItem.ToString(),
                    CityMunicipality = string.IsNullOrWhiteSpace(txtCityMunicipality.Text) ? null : txtCityMunicipality.Text.Trim(),
                    ForeignCountry = string.IsNullOrWhiteSpace(txtForeignCountry.Text) ? null : txtForeignCountry.Text.Trim(),
                    PaymentAmount = GetSafeDecimal(txtPaymentAmount.Text),
                    RfidTagNumberId = GetSafeRfidTagNumber(),
                    DateRegistered = registrationDate
                },
                Members = GetGroupMembersFromGrid(),
                TotalPaymentAmount = GetSafeDecimal(txtTotalPayment.Text),
                DateRegistered = registrationDate
            };
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

        private int GetSafeRfidTagNumber()
        {
            if (cboRFIDTag.SelectedValue != null && int.TryParse(cboRFIDTag.SelectedValue.ToString(), out int rfidTagNumber))
            {
                return rfidTagNumber;
            }
            return 0; // Return 0 or another appropriate default value if conversion fails
        }
        private decimal GetSafeDecimal(string input)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }
            else
            {
                MessageBox.Show("Invalid format for payment amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;  // Consider whether you want to halt processing or just warn and use a default
            }
        }

        private void UpdateTotalPayment()
        {
            decimal totalPayment = 0;
            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (row.Cells["PaymentAmount"].Value != null && decimal.TryParse(row.Cells["PaymentAmount"].Value.ToString(), out decimal paymentAmount))
                {
                    totalPayment += paymentAmount;
                }
            }
            txtTotalPayment.Text = totalPayment.ToString("F2");
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            PopulateVisitorTypeAndPayment();
        }
        private void PopulateVisitorTypeAndPayment()
        {
            try
            {
                PaymentBLL paymentBLL = new PaymentBLL();
                var payments = paymentBLL.GetAllPayments(); // Assuming this returns a DataTable

                if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                {
                    // Invalid age, reset the fields
                    cboVisitorType.SelectedIndex = -1;
                    txtPaymentAmount.Text = string.Empty;
                    return;
                }

                string visitorType;
                decimal paymentAmount;

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

                // Fetch payment amount based on visitor type
                var paymentRecord = payments.AsEnumerable()
                                            .FirstOrDefault(row => row["VisitorType"].ToString() == visitorType);

                if (paymentRecord != null)
                {
                    paymentAmount = Convert.ToDecimal(paymentRecord["PaymentAmount"]);
                }
                else
                {
                    throw new Exception("Payment record not found for visitor type: " + visitorType);
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

        private void dgvMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Check if the 'Delete' button was clicked in the DataGridView
            if (dgvMembers.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Remove the row
                dgvMembers.Rows.RemoveAt(e.RowIndex);

                // Recalculate totals after row is removed
                UpdateTotalPayment();
                UpdateTotalMembers();
            }
            else
            {
                MessageBox.Show("You can't remove an empty row ");
            }

        }
        private bool ValidateInputs(out string validationErrors)
        {
            var errors = new StringBuilder();

            // Validate representative fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                errors.AppendLine("First Name is required.");
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
                errors.AppendLine("Last Name is required.");
            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                errors.AppendLine("Age must be a valid positive number.");
            if (cboGender.SelectedItem == null)
                errors.AppendLine("Gender is required.");
            if (!decimal.TryParse(txtPaymentAmount.Text, out _))
                errors.AppendLine("Payment Amount must be a valid decimal.");

            // Validate group members
            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (!decimal.TryParse(row.Cells["PaymentAmount"].Value?.ToString(), out _))
                    {
                        errors.AppendLine($"Invalid payment amount in row {row.Index + 1}.");
                    }
                }
            }

            validationErrors = errors.ToString();
            return string.IsNullOrEmpty(validationErrors);
        }

        // Clear form fields
        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cboGender.SelectedIndex = -1;
            cboVisitorType.SelectedIndex = -1;
            chkIsPWD.Checked = false;
            txtPaymentAmount.Text = "0.00";  // Set default to zero or a similar safe value
            txtCityMunicipality.Clear();
            txtForeignCountry.Clear();
            cboRFIDTag.SelectedIndex = -1;
            txtTotalPayment.Text = "0.00";
            dgvMembers.Rows.Clear();
        }

        private void UpdateTotalPaymentAndMembers()
        {
            decimal totalPayment = 0;
            int totalMembers = 0;

            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (decimal.TryParse(row.Cells["PaymentAmount"].Value.ToString(), out decimal paymentAmount))
                    {
                        totalPayment += paymentAmount;  // Sum up the payment amount
                    }
                    totalMembers++;  // Count each row that is not the new row template
                }
            }

            txtTotalPayment.Text = totalPayment.ToString("F2");  // Display the total payment formatted as a fixed-point number
            lblTotalMembers.Text = totalMembers.ToString();  // Update a label or text box to show the number of members
        }

        private void dgvMembers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dgvMembers.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                UpdateTotalPaymentAndMembers();
            }
        }

       
        private void UpdateTotalMembers()
        {
            int totalMembers = dgvMembers.Rows.Cast<DataGridViewRow>()
                                   .Count(row => !row.IsNewRow);
            lblTotalMembers.Text = totalMembers.ToString();
        }

        private void frmGroupRegister_Load(object sender, EventArgs e)
        {
            UpdateTotalPaymentAndMembers();
        }
    }
}

