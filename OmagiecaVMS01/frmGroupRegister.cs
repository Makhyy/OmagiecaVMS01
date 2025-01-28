using BLL;
using MODELS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OmagiecaVMS01.OmagiecaVMS01DBDataSet2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OmagiecaVMS01
{
    public partial class frmGroupRegister : Form
    {
        decimal paymentAmount = 0;
        GroupRegistrationBLL groupRegistrationBLL = new GroupRegistrationBLL();

        private PaymentBLL paymentBLL;
        private VisitorBLL visitorBLL;
        private RFIDTagBLL rfidTagBLL = new RFIDTagBLL();

        private VisitorBLL _visitorBLL = new VisitorBLL();
        public frmGroupRegister()
        {
            InitializeComponent();
            LoadAvailableRFIDTagsToDisplay();
            txtCityMunicipality.TextChanged += TextBox_TextChanged;
            txtForeignCountry.TextChanged += TextBox_TextChanged;
        }

        private void ucfrmGroupRegister_Load(object sender, EventArgs e)
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
        public async Task AddGroupRegistrationAsync(GroupRegistration groupRegistration)
        {
            await groupRegistrationBLL.AddGroupRegistrationAsync(groupRegistration);
        }

        private async void btnSubmitRegistration_Click_1(object sender, EventArgs e)
        {
            // Aggregate payments and validate total
            UpdateTotalPayment();

            // Validate other inputs
            if (!ValidateInputFields(out string validationErrors))
            {
                MessageBox.Show(validationErrors, "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvMembers.Rows.Count == 0 || dgvMembers.Rows.Cast<DataGridViewRow>().All(row => row.IsNewRow))
            {
                MessageBox.Show("You must add at least one member to the group.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Build GroupRegistration object
                var group = CreateGroupRegistration(); // Make sure this method returns a valid GroupRegistration object

                // Save group registration asynchronously
                await groupRegistrationBLL.AddGroupRegistrationAsync(group);  // Pass the correct object here (group)

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
            string groupName = GetGroupName();
            Visitor representative = CreateRepresentativeVisitor();
            var members = GetGroupMembersFromGrid();
            decimal totalPayment = CalculateTotalPayment();

            return new GroupRegistration
            {
                GroupName = groupName,
                RepresentativeVisitor = representative,
                Members = members,
                TotalPaymentAmount = totalPayment,
                DateRegistered = DateTime.Now
            };
        }

        private string GetGroupName()
        {
            return string.IsNullOrWhiteSpace(txtGroupName.Text)
                ? $"{txtFirstName.Text.Trim()} Group"
                : txtGroupName.Text.Trim();
        }

        private Visitor CreateRepresentativeVisitor()
        {
            return new Visitor
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Age = int.Parse(txtAge.Text),
                VisitorType = cboVisitorType.SelectedItem.ToString(),
                IsPWD = chkIsPWD.Checked,
                Gender = cboGender.SelectedItem.ToString(),
                CityMunicipality = txtCityMunicipality.Text.Trim(),
                ForeignCountry = txtForeignCountry.Text.Trim(),
                PaymentAmount = decimal.Parse(txtPaymentAmount.Text),
                RfidTagNumberId = (int)cboRFIDTag.SelectedValue,
                DateRegistered = DateTime.Now
            };
        }

        private decimal CalculateTotalPayment()
        {
            decimal representativePayment = GetSafeDecimal(txtPaymentAmount.Text);
            decimal membersPayment = GetGroupMembersFromGrid().Sum(member => member.PaymentAmount);
            return representativePayment + membersPayment;
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
        private bool ValidateInputFields(out string errors)
        {
            var errorList = new List<string>();

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                errorList.Add("First Name is required.");
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
                errorList.Add("Last Name is required.");
            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                errorList.Add("Valid Age is required.");
            if (cboGender.SelectedItem == null)
                errorList.Add("Gender is required.");
            if (!decimal.TryParse(txtPaymentAmount.Text, out _))
                errorList.Add("Valid Payment Amount is required.");

            errors = string.Join(Environment.NewLine, errorList);
            return !errorList.Any();
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
            decimal representativePayment = paymentAmount;
            int representativeVisitor = 1;
            int totalMembers = 0;



            if (!string.IsNullOrWhiteSpace(txtFirstName.Text))
            {

                // lblTotalMembers.Text = "1";
                totalMembers = representativeVisitor + totalMembers;
            }
            if (decimal.TryParse(txtPaymentAmount.Text, out decimal defaultPayment))
            {
                totalPayment = totalPayment + defaultPayment; // Start with the default value from the default TextBox
                txtTotalPayment.Text = defaultPayment.ToString("F2");  // Display the total payment formatted as a fixed-point number
            }

            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (decimal.TryParse(row.Cells["PaymentAmount"].Value.ToString(), out decimal paymentAmount))
                    {
                        totalPayment += paymentAmount;  // Sum up the payment amount  // Sum up the payment amount
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
            txtPaymentAmount.Text = "0.00";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Check if textBox1 has text and adjust textBox2's enabled state
            txtForeignCountry.Enabled = string.IsNullOrEmpty(txtCityMunicipality.Text);

            // Check if textBox2 has text and adjust textBox1's enabled state
            txtCityMunicipality.Enabled = string.IsNullOrEmpty(txtForeignCountry.Text);
        }

        private void LoadAvailableRFIDTagsToDisplay()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                var rfidTags = visitorBLL.GetAvailableRFIDTagsToDisplay(); // Fetch RFID tags from BLL

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

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFirstName.Text))
            {

                lblTotalMembers.Text = "1";

            }
        }

        private void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}