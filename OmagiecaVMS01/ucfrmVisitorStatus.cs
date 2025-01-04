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
    public partial class ucfrmVisitorStatus : UserControl
    {
        public ucfrmVisitorStatus()
        {
            InitializeComponent();
            
        }

/*        private void LoadVisitors()
        {
            try
            {
                VisitorStatusBLL visitorstatusBLL = new VisitorStatusBLL();
                dgvVisitors.DataSource = visitorstatusBLL.GetVisitors();

                // Reorder columns
                
                dgvVisitors.Columns["FirstName"].DisplayIndex = 0;
                dgvVisitors.Columns["LastName"].DisplayIndex = 1;               
                dgvVisitors.Columns["VisitorTypeName"].DisplayIndex = 2;                                                             
                dgvVisitors.Columns["RfidTagNumber"].DisplayIndex = 3;
                dgvVisitors.Columns["DateRegistered"].DisplayIndex = 4;

                // Customize column headers (if needed)
                
                dgvVisitors.Columns["FirstName"].HeaderText = "First Name";
                dgvVisitors.Columns["LastName"].HeaderText = "Last Name";
                
                dgvVisitors.Columns["VisitorTypeName"].HeaderText = "Visitor Type";
                
                
                
                dgvVisitors.Columns["RfidTagNumber"].HeaderText = "RFID Tag Number";
                dgvVisitors.Columns["DateRegistered"].HeaderText = "Date Registered";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading visitors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/
       

        private void dgvActiveVisits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
