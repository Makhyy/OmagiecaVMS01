using DAL;
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

        public (DateTime Start, DateTime End) GetLastCompleteWeekRange()
        {
            DateTime today = DateTime.Today;
            int daysToSubtract = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
            DateTime weekStart = today.AddDays(-daysToSubtract - 7);
            DateTime weekEnd = weekStart.AddDays(6);

            return (weekStart, weekEnd);
        }
        public DataTable GetVisitorsForLastCompleteWeek()
        {
            var (weekStart, weekEnd) = GetLastCompleteWeekRange();
            return visitorDAL.GetVisitorsWeeklyReport(weekStart, weekEnd);
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
        public int GetVisitorsDaily()
        {
            return visitorDAL.GetVisitorDaily();
        }


        public DataTable GetVisitorsForSpecificDay(DateTime specificDay)
        {
            // You can add any business logic here if needed, for example:
            if (specificDay.Date > DateTime.Now.Date)
            {
                throw new ArgumentException("Cannot retrieve visitors for a future date.");
            }

            return visitorDAL.GetVisitorsForSpecificDay(specificDay);
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

                // Set default VisitorStatus if not provided
                if (string.IsNullOrEmpty(visitor.VisitorStatus))
                    visitor.VisitorStatus = "Registered";

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







        public int FetchDefaultStatusId()
        {
            try
            {
                return _visitDAL.GetDefaultStatusId();
            }
            catch (InvalidOperationException ex)
            {
                // Log exception or handle it as per your error handling policy
                throw new Exception("Failed to retrieve the default status ID.", ex);
            }
        }

        




        public void UpdateVisitor(Visitor visitor)
        {
            try
            {
                // Validate data before updating
                ValidateVisitor(visitor);

                // Pass the visitor to DAL for update
                _visitorDAL.UpdateVisitor(visitor);
            }
            catch (Exception ex)
            {
                // Wrap and rethrow the exception with additional context
                throw new Exception("An error occurred while updating the visitor.", ex);
            }
        }

        /// <summary>
        /// Deletes a visitor from the database by their ID.
        /// </summary>
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




        public List<Visitor> SearchVisitors(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    throw new ArgumentException("Search keyword cannot be empty.");

                return _visitorDAL.SearchVisitors(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching for visitors.", ex);
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
                if (!new[] { "Registered", "Entered", "Exited" }.Contains(visitor.VisitorStatus))
                    throw new ArgumentException("Invalid VisitorStatus value.");


            }
            catch (Exception ex)
            {
                // Wrap and rethrow the validation exception with additional context
                throw new Exception("Visitor validation failed: " + ex.Message, ex);
            }
        }
        public List<VisitorType> GetVisitorTypes()
        {
            try
            {
                return _visitorDAL.GetVisitorTypes(); // Ensure this exists in DAL
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving visitor types.", ex);
            }
        }
        public List<RFIDTag> GetRFIDTags()
        {
            try
            {
                return _visitorDAL.GetRFIDTags(); // Call DAL method
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving RFID tags.", ex);
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

        public void AddPayment(int visitorId, decimal paymentAmount)
        {
            try
            {
                if (visitorId <= 0)
                {
                    throw new ArgumentException("Invalid VisitorId.");
                }

                if (paymentAmount <= 0)
                {
                    throw new ArgumentException("Payment amount must be greater than zero.");
                }

                // Call DAL to add payment
                _visitorDAL.AddPayment(visitorId, paymentAmount);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding payment: " + ex.Message, ex);
            }
        }
        public void AssignRFIDTag(int visitorId, int rfidTagNumber)
        {
            try
            {
                if (visitorId <= 0)
                {
                    throw new ArgumentException("Invalid VisitorId.");
                }

                if (rfidTagNumber <= 0)
                {
                    throw new ArgumentException("Invalid RFID Tag Number.");
                }

                // Call DAL to assign RFID tag
                _visitorDAL.AssignRFIDTag(visitorId, rfidTagNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while assigning RFID tag: " + ex.Message, ex);
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

        public decimal GetTotalWeeklyRevenue()
        {
            return visitorDAL.GetTotalWeeklyRevenue();
        }
        public decimal GetTotalMonthlyRevenue()
        {
            return visitorDAL.GetTotalMonthlyRevenue();
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
        public int FetchTodaysVisitorCount()
        {
            return visitorDAL.GetTodaysVisitorCount(); 
        }

        public int GetVisitStatusIdByName(string statusName)
        {
            try
            {
                // Call DAL method to get status ID
                return visitorDAL.GetVisitStatusIdByName(statusName);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve status ID: " + ex.Message);
            }
        }
       

        public DataTable GetRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            return _visitorDAL.GetRevenueByDateRange(startDate, endDate);
        }

        public DataTable GetRevenueByDateRangeM(DateTime startDate, DateTime endDate)
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
    }
}