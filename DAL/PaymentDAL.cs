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
    }
}
