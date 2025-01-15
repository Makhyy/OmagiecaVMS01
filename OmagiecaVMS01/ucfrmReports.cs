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
    public partial class ucfrmReports : UserControl
    {
        public ucfrmReports()
        {
            InitializeComponent();
            // Attach the Load event handler
            this.Load += new EventHandler(ucfrmReports_Load);
        }

        private void ucfrmReports_Load(object sender, EventArgs e)
        {
            // Set a default report type when the UserControl loads
            cmbReportType.SelectedIndex = cmbReportType.Items.IndexOf("Visitor Report");
            // Manually call the selection change handler to load the default report
            LoadSelectedReport();
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedReport();
        }

        private void LoadSelectedReport()
        {
            pnlDynamicContent.Controls.Clear(); // Clear previous user controls

            string selectedReportType = cmbReportType.SelectedItem.ToString();

            if (selectedReportType == "Visitor Report")
            {
                ucfrmVisitorReportOptions visitorOptions = new ucfrmVisitorReportOptions();
                visitorOptions.Dock = DockStyle.Fill;
                pnlDynamicContent.Controls.Add(visitorOptions);
            }
            else if (selectedReportType == "Revenue Report")
            {
                ucfrmRevenueReportOptions revenueOptions = new ucfrmRevenueReportOptions();
                revenueOptions.Dock = DockStyle.Fill;
                pnlDynamicContent.Controls.Add(revenueOptions);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            // Implement functionality to generate a report based on current selection
        }
    }
}
