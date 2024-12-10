using MODELS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class RFIDTag
    {
        public int RfidTagNumberId { get; set; } // Primary Key
        public string RfidTagUID { get; set; }   // Unique Identifier
        public int RfidTagNumber { get; set; }  // RFID Number
        public RFIDTagStatus RfidStatus { get; set; }
        public int? VisitorId { get; set; }     // Nullable Foreign Key to Visitors table
    }
}
