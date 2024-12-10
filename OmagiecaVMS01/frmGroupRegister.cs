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
    public partial class frmGroupRegister : Form
    {
        GroupRegistrationBLL groupRegistrationBLL = new GroupRegistrationBLL();
        public frmGroupRegister()
        {
            InitializeComponent();
            
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
                    members.Add(new GroupMember
                    {
                        Age = int.Parse(row.Cells["Age"].Value.ToString()),
                        VisitorTypeId = row.Cells["VisitorType"].Value.ToString(),
                        PaymentAmount = decimal.Parse(row.Cells["PaymentAmount"].Value.ToString())
                    });
                }
            }

            return members;
        }

        private void btnSubmitRegistration_Click_1(object sender, EventArgs e)
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
                        VisitorTypeId = (int)cboVisitorType.SelectedValue
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

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            // Open Add Member popup form
           frmAddmember addMemberForm = new frmAddmember();
            if (addMemberForm.ShowDialog() == DialogResult.OK)
            {
                // Retrieve data from the popup form
                string age = addMemberForm.Age;
                string visitorType = addMemberForm.VisitorType;
                string paymentAmount = addMemberForm.PaymentAmount;
                string rfidTag = addMemberForm.RfidTag;

                // Add data to the DataGridView
                dgvMembers.Rows.Add(age, visitorType, paymentAmount, rfidTag);
            }
        }
        private decimal GetPaymentAmountForVisitorType(string visitorTypeId)
        {
            // Example: Hardcoded payment amounts
            switch (visitorTypeId)
            {
                case "Adult":
                    return 100.00m;
                case "Child":
                    return 50.00m;
                case "Senior":
                    return 70.00m;
                case "PWD":
                    return 80.00m;
                default:
                    throw new ArgumentException("Invalid visitor type.");
            }
        }
        private void UpdateTotalPayment()
        {
            decimal totalPayment = 0;

            // Iterate through DataGridView rows and sum up the PaymentAmount column
            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (row.Cells["PaymentAmount"].Value != null)
                {
                    totalPayment += Convert.ToDecimal(row.Cells["PaymentAmount"].Value);
                }
            }

            // Update the Total Payment textbox
            txtTotalPayment.Text = totalPayment.ToString("F2");
        }
       

    }
}

