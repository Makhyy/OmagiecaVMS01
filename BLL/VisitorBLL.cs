﻿using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BLL
{
    public class VisitorBLL
    {
        private VisitorDAL _visitorDAL;
        private RFIDTagBLL rfidTagBLL;
        private VisitDAL _visitDAL;
        public VisitorBLL()
        {
            _visitorDAL = new VisitorDAL();
            rfidTagBLL = new RFIDTagBLL();
            _visitDAL = new VisitDAL();
        }
        public DataTable GetVisitors()
        {
            try
            {
                return _visitorDAL.GetVisitors();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving visitors: " + ex.Message, ex);
            }
        }
        public DataTable GetVisitorsReport()
        {
            try
            {
                return _visitorDAL.GetVisitorsReport();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving visitors: " + ex.Message, ex);
}
        }
        public DataTable GetVisitorsWeeklyReport()
        {
            DateTime endDate = DateTime.Today; // Today's date
            DateTime startDate = endDate.AddDays(-6); // 7 days including today

            return visitorDAL.GetVisitorsWeeklyReport(startDate, endDate);
        }

        public DataTable GetVisitorsForToday()
        {
            return visitorDAL.GetVisitorsForSpecificDay(DateTime.Today);
        }
       
        public (DateTime Start, DateTime End) GetCurrentMonthRange()
        {
            DateTime today = DateTime.Today;
            DateTime monthStart = new DateTime(today.Year, today.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);

            return (monthStart, monthEnd);
        }
        public DataTable GetVisitorsForCurrentMonth()
        {
            var (monthStart, monthEnd) = GetCurrentMonthRange();
            return visitorDAL.GetVisitorsWeeklyReport(monthStart, monthEnd);
        }
        public DataTable GetVisitorsForDateRange(DateTime startDate, DateTime endDate)
        {
            return visitorDAL.GetVisitorsWeeklyReport(startDate, endDate);
        }
        public DataTable GetDailyRevenue(DateTime date)
        {
            return _visitorDAL.GetRevenueByDate(date);
        }
        public void RegisterVisitor(Visitor visitor, int rfidTagNumber)
        {
            try
            {
                if (visitor == null)
                    throw new ArgumentNullException(nameof(visitor), "Visitor object cannot be null.");
                // Assign the current logged-in user's ID
                visitor.UserAccountId = CurrentSession.UserAccountId;
                // Ensure UserAccountId is valid
                if (visitor.UserAccountId <= 0)
                    throw new ArgumentException("Invalid UserAccountId. Please ensure a user is logged in.");
                // Validate visitor data
                ValidateVisitor(visitor);

                // Check if RFID Tag is valid
                if (!_visitorDAL.IsRfidTagValid(rfidTagNumber))
                    throw new ArgumentException("Invalid RFID Tag Number. The tag does not exist or is not available.");

                // Add visitor and log visit in a single transactional method
                int visitorId = _visitorDAL.AddVisitorAndLogVisit(visitor);
                // Assign RFID Tag after successfully adding the visitor and logging the visit
                _visitorDAL.AssignRFIDTag(visitorId, rfidTagNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the visitor: " + ex.Message, ex);
            }
        }
       
        public void UpdateVisitor(Visitor visitor)
        {
            try
            {
                ValidateVisitor(visitor);  // Perform all necessary validations
                visitorDAL.UpdateVisitor(visitor);  // Pass to DAL for update
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update visitor: " + ex.Message, ex);
            }
        }
        /// Deletes a visitor from the database by their ID.
        public void DeleteVisitors(List<int> visitorIds)
        {
            if (visitorIds == null || !visitorIds.Any())
                throw new ArgumentException("No visitor IDs provided for deletion.");

            try
            {
                VisitorDAL visitorDAL = new VisitorDAL();
                visitorDAL.DeleteVisitors(visitorIds);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred in BLL while deleting visitors: " + ex.Message, ex);
            }
        }
        private void ValidateVisitor(Visitor visitor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(visitor.FirstName))
                {
                    throw new Exception("First Name is required.");
                }

                if (string.IsNullOrWhiteSpace(visitor.LastName))
                {
                    throw new Exception("Last Name is required.");
                }

                if (visitor.Age <= 0 || visitor.Age > 120)
                {
                    throw new Exception("Age must be between 1 and 120.");
                }

                if (visitor.PaymentAmount < 0)
                {
                    throw new Exception("Payment Amount cannot be negative.");
                }
          

                if (visitor.RfidTagNumberId < 0)
                {
                    throw new Exception("Invalid RFID Tag Number.");
                }

            }
            catch (Exception ex)
            {
                // Wrap and rethrow the validation exception with additional context
                throw new Exception("Visitor validation failed: " + ex.Message, ex);
            }
        }
        public List<RFIDTag> GetAvailableRFIDTagsToDisplay()
        {
            try
            {
                return _visitorDAL.GetAvailableRFIDTagsToDisplay(); // Call DAL method
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving RFID tags.", ex);
            }
        }
        private VisitorDAL visitorDAL = new VisitorDAL();
        public List<RFIDTag> GetAvailableRFIDTags()
        {
            try
            {
                return visitorDAL.GetAvailableRFIDTags();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred in BLL while retrieving available RFID tags: " + ex.Message, ex);
            }
        }
        public DataTable LoadRevenueVisitorData()
        {
            return _visitorDAL.LoadRevenueVisitorData();
        }
        public decimal GetTotalRevenue()
        {
            return visitorDAL.GetTotalRevenue();
        }
        public decimal GetTotalRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date must be after start date.");
            }
            return visitorDAL.GetTotalRevenueByDateRange(startDate, endDate);
        }
       
        public DataTable GetRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            return _visitorDAL.GetRevenueByDateRange(startDate, endDate);
        }

       
        public int GetDailyEnteredVisitorCount()
        {
            return _visitorDAL.GetEnteredVisitorCount();
        }
        public int GetDailyExitedVisitorCount()
        {
            return _visitorDAL.GetExitedVisitorCount();
        }
       
         public int GetRemainingVisitorCount()
        {
            return _visitorDAL.GetRemainingVisitorCount();
        }
        public DataTable GetDistinctVisitorTypes()
        {
            return visitorDAL.GetDistinctVisitorTypes();
        }
        public DataTable GetCombinedLocations()
        {
            return visitorDAL.GetCombinedLocations();
        }
    }
}