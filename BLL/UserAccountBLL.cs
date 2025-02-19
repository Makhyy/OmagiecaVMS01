using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserAccountBLL
    {
        private readonly UserAccountDAL userAccountDAL;

        public UserAccountBLL()
        {
            userAccountDAL = new UserAccountDAL(); // Initialize the DAL
        }

        // Add a New User Account
        public void AddUserAccount(UserAccount userAccount)
        {
            if (userAccount == null)
                throw new ArgumentNullException(nameof(userAccount), "User account cannot be null.");

            ValidateUserAccount(userAccount);

            try
            {
                if (IsUsernameTaken(userAccount.Username))
                    throw new BusinessLogicException("The username is already taken.");

                userAccountDAL.AddUserAccount(userAccount);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Error while adding user account.", ex);
            }
        }



        public class BusinessLogicException : Exception
        {
            public BusinessLogicException(string message) : base(message) { }
            public BusinessLogicException(string message, Exception innerException) : base(message, innerException) { }
        }

        // Get All User Accounts
        public List<UserAccount> GetAllUserAccounts()
        {
            UserAccountDAL dal = new UserAccountDAL();
            return dal.GetAllUserAccounts();
        }

        // Update an Existing User Account
        public void UpdateUserAccount(UserAccount userAccount)
        {
            if (userAccount == null)
            {
                throw new ArgumentNullException(nameof(userAccount), "User account cannot be null.");                                                                                                                               //error
            }

            if (userAccount.UserAccountId <= 0)
            {
                throw new Exception("Invalid UserAccountId.");
            }

            // Use centralized validation
            ValidateUserAccount(userAccount);

            try
            {
                userAccountDAL.UpdateUserAccount(userAccount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating user account: {ex.Message}", ex);
            }
        }


        // Delete a User Account
        public void DeleteUserAccount(int userAccountId)
        {
            if (userAccountId <= 0)
            {
                throw new Exception("Invalid UserAccountId.");
            }

            try
            {
                userAccountDAL.DeleteUserAccount(userAccountId); // Call DAL method
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting user account: {ex.Message}", ex);
            }
        }

        // Search User Accounts
        public List<UserAccount> SearchUserAccounts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                throw new Exception("Search keyword cannot be empty.");
            }

            try
            {
                return userAccountDAL.SearchUserAccounts(keyword); // Call DAL method
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while searching user accounts: {ex.Message}", ex);
            }
        }

        // Private Helper Method to Validate UserAccount Fields
        private void ValidateUserAccount(UserAccount userAccount)
        {
            if (string.IsNullOrWhiteSpace(userAccount.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(userAccount.LastName))
                throw new ArgumentException("Last name is required.");

            if (userAccount.Age <= 0 || userAccount.Age > 120)
                throw new ArgumentException("Age must be a positive number and realistic.");

            if (string.IsNullOrWhiteSpace(userAccount.Username) || userAccount.Username.Length < 5)
                throw new ArgumentException("Username must be at least 5 characters long.");

            if (string.IsNullOrWhiteSpace(userAccount.Password) || userAccount.Password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            if (string.IsNullOrWhiteSpace(userAccount.UserRole) ||
                !(userAccount.UserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                  userAccount.UserRole.Equals("Receptionist", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("User role must be either 'Admin' or 'Receptionist'.");
            }

            if (string.IsNullOrWhiteSpace(userAccount.UserStatus) ||
                !(userAccount.UserStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) ||
                  userAccount.UserStatus.Equals("Inactive", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("User status must be either 'Active' or 'Inactive'.");
            }
        }


        public List<UserAccount> GetFilteredUserAccounts(string filterBy)
        {
            try
            {
                return userAccountDAL.GetFilteredUserAccounts(filterBy); // Delegate to DAL
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while filtering user accounts: {ex.Message}", ex);
            }
        }
        public UserAccount AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            UserAccountDAL userAccountDAL = new UserAccountDAL();
            UserAccount userAccount = userAccountDAL.AuthenticateUser(username, password);

            if (userAccount == null)
            {
                throw new Exception("Invalid username or password.");
            }

            if (userAccount.UserStatus != "Active")
            {
                throw new Exception("Your account is not active. Please contact the administrator.");
            }

            // Set the logged-in user globally
            CurrentSession.UserAccountId = userAccount.UserAccountId; // Add this line to set the UserAccountId
            CurrentSession.Username = userAccount.Username;          // Ensure Username is also set

            return userAccount;
        }
        public bool ValidateSecurityQuestion(string username, string securityQuestion, string securityAnswer)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(securityQuestion) || string.IsNullOrWhiteSpace(securityAnswer))
            {
                throw new ArgumentException("All fields are required for validation.");
            }

            return userAccountDAL.ValidateSecurityQuestion(username, securityQuestion, securityAnswer);
        }


        public void UpdatePassword(string username, string newPassword)
        {
            UserAccountDAL userAccountDAL = new UserAccountDAL();
            userAccountDAL.UpdatePassword(username, newPassword);
        }
        public bool IsUsernameTaken(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty.");
            }

            return userAccountDAL.IsUsernameTaken(username);
        }

        public UserAccount GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.");

            return userAccountDAL.GetUserByUsername(username);
        }
        public UserAccount GetUserAccount(int userId)
        {
            // Call the DAL method to get user data
            return userAccountDAL.GetUserAccount(userId);
        }

        public bool UpdateUserAccountFromEdit(int userAccountId, string firstName, string lastName, int age, string gender, string address, string userRole)
        {
            UserAccountDAL userDal = new UserAccountDAL();
            return userDal.UpdateUserAccountFromEdit(userAccountId, firstName, lastName, age, gender, address, userRole);
        }
    }
    }
