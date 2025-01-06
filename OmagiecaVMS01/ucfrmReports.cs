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
           
        }
       


        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
           
        }
       

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlDynamicContent.Controls.Clear(); // Clear previous user controls

            if (cmbReportType.SelectedItem.ToString() == "Visitor Report")
            {
                ucfrmVisitorReportOptions visitorOptions = new ucfrmVisitorReportOptions();
                visitorOptions.Dock = DockStyle.Fill;
                pnlDynamicContent.Controls.Add(visitorOptions);

               
            }
            if (cmbReportType.SelectedItem.ToString() == "Revenue Report")
            {
                ucfrmRevenueReportOptions revenueOptions = new ucfrmRevenueReportOptions();
                revenueOptions.Dock = DockStyle.Fill;
                pnlDynamicContent.Controls.Add(revenueOptions);


            }
        }
    }
}
