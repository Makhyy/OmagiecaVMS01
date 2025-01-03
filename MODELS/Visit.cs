using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Visit
    {
        public int VisitId { get; set; }             // Primary key
        public int VisitorId { get; set; }          // Foreign key to Visitors table
        public int UserAccountId { get; set; }      // User (Admin/Receptionist) managing the visit
        public int RfidTagNumberId { get; set; }    // RFID Tag assigned for this visit
        public int PaymentId { get; set; }          // Payment details for the visit
        public string VisitStatus { get; set; }     // Status: Registered, Onsite, Exited
        public DateTime? EntryTime { get; set; }    // Time visitor entered the premises
        public DateTime? ExitTime { get; set; }     // Time visitor exited the premises
    }
}
