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
    public partial class ucfrmVisitorReportOptions : UserControl
    {
        private VisitorBLL visitorBLL = new VisitorBLL();
        public ucfrmVisitorReportOptions()
        {
            InitializeComponent();
            LoadVisitorsReport();
            LoadTotalVisitors();
            

            
        }
        private void LoadTotalVisitors()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                dgvVisitorsReport.DataSource = visitorBLL.GetVisitorsReport();
                labelTotalRecords.Text = $"Total Visitors: {dgvVisitorsReport.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadVisitorsReport()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                dgvVisitorsReport.DataSource = visitorBLL.GetVisitorsReport();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDaily_Click(object sender, EventArgs e)
        {
            try
            {
                VisitorBLL visitorManager = new VisitorBLL();
                DataTable filteredData = visitorManager.GetVisitorsForToday();  // This now fetches today's visitors
                dgvVisitorsReport.DataSource = filteredData;
                labelTotalRecords.Text = $"Total Visitor for Today: {filteredData.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWeekly_Click(object sender, EventArgs e)
        {
            try
            {
                VisitorBLL visitorManager = new VisitorBLL();
                DataTable filteredData = visitorManager.GetVisitorsWeeklyReport();
                dgvVisitorsReport.DataSource = filteredData;
                labelTotalRecords.Text = $"Total Visitors for this week: {filteredData.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            try
            {
                VisitorBLL visitorManager = new VisitorBLL();
                DataTable filteredData = visitorManager.GetVisitorsForCurrentMonth();
                dgvVisitorsReport.DataSource = filteredData;
                labelTotalRecords.Text = $"Total Visitors for this Month: {filteredData.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = dateTimePickerStartDate.Value.Date;
                DateTime endDate = dateTimePickerEndDate.Value.Date;

                if (endDate < startDate)
                {
                    MessageBox.Show("End date must be after start date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                VisitorBLL visitorManager = new VisitorBLL();
                DataTable filteredData = visitorManager.GetVisitorsForDateRange(startDate, endDate);
                dgvVisitorsReport.DataSource = filteredData;
                labelTotalRecords.Text = $"Total Visitors from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: {filteredData.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
