using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class ucfrmADashboard : UserControl
    {
        private VisitorBLL visitorBLL;
        public ucfrmADashboard()
        {
            InitializeComponent();

            UpdateVisitorCount();
            ucfrmRFIDMonitor rfidMonitor = new ucfrmRFIDMonitor();
            rfidMonitor.Dock = DockStyle.Fill;
            pnlRFIDMonitor.Controls.Add(rfidMonitor);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("MMMM dd yyyy");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("hh:mm:ss tt");

            
        }
        private void UpdateVisitorCount()
        {
            try
            {
                VisitorBLL visitorManager = new VisitorBLL();
                DataTable filteredData = visitorManager.GetVisitorsForToday();  // This now fetches today's visitors
                dgvVisitorsReport.DataSource = filteredData;
                labelTotalRecords.Text = $"{filteredData.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucfrmADashboard_Load(object sender, EventArgs e)
        {
            dgvVisitorsReport.Visible = false;
        }


        
    }
}
