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
    public partial class frmGroupMember : Form
    {
        private GroupMemberBLL groupMemberBLL;
        private Dictionary<int, Color> _colorMap;
        private int groupIdColumnIndex = -1; // To store column index of 'GroupId'
        public frmGroupMember()
        {
            InitializeComponent();
            groupMemberBLL = new GroupMemberBLL();
            LoadGroupMembers();
            
            dgvGroupMembers.DataSource = groupMemberBLL.GetAllGroupMembers();
           
            InitializeColumnIndex(); // Initialize column index after data is bound

           

            dgvGroupMembers.Refresh();
        }
        private void InitializeColumnIndex()
        {
            groupIdColumnIndex = dgvGroupMembers.Columns["GroupId"].Index;
        }
        private void LoadGroupMembers()
        {
            try
            {
                dgvGroupMembers.DataSource = groupMemberBLL.GetAllGroupMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading group members: " + ex.Message);
            }
        }
        private void dgvGroupMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
