using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VisitBLL
    {
        private VisitorDAL _visitorDAL;
        private VisitDAL _visitDAL;

        public VisitBLL()
        {
            _visitorDAL = new VisitorDAL();
        }

        public DataTable GetVisitorInformation()
        {
           VisitDAL _visitDAL = new VisitDAL(); // Assuming DAL class name
            return _visitDAL.GetVisitorDetails();
        }


    }

}