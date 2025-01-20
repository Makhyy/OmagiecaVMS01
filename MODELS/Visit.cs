using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Visit
    {
        
        
            public int VisitId { get; set; } // Primary key
            public int VisitorId { get; set; }
            public int RfidTagNumberId { get; set; }
            public DateTime? EntryTime { get; set; }
            public DateTime? ExitTime { get; set; } // Nullable for initial creation
            public int VisitStatusId { get; set; }
            
            
        

    }
}
