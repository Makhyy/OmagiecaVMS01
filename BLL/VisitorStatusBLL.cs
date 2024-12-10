using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VisitorStatusBLL
    {
        private VisitorStatusDAL _visitorStatusDAL;
        public VisitorStatusBLL()
        {
            _visitorStatusDAL = new VisitorStatusDAL();
        }
        public DataTable GetVisitors()
        {
            try
            {
                return _visitorStatusDAL.GetVisitors();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving visitors: " + ex.Message, ex);
            }
        }
    }
}
