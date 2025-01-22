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
        //
        public bool IsValidRFIDVisit(string rfidTagUID)
        {
            if (string.IsNullOrWhiteSpace(rfidTagUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidTagUID));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT CASE 
                           WHEN EXISTS (
                               SELECT 1
                               FROM RFIDTag R
                               INNER JOIN Visit V ON R.RfidTagNumberId = V.RfidTagNumberId
                               INNER JOIN Status S ON V.VisitStatusId = S.VisitStatusId
                               WHERE R.RfidTagUID = @RfidTagUID AND S.VisitStatus = 'Registered'
                           )
                           THEN 1
                           ELSE 0
                       END";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@RfidTagUID", SqlDbType.NVarChar).Value = rfidTagUID;

                        int result = (int)cmd.ExecuteScalar();
                        return result == 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or rethrow it for handling in the calling code
                throw new ApplicationException("An error occurred while validating the RFID tag.", ex);
            }
        }
        //--
        public bool IsValidRFIDVisitExit(string rfidTagUID)
        {
            if (string.IsNullOrWhiteSpace(rfidTagUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidTagUID));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT CASE 
                           WHEN EXISTS (
                               SELECT 2
                               FROM RFIDTag R
                               INNER JOIN Visit V ON R.RfidTagNumberId = V.RfidTagNumberId
                               INNER JOIN Status S ON V.VisitStatusId = S.VisitStatusId
                               WHERE R.RfidTagUID = @RfidTagUID AND S.VisitStatus = 'Entered'
                           )
                           THEN 2
                           ELSE 0
                       END";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@RfidTagUID", SqlDbType.NVarChar).Value = rfidTagUID;

                        int result = (int)cmd.ExecuteScalar();
                        return result == 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or rethrow it for handling in the calling code
                throw new ApplicationException("An error occurred while validating the RFID tag.", ex);
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
        public string GetCurrentVisitStatus(string rfidTagUID)
        {
            if (string.IsNullOrWhiteSpace(rfidTagUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidTagUID));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT Status.VisitStatus
                FROM Visit
                INNER JOIN Status ON Visit.VisitStatusId = Status.VisitStatusId
                INNER JOIN RFIDTag ON Visit.RfidTagNumberId = RFIDTag.RfidTagNumberId
                WHERE RFIDTag.RfidTagUID = @RfidTagUID
                ORDER BY Visit.EntryTime DESC"; 
        
            using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@RfidTagUID", SqlDbType.NVarChar).Value = rfidTagUID;

                        object result = cmd.ExecuteScalar();
                        return result == null ? "Not Found" : result.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log exception and rethrow or return a custom error message
                throw new ApplicationException("An error occurred while fetching the visit status.", ex);
            }
        }
        //entrance
        public void UpdateVisitStatus(string rfidUID, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(rfidUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidUID));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                UPDATE Visit
                SET VisitStatusId = (SELECT VisitStatusId FROM Status WHERE VisitStatus = @NewStatus),
                    EntryTime = CASE WHEN @NewStatus = 'Entered' THEN GETDATE() ELSE EntryTime END
                WHERE RfidTagNumberId = (SELECT RfidTagNumberId FROM RFIDTag WHERE RfidTagUID = @RfidTagUID)
                  AND EntryTime IS NULL;"; 

            using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@RfidTagUID", SqlDbType.NVarChar).Value = rfidUID;
                        cmd.Parameters.Add("@NewStatus", SqlDbType.NVarChar).Value = newStatus;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("No matching record found or visitor has already entered.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("An error occurred while updating the visitor status.", ex);
            }
        }
        //exit
        public void UpdateVisitStatusExit(string rfidUID, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(rfidUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidUID));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                UPDATE Visit
                SET VisitStatusId = (SELECT VisitStatusId FROM Status WHERE VisitStatus = @NewStatus),
                    ExitTime = CASE WHEN @NewStatus = 'Exited' THEN GETDATE() ELSE ExitTime END
                WHERE RfidTagNumberId = (SELECT RfidTagNumberId FROM RFIDTag WHERE RfidTagUID = @RfidTagUID)
                  AND ExitTime IS NULL;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@RfidTagUID", SqlDbType.NVarChar).Value = rfidUID;
                        cmd.Parameters.Add("@NewStatus", SqlDbType.NVarChar).Value = newStatus;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("No matching record found or visitor has already entered.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("An error occurred while updating the visitor status.", ex);
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