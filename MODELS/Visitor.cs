using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
   public class Visitor
    {
        public int VisitorId { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string VisitorType { get; set; }
        public bool IsPWD { get; set; }
        public string Gender { get; set; }
        public string CityMunicipality { get; set; }
        public string ForeignCountry { get; set; }
        public decimal PaymentAmount { get; set; }
        public int? RfidTagNumberId { get; set; }
        public DateTime DateRegistered { get; set; }
        public int? GroupId { get; set; }

       
        public int? UserAccountId { get; set; }

        public GroupRegistration Group { get; set; }
        public RFIDTag RFIDTag { get; set; }
    }
}
