using System;
using BLL;
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
    public partial class ucfrmRevenueReportOptions : UserControl
    {
        private VisitorBLL visitorBLL = new VisitorBLL();   
        public ucfrmRevenueReportOptions()
        {
            InitializeComponent();
            LoadVisitorRevenue();

        }
        private void LoadVisitorRevenue()
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                dgvRevenue.DataSource = visitorBLL.GetRevenueVisitorData();
                decimal totalPayment = visitorBLL.GetTotalRevenue();
                
                lblTotalRevenue.Text = $"Total Revenue: {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
            

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDaily_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now.Date; // This will get the current date without the time part.
            try
            {
                var revenueData = visitorBLL.GetDailyRevenue(today);
                if (revenueData.Rows.Count > 0)
                {
                    lblTotalRevenue.Text = $"Total Revenue for Today = ({today.ToShortDateString()}): {revenueData.Rows[0]["TotalRevenue"]}";
                }
                else
                {
                    lblTotalRevenue.Text = "No revenue data available for today.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnWeekly_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalPayment = visitorBLL.GetTotalWeeklyRevenue();
                lblTotalRevenue.Text = $"Total Revenue for this Week = {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnMontthly_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalPayment = visitorBLL.GetTotalMonthlyRevenue();
                // Format the output using Philippine Peso currency format
                lblTotalRevenue.Text = $"Total Revenue for this Month = {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalPayment = visitorBLL.GetTotalRevenue();
                // Format the output using Philippine Peso currency format
                lblTotalRevenue.Text = $"Total Revenue: {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCalculateRange_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;
            try
            {
                decimal totalPayment = visitorBLL.GetTotalRevenueByDateRange(startDate, endDate);
                lblTotalRevenue.Text = $"Total Payment from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
