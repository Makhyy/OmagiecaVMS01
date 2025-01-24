using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class GroupMember
    {
        public int GroupMemberId { get; set; } // Primary Key
        public int GroupId { get; set; } // Foreign Key to GroupRegistration
        public int Age { get; set; }
        public string VisitorType { get; set; } 
        public bool IsPWD { get; set; }
        public decimal PaymentAmount { get; set; } 
        public int RfidTagNumberId { get; set; } 
        public int VisitId { get; set; }    
    }

}
