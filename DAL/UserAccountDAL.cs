using MODELS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL
{
    public class UserAccountDAL
    {
        private string connectionString = Properties.Settings.Default.ConnectionString;

        public object MessageBox { get; private set; }

        // Add a new User Account
        public void AddUserAccount(UserAccount userAccount)
        {
            try
            {
                // Validate the user account before proceeding
                ValidateUserAccount(userAccount);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (IsUsernameTaken(userAccount.Username))
                    {
                        throw new ArgumentException("The username is already in use.");
                    }

                    if (userAccount.DateRegistered == default)
                    {
                        userAccount.DateRegistered = DateTime.Now;
                    }

                    string query = @"
                INSERT INTO UserAccount (FirstName, LastName, Age, Gender, Address, Username, Password, 
                                         UserRole, UserStatus, SecurityQuestion, SecurityAnswer, DateRegistered)
                VALUES (@FirstName, @LastName, @Age, @Gender, @Address, @Username, @Password, 
                        @UserRole, @UserStatus, @SecurityQuestion, @SecurityAnswer, @DateRegistered)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", userAccount.FirstName);
                    command.Parameters.AddWithValue("@LastName", userAccount.LastName);
                    command.Parameters.AddWithValue("@Age", userAccount.Age);
                    command.Parameters.AddWithValue("@Gender", userAccount.Gender);
                    command.Parameters.AddWithValue("@Address", userAccount.Address);
                    command.Parameters.AddWithValue("@Username", userAccount.Username);
                    command.Parameters.AddWithValue("@Password", userAccount.Password);
                    command.Parameters.AddWithValue("@UserRole", userAccount.UserRole);
                    command.Parameters.AddWithValue("@UserStatus", userAccount.UserStatus);
                    command.Parameters.AddWithValue("@SecurityQuestion", userAccount.SecurityQuestion);
                    command.Parameters.AddWithValue("@SecurityAnswer", userAccount.SecurityAnswer);
                    command.Parameters.AddWithValue("@DateRegistered", userAccount.DateRegistered);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation
            {
                throw new ArgumentException("The username is already in use.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the user account: " + ex.Message, ex);
            }
        }

        private void ValidateUserAccount(UserAccount userAccount)
        {
            if (string.IsNullOrWhiteSpace(userAccount.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(userAccount.LastName))
                throw new ArgumentException("Last name is required.");

            if (userAccount.Age <= 0 || userAccount.Age > 120)
                throw new ArgumentException("Age must be a positive number and realistic.");

            if (string.IsNullOrWhiteSpace(userAccount.Gender) || (userAccount.Gender != "Male" && userAccount.Gender != "Female"))
                throw new ArgumentException("Gender must be specified as 'Male' or 'Female'.");

            if (string.IsNullOrWhiteSpace(userAccount.Username) || userAccount.Username.Length < 5)
                throw new ArgumentException("Username must be at least 5 characters long.");

            if (string.IsNullOrWhiteSpace(userAccount.Password) || userAccount.Password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            if (string.IsNullOrWhiteSpace(userAccount.UserRole) || (userAccount.UserRole != "Admin" && userAccount.UserRole != "Receptionist"))
                throw new ArgumentException("User role must be 'Admin' or 'Receptionist'.");

            if (string.IsNullOrWhiteSpace(userAccount.UserStatus) || (userAccount.UserStatus != "Active" && userAccount.UserStatus != "Inactive"))
                throw new ArgumentException("User status must be 'Active' or 'Inactive'.");
        }





        public List<UserAccount> GetAllUserAccounts()
        {
            List<UserAccount> users = new List<UserAccount>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT UserAccountId, FirstName, LastName, Age, Gender, Address, Username, Password, 
                       UserRole, UserStatus, SecurityQuestion, SecurityAnswer
                FROM UserAccount";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new UserAccount
                        {
                            UserAccountId = (int)reader["UserAccountId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = (int)reader["Age"],
                            Gender = reader["Gender"].ToString(),
                            Address = reader["Address"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            UserRole = reader["UserRole"].ToString(),
                            UserStatus = reader["UserStatus"].ToString(),
                            SecurityQuestion = reader["SecurityQuestion"].ToString(),
                            SecurityAnswer = reader["SecurityAnswer"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user accounts: " + ex.Message, ex);
            }

            return users;
        }



        // Update an Existing User Account
        public void UpdateUserAccount(UserAccount userAccount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE UserAccount 
                         SET FirstName = @FirstName, 
                             LastName = @LastName, 
                             Age = @Age, 
                             Gender = @Gender, 
                             Address = @Address, 
                             Username = @Username, 
                             Password = @Password, 
                             UserRole = @UserRole, 
                             UserStatus = @UserStatus, 
                             SecurityQuestion = @SecurityQuestion, 
                             SecurityAnswer = @SecurityAnswer
                         WHERE UserAccountId = @UserAccountId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserAccountId", userAccount.UserAccountId);
                command.Parameters.AddWithValue("@FirstName", userAccount.FirstName);
                command.Parameters.AddWithValue("@LastName", userAccount.LastName);
                command.Parameters.AddWithValue("@Age", userAccount.Age);
                command.Parameters.AddWithValue("@Gender", userAccount.Gender);
                command.Parameters.AddWithValue("@Address", userAccount.Address);
                command.Parameters.AddWithValue("@Username", userAccount.Username);
                command.Parameters.AddWithValue("@Password", userAccount.Password);
                command.Parameters.AddWithValue("@UserRole", userAccount.UserRole);
                command.Parameters.AddWithValue("@UserStatus", userAccount.UserStatus);
                command.Parameters.AddWithValue("@SecurityQuestion", userAccount.SecurityQuestion);
                command.Parameters.AddWithValue("@SecurityAnswer", userAccount.SecurityAnswer);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        // Delete a User Account
        public void DeleteUserAccount(int userAccountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM UserAccount WHERE UserAccountId = @UserAccountId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserAccountId", userAccountId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user account: " + ex.Message, ex);
            }
        }

        // Search User Accounts
        public List<UserAccount> SearchUserAccounts(string keyword)
        {
            List<UserAccount> userAccounts = new List<UserAccount>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT * FROM UserAccount
                        WHERE FirstName LIKE @Keyword
                           OR LastName LIKE @Keyword
                           OR Username LIKE @Keyword";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userAccounts.Add(new UserAccount
                        {
                            UserAccountId = (int)reader["UserAccountId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = (int)reader["Age"],
                            Gender = reader["Gender"].ToString(),
                            Address = reader["Address"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            UserRole = reader["UserRole"].ToString(),
                            UserStatus = reader["UserStatus"].ToString(),
                            DateRegistered = (DateTime)reader["DateRegistered"]
                        });
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching user accounts: " + ex.Message, ex);
            }

            return userAccounts;
        }
        public List<UserAccount> GetFilteredUserAccounts(string filterBy)
        {
            List<UserAccount> userAccount = new List<UserAccount>();
            string query = "";

            // Define filtering logic
            switch (filterBy.ToLower())
            {
                case "active":
                    query = "SELECT * FROM UserAccount WHERE UserStatus = 'Active'";
                    break;
                case "inactive":
                    query = "SELECT * FROM UserAccount WHERE UserStatus = 'Inactive'";
                    break;
                case "admin":
                    query = "SELECT * FROM UserAccount WHERE UserRole = 'Admin'";
                    break;
                case "receptionist":
                    query = "SELECT * FROM UserAccount WHERE UserRole = 'Receptionist'";
                    break;
                default: // "All"
                    query = "SELECT * FROM UserAccount"; // Show all
                    break;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userAccount.Add(new UserAccount
                        {
                            UserAccountId = (int)reader["UserAccountId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = (int)reader["Age"],
                            Gender = reader["Gender"].ToString(),
                            Address = reader["Address"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            UserRole = reader["UserRole"].ToString(),
                            UserStatus = reader["UserStatus"].ToString(),
                        });
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while filtering user accounts: " + ex.Message, ex);
            }

            return userAccount;
        }
        public UserAccount AuthenticateUser(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT UserAccountId, FirstName, LastName, UserRole, UserStatus 
                FROM UserAccount 
                WHERE Username = @Username AND Password = @Password";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new UserAccount
                        {
                            UserAccountId = (int)reader["UserAccountId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            UserRole = reader["UserRole"].ToString(),
                            UserStatus = reader["UserStatus"].ToString(),
                        };
                    }
                    else
                    {
                        return null; // No matching user found
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while authenticating the user: " + ex.Message, ex);
            }
        }
        public bool ValidateSecurityQuestion(string username, string securityQuestion, string securityAnswer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM UserAccount 
                         WHERE Username = @Username 
                         AND SecurityQuestion = @SecurityQuestion 
                         AND SecurityAnswer = @SecurityAnswer";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@SecurityQuestion", securityQuestion);
                command.Parameters.AddWithValue("@SecurityAnswer", securityAnswer);

                connection.Open();
                int result = (int)command.ExecuteScalar();
                return result > 0;
            }
        }

        public void UpdatePassword(string username, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE UserAccount SET Password = @NewPassword WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool IsUsernameTaken(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM UserAccount WHERE LOWER(Username) = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username.ToLower()); // Ensure case-insensitivity

                connection.Open();
                int result = (int)command.ExecuteScalar(); // Retrieve count
                return result > 0; // Return true if username exists
            }
        }

        public UserAccount GetUserByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT UserAccountId, FirstName, LastName, Username, UserRole, UserStatus, 
                   SecurityQuestion, SecurityAnswer, DateRegistered 
            FROM UserAccount 
            WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new UserAccount
                    {
                        UserAccountId = (int)reader["UserAccountId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Username = reader["Username"].ToString(),
                        UserRole = reader["UserRole"].ToString(),
                        UserStatus = reader["UserStatus"].ToString(),
                        SecurityQuestion = reader["SecurityQuestion"].ToString(),
                        SecurityAnswer = reader["SecurityAnswer"].ToString(),
                        DateRegistered = (DateTime)reader["DateRegistered"]
                    };
                }

                return null;
            }
        }






    }
}
