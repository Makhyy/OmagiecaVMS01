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

        public VisitBLL()
        {
            _visitorDAL = new VisitorDAL();
        }

       

    }

}