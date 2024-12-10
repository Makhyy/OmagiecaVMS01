using BLL;
using MODELS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class ucfrmUsers : UserControl
    {
        private readonly UserAccountBLL userAccountBLL;
        public ucfrmUsers()
        {
            InitializeComponent();
            userAccountBLL = new UserAccountBLL();
        }
        private void LoadUserAccounts()
        {
            try
            {
                dgvUserAccounts.DataSource = userAccountBLL.GetAllUserAccounts();

                // Customize DataGridView columns
                dgvUserAccounts.Columns["Password"].Visible = false; // Hide sensitive data
                dgvUserAccounts.Columns["SecurityQuestion"].Visible = false;
                dgvUserAccounts.Columns["SecurityAnswer"].Visible = false;

                dgvUserAccounts.Columns["UserAccountId"].HeaderText = "User ID";
                dgvUserAccounts.Columns["FirstName"].HeaderText = "First Name";
                dgvUserAccounts.Columns["LastName"].HeaderText = "Last Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading user accounts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Validate input fields
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                UserAccountBLL userAccountBLL = new UserAccountBLL();

                // Step 2: Check if the username already exists
                if (userAccountBLL.IsUsernameTaken(txtUsername.Text.Trim()))
                {
                    // Stop execution if the username exists
                    MessageBox.Show("The username is already in use. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                // Step 3: Proceed with adding the user
                UserAccount userAccount = new UserAccount
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Age = int.Parse(txtAge.Text),
                    Gender = cboGender.SelectedItem.ToString(),
                    Address = txtAddress.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(), // Hash passwords in production
                    UserRole = cboUserRole.SelectedItem.ToString(),
                    UserStatus = cboUserStatus.SelectedItem.ToString(),
                    SecurityQuestion = cboSecurityQuestion.SelectedItem.ToString(),
                    SecurityAnswer = txtSecurityAnswer.Text.Trim()
                };

                // Step 4: Add the user account
                userAccountBLL.AddUserAccount(userAccount);

                MessageBox.Show("User account added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserAccounts(); // Refresh the DataGridView
                ClearInputs(); // Clear form inputs
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show(" error occurred while adding the user account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate selection
                if (dgvUserAccounts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a user account to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get selected UserAccountId
                int userAccountId = (int)dgvUserAccounts.SelectedRows[0].Cells["UserAccountId"].Value;

                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("First Name and Last Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                {
                    MessageBox.Show("Please enter a valid age.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboGender.SelectedItem == null || string.IsNullOrWhiteSpace(cboGender.SelectedItem.ToString()))
                {
                    MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create UserAccount object
                UserAccount userAccount = new UserAccount
                {
                    UserAccountId = userAccountId,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Age = age,
                    Gender = cboGender.SelectedItem.ToString(),
                    Address = txtAddress.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    UserRole = cboUserRole.SelectedItem.ToString(),
                    UserStatus = cboUserStatus.SelectedItem.ToString(),
                    SecurityQuestion = cboSecurityQuestion.SelectedText.ToString(),
                    SecurityAnswer = txtSecurityAnswer.Text.ToString(),
                };

                // Confirmation dialog
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to update this user account?\n\n" +
                    $"Name: {userAccount.FirstName} {userAccount.LastName}\n" +
                    $"Username: {userAccount.Username}\n" +
                    $"Role: {userAccount.UserRole}\n" +
                    $"Status: {userAccount.UserStatus}",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return; // Cancel operation if the user clicks "No"
                }

                // Update user account
                UserAccountBLL userAccountBLL = new UserAccountBLL();
                userAccountBLL.UpdateUserAccount(userAccount);

                MessageBox.Show("User account updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserAccounts(); // Refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the user account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate selection
                if (dgvUserAccounts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a user account to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected user account?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;

                // Get selected UserAccountId
                int userAccountId = (int)dgvUserAccounts.SelectedRows[0].Cells["UserAccountId"].Value;

                // Delete user account
                UserAccountBLL userAccountBLL = new UserAccountBLL();
                userAccountBLL.DeleteUserAccount(userAccountId);

                MessageBox.Show("User account deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the user account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("Please enter a keyword to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UserAccountBLL userAccountBLL = new UserAccountBLL();
                dgvUserAccounts.DataSource = userAccountBLL.SearchUserAccounts(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching for user accounts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cboGender.SelectedIndex = -1;
            txtAddress.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cboUserRole.SelectedIndex = -1;
            cboUserStatus.SelectedIndex = -1;
            cboSecurityQuestion.SelectedIndex = -1;
            txtSecurityAnswer.Clear();
            dgvUserAccounts.ClearSelection(); // Clear DataGridView selection
        }



        private void ucfrmUsers_Load(object sender, EventArgs e)
        {
            // Populate Filter By ComboBox
            cboFilterBy.Items.Add("All");
            cboFilterBy.Items.Add("Active");
            cboFilterBy.Items.Add("Inactive");
            cboFilterBy.Items.Add("Admin");
            cboFilterBy.Items.Add("Receptionist");

            cboFilterBy.SelectedIndex = 0; // Default to "All"
            LoadUserAccounts(); // Load all user accounts initially
            
        }

        private void dgvUserAccounts_SelectionChanged(object sender, EventArgs e)
        {
            try

            {

                // Ensure a row is selected
                if (dgvUserAccounts.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvUserAccounts.SelectedRows[0];

                    // Populate text boxes with selected user data
                    txtFirstName.Text = selectedRow.Cells["FirstName"].Value.ToString();
                    txtLastName.Text = selectedRow.Cells["LastName"].Value.ToString();
                    txtAge.Text = selectedRow.Cells["Age"].Value.ToString();
                    cboGender.SelectedItem = selectedRow.Cells["Gender"].Value.ToString();
                    txtAddress.Text = selectedRow.Cells["Address"].Value.ToString();
                    txtUsername.Text = selectedRow.Cells["Username"].Value.ToString();
                    txtPassword.Text = selectedRow.Cells["Password"].Value.ToString();
                    cboUserRole.SelectedItem = selectedRow.Cells["UserRole"].Value.ToString();
                    cboUserStatus.SelectedItem = selectedRow.Cells["UserStatus"].Value.ToString();
                    cboSecurityQuestion.SelectedItem = selectedRow.Cells["SecurityQuestion"].Value.ToString() ;
                    txtSecurityAnswer.Text = selectedRow.Cells["SecurityAnswer"].Value.ToString();

                    btnUpdate.Enabled = dgvUserAccounts.SelectedRows.Count > 0;
                    btnDelete.Enabled = dgvUserAccounts.SelectedRows.Count > 0;

                }

            }   
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while selecting a user account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string filterBy = cboFilterBy.SelectedItem.ToString();
                UserAccountBLL userAccountBLL = new UserAccountBLL();
                dgvUserAccounts.DataSource = userAccountBLL.GetFilteredUserAccounts(filterBy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while filtering user accounts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid age.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            if (cboGender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGender.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (cboUserRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a user role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboUserRole.Focus();
                return false;
            }

            if (cboUserStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a user status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboUserStatus.Focus();
                return false;
            }

            if (cboSecurityQuestion.SelectedItem == null || string.IsNullOrWhiteSpace(txtSecurityAnswer.Text))
            {
                MessageBox.Show("Security Question and Answer are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSecurityQuestion.Focus();
                return false;
            }

            return true; // All validations passed
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
    }
}
