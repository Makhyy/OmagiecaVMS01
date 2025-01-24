using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class GroupMemberBLL
    {
        private GroupMemberDAL groupMemberDAL;

        // Constructor
        public GroupMemberBLL()
        {
            groupMemberDAL = new GroupMemberDAL(); // Initialize the DAL object here
        }

        public DataTable GetAllGroupMembers()
        {
            return groupMemberDAL.GetAllGroupMembers(); // Now this call should work as expected
        }
    }
}
