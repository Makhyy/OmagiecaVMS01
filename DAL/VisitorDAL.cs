using MODELS;
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

        public int AddVisitor(Visitor visitor)
        {
            string query = @"INSERT INTO Visitors 
(FirstName, LastName, Age, VisitorType, IsPWD, Gender, CityMunicipality, ForeignCountry, PaymentAmount, DateRegistered, RfidTagNumberId, VisitorStatus, UserAccountId)
OUTPUT INSERTED.VisitorId
VALUES 
(@FirstName, @LastName, @Age, @VisitorType, @IsPWD, @Gender, @CityMunicipality, @ForeignCountry, @PaymentAmount, @DateRegistered, @RfidTagNumberId, @VisitorStatus, @UserAccountId)";


            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;  // Initialize transaction to null

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction(); // Start a new transaction

                if (!new[] { "Registered", "Entered", "Exited" }.Contains(visitor.VisitorStatus))
                {
                    throw new ArgumentException("Invalid VisitorStatus value.");
                }
                if (visitor.UserAccountId <= 0)
                {
                    throw new ArgumentException("Invalid UserAccountId.");
                }



                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@FirstName", visitor.FirstName);
                    command.Parameters.AddWithValue("@LastName", visitor.LastName);
                    command.Parameters.AddWithValue("@Age", visitor.Age);
                    command.Parameters.AddWithValue("@VisitorType", visitor.VisitorType);
                    command.Parameters.AddWithValue("@IsPWD", visitor.IsPWD);
                    command.Parameters.AddWithValue("@Gender", visitor.Gender);
                    command.Parameters.AddWithValue("@CityMunicipality", visitor.CityMunicipality ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ForeignCountry", visitor.ForeignCountry ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentAmount", visitor.PaymentAmount);
                    command.Parameters.AddWithValue("@RfidTagNumberId", visitor.RfidTagNumberId);
                    command.Parameters.AddWithValue("@DateRegistered", visitor.DateRegistered);
                    command.Parameters.AddWithValue("@VisitorStatus", visitor.VisitorStatus ?? "Registered");
                    command.Parameters.AddWithValue("@UserAccountId", visitor.UserAccountId);


                    int visitorId = (int)command.ExecuteScalar();
                    transaction.Commit();  // Commit the transaction
                    return visitorId;
                }
            }
            catch (Exception)
            {
                if (transaction != null) transaction.Rollback();  // Rollback the transaction on error
                throw;  // Re-throw the exception to the caller
            }
            finally
            {
                if (connection != null) connection.Close();  // Ensure the connection is closed
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
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Visitors 
                 SET FirstName = @FirstName, 
                     LastName = @LastName, 
                     Age = @Age, 
                     VisitorType = @VisitorType,  
                     IsPWD = @IsPWD, 
                     Gender = @Gender, 
                     CityMunicipality = @CityMunicipality, 
                     ForeignCountry = @ForeignCountry, 
                     PaymentAmount = @PaymentAmount, 
                     RfidTagNumberId = @RfidTagNumberId,  
                     VisitorStatus = @VisitorStatus, 
                     UserAccountId = @UserAccountId
                 WHERE VisitorId = @VisitorId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@VisitorId", SqlDbType.Int).Value = visitor.VisitorId;
                        command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = visitor.FirstName;
                        command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = visitor.LastName;
                        command.Parameters.Add("@Age", SqlDbType.Int).Value = visitor.Age;
                        command.Parameters.Add("@VisitorType", SqlDbType.VarChar).Value = visitor.VisitorType;
                        command.Parameters.Add("@IsPWD", SqlDbType.Bit).Value = visitor.IsPWD;
                        command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = visitor.Gender;
                        command.Parameters.Add("@CityMunicipality", SqlDbType.VarChar).Value = visitor.CityMunicipality ?? (object)DBNull.Value;
                        command.Parameters.Add("@ForeignCountry", SqlDbType.VarChar).Value = visitor.ForeignCountry ?? (object)DBNull.Value;
                        command.Parameters.Add("@PaymentAmount", SqlDbType.Decimal).Value = visitor.PaymentAmount;
                        command.Parameters.Add("@RfidTagNumberId", SqlDbType.Int).Value = (visitor.RfidTagNumberId > 0) ? (object)visitor.RfidTagNumberId : DBNull.Value;
                        command.Parameters.AddWithValue("@VisitorStatus", visitor.VisitorStatus);
                        command.Parameters.AddWithValue("@UserAccountId", visitor.UserAccountId);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)  // Catch more specific exceptions if possible
            {
                // Log the exception here
                throw new ApplicationException("Failed to update visitor information.", ex);
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
                     OR VisitorStatus LIKE @Keyword)
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
                            VisitorStatus = reader["VisitorStatus"].ToString(),
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

        public void DeleteVisitor(int visitorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Attempt to unassign RFID tags, but do not fail if none are found
                    UnassignRFIDTags(visitorId);

                    // Proceed to delete the visitor
                    string deleteQuery = "DELETE FROM Visitors WHERE VisitorId = @VisitorId";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction);
                    deleteCommand.Parameters.AddWithValue("@VisitorId", visitorId);
                    int affectedRows = deleteCommand.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        throw new KeyNotFoundException("No visitor found with ID " + visitorId);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Failed to delete visitor with ID " + visitorId + ". Error: " + ex.Message, ex);
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
                WHERE VisitorId = @VisitorId AND RfidStatus = 'In Use'";

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
                string sqlQuery = @"
                SELECT CAST(DateRegistered AS DATE) AS Date, SUM(PaymentAmount) AS TotalRevenue
                FROM Visitor
                WHERE DateRegistered BETWEEN @StartDate AND @EndDate
                GROUP BY CAST(DateRegistered AS DATE)
                ORDER BY Date";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
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

        public void AddNewVisit(Visit visit)
        {
            string query = @"
        INSERT INTO Visit (VisitorId, UserAccountId, VisitStatusId, EntryTime, ExitTime, RfidTagNumberId)
        VALUES (@VisitorId, @UserAccountId, @VisitStatusId, @EntryTime, @ExitTime, @RfidTagNumberId)";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitorId", visit.VisitorId);
            command.Parameters.AddWithValue("@UserAccountId", visit.UserAccountId);
            command.Parameters.AddWithValue("@VisitStatusId", visit.VisitStatusId);
            command.Parameters.AddWithValue("@EntryTime", (object)visit.EntryTime ?? DBNull.Value);
            command.Parameters.AddWithValue("@ExitTime", (object)visit.ExitTime ?? DBNull.Value);
            command.Parameters.AddWithValue("@RfidTagNumberId", (object)visit.RfidTagNumberId ?? DBNull.Value);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

    }
}

        
