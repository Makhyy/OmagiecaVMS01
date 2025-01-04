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
    public class VisitDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;
        public void AddVisit(Visit visit)
        {
            string query = @"
        INSERT INTO Visits (VisitorId, UserAccountId, RfidTagNumberId, PaymentId, VisitStatusId, EntryTime, ExitTime)
        VALUES (@VisitorId, @UserAccountId, @RfidTagNumberId, @PaymentId, @VisitStatusId, @EntryTime, @ExitTime);
        SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VisitorId", visit.VisitorId);
                command.Parameters.AddWithValue("@UserAccountId", visit.UserAccountId);
                command.Parameters.AddWithValue("@RfidTagNumberId", visit.RfidTagNumberId);
                command.Parameters.AddWithValue("@PaymentId", visit.PaymentAmount);
                command.Parameters.AddWithValue("@VisitStatusId", visit.VisitStatusId);
                command.Parameters.AddWithValue("@EntryTime", visit.EntryTime);
                command.Parameters.AddWithValue("@ExitTime", visit.ExitTime ?? (object)DBNull.Value);
                connection.Open();
                visit.VisitId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public DataTable GetVisitorStatuses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT VisitorId, FirstName, LastName, VisitStatusId FROM Visitors JOIN Visits ON Visitors.VisitorId = Visits.VisitorId";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public void UpdateVisitorStatus(int visitorId, int statusId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Visits SET VisitStatusId = @StatusId WHERE VisitorId = @VisitorId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VisitorId", visitorId);
                command.Parameters.AddWithValue("@StatusId", statusId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


    }

}
