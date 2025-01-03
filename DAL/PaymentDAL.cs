using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MODELS;

namespace DAL
{
    public class PaymentDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;

        // Insert Payment with PWDDiscount
        public int InsertPayment(string visitorType, decimal paymentAmount, decimal pwdDiscount)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Payment (VisitorType, PaymentAmount, PWDDiscount)
                                 VALUES (@VisitorType, @PaymentAmount, @PWDDiscount)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VisitorType", visitorType);
                    cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                    cmd.Parameters.AddWithValue("@PWDDiscount", pwdDiscount);

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        // Get all Payments
        public DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Payment";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetPaymentByVisitorType(string visitorType)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Payment WHERE VisitorType = @VisitorType";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VisitorType", visitorType);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }



        public decimal FetchPWDDiscount(string visitorType)
        {
            try
            {
                string query = "SELECT PWDDiscount FROM Payment WHERE VisitorType = @VisitorType";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VisitorType", visitorType); // Pass VisitorType parameter
                        conn.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToDecimal(result); // Return fetched discount
                        }
                        else
                        {
                            return 0; // Default to 0 if no discount is found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching PWD Discount from the database.", ex);
            }
        }


        // Delete Payment by ID
        public bool DeletePayment(int paymentId)
        {
            bool isDeleted = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM Payment WHERE PaymentId = @PaymentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isDeleted = rowsAffected > 0;
                }
            }

            return isDeleted;
        }

        // Update Payment with PWDDiscount
        public bool UpdatePayment(int paymentId, string visitorType, decimal paymentAmount, decimal pwdDiscount)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Payment 
                                 SET VisitorType = @VisitorType, 
                                     PaymentAmount = @PaymentAmount,
                                     PWDDiscount = @PWDDiscount
                                 WHERE PaymentId = @PaymentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                    cmd.Parameters.AddWithValue("@VisitorType", visitorType);
                    cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                    cmd.Parameters.AddWithValue("@PWDDiscount", pwdDiscount);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0;
                }
            }

            return isUpdated;
        }
        // Method to get PaymentId by VisitorId
        public int GetPaymentIdByVisitorId(int visitorId)
        {
            string query = "SELECT PaymentId FROM Payment WHERE VisitorId = @VisitorId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VisitorId", visitorId);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int paymentId))
                    {
                        return paymentId; // Return existing PaymentId
                    }
                    else
                    {
                       
                        return visitorId;
                    }
                }
            }
        }
        
    }
}
