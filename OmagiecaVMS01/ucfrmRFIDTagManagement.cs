using MODELS;
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
using MODELS.Enums;

namespace OmagiecaVMS01
{
    public partial class ucfrmRFIDTagManagement : UserControl
    {
        public ucfrmRFIDTagManagement()
        {
            InitializeComponent();
            PopulateRFIDStatus(); // Populate the ComboBox
            LoadRFIDTags(); // Load initial data
            dgvRFIDTags.SelectionChanged += dgvRFIDTags_SelectionChanged;
            txtSearch.TextChanged += txtSearch_TextChanged; // Ensure this line is added
            cboRfidStatus.Text = "Available";
            dgvRFIDTags.Columns["VisitorId"].Visible = false; 
        }
        private void PopulateRFIDStatus()
        {
            cboRfidStatus.DataSource = Enum.GetValues(typeof(RFIDTagStatus));
            cboRfidStatus.SelectedIndex = -1; // Optional: start with no selection
        }

        private void rfidTagBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.rfidTagBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.omagiecaVMS01DBDataSet2);

        }
        private void LoadRFIDTags()
        {
            try
            {
                RFIDTagBLL rfidBLL = new RFIDTagBLL();
                dgvRFIDTags.DataSource = rfidBLL.GetAllRFIDTags();
                CustomizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading RFID tags: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSaveTag_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate RFID UID
                if (string.IsNullOrWhiteSpace(txtRfidTagUID.Text))
                {
                    MessageBox.Show("RFID Tag UID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRfidTagUID.Focus();
                    return;
                }

                // Validate and parse RFID Tag Number
                if (!int.TryParse(txtRfidTagNumber.Text, out int rfidNumber) || rfidNumber <= 0)
                {
                    MessageBox.Show("Please enter a valid RFID Tag Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRfidTagNumber.Focus();
                    return;
                }

                 //Validate RFID Status
                if (cboRfidStatus.SelectedItem == null ||
                    !Enum.TryParse<RFIDTagStatus>(cboRfidStatus.SelectedItem.ToString(), out RFIDTagStatus status))
                {
                    MessageBox.Show("Please select a valid RFID status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboRfidStatus.Focus();
                    return;
                }

                // Check for duplicate RFID Tag Number
                RFIDTagBLL rfidBLL = new RFIDTagBLL();
                if (rfidBLL.IsRFIDTagNumberExists(rfidNumber))
                {
                    MessageBox.Show("The RFID Tag Number already exists. Please use a different number.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRfidTagNumber.Focus();
                    return;
                }

                // Create RFIDTag object
                RFIDTag rfidTag = new RFIDTag
                {
                    RfidTagUID = txtRfidTagUID.Text.Trim(),
                    RfidTagNumber = rfidNumber,
                    RfidStatus = status,
                };

                // Save RFID Tag
                rfidBLL.AddRFIDTag(rfidTag);

                MessageBox.Show("RFID Tag added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload DataGridView
                LoadRFIDTags();

                // Clear input fields
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the RFID tag: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            txtRfidTagUID.Clear();
            txtRfidTagNumber.Clear();
            cboRfidStatus.SelectedIndex = -1;
            txtRfidTagUID.Focus();
        }
        private void CustomizeColumns()
        {
            try
            {
                if (dgvRFIDTags.Columns["RfidTagNumberId"] != null)
                    dgvRFIDTags.Columns["RfidTagNumberId"].HeaderText = "Tag ID";

                if (dgvRFIDTags.Columns["RfidTagUID"] != null)
                    dgvRFIDTags.Columns["RfidTagUID"].HeaderText = "UID";

                if (dgvRFIDTags.Columns["RfidTagNumber"] != null)
                    dgvRFIDTags.Columns["RfidTagNumber"].HeaderText = "Tag Number";

                if (dgvRFIDTags.Columns["RfidStatus"] != null)
                    dgvRFIDTags.Columns["RfidStatus"].HeaderText = "Status";

                

               

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while customizing columns: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRFIDTags.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an RFID tag to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected RFID tag ID
                int selectedTagId = (int)dgvRFIDTags.SelectedRows[0].Cells["RfidTagNumberId"].Value;

                // Ask for confirmation
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this RFID tag?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Proceed with deletion
                    RFIDTagBLL rfidBLL = new RFIDTagBLL();
                    rfidBLL.DeleteRFIDTag(selectedTagId);

                    MessageBox.Show("RFID tag deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the RFID tags list
                    LoadRFIDTags();
                }
                else
                {
                    // User chose not to delete
                    MessageBox.Show("Deletion canceled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the RFID tag: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate if any row is selected
                if (dgvRFIDTags.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an RFID tag to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm the update action
                DialogResult confirmResult = MessageBox.Show(
                    "Are you sure you want to update this RFID tag?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    // If the user chooses "No", exit the update process
                    return;
                }

                // Get the selected RFID tag ID
                int selectedTagId = (int)dgvRFIDTags.SelectedRows[0].Cells["RfidTagNumberId"].Value;

                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtRfidTagUID.Text))
                {
                    MessageBox.Show("RFID Tag UID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRfidTagUID.Focus();
                    return;
                }

                if (!int.TryParse(txtRfidTagNumber.Text, out int tagNumber))
                {
                    MessageBox.Show("Please enter a valid RFID Tag Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRfidTagNumber.Focus();
                    return;
                }

                if (cboRfidStatus.SelectedItem == null || !Enum.TryParse(cboRfidStatus.SelectedItem.ToString(), out RFIDTagStatus status))
                {
                    MessageBox.Show("Please select a valid RFID Tag Status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboRfidStatus.Focus();
                    return;
                }

                // Create an RFIDTag object for updating
                RFIDTag rfidTag = new RFIDTag
                {
                    RfidTagNumberId = selectedTagId,
                    RfidTagUID = txtRfidTagUID.Text.Trim(),
                    RfidTagNumber = tagNumber,
                    RfidStatus = status,
                };

                // Update the RFID tag
                RFIDTagBLL rfidBLL = new RFIDTagBLL();
                rfidBLL.UpdateRFIDTag(rfidTag);

                // Success message and refresh
                MessageBox.Show("RFID tag updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRFIDTags();
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show("An error occurred while updating the RFID tag: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ucfrmRFIDTagManagement_Load(object sender, EventArgs e)
        {
            LoadRFIDTags();
            CustomizeColumns();
        }

        private void dgvRFIDTags_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvRFIDTags.SelectedRows.Count > 0)
                {
                    // Get the currently selected row
                    DataGridViewRow selectedRow = dgvRFIDTags.SelectedRows[0];

                    // Populate the input fields
                    txtRfidTagUID.Text = selectedRow.Cells["RfidTagUID"].Value?.ToString();
                    txtRfidTagNumber.Text = selectedRow.Cells["RfidTagNumber"].Value?.ToString();
                    cboRfidStatus.SelectedItem = Enum.TryParse<RFIDTagStatus>(selectedRow.Cells["RfidStatus"].Value?.ToString(), out var status) ? status : (object)null;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating fields: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           try
    {
        // Get the search keyword from the textbox
        string keyword = txtSearch.Text.Trim();

        // Call the BLL to search for RFID tags
        RFIDTagBLL rfidBLL = new RFIDTagBLL();
        var searchResults = rfidBLL.SearchRFIDTags(keyword);

        // Bind the search results to the DataGridView
        dgvRFIDTags.DataSource = searchResults;

        // Check if results are empty and display a message if no results found
        if (searchResults == null || searchResults.Count == 0)
        {
            MessageBox.Show("No RFID tags found with the provided search term.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Optionally customize columns again (if required)
        CustomizeColumns();
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occurred while searching RFID tags: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Check if the search box is empty
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadRFIDTags(); // Load all RFID tags if search text is cleared
            }
            else
            {
                // Optionally, you could start filtering the DataGridView as the user types, for real-time searching
                // For now, we do nothing here, as searching is handled by the Search button click event
            }
        }
    }
}
