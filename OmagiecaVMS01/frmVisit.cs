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
        private VisitBLL visitBLL = new VisitBLL();
        public frmVisit()
        {
            InitializeComponent();
            DisplayVisitorData();
        }
        private void DisplayVisitorData()
        {
            VisitBLL visitBLL = new VisitBLL(); 
            dgvVisitorStatus.DataSource = visitBLL.GetVisitorInformation();
        }

        private void btnRegisterVisitor_Click(object sender, EventArgs e)
        {
           
        }
    }
}
