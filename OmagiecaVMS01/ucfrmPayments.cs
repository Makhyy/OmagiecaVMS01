using BLL;
using MODELS;
using MODELS.Enums;
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
    public partial class ucfrmPayments : UserControl
    {
        private PaymentBLL paymentBLL;
        public ucfrmPayments()
        {
            InitializeComponent();
            paymentBLL = new PaymentBLL();
        }
        private void LoadPaymentData()
        {
            try
            {
                DataTable paymentData = paymentBLL.GetAllPayments();
                dgvPayments.DataSource = paymentData;
                dgvPayments.Columns["PaymentId"].Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment data: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string visitorType = cmbPaymentAmountName.SelectedItem.ToString();
                decimal paymentAmount = decimal.Parse(txtPaymentAmount.Text);
                

                bool isAdded = paymentBLL.AddPayment(visitorType, paymentAmount);
                ClearFields();
                if (isAdded)
                {
                    MessageBox.Show("Payment added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add payment.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ucfrmPayments_Load(object sender, EventArgs e)
        {
            LoadPaymentData();
            PaymentId.Visible = false;
            txtPaymentId.Visible = false;


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPayments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a payment record to delete.");
                    return;
                }

                // Get the PaymentId from the selected row
                int paymentId = Convert.ToInt32(dgvPayments.SelectedRows[0].Cells["PaymentId"].Value);

                // Confirm deletion
                DialogResult dialogResult = MessageBox.Show(
                    "Are you sure you want to delete this payment?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // Call the BLL delete method
                    bool isDeleted = paymentBLL.DeletePayment(paymentId);

                    if (isDeleted)
                    {
                        MessageBox.Show("Payment deleted successfully!");
                        LoadPaymentData(); // Refresh DataGridView to reflect changes
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete payment.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void ClearFields()
        {
            // Clear the ComboBox selection
            cmbPaymentAmountName.SelectedIndex = -1; // Or reset to a default value if needed

            // Clear the Payment Amount TextBox
            txtPaymentAmount.Text = string.Empty;

            // Optionally clear the DataGridView selection
            if (dgvPayments.DataSource != null)
            {
                dgvPayments.ClearSelection();
            }

            // Reset focus to the first input field (optional)
            cmbPaymentAmountName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFields();
                MessageBox.Show("Fields have been cleared!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the Payment ID
                if (string.IsNullOrEmpty(PaymentId.Text))
                {
                    MessageBox.Show("Please select a payment record to update.");
                    return;
                }

                int paymentId = int.Parse(PaymentId.Text); // Get PaymentId
                string paymentAmountName = cmbPaymentAmountName.Text; // Get selected PaymentAmountName
                if (!decimal.TryParse(txtPaymentAmount.Text, out decimal paymentAmount) || paymentAmount <= 0)
                {
                    MessageBox.Show("Please enter a valid payment amount.");
                    return;
                }

                // Call BLL to update the payment
                bool isUpdated = paymentBLL.UpdatePayment(paymentId, paymentAmountName, paymentAmount);

                if (isUpdated)
                {
                    MessageBox.Show("Payment updated successfully!");
                    LoadPaymentData(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("Failed to update payment.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dgvPayments_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = dgvPayments.Rows[e.RowIndex];

                // Populate the controls with data from the selected row
                PaymentId.Text = row.Cells["PaymentId"].Value.ToString(); // Payment ID (hidden or read-only)
                cmbPaymentAmountName.Text = row.Cells["VisitorType"].Value.ToString(); // Payment type
                txtPaymentAmount.Text = row.Cells["PaymentAmount"].Value.ToString(); // Payment amount
            }
        }
    }
}   
