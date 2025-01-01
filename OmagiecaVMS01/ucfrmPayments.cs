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
                // Validate Visitor Type
                if (string.IsNullOrWhiteSpace(cmbPaymentAmountName.Text))
                {
                    MessageBox.Show(
                        "Please enter or select a Visitor Type.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                string visitorType = cmbPaymentAmountName.Text;

                // Validate Payment Amount
                if (!decimal.TryParse(txtPaymentAmount.Text, out decimal paymentAmount) || paymentAmount <= 0)
                {
                    MessageBox.Show(
                        "Please enter a valid Payment Amount greater than 0.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Validate PWD Discount
                if (!decimal.TryParse(txtPWDDiscount.Text, out decimal pwdDiscount) || pwdDiscount < 0 || pwdDiscount > paymentAmount)
                {
                    MessageBox.Show(
                        "PWD Discount must be a number between 0 and the Payment Amount.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Check if the Visitor Type already exists
                DataTable paymentData = paymentBLL.GetAllPayments();
                DataRow[] existingRows = paymentData.Select($"VisitorType = '{visitorType}'");

                if (existingRows.Length > 0)
                {
                    // Visitor Type already exists
                    DialogResult result = MessageBox.Show(
                        "Visitor Type already exists. Do you want to update the existing record?",
                        "Update Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        int paymentId = Convert.ToInt32(existingRows[0]["PaymentId"]);
                        bool isUpdated = paymentBLL.UpdatePayment(paymentId, visitorType, paymentAmount, pwdDiscount);

                        if (isUpdated)
                        {
                            MessageBox.Show(
                                "Payment details updated successfully.",
                                "Update Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to update payment details. Please try again.",
                                "Update Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
                else
                {
                    // Visitor Type does not exist, add a new record
                    bool isAdded = paymentBLL.AddPayment(visitorType, paymentAmount, pwdDiscount);

                    if (isAdded)
                    {
                        MessageBox.Show(
                            "Payment added successfully.",
                            "Add Successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to add payment. Please try again.",
                            "Add Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }

                // Refresh the DataGridView and clear fields
                LoadPaymentData();
                ClearFields();
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Invalid input format. Please ensure all fields are filled correctly.",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }




        private void ucfrmPayments_Load(object sender, EventArgs e)
        {
            LoadPaymentData();
            PaymentId.Visible = false;
            txtPaymentId.Visible = false;

            // Populate Visitor Type ComboBox
            cmbPaymentAmountName.Items.Clear();
            cmbPaymentAmountName.Items.Add("Child");
            cmbPaymentAmountName.Items.Add("Adult");
            cmbPaymentAmountName.Items.Add("Senior Citizen");


            cmbPaymentAmountName.SelectedIndex = -1; // Ensure no default selection

            // Set default values for input fields
            txtPaymentAmount.Text = "0.00"; // Default value for Payment Amount
            txtPWDDiscount.Text = "0.00";  // Default value for PWD Discount

            // Optional: Set focus to the first field
            cmbPaymentAmountName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a row is selected in the DataGridView
                if (dgvPayments.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Please select a payment record to delete.",
                        "Delete Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Get the PaymentId from the selected row
                int paymentId = Convert.ToInt32(dgvPayments.SelectedRows[0].Cells["PaymentId"].Value);

                // Confirm deletion with the user
                DialogResult dialogResult = MessageBox.Show(
                    "Are you sure you want to delete this payment? This action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // Attempt to delete the payment record
                    bool isDeleted = paymentBLL.DeletePayment(paymentId);

                    if (isDeleted)
                    {
                        MessageBox.Show(
                            "Payment record deleted successfully.",
                            "Delete Successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        LoadPaymentData(); // Refresh DataGridView to reflect changes
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to delete the payment record. Please try again.",
                            "Delete Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Invalid selection. Please ensure you have selected a valid payment record.",
                    "Delete Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void ClearFields()
        {
            cmbPaymentAmountName.SelectedIndex = -1;
            txtPaymentAmount.Text = string.Empty;
            txtPWDDiscount.Text = string.Empty;

            if (dgvPayments.DataSource != null)
            {
                dgvPayments.ClearSelection();
            }

            cmbPaymentAmountName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset ComboBox (Visitor Type)
                cmbPaymentAmountName.SelectedIndex = -1; // Clear selection
                cmbPaymentAmountName.Text = string.Empty; // Clear any text input (if allowed)

                // Reset TextBox fields
                txtPaymentAmount.Text = "0.00";          // Reset Payment Amount
                txtPWDDiscount.Text = "0.00";           // Reset PWD Discount

                // Clear DataGridView selection (if applicable)
                if (dgvPayments.DataSource != null)
                {
                    dgvPayments.ClearSelection();
                }

               
                // Notify user
                MessageBox.Show("Fields have been cleared and reset to default values!", "Clear Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate if a Payment ID is selected
                if (string.IsNullOrEmpty(PaymentId.Text))
                {
                    MessageBox.Show(
                        "Please select a payment record to update.",
                        "Update Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                int paymentId = int.Parse(PaymentId.Text);
                string visitorType = cmbPaymentAmountName.Text;

                // Validate Payment Amount
                if (!decimal.TryParse(txtPaymentAmount.Text, out decimal paymentAmount) || paymentAmount <= 0)
                {
                    MessageBox.Show(
                        "Please enter a valid Payment Amount greater than 0.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Validate PWD Discount
                if (!decimal.TryParse(txtPWDDiscount.Text, out decimal pwdDiscount) || pwdDiscount < 0 || pwdDiscount > paymentAmount)
                {
                    MessageBox.Show(
                        "PWD discount must be a number between 0 and the Payment Amount.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Attempt to update the payment
                bool isUpdated = paymentBLL.UpdatePayment(paymentId, visitorType, paymentAmount, pwdDiscount);

                if (isUpdated)
                {
                    MessageBox.Show(
                        "Payment details updated successfully!",
                        "Update Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    LoadPaymentData(); // Refresh the DataGridView
                }
                else
                {
                    MessageBox.Show(
                        "Failed to update the payment details. Please try again.",
                        "Update Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Invalid input format. Please ensure all fields are filled correctly.",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void dgvPayments_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPayments.Rows[e.RowIndex];

                PaymentId.Text = row.Cells["PaymentId"].Value.ToString();
                cmbPaymentAmountName.Text = row.Cells["VisitorType"].Value.ToString();
                txtPaymentAmount.Text = row.Cells["PaymentAmount"].Value.ToString();
                txtPWDDiscount.Text = row.Cells["PWDDiscount"].Value.ToString();
            }
        }
    }
}   
