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

namespace OmagiecaVMS01
{
    public partial class ucfrmGroupRegister : UserControl
    {
        GroupRegistrationBLL groupRegistrationBLL = new GroupRegistrationBLL();
        public ucfrmGroupRegister()
        {
            InitializeComponent();
            txtCityMunicipality.TextChanged += TextBox_TextChanged;
            txtForeignCountry.TextChanged += TextBox_TextChanged;
        }
       

        private void ucfrmGroupRegister_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmitRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                // Build GroupRegistration object
                var group = new GroupRegistration
                {
                    RepresentativeVisitor = new Visitor
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Age = int.Parse(txtAge.Text),
                        Gender = cboGender.Text,
                        VisitorType = cboVisitorType.SelectedItem.ToString(),
                    },
                    Members = GetGroupMembersFromGrid(),
                    TotalPaymentAmount = decimal.Parse(txtTotalPayment.Text),
                    DateRegistered = DateTime.Now
                };

                // Call BLL to save the group registration
                groupRegistrationBLL.AddGroupRegistration(group);

                MessageBox.Show("Group registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting group: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<GroupMember> GetGroupMembersFromGrid()
        {
            var members = new List<GroupMember>();

            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Parsing Age, ensuring it's a valid integer
                    if (!int.TryParse(row.Cells["Age"].Value.ToString(), out int age))
                    {
                        MessageBox.Show("Invalid age format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;  // Skip this row or handle the error appropriately
                    }

                    // Parsing PaymentAmount, ensuring it's a valid decimal
                    if (!decimal.TryParse(row.Cells["PaymentAmount"].Value.ToString(), out decimal paymentAmount))
                    {
                        MessageBox.Show("Invalid payment format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;  // Skip this row or handle the error appropriately
                    }

                    // Parsing IsPWD, ensuring it's a valid boolean
                    bool isPWD = false;  // Default to false if parsing fails or if it's null
                    if (row.Cells["IsPWD"].Value != null && !bool.TryParse(row.Cells["IsPWD"].Value.ToString(), out isPWD))
                    {
                        MessageBox.Show("Invalid PWD format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;  // Optional: You may choose to continue or default to false
                    }

                    // Parsing RfidTagNumber, ensuring it's a valid integer
                    if (!int.TryParse(row.Cells["RfidTagNumberId"].Value?.ToString(), out int rfidTagNumber))
                    {
                        MessageBox.Show("Invalid RFID tag number format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;  // Skip this row or handle the error appropriately
                    }

                    // Add validated member to the list
                    members.Add(new GroupMember
                    {
                        Age = age,
                        VisitorType = row.Cells["VisitorType"].Value.ToString(),
                        IsPWD = isPWD,
                        PaymentAmount = paymentAmount,
                        RfidTagNumberId = rfidTagNumber  // Ensure this is included and validated
                    });
                }
            }

            return members;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Check if textBox1 has text and adjust textBox2's enabled state
            txtForeignCountry.Enabled = string.IsNullOrEmpty(txtCityMunicipality.Text);

            // Check if textBox2 has text and adjust textBox1's enabled state
            txtCityMunicipality.Enabled = string.IsNullOrEmpty(txtForeignCountry.Text);
        }
        private void txtForeignCountry_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCityMunicipality_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
