using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RFIDMonitorDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;



        public bool IsValidRFID(string rfidTagUID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Modified to join RFIDTag and Visitors tables
                string query = @"
            SELECT COUNT(*)
            FROM RFIDTag
            INNER JOIN Visitors ON RFIDTag.RfidTagNumberId = Visitors.RfidTagNumberId
            WHERE RFIDTag.RfidTagUID = @RfidTagUID AND Visitors.VisitorStatus = 'Registered'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RfidTagUID", rfidTagUID);
                    int result = (int)cmd.ExecuteScalar();
                    return result > 0;
                }
            }
        }
        public void UpdateVisitorStatus(string rfidTagUID, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Update to modify the appropriate visitor record based on RFID UID
                string query = @"
            UPDATE Visitors
            SET VisitorStatus = @NewStatus
            FROM Visitors
            INNER JOIN RFIDTag ON Visitors.RfidTagNumberId = RFIDTag.RfidTagNumberId
            WHERE RFIDTag.RfidTagUID = @RfidTagUID AND Visitors.VisitorStatus = 'Registered'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmd.Parameters.AddWithValue("@RfidTagUID", rfidTagUID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("No eligible visitor found or visitor status update failed.");
                    }
                }
            }
        }
        public void UpdateVisitorStatusExit(string rfidTagUID, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Update to modify the appropriate visitor record based on RFID UID
                string query = @"
            UPDATE Visitors
            SET VisitorStatus = @NewStatus
            FROM Visitors
            INNER JOIN RFIDTag ON Visitors.RfidTagNumberId = RFIDTag.RfidTagNumberId
            WHERE RFIDTag.RfidTagUID = @RfidTagUID AND Visitors.VisitorStatus = 'Entered'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmd.Parameters.AddWithValue("@RfidTagUID", rfidTagUID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("No eligible visitor found or visitor status update failed.");
                    }
                }
            }
        }
        public void UpdateVisitorStatus(int rfidTagNumber)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE Visitors
                SET VisitorStatus = CASE 
                    WHEN EXISTS (
                        SELECT 1 FROM RFIDTag 
                        WHERE RfidTagNumber = @RfidTagNumber AND RfidStatus = 'In Use'
                    ) THEN 'Checked-In'
                    ELSE 'Checked-Out'
                END
                WHERE VisitorId = (
                    SELECT VisitorId FROM RFIDTag WHERE RfidTagNumber = @RfidTagNumber
                );
            ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RfidTagNumber", rfidTagNumber);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public interface IVisitorDataAccess
        {
            void UpdateVisitorStatus(int rfidTagNumber);
        }
        public DataTable GetVisitors()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT V.VisitorId, V.FirstName, V.LastName, V.Age, V.VisitorType,
                             V.IsPWD, V.Gender, V.CityMunicipality, V.ForeignCountry, V.PaymentAmount, 
                             R.RFIDTagNumber, V.DateRegistered, V.VisitorStatus, V.UserAccountId
                      FROM Visitors V
                      JOIN RFIDTag R ON V.RfidTagNumberId = R.RfidTagNumberId";

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
        public void UpdateVisitorStatus(int visitorId, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Visitors SET VisitorStatus = @VisitorStatus WHERE VisitorId = @VisitorId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VisitorStatus", newStatus);
                    cmd.Parameters.AddWithValue("@VisitorId", visitorId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
