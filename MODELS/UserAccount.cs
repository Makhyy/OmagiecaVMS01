using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; } // Nullable to handle optional input
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; } // Admin, Receptionist
        public string UserStatus { get; set; } // Active, Inactive
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public DateTime? DateRegistered { get; set; } // Nullable to handle optional input

        public UserAccount()
        {
            DateRegistered = DateTime.Now; // Default value
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("First Name is required.");

            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("Last Name is required.");

            if (Age.HasValue && (Age <= 0 || Age > 120))
                throw new ArgumentException("Age must be a positive number and realistic.");

            if (string.IsNullOrWhiteSpace(Username))
                throw new ArgumentException("Username is required.");

            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            if (string.IsNullOrWhiteSpace(UserRole) ||
                !(UserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                  UserRole.Equals("Receptionist", StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("User role must be either 'Admin' or 'Receptionist'.");

            if (string.IsNullOrWhiteSpace(UserStatus) ||
                !(UserStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) ||
                  UserStatus.Equals("Inactive", StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("User status must be either 'Active' or 'Inactive'.");
        }

        public override string ToString()
        {
            return $"UserAccountId: {UserAccountId}, Username: {Username}, Role: {UserRole}, Status: {UserStatus}, Registered: {DateRegistered}";
        }
    }

}
