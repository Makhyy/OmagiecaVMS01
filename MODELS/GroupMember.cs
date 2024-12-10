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
        public string VisitorTypeId { get; set; } // Visitor type (e.g., child, adult)
        public decimal PaymentAmount { get; set; } // Payment amount for this member
    }

}
