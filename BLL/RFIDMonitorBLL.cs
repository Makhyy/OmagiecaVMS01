using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RFIDMonitorBLL
    {
        private RFIDMonitorDAL rfidMonitorDAL;

        public RFIDMonitorBLL()
        {
            rfidMonitorDAL = new RFIDMonitorDAL();  // Initialize the DAL instance
        }
       
        public string GetVisitStatusByRfidTag(string rfidTagUID)
        {
            if (string.IsNullOrWhiteSpace(rfidTagUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidTagUID));

            return rfidMonitorDAL.GetCurrentVisitStatus(rfidTagUID);
        }


        // Generic method for updating visitor status
        private void UpdateVisitorStatus(string rfidTag, string newStatus, Func<string, bool> validationMethod, Action<string, string> updateMethod, string errorMessage)
        {
            try
            {
                // Validate the RFID tag
                if (validationMethod(rfidTag))
                {
                    // Update the visitor status
                    updateMethod(rfidTag, newStatus);
                }
                else
                {
                    // Handle invalid RFID tag or status
                    throw new InvalidOperationException(errorMessage);
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                // Example: logger.LogError(ex, "Failed to update visitor status.");
                throw new InvalidOperationException("Failed to update visitor status: " + ex.Message, ex);
            }
        }

        // Entrance BLL
        public void UpdateVisitStatus(string rfidTag, string newStatus)
        {
            UpdateVisitorStatus(
                rfidTag,
                newStatus,
                rfidMonitorDAL.IsValidRFIDVisitEntrance,
                rfidMonitorDAL.UpdateVisitStatus,
                "RFID tag is not valid or visitor is not in a 'Registered' status."
            );
        }

        // Exit BLL
        public void UpdateVisitStatusExit(string rfidTag, string newStatus)
        {
            UpdateVisitorStatus(
                rfidTag,
                newStatus,
                rfidMonitorDAL.IsValidRFIDVisitExit,
                rfidMonitorDAL.UpdateVisitStatusExit,
                "RFID tag is not valid or visitor is not in an 'Entered' status."
            );
        }
    }


}
