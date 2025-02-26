﻿using MODELS;
using MODELS.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VisitorDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;
        private SqlCommand CreateCommand(string query, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);
            // Set additional command properties here if needed
            return command;
        }
      



        public string ConnectionString { get; set; }

        public DataTable GetVisitors()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    V.VisitorId,
                    V.FirstName,
                    V.LastName,
                    V.Age,
                    V.VisitorType,
                    V.IsPWD,
                    V.Gender,
                    V.CityMunicipality,
                    V.ForeignCountry,
                    V.PaymentAmount,
                    R.RFIDTagNumber,
                    V.DateRegistered,
                    S.VisitStatus AS VisitStatus,
                    V.UserAccountId,
                    VIS.EntryTime,
                    VIS.ExitTime,
                    V.GroupId
                FROM 
                    Visitors V
                JOIN 
                    RFIDTag R ON V.RfidTagNumberId = R.RfidTagNumberId
                LEFT JOIN 
                    Visit VIS ON V.VisitorId = VIS.VisitorId
                LEFT JOIN 
                    Status S ON VIS.VisitStatusId = S.VisitStatusId";
               


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


        public DataTable GetVisitorsReport()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT VisitorId, FirstName, LastName, Age, VisitorType,
                             IsPWD, Gender, CityMunicipality, ForeignCountry,
                             DateRegistered
                      FROM Visitors ";


                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable visitorTable = new DataTable();
                adapter.Fill(visitorTable);

                return visitorTable;
            }
        }





        // Inserts visitor information into the database

        public int AddVisitorAndLogVisit(Visitor visitor)
        {
            string addVisitorQuery = @"
        INSERT INTO Visitors 
        (FirstName, LastName, Age, VisitorType, IsPWD, Gender, CityMunicipality, ForeignCountry, PaymentAmount, DateRegistered, RfidTagNumberId,  UserAccountId)
        OUTPUT INSERTED.VisitorId
        VALUES 
        (@FirstName, @LastName, @Age, @VisitorType, @IsPWD, @Gender, @CityMunicipality, @ForeignCountry, @PaymentAmount, @DateRegistered, @RfidTagNumberId, @UserAccountId)";

            string logVisitQuery = @"
        INSERT INTO Visit 
        (VisitorId, RfidTagNumberId, VisitStatusId)
        VALUES 
        (@VisitorId, @RfidTagNumberId, 1)";  // Assuming default VisitStatusId '1' is 'Registered'

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                // First, add the visitor
                using (SqlCommand visitorCommand = new SqlCommand(addVisitorQuery, connection, transaction))
                {
                    // Add all parameters for visitor
                    visitorCommand.Parameters.AddWithValue("@FirstName", visitor.FirstName);
                    visitorCommand.Parameters.AddWithValue("@LastName", visitor.LastName);
                    visitorCommand.Parameters.AddWithValue("@Age", visitor.Age);
                    visitorCommand.Parameters.AddWithValue("@VisitorType", visitor.VisitorType);
                    visitorCommand.Parameters.AddWithValue("@IsPWD", visitor.IsPWD);
                    visitorCommand.Parameters.AddWithValue("@Gender", visitor.Gender);
                    visitorCommand.Parameters.AddWithValue("@CityMunicipality", visitor.CityMunicipality ?? (object)DBNull.Value);
                    visitorCommand.Parameters.AddWithValue("@ForeignCountry", visitor.ForeignCountry ?? (object)DBNull.Value);
                    visitorCommand.Parameters.AddWithValue("@PaymentAmount", visitor.PaymentAmount);
                    visitorCommand.Parameters.AddWithValue("@RfidTagNumberId", visitor.RfidTagNumberId);
                    visitorCommand.Parameters.AddWithValue("@DateRegistered", visitor.DateRegistered);
                   
                    visitorCommand.Parameters.AddWithValue("@UserAccountId", visitor.UserAccountId);

                    int visitorId = (int)visitorCommand.ExecuteScalar();

                    // Next, log the visit
                    using (SqlCommand visitCommand = new SqlCommand(logVisitQuery, connection, transaction))
                    {
                        visitCommand.Parameters.AddWithValue("@VisitorId", visitorId);
                        visitCommand.Parameters.AddWithValue("@RfidTagNumberId", visitor.RfidTagNumberId);
                       

                        visitCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return visitorId;
                }
            }
            catch (Exception)
            {
                if (transaction != null) transaction.Rollback();
                throw;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }


       

        public void UpdateVisitorStatus(int visitorId, string visitorStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Visitors 
                             SET VisitorStatus = @VisitorStatus
                             WHERE VisitorId = @VisitorId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VisitorId", visitorId);
                    command.Parameters.AddWithValue("@VisitorStatus", visitorStatus);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the visitor status: " + ex.Message, ex);
            }
        }


        public void UpdateVisitor(Visitor visitor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Visitors SET
                 FirstName = @FirstName, LastName = @LastName, Age = @Age, 
                 VisitorType = @VisitorType, IsPWD = @IsPWD, Gender = @Gender, 
                 CityMunicipality = @CityMunicipality, ForeignCountry = @ForeignCountry,
                 PaymentAmount = @PaymentAmount,   
                  UserAccountId = @UserAccountId,
                 GroupId = @GroupId
                 WHERE VisitorId = @VisitorId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VisitorId", visitor.VisitorId);
                    command.Parameters.AddWithValue("@FirstName", visitor.FirstName);
                    command.Parameters.AddWithValue("@LastName", visitor.LastName);
                    command.Parameters.AddWithValue("@Age", visitor.Age);
                    command.Parameters.AddWithValue("@VisitorType", visitor.VisitorType); // Ensure this is the correct data type (int)
                    command.Parameters.AddWithValue("@IsPWD", (object)visitor.IsPWD ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Gender", visitor.Gender);
                    command.Parameters.AddWithValue("@CityMunicipality", visitor.CityMunicipality ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ForeignCountry", visitor.ForeignCountry ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentAmount", visitor.PaymentAmount);
                    
                    command.Parameters.AddWithValue("@UserAccountId", (object)visitor.UserAccountId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@GroupId", (object)visitor.GroupId ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Visitor> SearchVisitors(string keyword, int? userAccountId = null)
        {
            List<Visitor> visitors = new List<Visitor>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT * 
                FROM Visitors
                WHERE 
                    (FirstName LIKE @Keyword 
                     OR LastName LIKE @Keyword
                     OR CityMunicipality LIKE @Keyword
                     OR ForeignCountry LIKE @Keyword
                    
                    AND (@UserAccountId IS NULL OR UserAccountId = @UserAccountId)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@UserAccountId", (object)userAccountId ?? DBNull.Value);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        visitors.Add(new Visitor
                        {
                            VisitorId = (int)reader["VisitorId"],
                            VisitorType = reader["VisitorType"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = (int)reader["Age"],
                            IsPWD = (bool)reader["IsPWD"],
                            Gender = reader["Gender"].ToString(),
                            CityMunicipality = reader["CityMunicipality"].ToString(),
                            ForeignCountry = reader["ForeignCountry"].ToString(),
                            PaymentAmount = (decimal)reader["PaymentAmount"],
                            RfidTagNumberId = reader.IsDBNull(reader.GetOrdinal("RfidTagNumberId")) ? 0 : (int)reader["RfidTagNumberId"],
                            DateRegistered = (DateTime)reader["DateRegistered"],

                            UserAccountId = (int)reader["UserAccountId"]
                        });
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching for visitors: " + ex.Message, ex);
            }

            return visitors;
        }

        public void DeleteVisitors(List<int> visitorIds)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Optionally unassign RFID tags
                    foreach (int visitorId in visitorIds)
                    {
                        UnassignRFIDTags(visitorId);
                    }

                    // Proceed to delete the visitors
                    string deleteQuery = "DELETE FROM Visitors WHERE VisitorId IN (" + String.Join(", ", visitorIds) + ")";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction);
                    int affectedRows = deleteCommand.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        throw new KeyNotFoundException("No visitors found with the provided IDs.");
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Failed to delete visitors. Error: " + ex.Message, ex);
                }
            }
        }








        public List<VisitorType> GetVisitorTypes()
        {
            List<VisitorType> visitorTypes = new List<VisitorType>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT VisitorTypeId, VisitorTypeName FROM VisitorType";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        visitorTypes.Add(new VisitorType
                        {
                            VisitorTypeId = (int)reader["VisitorTypeId"],
                            VisitorTypeName = reader["VisitorTypeName"].ToString()
                        });
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving visitor types: " + ex.Message, ex);
            }

            return visitorTypes;
        }
        public List<RFIDTag> GetAvailableRFIDTags()
        {
            List<RFIDTag> rfidTags = new List<RFIDTag>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RfidTagNumberId, RfidTagNumber FROM RFIDTag WHERE RfidStatus = 'Available' AND VisitorId IS NULL";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    rfidTags.Add(new RFIDTag
                    {
                        RfidTagNumberId = (int)reader["RfidTagNumberId"],
                        RfidTagNumber = (int)reader["RfidTagNumber"]
                    });
                }

                reader.Close();
            }
            return rfidTags;
        }

        public List<RFIDTag> GetRFIDTags()
        {
            List<RFIDTag> rfidTags = new List<RFIDTag>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT RfidTagNumberId, RfidTagNumber, RfidStatus FROM RFIDTag";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RFIDTag rfidTag = new RFIDTag
                        {
                            RfidTagNumberId = (int)reader["RfidTagNumberId"],
                            RfidTagNumber = (int)reader["RfidTagNumber"],


                        };

                        // Safe parsing for RFIDStatus
                        if (!Enum.TryParse<RFIDTagStatus>(reader["RfidStatus"].ToString(), out RFIDTagStatus status))
                        {
                            status = RFIDTagStatus.Inactive; // Default to Inactive if parsing fails
                        }
                        rfidTag.RfidStatus = status;

                        rfidTags.Add(rfidTag);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving RFID tags: " + ex.Message, ex);
            }

            return rfidTags;
        }
        public bool IsRfidTagValid(int rfidTagNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM RFIDTag WHERE RfidTagNumberId = @RfidTagNumberId AND RfidStatus = 'Available'";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RfidTagNumberId", rfidTagNumber);
                connection.Open();
                int result = (int)command.ExecuteScalar();
                return result > 0;
            }
        }
        public List<RFIDTag> GetAvailableRFIDTagsToDisplay()
        {
            List<RFIDTag> rfidTags = new List<RFIDTag>();
           
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Only select RFID tags that have 'Available' status
                    const string query = "SELECT RfidTagNumberId, RfidTagNumber, RfidStatus FROM RFIDTag WHERE RfidStatus = 'Available'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RFIDTag rfidTag = new RFIDTag
                                {
                                    RfidTagNumberId = reader.GetInt32(reader.GetOrdinal("RfidTagNumberId")),
                                    RfidTagNumber = reader.GetInt32(reader.GetOrdinal("RfidTagNumber")),
                                    RfidStatus = Enum.TryParse(reader["RfidStatus"].ToString(), true, out RFIDTagStatus status) ? status : RFIDTagStatus.Inactive
                                };
                                rfidTags.Add(rfidTag);
                            }
                        }
                    }
                }
            
          
            return rfidTags;
        }




        public void AddPayment(int visitorId, decimal paymentAmount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Payment (VisitorId, PaymentAmount, PaymentDate)
                             VALUES (@VisitorId, @PaymentAmount, GETDATE())";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VisitorId", visitorId);
                    command.Parameters.AddWithValue("@PaymentAmount", paymentAmount);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding payment data: " + ex.Message, ex);
            }
        }
        public void AssignRFIDTag(int visitorId, int rfidTagNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE RFIDTag 
                             SET VisitorId = @VisitorId, 
                                 RfidStatus = @RfidStatus
                             WHERE RfidTagNumberId = @RfidTagNumberId AND RfidStatus = 'Available'"; // Ensuring current status allows update

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VisitorId", visitorId);
                    command.Parameters.AddWithValue("@RfidStatus", "InUse"); // Set to a valid status
                    command.Parameters.AddWithValue("@RfidTagNumberId", rfidTagNumber);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("RFID tag is not available for assignment or does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while assigning the RFID tag: " + ex.Message, ex);
            }
        }

        public void UnassignRFIDTags(int visitorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                UPDATE RFIDTag 
                SET VisitorId = NULL, 
                    RfidStatus = 'Available' 
                WHERE VisitorId = @VisitorId AND RfidStatus = 'InUse'";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VisitorId", visitorId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Log a warning if no tags were unassigned
                        Console.WriteLine($"No RFID tags were unassigned for VisitorId {visitorId}.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    Console.WriteLine($"Error in UnassignRFIDTags: {ex.Message}");
                    throw; // Re-throw the exception to the caller
                }
            }
        }


        public DataTable GetVisitorsByStatus(string visitorStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT VisitorId, FirstName, LastName, VisitorStatus 
                             FROM Visitors 
                             WHERE VisitorStatus = @VisitorStatus";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@VisitorStatus", visitorStatus);

                    DataTable visitorTable = new DataTable();
                    adapter.Fill(visitorTable);

                    return visitorTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving visitors by status: " + ex.Message, ex);
            }
        }
        public DataTable GetVisitorsForSpecificDay(DateTime specificDay)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT VisitorId, FirstName, LastName, Age, VisitorType,
                             IsPWD, Gender, CityMunicipality, ForeignCountry,
                             DateRegistered
                      FROM Visitors 
                    WHERE CAST(DateRegistered AS DATE) = @SpecificDay";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SpecificDay", specificDay.Date);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
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
        public int GetVisitorDaily()
        {
            try
            {
                int specificDay = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT COUNT(*) FROM Visitors
                      FROM Visitors 
                   ";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SpecificDay", specificDay);



                    return specificDay;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving visitors: " + ex.Message, ex);
            }
        }
        public DataTable GetVisitorsWeeklyReport(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT VisitorId, FirstName, LastName, Age, VisitorType,
                             IsPWD, Gender, CityMunicipality, ForeignCountry,
                             DateRegistered
                      FROM Visitors 
                WHERE DateRegistered >= @StartDate AND DateRegistered <= @EndDate";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", startDate.Date);
                command.Parameters.AddWithValue("@EndDate", endDate.Date);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable visitorTable = new DataTable();
                adapter.Fill(visitorTable);

                return visitorTable;
            }
        }
        public DataTable GetRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT VisitorId, PaymentAmount, DateRegistered
        FROM Visitors
        WHERE DateRegistered >= @StartDate AND DateRegistered <= @EndDate";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }




        public DataTable GetDailyRevenue(DateTime specificDate)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                SELECT CAST(DateRegistered AS DATE) AS Date, SUM(PaymentAmount) AS TotalRevenue
                FROM Visitors
                WHERE CAST(DateRegistered AS DATE) = @SpecificDate
                GROUP BY CAST(DateRegistered AS DATE)";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@SpecificDate", specificDate);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable LoadRevenueVisitorData()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
        SELECT VisitorId, PaymentAmount, DateRegistered
        FROM Visitors";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }


        public decimal GetTotalWeeklyRevenue()
        {
            decimal totalPayment = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                SELECT SUM(PaymentAmount) AS TotalPayment
                FROM Visitors
                WHERE DateRegistered >= DATEADD(DAY, -7, CAST(GETDATE() AS DATE))";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalPayment = Convert.ToDecimal(result);
                }
            }
            return totalPayment;
        }
        public decimal GetTotalMonthlyRevenue()
        {
            decimal totalPayment = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                SELECT SUM(PaymentAmount) AS TotalPayment
                FROM Visitors
                WHERE DateRegistered >= DATEADD(MONTH, -1, CAST(GETDATE() AS DATE))";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalPayment = Convert.ToDecimal(result);
                }
            }
            return totalPayment;
        }
        public decimal GetTotalRevenue()
        {
            decimal totalPayment = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                SELECT SUM(PaymentAmount) AS TotalPayment
                FROM Visitors";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalPayment = Convert.ToDecimal(result);
                }
            }
            return totalPayment;
        }
        public decimal GetTotalRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            decimal totalPayment = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                SELECT SUM(PaymentAmount) AS TotalPayment
                FROM Visitors
                WHERE DateRegistered >= @StartDate AND DateRegistered <= @EndDate";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalPayment = Convert.ToDecimal(result);
                }
            }
            return totalPayment;
        }

        public int GetTodaysVisitorCount()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Use the correct field name 'RegisteredDate' for filtering today's visitors
                string query = "SELECT COUNT(*) FROM Visitors WHERE CAST(RegisteredDate AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    int count = (int)cmd.ExecuteScalar(); // Safely cast as the return is expected to be a non-null integer
                    return count;
                }
                catch (Exception ex)
                {
                    // It's a good idea to handle possible exceptions that might occur during database operations
                    Console.WriteLine("Error in GetTodaysVisitorCount: " + ex.Message);
                    return 0; // Return 0 or handle accordingly if an error occurs
                }
                finally
                {
                    con.Close(); // Ensure the connection is closed even if an exception occurs
                }
            }
        }


        public List<Visitor> GetAllVisitors()
        {
            List<Visitor> visitors = new List<Visitor>();
            string query = "SELECT * FROM Visitors";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = CreateCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        visitors.Add(new Visitor
                        {
                            VisitorId = Convert.ToInt32(reader["VisitorId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            // Other fields...
                        });
                    }
                }
            }
            return visitors;
        }
        public int GetVisitStatusIdByName(string statusName)
        {
            string query = "SELECT VisitStatusId FROM VisitStatus WHERE VisitStatusName = @StatusName";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StatusName", statusName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                // If no result found, default to '1' for 'Registered'
                if (result != null)
                    return Convert.ToInt32(result);
                else
                    return 1;  // Default VisitStatusId for 'Registered'
            }
            catch (Exception)
            {
                // In case of any other exception, still return the default status ID
                return 1;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        




        public DataTable GetRevenueByDate(DateTime date)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT VisitorId, PaymentAmount, DateRegistered
        FROM Visitors
        WHERE CAST(DateRegistered AS DATE) = @Date";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public DataTable GetRevenueByDateRangeM(DateTime startDate, DateTime endDate)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT VisitorId, PaymentAmount, DateRegistered
        FROM Visitors
        WHERE DateRegistered >= @StartDate AND DateRegistered <= @EndDate";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
        public int GetEnteredVisitorCount()
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Using DATEDIFF to filter entries from today
                string query = @"
          SELECT COUNT(*) 
  FROM Visit
  WHERE VisitStatusId = '2'
           "; // Only include records where the difference in days is 0

                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Handle exceptions (you might want to log this error)
                    throw new Exception("Error fetching entered visitor count: " + ex.Message);
                }
            }
            return count;
        }
        public int GetExitedVisitorCount()
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Using DATEDIFF to filter entries from today
                string query = @"
           SELECT COUNT(*) 
  FROM Visit
  WHERE VisitStatusId = '3'"; // Only include records where the difference in days is 0

                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Handle exceptions (you might want to log this error)
                    throw new Exception("Error fetching entered visitor count: " + ex.Message);
                }
            }
            return count;
        }
        public int GetRemainingVisitorCount()
        {
            int remainingVisitors = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Optimized query to count remaining visitors (entered not exited)
                string query = @"
SELECT SUM(CASE WHEN VisitStatusId = '2' THEN 1 ELSE 0 END) -
       SUM(CASE WHEN VisitStatusId = '3' THEN 1 ELSE 0 END) AS RemainingVisitors
FROM Visit";


                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    remainingVisitors = result != DBNull.Value ? (int)result : 0; // Ensure no DBNull issues
                }
                catch (Exception ex)
                {
                    // Handle exceptions (you might want to log this error)
                    throw new Exception("Error fetching remaining visitor count: " + ex.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            // Ensure the count doesn't go negative
            return Math.Max(0, remainingVisitors);
        }

        public DataTable GetAllVisitorsData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Visitors", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable SearchVisitorsData(string searchTerm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Visitors WHERE FirstName LIKE @SearchTerm OR LastName LIKE @SearchTerm OR CityMunicipality LIKE @SearchTerm", con);
                    cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception )
                {
                    // Log the exception here
                    
                    throw; // Rethrow the exception to be handled by the BLL
                }
            }
            return dt;
        }

        public DataTable GetDistinctVisitorTypes()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT VisitorType FROM Visitors", conn);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetCombinedLocations()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Assuming CityMunicipality and ForeignCountry are from the same table, adjust if not.
                SqlCommand cmd = new SqlCommand(@"
            SELECT DISTINCT CityMunicipality AS Location, 'City/Municipality' AS Type FROM Visitors WHERE CityMunicipality IS NOT NULL
            UNION
            SELECT DISTINCT ForeignCountry AS Location, 'Foreign Country' AS Type FROM Visitors WHERE ForeignCountry IS NOT NULL
            ORDER BY Location", conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }


    }
}

        
