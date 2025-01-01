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

        // Add Payment with PWDDiscount
        public bool AddPayment(string visitorType, decimal paymentAmount, decimal pwdDiscount)
        {
            if (paymentAmount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            if (pwdDiscount < 0)
            {
                throw new ArgumentException("PWD discount cannot be negative.");
            }

            int result = paymentDAL.InsertPayment(visitorType, paymentAmount, pwdDiscount);
            return result > 0;
        }

        // Get all Payments
        public DataTable GetAllPayments()
        {
            return paymentDAL.GetAllPayments();
        }

        public DataTable GetPaymentByVisitorType(string visitorType)
        {
            if (string.IsNullOrWhiteSpace(visitorType))
            {
                throw new ArgumentException("Visitor Type cannot be empty.");
            }

            return paymentDAL.GetPaymentByVisitorType(visitorType);
        }


        // Delete Payment by ID
        public bool DeletePayment(int paymentId)
        {
            if (paymentId <= 0)
            {
                throw new ArgumentException("Invalid Payment ID.");
            }

            return paymentDAL.DeletePayment(paymentId);
        }

        // Update Payment with PWDDiscount
        public bool UpdatePayment(int paymentId, string visitorType, decimal paymentAmount, decimal pwdDiscount)
        {
            if (paymentId <= 0)
            {
                throw new ArgumentException("Invalid Payment ID.");
            }

            if (string.IsNullOrWhiteSpace(visitorType))
            {
                throw new ArgumentException("Visitor type cannot be empty.");
            }

            if (paymentAmount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            if (pwdDiscount < 0)
            {
                throw new ArgumentException("PWD discount cannot be negative.");
            }

            return paymentDAL.UpdatePayment(paymentId, visitorType, paymentAmount, pwdDiscount);
        }
    }
}
