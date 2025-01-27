﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MODELS;

namespace DAL
{
    public class GroupRegistrationDAL
    {
        private readonly string connectionString = Properties.Settings.Default.ConnectionString;

        // Add a new group registration with its members
        public async Task AddGroupRegistrationAsync(GroupRegistration groupRegistration)
        {
            Console.WriteLine("Group Registration:");
            Console.WriteLine($"Representative Age: {groupRegistration.RepresentativeVisitor.Age}");
            foreach (var member in groupRegistration.Members)
            {
                Console.WriteLine($"Member Age: {member.Age}, VisitorType: {member.VisitorType}, PaymentAmount: {member.PaymentAmount}");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Insert Visitors, GroupRegistration, Visit, GroupMembers as per your provided logic
                        // Ensure this matches the logic in your provided method

                        // COMMIT transaction
                        transaction.Commit();
                    }
                    catch
                    {
                        // ROLLBACK transaction in case of error
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the group registration: " + ex.Message, ex);
            }
        }
    



    // Retrieve a group registration with its members
    public GroupRegistration GetGroupRegistrationById(int groupId)
        {
            GroupRegistration groupRegistration = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch GroupRegistration details
                    string groupQuery = "SELECT * FROM GroupRegistration WHERE GroupId = @GroupId";
                    SqlCommand groupCommand = new SqlCommand(groupQuery, connection);
                    groupCommand.Parameters.AddWithValue("@GroupId", groupId);

                    using (SqlDataReader reader = groupCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            groupRegistration = new GroupRegistration
                            {
                                GroupId = (int)reader["GroupId"],
                                RepresentativeVisitorId = (int)reader["RepresentativeVisitorId"],
                                GroupName = reader["GroupName"]?.ToString(),
                                TotalMembers = (int)reader["TotalMembers"],
                                TotalPaymentAmount = (decimal)reader["TotalPaymentAmount"],
                                DateRegistered = (DateTime)reader["DateRegistered"]
                            };
                        }
                    }

                    // Fetch GroupMember details
                    if (groupRegistration != null)
                    {
                        string memberQuery = "SELECT * FROM GroupMember WHERE GroupId = @GroupId";
                        SqlCommand memberCommand = new SqlCommand(memberQuery, connection);
                        memberCommand.Parameters.AddWithValue("@GroupId", groupId);

                        using (SqlDataReader reader = memberCommand.ExecuteReader())
                        {
                            groupRegistration.Members = new List<GroupMember>();

                            while (reader.Read())
                            {
                                groupRegistration.Members.Add(new GroupMember
                                {
                                    GroupMemberId = (int)reader["GroupMemberId"],
                                    GroupId = (int)reader["GroupId"],
                                    Age = (int)reader["Age"],
                                    VisitorType = reader["VisitorType"].ToString(),
                                    PaymentAmount = (decimal)reader["PaymentAmount"],
                                    RfidTagNumberId = (int)reader["RfidTagNumberId"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the group registration: " + ex.Message, ex);
            }

            return groupRegistration;
        }


        // Update a group registration
        public void UpdateGroupRegistration(GroupRegistration groupRegistration)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    // Update GroupRegistration
                    string groupQuery = @"
                        UPDATE GroupRegistration
                        SET RepresentativeVisitorId = @RepresentativeVisitorId,
                            GroupName = @GroupName,
                            TotalMembers = @TotalMembers,
                            TotalPaymentAmount = @TotalPaymentAmount,
                            DateRegistered = @DateRegistered
                        WHERE GroupId = @GroupId";

                    SqlCommand groupCommand = new SqlCommand(groupQuery, connection, transaction);
                    groupCommand.Parameters.AddWithValue("@GroupId", groupRegistration.GroupId);
                    groupCommand.Parameters.AddWithValue("@RepresentativeVisitorId", groupRegistration.RepresentativeVisitorId);
                    groupCommand.Parameters.AddWithValue("@GroupName", groupRegistration.GroupName ?? (object)DBNull.Value);
                    groupCommand.Parameters.AddWithValue("@TotalMembers", groupRegistration.TotalMembers);
                    groupCommand.Parameters.AddWithValue("@TotalPaymentAmount", groupRegistration.TotalPaymentAmount);
                    groupCommand.Parameters.AddWithValue("@DateRegistered", groupRegistration.DateRegistered);
                    groupCommand.ExecuteNonQuery();

                    // Delete existing GroupMember entries
                    string deleteMembersQuery = "DELETE FROM GroupMember WHERE GroupId = @GroupId";
                    SqlCommand deleteCommand = new SqlCommand(deleteMembersQuery, connection, transaction);
                    deleteCommand.Parameters.AddWithValue("@GroupId", groupRegistration.GroupId);
                    deleteCommand.ExecuteNonQuery();

                    // Insert updated GroupMember entries
                    string memberQuery = @"
                        INSERT INTO GroupMember (GroupId, Age, VisitorType, PaymentAmount, RfidTagNumberId)
                        VALUES (@GroupId, @Age, @VisitorType, @PaymentAmount, @RfidTagNumberId)";

                    foreach (var member in groupRegistration.Members)
                    {
                        SqlCommand memberCommand = new SqlCommand(memberQuery, connection, transaction);
                        memberCommand.Parameters.AddWithValue("@GroupId", groupRegistration.GroupId);
                        memberCommand.Parameters.AddWithValue("@Age", member.Age);
                        memberCommand.Parameters.AddWithValue("@VisitorTypeId", member.VisitorType);
                        memberCommand.Parameters.AddWithValue("@PaymentAmount", member.PaymentAmount);
                        memberCommand.Parameters.AddWithValue("@RfidTagNumberId", member.RfidTagNumberId);
                        memberCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the group registration: " + ex.Message, ex);
            }
        }

        // Delete a group registration
        public void DeleteGroupRegistration(int groupId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    // Delete GroupMember entries
                    string deleteMembersQuery = "DELETE FROM GroupMember WHERE GroupId = @GroupId";
                    SqlCommand deleteCommand = new SqlCommand(deleteMembersQuery, connection, transaction);
                    deleteCommand.Parameters.AddWithValue("@GroupId", groupId);
                    deleteCommand.ExecuteNonQuery();

                    // Delete GroupRegistration
                    string deleteGroupQuery = "DELETE FROM GroupRegistration WHERE GroupId = @GroupId";
                    SqlCommand groupCommand = new SqlCommand(deleteGroupQuery, connection, transaction);
                    groupCommand.Parameters.AddWithValue("@GroupId", groupId);
                    groupCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the group registration: " + ex.Message, ex);
            }
        }

        public List<GroupRegistration> GetAllGroupRegistrations()
        {
            List<GroupRegistration> groups = new List<GroupRegistration>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT g.GroupId, g.RepresentativeVisitorId, g.GroupName, g.TotalMembers, 
                               g.TotalPaymentAmount, g.DateRegistered, 
                               v.FirstName AS RepresentativeFirstName, v.LastName AS RepresentativeLastName
                        FROM GroupRegistration g
                        LEFT JOIN Visitors v ON g.RepresentativeVisitorId = v.VisitorId";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GroupRegistration group = new GroupRegistration
                            {
                                GroupId = (int)reader["GroupId"],
                                RepresentativeVisitorId = (int)reader["RepresentativeVisitorId"],
                                GroupName = reader["GroupName"].ToString(),
                                TotalMembers = (int)reader["TotalMembers"],
                                TotalPaymentAmount = (decimal)reader["TotalPaymentAmount"],
                                DateRegistered = (DateTime)reader["DateRegistered"],
                                RepresentativeVisitor = new Visitor
                                {
                                    FirstName = reader["RepresentativeFirstName"].ToString(),
                                    LastName = reader["RepresentativeLastName"].ToString()
                                }
                            };

                            groups.Add(group);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving group registrations: " + ex.Message, ex);
            }

            return groups;
        }

        public DataTable GetAllGroupRegistrationsData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM GroupRegistration";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}