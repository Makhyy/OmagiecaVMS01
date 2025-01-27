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
        public int VisitId { get; set; }    
        public frmAddmember()
        {
            InitializeComponent();
            LoadAvailableRFIDTagsToDisplay();
            chkIsPWD.CheckedChanged += chkIsPWD_CheckedChanged;
        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            Age = int.Parse(txtAge.Text);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
            VisitorType = cboVisitorType.Text;
            IsPWD = chkIsPWD.Checked;
            PaymentAmount = decimal.Parse(txtPaymentAmount.Text);

            RfidTagNumberId = int.Parse(cboRFIDTag.Text);

            DialogResult = DialogResult.OK; // Return data to parent form
           // LoadAvailableRFIDTagsToDisplay();
            Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        

        private void frmAddmember_Load(object sender, EventArgs e)
        {
           
        }

        private void chkIsPWD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.TryParse(txtPaymentAmount.Text, out decimal originalAmount))
                {
                    string visitorType = cboVisitorType.Text;

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
    }
}
