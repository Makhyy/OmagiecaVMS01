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
        public bool IsValidRFIDExit(string rfidTagUID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Modified to join RFIDTag and Visitors tables
                string query = @"
            SELECT COUNT(*)
            FROM RFIDTag
            INNER JOIN Visitors ON RFIDTag.RfidTagNumberId = Visitors.RfidTagNumberId
            WHERE RFIDTag.RfidTagUID = @RfidTagUID AND Visitors.VisitorStatus = 'Entered'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RfidTagUID", rfidTagUID);
                    int result = (int)cmd.ExecuteScalar();
                    return result > 0;
                }
            }
        }
        public string GetCurrentVisitorStatus(string rfidTagUID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT Visitors.VisitorStatus
                FROM Visitors
                INNER JOIN RFIDTag ON Visitors.RfidTagNumberId = RFIDTag.RfidTagNumberId
                WHERE RFIDTag.RfidTagUID = @RfidTagUID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RfidTagUID", rfidTagUID);

                    object result = cmd.ExecuteScalar();
                    return result == null ? "Not Found" : result.ToString();
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
        WHERE RFIDTag.RfidTagUID = @RfidTagUID AND (Visitors.VisitorStatus = 'Registered' OR Visitors.VisitorStatus = 'Entered')";

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
            WHERE RFIDTag.RfidTagUID = @RfidTagUID AND (Visitors.VisitorStatus = 'Entered' OR Visitors.VisitorStatus = 'Exited')";

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


    }
}