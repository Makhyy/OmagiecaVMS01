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
using Excel = Microsoft.Office.Interop.Excel;

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
                    lblTotalRevenue.Text = $"Total Revenue for Today ({today.ToShortDateString()}): {revenueData.Rows[0]["TotalRevenue"]}";
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

        private void button3_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvRevenue);
        }

       private void ExportToExcel(DataGridView dataGridView)
{
    if (dataGridView.Rows.Count == 0)
    {
        MessageBox.Show("No data to export.");
        return;
    }

    Excel.Application excelApp = new Excel.Application();
    if (excelApp == null)
    {
        MessageBox.Show("Excel is not installed on this system.");
        return;
    }

    Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

    // Adding column headers
    for (int i = 1; i <= dataGridView.Columns.Count; i++)
    {
        worksheet.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
    }

    // Adding row data
    for (int i = 0; i < dataGridView.Rows.Count; i++)
    {
        for (int j = 0; j < dataGridView.Columns.Count; j++)
        {
            worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value;
        }
    }

    // Adding total revenue
    // Assuming total revenue is displayed at the bottom of your DataGridView or separately
    int totalRow = dataGridView.Rows.Count + 2; // Adjust this index based on your actual row count + 1 for the empty space
    worksheet.Cells[totalRow, 1] = "Total Revenue:";
    worksheet.Cells[totalRow, 2] = lblTotalRevenue.Text.Replace("Total Revenue: ", ""); // Strip label text if necessary

    // Optional: Formatting the total row
    Excel.Range totalRange = worksheet.Range[worksheet.Cells[totalRow, 1], worksheet.Cells[totalRow, 2]];
    totalRange.Font.Bold = true;
    totalRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);

    // Setting Excel properties
    worksheet.Columns.AutoFit();
    excelApp.Visible = true;

    // Cleanup
    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worksheet);
    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workbook);
    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
}

        private void dgvRevenue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
