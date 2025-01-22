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
using System.Windows.Forms.DataVisualization.Charting;

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
                dgvRevenue.DataSource = visitorBLL.LoadRevenueVisitorData();

                // Calculate total payment
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
            DateTime today = DateTime.Now.Date; // Get the current date (without time).
            try
            {
                var revenueData = visitorBLL.GetDailyRevenue(today);
                dgvRevenue.DataSource = revenueData; // Update the DataGridView DataSource

                if (revenueData.Rows.Count > 0)
                {
                    decimal totalPayment = revenueData.AsEnumerable()
                        .Sum(row => row.Field<decimal>("PaymentAmount"));
                    lblTotalRevenue.Text = $"Total Revenue for Today ({today.ToShortDateString()}): {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
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

            UpdateChartWithData(); // Update chart after setting new data source
        }





        private void btnWeekly_Click(object sender, EventArgs e)
        {
            try
            {
                // Calculate the start and end dates of the current week
                DateTime today = DateTime.Now;
                DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday); // Adjust for Monday as the start of the week
                DateTime endOfWeek = startOfWeek.AddDays(6); // Sunday is the last day of the week

                // Fetch weekly revenue data
                var revenueData = visitorBLL.GetRevenueByDateRange(startOfWeek, endOfWeek);
                dgvRevenue.DataSource = revenueData; // Bind the filtered data to the DataGridView

                // Calculate and display the total revenue for the week
                if (revenueData.Rows.Count > 0)
                {
                    decimal totalPayment = revenueData.AsEnumerable()
                        .Sum(row => row.Field<decimal>("PaymentAmount"));
                    lblTotalRevenue.Text = $"Total Revenue for this Week = {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
                }
                else
                {
                    lblTotalRevenue.Text = "No revenue data available for this week.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            UpdateChartWithData(); // Update chart after loading data
        }



        private void btnMontthly_Click(object sender, EventArgs e)
        {
            try
            {
                // Calculate the first and last dates of the current month
                DateTime today = DateTime.Now;
                DateTime startOfMonth = new DateTime(today.Year, today.Month, 1); // First day of the month
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Last day of the month

                // Fetch monthly revenue data
                var revenueData = visitorBLL.GetRevenueByDateRange(startOfMonth, endOfMonth);
                dgvRevenue.DataSource = revenueData; // Bind the filtered data to the DataGridView

                // Calculate and display the total revenue for the month
                if (revenueData.Rows.Count > 0)
                {
                    decimal totalPayment = revenueData.AsEnumerable()
                        .Sum(row => row.Field<decimal>("PaymentAmount"));
                    lblTotalRevenue.Text = $"Total Revenue for this Month = {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
                }
                else
                {
                    lblTotalRevenue.Text = "No revenue data available for this month.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            UpdateChartWithData(); // Update chart after loading data
        }



        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve all revenue data
                var revenueData = visitorBLL.LoadRevenueVisitorData();
                dgvRevenue.DataSource = revenueData; // Bind the retrieved data to the DataGridView

                // Calculate and display the total revenue
                if (revenueData.Rows.Count > 0)
                {
                    decimal totalPayment = revenueData.AsEnumerable()
                        .Sum(row => row.Field<decimal>("PaymentAmount"));
                    lblTotalRevenue.Text = $"Total Revenue: {totalPayment.ToString("C", new System.Globalization.CultureInfo("en-PH"))}";
                }
                else
                {
                    lblTotalRevenue.Text = "No total revenue data available.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            UpdateChartWithData(); // Update chart after loading data
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

            // Initialize Excel application
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not installed on this system.");
                return;
            }

            // Create a new workbook
            Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);

            // Sheet 1: Revenue Data Table
            Excel.Worksheet dataSheet = (Excel.Worksheet)workbook.Sheets[1];
            dataSheet.Name = "Revenue Data";

            // Add column headers to Sheet 1
            for (int i = 1; i <= dataGridView.Columns.Count; i++)
            {
                dataSheet.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
            }

            // Add row data to Sheet 1
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    dataSheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value;
                }
            }

            // Add total revenue row to Sheet 1
            int totalRow = dataGridView.Rows.Count + 2;
            dataSheet.Cells[totalRow, 1] = "Total Revenue:";
            dataSheet.Cells[totalRow, 2] = lblTotalRevenue.Text.Replace("Total Revenue: ", "");
            Excel.Range totalRange = dataSheet.Range[dataSheet.Cells[totalRow, 1], dataSheet.Cells[totalRow, 2]];
            totalRange.Font.Bold = true;
            totalRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);

            // Sheet 2: Revenue Chart
            Excel.Worksheet chartSheet = (Excel.Worksheet)workbook.Sheets.Add();
            chartSheet.Name = "Revenue Chart";

            // Add chart data to Sheet 2
            chartSheet.Cells[1, 1] = "Date";
            chartSheet.Cells[1, 2] = "Revenue";

            // Extract data from the chart series
            Series series = chartRevenue.Series[0];
            int row = 2;
            foreach (var point in series.Points)
            {
                if (point.XValue != 0 && point.YValues[0] != 0)
                {
                    DateTime date = DateTime.FromOADate(point.XValue);
                    chartSheet.Cells[row, 1] = date.ToString("MMM dd, yyyy");
                    chartSheet.Cells[row, 2] = point.YValues[0];
                    row++;
                }
            }

            // Calculate the center position for the chart
            double centerX = chartSheet.Columns[1].Width * 5; // Adjust based on your column width
            double centerY = (row - 1) * 15; // Adjust based on your row count

            // Create and embed the chart in Sheet 2
            Excel.ChartObjects charts = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject chartObject = charts.Add(centerX, centerY, 500, 300); // Adjust size
            Excel.Chart chart = chartObject.Chart;

            // Set the chart's data range
            Excel.Range chartRange = chartSheet.Range[
                chartSheet.Cells[2, 1],
                chartSheet.Cells[row - 1, 2]
            ];
            chart.SetSourceData(chartRange);

            // Customize the chart
            chart.ChartType = Excel.XlChartType.xlColumnClustered;
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Revenue Over Time";

            // Save the workbook
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RevenueReport.xlsx");
            workbook.SaveAs(filePath);

            // Show Excel
            excelApp.Visible = true;

            MessageBox.Show($"Export successful! File saved to: {filePath}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Cleanup
            System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(chartSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }




        private void dgvRevenue_CellContentClick(object sender, EventArgs e)
        {
            UpdateChartWithData();
        }

        private void UpdateChartWithData()
        {
            chartRevenue.Series.Clear();
            chartRevenue.ChartAreas.Clear();

            Series series = new Series("Revenue")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.DateTime
            };

            ChartArea area = new ChartArea();
            chartRevenue.ChartAreas.Add(area);
            chartRevenue.Series.Add(series);

            int dataCount = dgvRevenue.Rows.Count;
            double pointWidth = dataCount > 30 ? 0.5 : (dataCount > 10 ? 0.7 : 0.9);
            series["PointWidth"] = pointWidth.ToString();

            foreach (DataGridViewRow row in dgvRevenue.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (DateTime.TryParse(row.Cells["DateRegistered"].Value?.ToString(), out DateTime date) && Decimal.TryParse(row.Cells["PaymentAmount"].Value?.ToString(), out decimal payment))
                    {
                        series.Points.AddXY(date, payment);
                    }
                }
            }

            // Adjust axis and interval settings based on data density
            ConfigureChartArea(series.Points.Count);  // Pass the count of data points to dynamically adjust settings
            chartRevenue.Titles.Clear();
            chartRevenue.Titles.Add("Revenue Over Time");
        }

        private void ConfigureChartArea(int dataCount)
        {
            chartRevenue.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd, yyyy";
            chartRevenue.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartRevenue.ChartAreas[0].AxisX.IntervalType = dataCount > 30 ? DateTimeIntervalType.Days : dataCount > 10 ? DateTimeIntervalType.Weeks : DateTimeIntervalType.Months;
        }






    }
}
