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
    public partial class ucfrmVisitorStatus : UserControl
    {
        private PaymentBLL paymentBLL = new PaymentBLL();
        public ucfrmVisitorStatus()
        {
            InitializeComponent();
            LoadVisitors();
            dgvVisitors.Columns["VisitorId"].Visible = false;
            dgvVisitors.Columns["Age"].Visible = false;
            dgvVisitors.Columns["IsPWD"].Visible = false;
            dgvVisitors.Columns["Gender"].Visible = false;
            dgvVisitors.Columns["CityMunicipality"].Visible = false;
            dgvVisitors.Columns["ForeignCountry"].Visible = false;
            dgvVisitors.Columns["PaymentAmount"].Visible = false;
            dgvVisitors.Columns["UserAccountId"].Visible = false;
            dgvVisitors.Columns["DateRegistered"].Visible = false;

            //dgvVisitors.Columns["EntryTime"].DefaultCellStyle.NullValue = "Not Entered";
           // dgvVisitors.Columns["ExitTime"].DefaultCellStyle.NullValue = "Not Exited";
        }

        private void LoadVisitors()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                dgvVisitors.DataSource = visitorBLL.GetVisitors();


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvActiveVisits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadVisitors();
        }
    }
}
