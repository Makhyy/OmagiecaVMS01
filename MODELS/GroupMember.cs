using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class GroupMember
    {

        public int GroupMemberId { get; set; }  // Primary key
        public int GroupId { get; set; }  // Foreign key to GroupRegistration
        public int Age { get; set; }
        public string VisitorType { get; set; }
        public bool IsPWD { get; set; }
        public decimal PaymentAmount { get; set; }

        // This property should match your database schema
        public int RfidTagNumberId { get; set; }  // Foreign key to RFIDTag
        public int VisitId { get; set; }  // Foreign key to Visit

    }

}
