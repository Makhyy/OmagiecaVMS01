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
                    string query = @"SELECT v.VisitorId, vt.VisitorTypeName, v.FirstName, v.LastName, v.Age, 
                                    v.IsPWD, v.Gender, v.CityMunicipality, v.ForeignCountry, v.DateRegistered,
                                    p.PaymentAmount, p.PaymentDate, r.RfidTagNumber
                             FROM Visitors v
                             INNER JOIN VisitorType vt ON v.VisitorTypeId = vt.VisitorTypeId
                             LEFT JOIN Payment p ON v.VisitorId = p.VisitorId
                             LEFT JOIN RFIDTag r ON v.VisitorId = r.VisitorId";

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
            int visitorId;
            string query = @"
            INSERT INTO Visitors 
            (VisitorTypeId, FirstName, LastName, Age, IsPWD, Gender, CityMunicipality, ForeignCountry, DateRegistered)
            OUTPUT INSERTED.VisitorId
            VALUES 
            (@VisitorTypeId, @FirstName, @LastName, @Age, @IsPWD, @Gender, @CityMunicipality, @ForeignCountry, @DateRegistered)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VisitorTypeId", visitor.VisitorTypeId);
                    command.Parameters.AddWithValue("@FirstName", visitor.FirstName);
                    command.Parameters.AddWithValue("@LastName", visitor.LastName);
                    command.Parameters.AddWithValue("@Age", visitor.Age);
                    command.Parameters.AddWithValue("@IsPWD", visitor.IsPWD);
                    command.Parameters.AddWithValue("@Gender", visitor.Gender);
                    command.Parameters.AddWithValue("@CityMunicipality", visitor.CityMunicipality ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ForeignCountry", visitor.ForeignCountry ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DateRegistered", visitor.DateRegistered);

                    visitorId = (int)command.ExecuteScalar();
                }
            }

            return visitorId;
        }








        public void UpdateVisitor(Visitor visitor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Visitors 
                                    SET VisitorTypeId = @VisitorTypeId, 
                                        FirstName = @FirstName, 
                                        LastName = @LastName, 
                                        Age = @Age, 
                                        IsPWD = @IsPWD, 
                                        Gender = @Gender, 
                                        CityMunicipality = @CityMunicipality, 
                                        ForeignCountry = @ForeignCountry, 
                                        PaymentAmount = @PaymentAmount, 
                                        RfidTagNumber = @RfidTagNumber 
                                    WHERE VisitorId = @VisitorId";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@VisitorId", visitor.VisitorId);
                    command.Parameters.AddWithValue("@VisitorTypeId", visitor.VisitorTypeId);
                    command.Parameters.AddWithValue("@FirstName", visitor.FirstName);
                    command.Parameters.AddWithValue("@LastName", visitor.LastName);
                    command.Parameters.AddWithValue("@Age", visitor.Age);
                    command.Parameters.AddWithValue("@IsPWD", visitor.IsPWD);
                    command.Parameters.AddWithValue("@Gender", visitor.Gender);
                    command.Parameters.AddWithValue("@CityMunicipality", visitor.CityMunicipality ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ForeignCountry", visitor.ForeignCountry ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentAmount", visitor.PaymentAmount);
                    command.Parameters.AddWithValue("@RfidTagNumber", visitor.RfidTagNumber > 0 ? (object)visitor.RfidTagNumber : DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating a visitor: " + ex.Message);
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
                            VisitorTypeId = (int)reader["VisitorTypeId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = (int)reader["Age"],
                            IsPWD = (bool)reader["IsPWD"],
                            Gender = reader["Gender"].ToString(),
                            CityMunicipality = reader["CityMunicipality"].ToString(),
                            ForeignCountry = reader["ForeignCountry"].ToString(),
                            PaymentAmount = (decimal)reader["PaymentAmount"],
                            RfidTagNumber = reader.IsDBNull(reader.GetOrdinal("RfidTagNumber")) ? 0 : (int)reader["RfidTagNumber"],
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
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Visitors WHERE VisitorId = @VisitorId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VisitorId", visitorId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting a visitor: " + ex.Message, ex);
            }
        }


        public void ArchiveVisitor(int visitorId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Archive Visitor
                        string archiveVisitorQuery = @"
                    INSERT INTO ArchivedVisitors (VisitorId, VisitorTypeId, FirstName, LastName, Age, IsPWD, Gender,
                                                  CityMunicipality, ForeignCountry, DateRegistered, ArchivedDate)
                    SELECT VisitorId, VisitorTypeId, FirstName, LastName, Age, IsPWD, Gender,
                           CityMunicipality, ForeignCountry, DateRegistered, GETDATE()
                    FROM Visitors
                    WHERE VisitorId = @VisitorId;

                    DELETE FROM Visitors WHERE VisitorId = @VisitorId;
                ";
                        SqlCommand archiveVisitorCommand = new SqlCommand(archiveVisitorQuery, connection, transaction);
                        archiveVisitorCommand.Parameters.AddWithValue("@VisitorId", visitorId);
                        archiveVisitorCommand.ExecuteNonQuery();

                        // Archive Payments
                        string archivePaymentsQuery = @"
                    INSERT INTO ArchivedPayments (PaymentId, VisitorId, PaymentAmount, PaymentDate, ArchivedDate)
                    SELECT PaymentId, VisitorId, PaymentAmount, PaymentDate, GETDATE()
                    FROM Payment
                    WHERE VisitorId = @VisitorId;

                    DELETE FROM Payment WHERE VisitorId = @VisitorId;
                ";
                        SqlCommand archivePaymentsCommand = new SqlCommand(archivePaymentsQuery, connection, transaction);
                        archivePaymentsCommand.Parameters.AddWithValue("@VisitorId", visitorId);
                        archivePaymentsCommand.ExecuteNonQuery();

                        // Update RFID Tag to Make It Available
                        string updateRFIDTagQuery = @"
                    UPDATE RFIDTag
                    SET RfidStatus = 'Available', VisitorId = NULL
                    WHERE VisitorId = @VisitorId;
                ";
                        SqlCommand updateRFIDTagCommand = new SqlCommand(updateRFIDTagQuery, connection, transaction);
                        updateRFIDTagCommand.Parameters.AddWithValue("@VisitorId", visitorId);
                        updateRFIDTagCommand.ExecuteNonQuery();

                        transaction.Commit(); // Commit transaction
                    }
                    catch
                    {
                        transaction.Rollback(); // Rollback transaction on error
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while archiving the visitor: " + ex.Message, ex);
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
            try
            {   
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT RfidTagNumberId, RfidTagNumber FROM RFIDTag WHERE RfidStatus = 'Available'";
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
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving available RFID tags: " + ex.Message, ex);
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
                    string query = "SELECT RfidTagNumberId, RfidTagNumber, RfidStatus, VisitorId FROM RFIDTag";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RFIDTag rfidTag = new RFIDTag
                        {
                            RfidTagNumberId = (int)reader["RfidTagNumberId"],
                            RfidTagNumber = (int)reader["RfidTagNumber"],
                            VisitorId = reader.IsDBNull(reader.GetOrdinal("VisitorId")) ? null : (int?)reader["VisitorId"]
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
                             SET VisitorId = @VisitorId, RfidStatus = 'In Use'
                             WHERE RfidTagNumber = @RfidTagNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VisitorId", visitorId);
                    command.Parameters.AddWithValue("@RfidTagNumber", rfidTagNumber);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while assigning the RFID tag: " + ex.Message, ex);
            }
        }




    }
}
