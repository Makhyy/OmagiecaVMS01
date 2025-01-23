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
        private VisitBLL visitBLL = new VisitBLL();
        public ucfrmVisitorStatus()
        {
            InitializeComponent();
            LoadVisitors();
          //  LoadVisitData();
            dgvVisitors.Columns["VisitorId"].Visible = false;
            dgvVisitors.Columns["Age"].Visible = false;
            dgvVisitors.Columns["IsPWD"].Visible = false;
            dgvVisitors.Columns["Gender"].Visible = false;
            dgvVisitors.Columns["CityMunicipality"].Visible = false;
            dgvVisitors.Columns["ForeignCountry"].Visible = false;
            dgvVisitors.Columns["PaymentAmount"].Visible = false;
            dgvVisitors.Columns["UserAccountId"].Visible = false;
            dgvVisitors.Columns["DateRegistered"].Visible = false;



          
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
        /*
        private void LoadVisitData()
        {
            try
            {
                // Call the BLL method to get the visit data
                DataTable visitData = visitBLL.GetVisitData();

                // Check if the data is null or empty
                if (visitData == null || visitData.Rows.Count == 0)
                {
                    dgvVisitors.DataSource = null; // Clear the DataGridView if there's no data
                    MessageBox.Show("No data available to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit the method
                }

                // Bind the data to the DataGridView
                dgvVisitors.DataSource = visitData;

                // Format the EntryTime and ExitTime columns if they exist
                if (dgvVisitors.Columns["EntryTime"] != null)
                {
                    dgvVisitors.Columns["EntryTime"].DefaultCellStyle.Format = "hh:mm:ss";
                }

                if (dgvVisitors.Columns["ExitTime"] != null)
                {
                    dgvVisitors.Columns["ExitTime"].DefaultCellStyle.Format = "hh:mm:ss";
                }
            }
            catch (Exception ex)
            {
                // Handle and display any errors
                MessageBox.Show($"An error occurred while loading visit data: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */


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
