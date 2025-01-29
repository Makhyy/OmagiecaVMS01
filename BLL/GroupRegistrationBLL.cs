using System;
using System.Collections.Generic;
using MODELS;
using DAL;
using System.Data;
using System.Threading.Tasks;

namespace BLL
{
    public class GroupRegistrationBLL
    {
        private GroupRegistrationDAL groupRegistrationDAL;
        private VisitorDAL _visitorDAL;
        private RFIDTagBLL rfidTagBLL;
        private VisitDAL _visitDAL;

        public GroupRegistrationBLL()
        {
            groupRegistrationDAL = new GroupRegistrationDAL(); // Initialize DAL
        }

        /*   // Add a new Group Registration
           public void AddGroupRegistration(GroupRegistration groupRegistration)
           {
               if (groupRegistration == null)
                   throw new ArgumentNullException(nameof(groupRegistration), "Group registration data cannot be null.");

               if (groupRegistration.Members == null || groupRegistration.Members.Count == 0)
                   throw new ArgumentException("A group must have at least one member.");

               if (groupRegistration.TotalPaymentAmount <= 0)
                   throw new ArgumentException("Total payment amount must be greater than zero.");

               if (groupRegistration.DateRegistered < new DateTime(1753, 1, 1))
                   throw new ArgumentOutOfRangeException(nameof(groupRegistration.DateRegistered), "Date registered must be a valid date and cannot be before January 1, 1753.");

               try
               {
                   groupRegistrationDAL.AddGroupRegistration(groupRegistration);
                   // Log success (optional)
               }
               catch (Exception ex)
               {
                   // Log the error details (add logging mechanism here)
                   throw new Exception("An error occurred while adding the group registration: " + ex.Message, ex);
               }
           }
        */
        public async Task AddGroupRegistrationAsync(GroupRegistration groupRegistration)
        {
          
            await groupRegistrationDAL.AddGroupRegistrationAsync(groupRegistration);
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

        // Update an Existing Group Registration
        public void UpdateGroupRegistration(GroupRegistration groupRegistration)
        {
            if (groupRegistration == null)
            {
                throw new ArgumentNullException(nameof(groupRegistration), "Group registration data cannot be null.");
            }

            if (groupRegistration.GroupId <= 0)
            {
                throw new ArgumentException("Invalid Group ID.");
            }

            try
            {
                groupRegistrationDAL.UpdateGroupRegistration(groupRegistration);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the group registration: " + ex.Message, ex);
            }
        }

        // Delete a Group Registration
        public void DeleteGroupRegistration(int groupId)
        {
            if (groupId <= 0)
            {
                throw new ArgumentException("Invalid Group ID.");
            }

            try
            {
                groupRegistrationDAL.DeleteGroupRegistration(groupId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the group registration: " + ex.Message, ex);
            }
        }

        // Get Group Registration by ID
        public GroupRegistration GetGroupById(int groupId)
        {
            if (groupId <= 0)
                throw new ArgumentException("Invalid Group ID.");

            var group = groupRegistrationDAL.GetGroupRegistrationById(groupId);
            if (group == null)
                throw new KeyNotFoundException($"No group registration found with ID {groupId}.");

            return group;
        }
        public DataTable GetAllGroupRegistrations()
        {
            return groupRegistrationDAL.GetAllGroupRegistrationsData();
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

    }
}
