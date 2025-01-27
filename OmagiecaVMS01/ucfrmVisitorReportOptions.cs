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
                // Step 1: Get the selected dates
                DateTime startDate = dateTimePickerStartDate.Value.Date;
                DateTime endDate = dateTimePickerEndDate.Value.Date;

                // Step 2: Validate the date range
                if (endDate < startDate)
                {
                    MessageBox.Show("End date must be on or after the start date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Step 3: Fetch filtered data for the date range
                VisitorBLL visitorManager = new VisitorBLL();
                DataTable filteredData = visitorManager.GetVisitorsForDateRange(startDate, endDate);

                if (filteredData.Rows.Count > 0)
                {
                    // Step 4: Bind the data to the DataGridView
                    dgvVisitorsReport.DataSource = filteredData;

                    // Step 5: Update the total visitors count label
                    labelTotalRecords.Text = $"Total Visitors from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: {filteredData.Rows.Count}";
                }
                else
                {
                    // Step 6: Handle the case where no data is found
                    dgvVisitorsReport.DataSource = null;
                    labelTotalRecords.Text = $"No visitors found from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.";
                    MessageBox.Show("No records found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Step 7: Handle unexpected errors
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ExportToExcelWithTwoSheets(dgvVisitorsReport);
            

        }
        private void ExportToExcelWithTwoSheets(DataGridView dataGridView)
        {
            try
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

                // Sheet 1: Table View
                Excel.Worksheet tableSheet = (Excel.Worksheet)workbook.Sheets[1];
                tableSheet.Name = "Visitor Data";

                // Add column headers to Sheet 1 with background color
                for (int i = 1; i <= dataGridView.Columns.Count; i++)
                {
                    Excel.Range headerCell = tableSheet.Cells[1, i];
                    headerCell.Value = dataGridView.Columns[i - 1].HeaderText;
                    headerCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGreen);
                    headerCell.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    headerCell.Font.Bold = true;
                }

                // Add row data to Sheet 1
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        tableSheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value;
                    }
                }

                // Add "Total Visitors" row at the end
                int totalRow = dataGridView.Rows.Count + 2;
                tableSheet.Cells[totalRow, 1] = "Total Visitors";
                tableSheet.Cells[totalRow, 2] = labelTotalRecords.Text.Replace("Total Visitors: ", "");

                // Apply formatting to the "Total Visitors" row
                Excel.Range totalRowRange = tableSheet.Range[
                    tableSheet.Cells[totalRow, 1],
                    tableSheet.Cells[totalRow, dataGridView.Columns.Count]
                ];
                totalRowRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGoldenrodYellow);
                totalRowRange.Font.Bold = true;

                // Format the entire table with borders and alternating row colors
                Excel.Range tableRange = tableSheet.Range[
                    tableSheet.Cells[1, 1],
                    tableSheet.Cells[totalRow, dataGridView.Columns.Count]
                ];
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // Apply alternating row colors
                for (int i = 2; i <= dataGridView.Rows.Count + 1; i++)
                {
                    Excel.Range rowRange = tableSheet.Range[
                        tableSheet.Cells[i, 1],
                        tableSheet.Cells[i, dataGridView.Columns.Count]
                    ];
                    if (i % 2 == 0)
                    {
                        rowRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    }
                }

                // Autofit the columns to ensure content is visible
                tableSheet.Columns.AutoFit();

                // Sheet 2: Chart View
                Excel.Worksheet chartSheet = (Excel.Worksheet)workbook.Sheets.Add();
                chartSheet.Name = "Visitor Chart";

                // Add data for the chart to Sheet 2
                chartSheet.Cells[1, 1] = "Visitor Type";
                chartSheet.Cells[1, 2] = "Visitor Count";

                int row = 2;
                foreach (var point in chart1.Series[0].Points)
                {
                    chartSheet.Cells[row, 1] = point.AxisLabel;
                    chartSheet.Cells[row, 2] = point.YValues[0];
                    row++;
                }

                // Create and embed the chart in Sheet 2
                Excel.ChartObjects charts = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
                Excel.ChartObject chartObject = charts.Add(60, 10, 500, 300);
                Excel.Chart chart = chartObject.Chart;

                Excel.Range chartRange = chartSheet.Range[
                    chartSheet.Cells[2, 1],
                    chartSheet.Cells[row - 1, 2]
                ];
                chart.SetSourceData(chartRange);

                chart.ChartType = Excel.XlChartType.xlColumnClustered;
                chart.HasTitle = true;
                chart.ChartTitle.Text = "Visitor Count by Type";

               

                // Show Excel
                excelApp.Visible = true;

                

                // Cleanup
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tableSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(chartSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
