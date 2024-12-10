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

        public int InsertPayment(string visitorType, decimal paymentAmount, DateTime paymentDate)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Payment (VisitorType, PaymentAmount, PaymentDate)
                         VALUES (@VisitorType, @PaymentAmount, @PaymentDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VisitorType", visitorType);
                    cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                    cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
        public DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT PaymentId, VisitorType, PaymentAmount, PaymentDate FROM Payment";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
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
        public bool UpdatePayment(int paymentId, string visitorType, decimal paymentAmount)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Payment 
                         SET VisitorType = @VisitorType, 
                             PaymentAmount = @PaymentAmount 
                         WHERE PaymentId = @PaymentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                    cmd.Parameters.AddWithValue("@VisitorType", visitorType);
                    cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0;
                }
            }

            return isUpdated;
        }



    }
}
