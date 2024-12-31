using DAL;
using MODELS;
using MODELS.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RFIDTagBLL
    {
        public void AddRFIDTag(RFIDTag rfidTag)
        {
            if (rfidTag == null)
                throw new ArgumentNullException(nameof(rfidTag), "RFIDTag cannot be null.");

            RFIDTagDAL dal = new RFIDTagDAL();
            dal.AddRFIDTag(rfidTag);
        }

        public DataTable GetAllRFIDTags()
        {
            RFIDTagDAL dal = new RFIDTagDAL();
            return dal.GetAllRFIDTags();
        }

        public void UpdateRFIDTag(RFIDTag rfidTag)
        {
            if (rfidTag == null)
                throw new ArgumentNullException(nameof(rfidTag), "RFIDTag cannot be null.");

            RFIDTagDAL dal = new RFIDTagDAL();
            dal.UpdateRFIDTag(rfidTag);
        }
        public void DeleteRFIDTag(int rfidTagNumberId)
        {
            if (rfidTagNumberId <= 0)
                throw new ArgumentException("Invalid RFID tag ID.", nameof(rfidTagNumberId));

            RFIDTagDAL dal = new RFIDTagDAL();
            dal.DeleteRFIDTag(rfidTagNumberId);
        }


        public void AssignRFIDTag(int visitorId, int rfidTagNumber)
        {
            RFIDTagDAL rfidDal = new RFIDTagDAL();
            RFIDTag rfidTag = rfidDal.GetRFIDTagByNumber(rfidTagNumber);

            if (rfidTag.RfidStatus != RFIDTagStatus.Available)
            {
                throw new InvalidOperationException("This RFID tag is not available for assignment.");
            }

            rfidTag.RfidStatus = RFIDTagStatus.InUse;
            

            rfidDal.UpdateRFIDTag(rfidTag);
        }
        public bool IsRFIDTagNumberExists(int rfidTagNumber)
        {
            RFIDTagDAL dal = new RFIDTagDAL();
            return dal.IsRFIDTagNumberExists(rfidTagNumber);
        }
        public List<RFIDTag> SearchRFIDTags(string keyword)
        {
            RFIDTagDAL rfidDal = new RFIDTagDAL();
            return rfidDal.SearchRFIDTags(keyword);
        }


    }

}
