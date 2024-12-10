using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    
    public class VisitorStatusDAL
    {
        
            private string connectionString = Properties.Settings.Default.ConnectionString;

            public DataTable GetVisitors()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"SELECT  vt.VisitorTypeName, v.FirstName, v.LastName, 
                                        v.DateRegistered,
                                      r.RfidTagNumber
                             FROM Visitors v
                             INNER JOIN VisitorType vt ON v.VisitorTypeId = vt.VisitorTypeId
                             
                             LEFT JOIN RFIDTag r ON v.VisitorId = r.VisitorId";

                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataTable visitorTable = new DataTable();
                        adapter.Fill(visitorTable);

                        return visitorTable;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving visitors: " + ex.Message, ex);
                }
            }



        }
    }
