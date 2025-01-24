using MODELS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GroupMemberDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;
        public DataTable GetAllGroupMembers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM GroupMember";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public DataTable GetGroupData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Update the column name in the query below
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT GroupId FROM Visitors", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }


    }
}
