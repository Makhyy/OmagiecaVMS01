using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BLL
{

    public class GroupMemberBLL
    {
        private GroupMemberDAL groupMemberDAL;

        
        public GroupMemberBLL()
        {
            groupMemberDAL = new GroupMemberDAL(); 
        }
        public DataTable GetAllGroupMembers()
        {
            return groupMemberDAL.GetAllGroupMembers(); 
        }
    }
}
