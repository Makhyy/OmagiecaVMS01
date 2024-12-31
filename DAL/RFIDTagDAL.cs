using MODELS;
using MODELS.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class RFIDTagDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;
        public void AddRFIDTag(RFIDTag rfidTag)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO RFIDTag (RfidTagUID, RfidTagNumber, RfidStatus, VisitorId)
                             VALUES (@RfidTagUID, @RfidTagNumber, @RfidStatus, @VisitorId)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RfidTagUID", rfidTag.RfidTagUID);
                    command.Parameters.AddWithValue("@RfidTagNumber", rfidTag.RfidTagNumber);
                    command.Parameters.AddWithValue("@RfidStatus", rfidTag.RfidStatus.ToString());
                    command.Parameters.AddWithValue("@VisitorId", DBNull.Value);  // Assuming new tags aren't assigned to a visitor.

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the RFID tag: " + ex.Message, ex);
            }
        }






        public DataTable GetAllRFIDTags()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT RfidTagNumberId, RfidTagUID, RfidTagNumber, RfidStatus, VisitorId
                FROM RFIDTag";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable rfidTable = new DataTable();
                    adapter.Fill(rfidTable);
                    return rfidTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving RFID tags: " + ex.Message, ex);
            }
        }


        public void UpdateRFIDTag(RFIDTag rfidTag)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                UPDATE RFIDTag 
                SET RfidTagUID = @RfidTagUID, 
                    RfidTagNumber = @RfidTagNumber, 
                    RfidStatus = @RfidStatus,
                    VisitorId = @VisitorId
                WHERE RfidTagNumberId = @RfidTagNumberId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RfidTagUID", rfidTag.RfidTagUID);
                    command.Parameters.AddWithValue("@RfidTagNumber", rfidTag.RfidTagNumber);
                    command.Parameters.AddWithValue("@RfidStatus", rfidTag.RfidStatus.ToString());
                    command.Parameters.AddWithValue("@VisitorId", rfidTag.VisitorId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RfidTagNumberId", rfidTag.RfidTagNumberId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the RFID tag: " + ex.Message, ex);
            }
        }



        public void DeleteRFIDTag(int rfidTagNumberId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM RFIDTag WHERE RfidTagNumberId = @RfidTagNumberId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RfidTagNumberId", rfidTagNumberId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the RFID tag: " + ex.Message, ex);
            }
        }
        public RFIDTag GetRFIDTagByNumber(int rfidTagNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT RfidTagNumberId, RfidTagUID, RfidTagNumber, RfidStatus
                FROM RFIDTag
                WHERE RfidTagNumber = @RfidTagNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RfidTagNumber", rfidTagNumber);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new RFIDTag
                        {
                            RfidTagNumberId = (int)reader["RfidTagNumberId"],
                            RfidTagUID = reader["RfidTagUID"].ToString(),
                            RfidTagNumber = (int)reader["RfidTagNumber"],
                            RfidStatus = (RFIDTagStatus)Enum.Parse(typeof(RFIDTagStatus), reader["RfidStatus"].ToString()),
                            
                        };
                    }
                    else
                    {
                        return null; // RFID tag not found
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the RFID tag: " + ex.Message, ex);
            }
        }
        public bool IsRFIDTagNumberExists(int rfidTagNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(1) FROM RFIDTag WHERE RfidTagNumber = @RfidTagNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RfidTagNumber", rfidTagNumber);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0; // Return true if the count is greater than 0
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking the RFID tag number: " + ex.Message, ex);
            }
        }
        public List<RFIDTag> SearchRFIDTags(string keyword)
        {
            List<RFIDTag> rfidTags = new List<RFIDTag>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT RfidTagNumberId, RfidTagUID, RfidTagNumber, RfidStatus
                             FROM RFIDTag
                             WHERE RfidTagUID LIKE @Keyword OR RfidTagNumber LIKE @Keyword";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        rfidTags.Add(new RFIDTag
                        {
                            RfidTagNumberId = (int)reader["RfidTagNumberId"],
                            RfidTagUID = reader["RfidTagUID"].ToString(),
                            RfidTagNumber = (int)reader["RfidTagNumber"],
                            RfidStatus = Enum.TryParse(reader["RfidStatus"].ToString(), out RFIDTagStatus status) ? status : RFIDTagStatus.Available,
                           
                        });
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching RFID tags: " + ex.Message, ex);
            }

            return rfidTags;
        }






    }
}
