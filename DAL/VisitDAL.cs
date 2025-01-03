using MODELS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VisitDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;

        // Insert a new visit
        public bool InsertVisit(Visit visit)
        {
            string query = @"
            INSERT INTO Visit (VisitorId, UserAccountId, RfidTagNumberId, PaymentId, VisitStatus)
            VALUES (@VisitorId, @UserAccountId, @RfidTagNumberId, @PaymentId, @VisitStatus)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VisitorId", visit.VisitorId);
                    command.Parameters.AddWithValue("@UserAccountId", visit.UserAccountId);
                    command.Parameters.AddWithValue("@RfidTagNumberId", visit.RfidTagNumberId);
                    command.Parameters.AddWithValue("@PaymentId", visit.PaymentId);
                    command.Parameters.AddWithValue("@VisitStatus", visit.VisitStatus);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // Retrieve a visit by VisitorId
        public Visit GetVisitByVisitorId(int visitorId)
        {
            string query = "SELECT * FROM Visit WHERE VisitorId = @VisitorId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VisitorId", visitorId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Visit
                            {
                                VisitId = (int)reader["VisitId"],
                                VisitorId = (int)reader["VisitorId"],
                                UserAccountId = (int)reader["UserAccountId"],
                                RfidTagNumberId = (int)reader["RfidTagNumberId"],
                                PaymentId = (int)reader["PaymentId"],
                                VisitStatus = reader["VisitStatus"].ToString(),
                                EntryTime = reader.IsDBNull(reader.GetOrdinal("EntryTime")) ? null : (DateTime?)reader["EntryTime"],
                                ExitTime = reader.IsDBNull(reader.GetOrdinal("ExitTime")) ? null : (DateTime?)reader["ExitTime"]
                            };
                        }
                    }
                }
            }
            return null;
        }
        public List<Visit> GetVisitsByStatus(string status)
        {
            string query = "SELECT * FROM Visit WHERE VisitStatus = @Status";
            List<Visit> visits = new List<Visit>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            visits.Add(new Visit
                            {
                                VisitId = (int)reader["VisitId"],
                                VisitorId = (int)reader["VisitorId"],
                                UserAccountId = (int)reader["UserAccountId"],
                                RfidTagNumberId = (int)reader["RfidTagNumberId"],
                                PaymentId = (int)reader["PaymentId"],
                                VisitStatus = reader["VisitStatus"].ToString(),
                                EntryTime = reader.IsDBNull(reader.GetOrdinal("EntryTime")) ? null : (DateTime?)reader["EntryTime"],
                                ExitTime = reader.IsDBNull(reader.GetOrdinal("ExitTime")) ? null : (DateTime?)reader["ExitTime"]
                            });
                        }
                    }
                }
            }
            return visits;
        }
        public bool UpdateVisitStatus(int visitId, string status, DateTime timestamp)
        {
            string query = @"
        UPDATE Visit
        SET VisitStatus = @Status, 
            EntryTime = CASE WHEN @Status = 'Onsite' THEN @Timestamp ELSE EntryTime END,
            ExitTime = CASE WHEN @Status = 'Exited' THEN @Timestamp ELSE ExitTime END
        WHERE VisitId = @VisitId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VisitId", visitId);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Timestamp", timestamp);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public List<Visit> GetAllVisits()
        {
            string query = "SELECT * FROM Visit";
            List<Visit> visits = new List<Visit>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            visits.Add(new Visit
                            {
                                VisitId = (int)reader["VisitId"],
                                VisitorId = (int)reader["VisitorId"],
                                UserAccountId = (int)reader["UserAccountId"],
                                RfidTagNumberId = (int)reader["RfidTagNumberId"],
                                PaymentId = (int)reader["PaymentId"],
                                VisitStatus = reader["VisitStatus"].ToString(),
                                EntryTime = reader.IsDBNull(reader.GetOrdinal("EntryTime")) ? null : (DateTime?)reader["EntryTime"],
                                ExitTime = reader.IsDBNull(reader.GetOrdinal("ExitTime")) ? null : (DateTime?)reader["ExitTime"]
                            });
                        }
                    }
                }
            }
            return visits;
        }


    }

}
