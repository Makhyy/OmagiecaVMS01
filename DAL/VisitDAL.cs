﻿using MODELS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VisitDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;
        public bool AddVisit(Visit visit)
        {
            string commandText = @"
        INSERT INTO Visit (VisitorId, RfidTagNumberId, EntryTime, VisitStatusId)
        VALUES (@VisitorId, @RfidTagNumberId, @EntryTime, @VisitStatusId);";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;  // Initialize transaction to null

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();  // Begin transaction

                using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                {
                    command.Parameters.AddWithValue("@VisitorId", visit.VisitorId);
                    command.Parameters.AddWithValue("@RfidTagNumberId", visit.RfidTagNumberId);
                    command.Parameters.AddWithValue("@EntryTime", visit.EntryTime);
                    command.Parameters.AddWithValue("@VisitStatusId", visit.VisitStatusId);

                    command.ExecuteNonQuery();
                }

                transaction.Commit();  // Commit transaction
                return true;
            }
            catch (Exception)
            {
                transaction?.Rollback();  // Rollback transaction if an exception occurs
                throw;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public DataTable GetVisitorStatuses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT VisitorId, FirstName, LastName, VisitStatusId FROM Visitors JOIN Visits ON Visitors.VisitorId = Visits.VisitorId";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public bool IsValidStatus(int statusId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Status WHERE StatusId = @StatusId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StatusId", statusId);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public int GetDefaultStatusId()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string commandText = "SELECT TOP 1 StatusId FROM Status WHERE IsDefault = 1";
                using (var command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    throw new InvalidOperationException("No default status ID found in the database.");
                }
            }
        }

        public void UpdateVisitorStatus(int visitorId, int statusId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Visits SET VisitStatusId = @StatusId WHERE VisitorId = @VisitorId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VisitorId", visitorId);
                command.Parameters.AddWithValue("@StatusId", statusId);
                connection.Open();
                command.ExecuteNonQuery();
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

        public DataTable GetVisitorDetails()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = @"
            SELECT 
                v.VisitorId, 
                rt.RfidTagNumber, 
                v. VisitorStatus, 
                vis.EntryTime, 
                vis.ExitTime
            FROM Visitors v
            JOIN RFIDTag rt ON v.VisitorId = rt.VisitorId
            LEFT JOIN Visit vis ON v.VisitorId = vis.VisitorId";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

    }

}
