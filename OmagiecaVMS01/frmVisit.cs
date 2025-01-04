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
    public partial class frmVisit : Form
    {
        private VisitorBLL visitorBLL = new VisitorBLL();   
        public frmVisit()
        {
            InitializeComponent();
        }
        /*
        private void LoadVisitorStatuses()
        {
            DataTable visitorStatuses = visitorBLL.GetVisitorStatuses();
            dgvVisitorStatuses.DataSource = visitorStatuses;
            // Customize columns, set headers, etc.
        }
        */

        private void btnRegisterVisitor_Click(object sender, EventArgs e)
        {
           
        }
    }
}
