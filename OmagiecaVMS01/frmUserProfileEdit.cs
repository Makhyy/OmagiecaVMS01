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
    public partial class frmUserProfileEdit : Form
    {

        private UserAccountBLL userAccountBLL;
        private int userId;
        public frmUserProfileEdit(int userId)
        {
            InitializeComponent();
          
            

            this.userId = userId;
            userAccountBLL = new UserAccountBLL(); // Assuming the BLL handles connection strings internally
            LoadUserData();
            
        }
      

        private void LoadUserData()
        {
            var user = userAccountBLL.GetUserAccount(userId);
            if (user != null)
            {
                txtFirstName.Text = user.FirstName ?? "";
                txtLastName.Text = user.LastName ?? "";
                txtAge.Text = user.Age?.ToString() ?? "";
                cboGender.SelectedIndex = cboGender.Items.IndexOf(user.Gender);
                txtAddress.Text = user.Address?.ToString() ?? "";
                cboUserRole.SelectedIndex = cboUserRole.Items.IndexOf(user.UserRole);





                // Populate other fields accordingly
            }
            else
            {
                MessageBox.Show("Failed to load user data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserProfileEdit_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }
    }
}
