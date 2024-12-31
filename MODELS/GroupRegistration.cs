using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class GroupRegistration

    {
        public int GroupId { get; set; } // Primary Key
        public int RepresentativeVisitorId { get; set; } // Links to VisitorId in Visitors
        public string GroupName { get; set; } // Optional group name
        public int TotalMembers { get; set; } // Total members in the group
        public decimal TotalPaymentAmount { get; set; } // Total payment for the group
        public DateTime DateRegistered { get; set; } = DateTime.Now; // Registration date

        // Navigation Properties
        public Visitor RepresentativeVisitor { get; set; } // Links to Visitor model
        public List<GroupMember> Members { get; set; } // Group members
    }

}
