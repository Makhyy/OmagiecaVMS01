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
        public string Age { get; set; }
        public string VisitorType { get; set; }
        public string PaymentAmount { get; set; }
        public string RfidTag { get; set; }
        public frmAddmember()
        {
            InitializeComponent();
        }
        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            // Auto-populate VisitorType and PaymentAmount based on Age
            int age = int.Parse(txtAge.Text);

            if (age < 12)
            {
                VisitorType = "Child";
                PaymentAmount = "50"; // Example amount
            }
            else if (age <= 60)
            {
                VisitorType = "Adult";
                PaymentAmount = "100"; // Example amount
            }
            else
            {
                VisitorType = "Senior";
                PaymentAmount = "70"; // Example amount
            }

            cmbVisitorType.Text = VisitorType;
            cmbVisitorType.Text = PaymentAmount;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Age = txtAge.Text;
            VisitorType = cmbVisitorType.Text;
            PaymentAmount = txtPaymentAmount.Text;
            RfidTag = txtRfidTag.Text;

            DialogResult = DialogResult.OK; // Return data to parent form
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
