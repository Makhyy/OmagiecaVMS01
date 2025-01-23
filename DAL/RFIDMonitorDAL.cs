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



        /*
         //entrance
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
         //exit
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
       */

        public bool IsValidRFIDVisit(string rfidTagUID, string visitStatus)
        {
            if (string.IsNullOrWhiteSpace(rfidTagUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidTagUID));

            if (string.IsNullOrWhiteSpace(visitStatus))
                throw new ArgumentException("Visit status cannot be null or empty.", nameof(visitStatus));

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
                        WHERE R.RfidTagUID = @RfidTagUID AND S.VisitStatus = @VisitStatus
                    )
                    THEN 1
                    ELSE 0
                END";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@RfidTagUID", SqlDbType.NVarChar).Value = rfidTagUID;
                        cmd.Parameters.Add("@VisitStatus", SqlDbType.NVarChar).Value = visitStatus;

                        int result = (int)cmd.ExecuteScalar();
                        return result == 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception (replace this comment with logging logic)
                throw new ApplicationException("An error occurred while validating the RFID tag.", ex);
            }
        }

        public bool IsValidRFIDVisitEntrance(string rfidTagUID)
        {
            return IsValidRFIDVisit(rfidTagUID, "Registered");
        }

        public bool IsValidRFIDVisitExit(string rfidTagUID)
        {
            return IsValidRFIDVisit(rfidTagUID, "Entered");
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
                
                throw new ApplicationException("An error occurred while fetching the visit status.", ex);
            }
        }

        // Entrance
        public void UpdateVisitStatus(string rfidUID, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(rfidUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidUID));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string query = @"
                UPDATE Visit
                SET VisitStatusId = (SELECT VisitStatusId FROM Status WHERE VisitStatus = @VisitStatus),
                    EntryTime = CASE WHEN @VisitStatus = 'Entered' THEN CAST(GETDATE() AS TIME) ELSE EntryTime END
                WHERE RfidTagNumberId = (SELECT RfidTagNumberId FROM RFIDTag WHERE RfidTagUID = @RfidUID)
                  AND EntryTime IS NULL;";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.Add("@RfidUID", SqlDbType.NVarChar).Value = rfidUID;
                            cmd.Parameters.Add("@VisitStatus", SqlDbType.NVarChar).Value = newStatus;

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 0)
                                throw new InvalidOperationException("No matching record found or visitor has already entered.");

                            transaction.Commit();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("An error occurred while updating the visitor status.", ex);
            }
        }

        // Exit
        public void UpdateVisitStatusExit(string rfidUID, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(rfidUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidUID));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string query = @"
                UPDATE Visit
                SET VisitStatusId = (SELECT VisitStatusId FROM Status WHERE VisitStatus = @VisitStatus),
                    ExitTime = CASE WHEN @VisitStatus = 'Exited' THEN CAST(GETDATE() AS TIME) ELSE ExitTime END
                WHERE RfidTagNumberId = (SELECT RfidTagNumberId FROM RFIDTag WHERE RfidTagUID = @RfidUID)
                  AND ExitTime IS NULL;";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.Add("@RfidUID", SqlDbType.NVarChar).Value = rfidUID;
                            cmd.Parameters.Add("@VisitStatus", SqlDbType.NVarChar).Value = newStatus;

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 0)
                                throw new InvalidOperationException("No matching record found or visitor has already exited.");

                            transaction.Commit();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("An error occurred while updating the visitor status.", ex);
            }
        }








    }
}