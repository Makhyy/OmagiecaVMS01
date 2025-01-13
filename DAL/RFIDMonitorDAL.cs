﻿using System;
using System.Collections.Generic;
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

    }
}
