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

    }
}
