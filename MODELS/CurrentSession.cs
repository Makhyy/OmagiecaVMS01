using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    
    public static class CurrentSession
    {
        
        public static int UserAccountId { get; set; }
        public static string Username { get; set; }
        public static string UserRole{ get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }

        // Add a CurrentUser property if needed
        public static UserAccounts CurrentUser => new UserAccounts
        {
            UserAccountId = UserAccountId,
            Username = Username,
            UserRole = UserRole,
            FirstName = FirstName,
            LastName = LastName,    

            // Add other properties if required
        };

        public class UserAccounts
        {
            public int UserAccountId { get; set; }
            public string Username { get; set; }
            public string UserRole { get; set; }
            public  string FirstName { get; set; }
            public string LastName { get; set; }

            // Add more fields as needed
        }
        public static void Logout()
        {
            UserAccountId = 0;
            Username = null;
            // Reset other properties if there are any
        }
    }

}
