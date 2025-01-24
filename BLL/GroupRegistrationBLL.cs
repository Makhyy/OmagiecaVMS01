using System;
using System.Collections.Generic;
using MODELS;
using DAL;
using System.Data;

namespace BLL
{
    public class GroupRegistrationBLL
    {
        private readonly GroupRegistrationDAL groupRegistrationDAL;

        public GroupRegistrationBLL()
        {
            groupRegistrationDAL = new GroupRegistrationDAL(); // Initialize DAL
        }

        // Add a new Group Registration
        public void AddGroupRegistration(GroupRegistration groupRegistration)
        {
            if (groupRegistration == null)
            {
                throw new ArgumentNullException(nameof(groupRegistration), "Group registration data cannot be null.");
            }

            if (groupRegistration.Members == null || groupRegistration.Members.Count == 0)
            {
                throw new ArgumentException("A group must have at least one member.");
            }

            if (groupRegistration.TotalPaymentAmount <= 0)
            {
                throw new ArgumentException("Total payment amount must be greater than zero.");
            }
            if (groupRegistration.DateRegistered < new DateTime(1753, 1, 1))
            {
                throw new ArgumentOutOfRangeException(nameof(groupRegistration.DateRegistered), "DateRegistered must be after 1/1/1753.");
            }

            try
            {
                groupRegistrationDAL.AddGroupRegistration(groupRegistration);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the group registration: " + ex.Message, ex);
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
            {
                throw new ArgumentException("Invalid Group ID.");
            }

            try
            {
                return groupRegistrationDAL.GetGroupRegistrationById(groupId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the group registration: " + ex.Message, ex);
            }
        }

        // Get All Group Registrations
        public List<GroupRegistration> GetAllGroups()
        {
            try
            {
                return groupRegistrationDAL.GetAllGroupRegistrations();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving group registrations: " + ex.Message, ex);
            }
        }
        public DataTable GetAllGroupRegistrations()
        {
            return groupRegistrationDAL.GetAllGroupRegistrationsData();
        }
    }
}
