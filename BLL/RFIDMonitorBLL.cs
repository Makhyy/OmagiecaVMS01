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
        public string GetCurrentVisitorStatus(string rfidTagUID)
        {
            return rfidMonitorDAL.GetCurrentVisitorStatus(rfidTagUID);
        }
        public string GetVisitStatusByRfidTag(string rfidTagUID)
        {
            if (string.IsNullOrWhiteSpace(rfidTagUID))
                throw new ArgumentException("RFID Tag UID cannot be null or empty.", nameof(rfidTagUID));

            return rfidMonitorDAL.GetCurrentVisitStatus(rfidTagUID);
        }

        public void UpdateVisitorStatus(string rfidTag, string newStatus)
        {
            try
            {
                // First, validate the RFID tag is associated with a registered visitor
                if (rfidMonitorDAL.IsValidRFIDVisit(rfidTag))
                {
                    // Update the visitor status if the RFID is valid and the visitor is registered
                    rfidMonitorDAL.UpdateVisitorStatus(rfidTag, newStatus);
                }
                else
                {
                    // Optionally handle logic for invalid RFID tags, e.g., logging or alerting
                    throw new InvalidOperationException("RFID tag is not valid or visitor is not in a 'Registered' status.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as necessary
                throw new Exception("Failed to update visitor status: " + ex.Message, ex);
            }
        }
        //using
        public void UpdateVisitStatus(string rfidTag, string newStatus)
        {
            try
            {
                // First, validate the RFID tag is associated with a registered visitor
                if (rfidMonitorDAL.IsValidRFIDVisit(rfidTag))
                {
                    // Update the visitor status if the RFID is valid and the visitor is registered
                    rfidMonitorDAL.UpdateVisitStatus(rfidTag, newStatus);
                }
                else
                {
                    // Optionally handle logic for invalid RFID tags, e.g., logging or alerting
                    throw new InvalidOperationException("RFID tag is not valid or visitor is not in a 'Registered' status.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as necessary
                throw new Exception("Failed to update visitor status: " + ex.Message, ex);
            }
        }
        //using2
        public void UpdateVisitStatusExit(string rfidTag, string newStatus)
        {
            try
            {
                // First, validate the RFID tag is associated with a registered visitor
                if (rfidMonitorDAL.IsValidRFIDVisitExit(rfidTag))
                {
                    // Update the visitor status if the RFID is valid and the visitor is registered
                    rfidMonitorDAL.UpdateVisitStatusExit(rfidTag, newStatus);
                }
                else
                {
                    // Optionally handle logic for invalid RFID tags, e.g., logging or alerting
                    throw new InvalidOperationException("RFID tag is not valid or visitor is not in a 'Registered' status.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as necessary
                throw new Exception("Failed to update visitor status: " + ex.Message, ex);
            }
        }
        public void UpdateVisitorStatusExit(string rfidTag, string newStatus)
        {
            try
            {
                // First, validate the RFID tag is associated with a registered visitor
                if (rfidMonitorDAL.IsValidRFIDVisitExit(rfidTag))
                {
                    // Update the visitor status if the RFID is valid and the visitor is registered
                    rfidMonitorDAL.UpdateVisitorStatusExit(rfidTag, newStatus);
                }
                else
                {
                    // Optionally handle logic for invalid RFID tags, e.g., logging or alerting
                    throw new InvalidOperationException("RFID tag is not valid or visitor is not in a 'Entered' status.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as necessary
                throw new Exception("Failed to update visitor status: " + ex.Message, ex);
            }
        }

    }
}