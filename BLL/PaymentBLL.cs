using System;
using System.Collections.Generic;
using MODELS;
using DAL;
using System.Data;

namespace BLL
{
    public class PaymentBLL
    {
        private PaymentDAL paymentDAL;

        public PaymentBLL() 
        {
           paymentDAL = new PaymentDAL();
        }

        public bool AddPayment(string visitorType, decimal paymentAmount)
        {
            if (paymentAmount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            int result = paymentDAL.InsertPayment(visitorType, paymentAmount);
            return result > 0;
        }
      
        public DataTable GetAllPayments()
        {
            return paymentDAL.GetAllPayments();
        }
        public bool DeletePayment(int paymentId)
        {
            if (paymentId <= 0)
            {
                throw new ArgumentException("Invalid Payment ID.");
            }

            return paymentDAL.DeletePayment(paymentId);
        }
        public bool UpdatePayment(int paymentId, string visitorType, decimal paymentAmount)
        {
            if (paymentId <= 0)
            {
                throw new ArgumentException("Invalid Payment ID.");
            }

            if (string.IsNullOrWhiteSpace(visitorType))
            {
                throw new ArgumentException("Payment amount name cannot be empty.");
            }

            if (paymentAmount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            return paymentDAL.UpdatePayment(paymentId, visitorType, paymentAmount);
        }



    }
}
