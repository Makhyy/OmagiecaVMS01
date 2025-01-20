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
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;


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
                DataTable filteredData = visitorManager.GetVisitorsForToday();  // This fetches today's visitors
                dgvVisitorsReport.DataSource = filteredData;
                labelTotalRecords.Text = $"Total Visitor for Today: {filteredData.Rows.Count}";

                // Update the chart with the latest data
                UpdateChartWithData();
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

                // Update the chart with the latest data
                UpdateChartWithData();
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

                // Update the chart with the latest data
                UpdateChartWithData();
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

        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                VisitorBLL visitorBLL = new VisitorBLL();
                dgvVisitorsReport.DataSource = visitorBLL.GetVisitorsReport();
                labelTotalRecords.Text = $"Total Visitors: {dgvVisitorsReport.Rows.Count}";

                // Update the chart with the latest data
                UpdateChartWithData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvVisitorsReport);
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

            // Adding total visitors
            int totalRow = dataGridView.Rows.Count + 2; // Adjust this index based on your actual row count + 1 for the empty space
            worksheet.Cells[dataGridView.Rows.Count + 2, 1] = "Total Visitors";
            worksheet.Cells[dataGridView.Rows.Count + 2, 2] = labelTotalRecords.Text.Replace("Total Visitors: ", "");

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

     
        private void chart1_Click(object sender, EventArgs e)
        {
            // Assuming you have a chart control named 'chart1' and a DataGridView named 'dataGridView1'
            UpdateChartWithData();
        }

        private void UpdateChartWithData()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            chart1.ChartAreas.Add(area);

            Series series = new Series("VisitorCount")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Green
            };
            chart1.Series.Add(series);

            Dictionary<string, int> visitorTypeCounts = new Dictionary<string, int>();

            foreach (DataGridViewRow row in dgvVisitorsReport.Rows)
            {
                if (!row.IsNewRow)
                {
                    string visitorType = row.Cells["VisitorType"].Value?.ToString() ?? "Unknown";
                    if (visitorTypeCounts.ContainsKey(visitorType))
                    {
                        visitorTypeCounts[visitorType]++;
                    }
                    else
                    {
                        visitorTypeCounts[visitorType] = 1;
                    }
                }
            }

            foreach (var entry in visitorTypeCounts)
            {
                series.Points.AddXY(entry.Key, entry.Value);
            }

            chart1.Titles.Clear();
            chart1.Titles.Add("Visitor Count by Type");
        }


    }
}
