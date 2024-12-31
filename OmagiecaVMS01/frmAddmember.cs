using BLL;
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
    public partial class frmAddmember : Form
    {
        public int Age { get; set; }
        public string VisitorType { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool    IsPWD { get; set; }
        public int RfidTagNumberId { get; set; }
        public frmAddmember()
        {
            InitializeComponent();
            LoadRFIDTags();
        }
        private void txtAge_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Age = int.Parse(txtAge.Text);
            VisitorType = cboVisitorType.Text;
            IsPWD = chkIsPWD.Checked;
            PaymentAmount = decimal.Parse(txtPaymentAmount.Text);

            RfidTagNumberId = int.Parse(cboRFIDTag.Text);

            DialogResult = DialogResult.OK; // Return data to parent form
            Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtAge_TextChanged_1(object sender, EventArgs e)
        {
            PopulateVisitorTypeAndPayment();
        }

        private void frmAddmember_Load(object sender, EventArgs e)
        {
           
        }
    }
}
