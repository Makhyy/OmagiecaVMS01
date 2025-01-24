using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmagiecaVMS01
{
    public partial class frmVisitorsData : Form
    {
        private VisitorBLL _visitorBLL = new VisitorBLL();
        private DataTable visitorsTable;
        public frmVisitorsData()
        {
            InitializeComponent();
            LoadVisitorsData();
        }


        private void LoadVisitorsData()
        {
            dgvVisitorsData.DataSource = _visitorBLL.GetVisitors();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {


            // Apply filter
            string filter = txtSearch.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(filter))
            {
                (dgvVisitorsData.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            }
            else
            {
                (dgvVisitorsData.DataSource as DataTable).DefaultView.RowFilter =
                    $"FirstName LIKE '%{filter}%' OR LastName LIKE '%{filter}%' OR CityMunicipality LIKE '%{filter}%'";
            }

        }
        private void PopulateVisitorTypeComboBox()
        {
            DataTable visitorTypes = _visitorBLL.GetDistinctVisitorTypes();
            cmbVisitorType.Items.Clear();
            cmbVisitorType.Items.Add("All");
            foreach (DataRow row in visitorTypes.Rows)
            {
                cmbVisitorType.Items.Add(row["VisitorType"].ToString());
            }
            cmbVisitorType.SelectedIndex = 0;
        }
        private void FilterDataGridViewByVisitorType()
        {
            string filterType = cmbVisitorType.SelectedItem.ToString();
            if (filterType == "All")
            {
                (dgvVisitorsData.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            }
            else
            {
                (dgvVisitorsData.DataSource as DataTable).DefaultView.RowFilter = $"VisitorType = '{filterType}'";
            }
        }

        private void cmbVisitorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataGridViewByVisitorType();
        }

        private void frmVisitorsData_Load(object sender, EventArgs e)
        {

            PopulateVisitorTypeComboBox();
            PopulateLocationsComboBox();
            PopulateLocationTypeComboBox();
        }
        private void PopulateLocationsComboBox()
        {
            DataTable locations = _visitorBLL.GetCombinedLocations();
            cmbLocationType.Items.Clear();
            cmbLocationType.Items.Add("All");  // Option to remove filter

            foreach (DataRow row in locations.Rows)
            {
                string type = row["Type"].ToString();
                string location = row["Location"].ToString();
                string displayText = $"{type}: {location}";
                cmbLocationType.Items.Add(displayText);
            }
            cmbLocationType.SelectedIndex = 0;  // Set 'All' as the default selection
        }

        private void cmbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataGridViewByLocationType();
        }
        private void PopulateLocationTypeComboBox()
        {
            cmbLocationType.Items.Clear();
            cmbLocationType.Items.Add("All");
            cmbLocationType.Items.Add("Local (City/Municipality)");
            cmbLocationType.Items.Add("Foreign (Country)");
            cmbLocationType.SelectedIndex = 0;  // Set 'All' as the default selection
        }
        private void FilterDataGridViewByLocationType()
        {
            string filterType = cmbLocationType.SelectedItem.ToString();

            if (filterType == "All")
            {
                (dgvVisitorsData.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            }
            else if (filterType == "Local (City/Municipality)")
            {
                (dgvVisitorsData.DataSource as DataTable).DefaultView.RowFilter = "CityMunicipality IS NOT NULL AND ForeignCountry IS NULL";
            }
            else if (filterType == "Foreign (Country)")
            {
                (dgvVisitorsData.DataSource as DataTable).DefaultView.RowFilter = "ForeignCountry IS NOT NULL AND CityMunicipality IS NULL";
            }
        }
    }
}
