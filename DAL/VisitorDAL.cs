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

        public DataTable GetVisitors()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT VisitorId,  FirstName, LastName, Age, VisitorType,
                                    IsPWD, Gender, CityMunicipality, ForeignCountry, PaymentAmount, RfidTagNumberId, DateRegistered
                                      
                             FROM Visitors ";
                             
                             
                            

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






        // Inserts visitor information into the database

        public int AddVisitor(Visitor visitor)
        {
            string query = @"INSERT INTO Visitors 
        (FirstName, LastName, Age, VisitorType, IsPWD, Gender, CityMunicipality, ForeignCountry, PaymentAmount, DateRegistered, RfidTagNumberId)
        OUTPUT INSERTED.VisitorId
        VALUES 
        (@FirstName, @LastName, @Age, @VisitorType, @IsPWD, @Gender, @CityMunicipality, @ForeignCountry, @PaymentAmount, @DateRegistered, @RfidTagNumberId)";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;  // Initialize transaction to null

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction(); // Start a new transaction

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
                                 RfidTagNumberId = @RfidTagNumberId  
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

        public List<Visitor> SearchVisitors(string keyword)
        {
            List<Visitor> visitors = new List<Visitor>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT * FROM Visitors 
                             WHERE FirstName LIKE @Keyword 
                                OR LastName LIKE @Keyword
                                OR CityMunicipality LIKE @Keyword
                                OR ForeignCountry LIKE @Keyword";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        visitors.Add(new Visitor
                        {
                            VisitorId = (int)reader["VisitorId"],
                            VisitorType = (string)reader["VisitorType"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = (int)reader["Age"],
                            IsPWD = (bool)reader["IsPWD"],
                            Gender = reader["Gender"].ToString(),
                            CityMunicipality = reader["CityMunicipality"].ToString(),
                            ForeignCountry = reader["ForeignCountry"].ToString(),
                            PaymentAmount = (decimal)reader["PaymentAmount"],
                            RfidTagNumberId = reader.IsDBNull(reader.GetOrdinal("RfidTagNumberId")) ? 0 : (int)reader["RfidTagNumberId"],
                            DateRegistered = (DateTime)reader["DateRegistered"]
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
                    command.Parameters.AddWithValue("@RfidStatus", "In Use"); // Set to a valid status
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
                connection.Open();
                string query = @"UPDATE RFIDTag SET VisitorId = NULL WHERE VisitorId = @VisitorId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VisitorId", visitorId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    // Log this information but do not throw an error
                    Console.WriteLine("No RFID tags found or already unassigned for the specified visitor.");
                }
            }
        }





    }
}
