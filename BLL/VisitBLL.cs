using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VisitBLL
    {
        private readonly VisitDAL _visitDAL;

        public VisitBLL()
        {
            _visitDAL = new VisitDAL();
        }

        public bool CreateVisit(Visit visit)
        {
            if (visit == null)
                throw new ArgumentNullException(nameof(visit), "Visit data cannot be null.");
            return _visitDAL.InsertVisit(visit);
        }

        public Visit GetVisitByVisitorId(int visitorId)
        {
            if (visitorId <= 0)
                throw new ArgumentException("VisitorId must be valid.");
            return _visitDAL.GetVisitByVisitorId(visitorId);
        }

        public bool UpdateVisitStatus(int visitId, string status, DateTime timestamp)
        {
            if (visitId <= 0)
                throw new ArgumentException("VisitId must be valid.");
            if (string.IsNullOrEmpty(status))
                throw new ArgumentException("Status cannot be null or empty.");
            return _visitDAL.UpdateVisitStatus(visitId, status, timestamp);
        }

        public List<Visit> GetActiveVisits()
        {
            return _visitDAL.GetVisitsByStatus("Onsite");
        }

        public List<Visit> GetAllVisits()
        {
            return _visitDAL.GetAllVisits();
        }
    }

}