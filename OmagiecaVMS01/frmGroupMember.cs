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
        public frmGroupMember()
        {
            InitializeComponent();
            groupMemberBLL = new GroupMemberBLL();
            LoadGroupMembers();
        }

        private void LoadGroupMembers()
        {
            dgvGroupMembers.DataSource = groupMemberBLL.GetAllGroupMembers();
        }
        private void dgvGroupMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
