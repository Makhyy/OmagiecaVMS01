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

        // Add a CurrentUser property if needed
        public static UserAccount CurrentUser => new UserAccount
        {
            UserAccountId = UserAccountId,
            Username = Username
            // Add other properties if required
        };

        public class UserAccount
        {
            public int UserAccountId { get; set; }
            public string Username { get; set; }
            // Add more fields as needed
        }
    }

}
