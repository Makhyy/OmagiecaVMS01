using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string VisitorType{ get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int VisitorId { get; set; }
    }
}
