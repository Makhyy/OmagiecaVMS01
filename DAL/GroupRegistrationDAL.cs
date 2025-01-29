using System;
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
            int visitStatusId = 1;
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
                        // Insert group members
                        string insertVisitQuery = @"
    INSERT INTO [dbo].[Visit] 
        ([VisitorId], [RfidTagNumberId], [VisitStatusId]) 
    OUTPUT INSERTED.VisitId
    VALUES 
        (@VisitorId, @RfidTagNumberId, 1);"; // Assuming '1' means 'Registered'

                        // Insert the representative visitor
                        string insertVisitorQuery = @"
                    INSERT INTO [dbo].[Visitors] 
                        ([FirstName], [LastName], [Age], [VisitorType], [IsPWD], [Gender], [CityMunicipality], [ForeignCountry], [PaymentAmount], [RfidTagNumberId], [DateRegistered])
                    OUTPUT INSERTED.VisitorId
                    VALUES
                        (@FirstName, @LastName, @Age, @VisitorType, @IsPWD, @Gender, @CityMunicipality, @ForeignCountry, @PaymentAmount, @RfidTagNumberId, @DateRegistered";

                        int representativeVisitorId;
                        using (SqlCommand cmd = new SqlCommand(insertVisitorQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@FirstName", groupRegistration.RepresentativeVisitor.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", groupRegistration.RepresentativeVisitor.LastName);
                            cmd.Parameters.AddWithValue("@Age", groupRegistration.RepresentativeVisitor.Age);
                            cmd.Parameters.AddWithValue("@VisitorType", groupRegistration.RepresentativeVisitor.VisitorType);
                            cmd.Parameters.AddWithValue("@IsPWD", groupRegistration.RepresentativeVisitor.IsPWD);
                            cmd.Parameters.AddWithValue("@Gender", groupRegistration.RepresentativeVisitor.Gender);
                            cmd.Parameters.AddWithValue("@CityMunicipality", (object)groupRegistration.RepresentativeVisitor.CityMunicipality ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ForeignCountry", (object)groupRegistration.RepresentativeVisitor.ForeignCountry ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@PaymentAmount", groupRegistration.RepresentativeVisitor.PaymentAmount);
                            cmd.Parameters.AddWithValue("@RfidTagNumberId", groupRegistration.RepresentativeVisitor.RfidTagNumberId);
                            cmd.Parameters.AddWithValue("@DateRegistered", groupRegistration.RepresentativeVisitor.DateRegistered);
                            

                            representativeVisitorId = (int)await cmd.ExecuteScalarAsync();
                        }

                        using (SqlCommand visitCmd = new SqlCommand(insertVisitQuery, connection, transaction))
                        {
                            visitCmd.Parameters.AddWithValue("@VisitorId", representativeVisitorId);
                           // visitCmd.Parameters.AddWithValue("@RfidTagNumberId", groupRegistration.RepresentativeVisitor.RfidTagNumberId);
                            visitCmd.Parameters.AddWithValue("@VisitStatusId", visitStatusId); // Use the dynamically fetched VisitStatusId

                            await visitCmd.ExecuteScalarAsync();
                        }


                        // Insert the group registration
                        string insertGroupRegistrationQuery = @"
                    INSERT INTO [dbo].[GroupRegistration]
                        ([RepresentativeVisitorId], [GroupName], [TotalMembers], [TotalPaymentAmount], [DateRegistered])
                    OUTPUT INSERTED.GroupId
                    VALUES
                        (@RepresentativeVisitorId, @GroupName, @TotalMembers, @TotalPaymentAmount, @DateRegistered)";

                        int groupId;
                        using (SqlCommand cmd = new SqlCommand(insertGroupRegistrationQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@RepresentativeVisitorId", representativeVisitorId);
                            cmd.Parameters.AddWithValue("@GroupName", groupRegistration.GroupName);
                            cmd.Parameters.AddWithValue("@TotalMembers", groupRegistration.TotalMembers);
                            cmd.Parameters.AddWithValue("@TotalPaymentAmount", groupRegistration.TotalPaymentAmount);
                            cmd.Parameters.AddWithValue("@DateRegistered", groupRegistration.DateRegistered);

                            groupId = (int)await cmd.ExecuteScalarAsync();
                        }

                        // Query to retrieve the RfidTagNumberId based on RfidTagNumber
                        string selectRfidTagNumberIdQuery = "SELECT RfidTagNumberId FROM [dbo].[RFIDTag] WHERE RfidTagNumber = @RfidTagNumber;";

                        // Make sure insertGroupMemberQuery is defined here (before usage)
                        string insertGroupMemberQuery = @"
INSERT INTO [dbo].[GroupMember] 
    ([GroupId], [Age], [VisitorType], [IsPWD], [PaymentAmount], [RfidTagNumberId], [VisitId]) 
VALUES 
    (@GroupId, @Age, @VisitorType, @IsPWD, @PaymentAmount, @RfidTagNumberId, @VisitId);";

                        foreach (var member in groupRegistration.Members)
                        {
                            int visitId;
                            using (SqlCommand visitCmd = new SqlCommand(insertVisitQuery, connection, transaction))
                            {
                                visitCmd.Parameters.AddWithValue("@VisitorId", representativeVisitorId); // Ensure VisitorId is correct
                                visitCmd.Parameters.AddWithValue("@RfidTagNumberId", member.RfidTagNumberId); // Ensure correct parameter name

                                visitId = (int)await visitCmd.ExecuteScalarAsync();
                            }

                            using (SqlCommand memberCmd = new SqlCommand(insertGroupMemberQuery, connection, transaction))
                            {
                                memberCmd.Parameters.AddWithValue("@GroupId", groupId);
                                memberCmd.Parameters.AddWithValue("@Age", member.Age);
                                memberCmd.Parameters.AddWithValue("@VisitorType", member.VisitorType);
                                memberCmd.Parameters.AddWithValue("@IsPWD", member.IsPWD);
                                memberCmd.Parameters.AddWithValue("@PaymentAmount", member.PaymentAmount);
                                memberCmd.Parameters.AddWithValue("@RfidTagNumberId", member.RfidTagNumberId); // Correct parameter
                                memberCmd.Parameters.AddWithValue("@VisitId", visitId); // Correct VisitId

                                await memberCmd.ExecuteNonQueryAsync();
                            }
                        }




                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch
                    {
                        // Rollback the transaction in case of an error
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
                                    RfidTagNumberId = (int)reader["RfidTagNumber"]
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