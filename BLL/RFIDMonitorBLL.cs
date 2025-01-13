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

        public void UpdateVisitorStatus(string rfidTag, string newStatus)
        {
            try
            {
                // First, validate the RFID tag is associated with a registered visitor
                if (rfidMonitorDAL.IsValidRFID(rfidTag))
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
    }
}
