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

        // Constructor
        public GroupMemberBLL()
        {
            groupMemberDAL = new GroupMemberDAL(); // Initialize the DAL object here
        }

        public DataTable GetAllGroupMembers()
        {
            return groupMemberDAL.GetAllGroupMembers(); // Now this call should work as expected
        }
        public Dictionary<int, Color> CreateColorMap()
        {
            DataTable dataTable = groupMemberDAL.GetGroupData();
            Dictionary<int, Color> colorMap = new Dictionary<int, Color>();

            // Handle each row in the dataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Handle DBNull for GroupId, assign -1 if null
                int groupId = row["GroupId"] == DBNull.Value ? -1 : Convert.ToInt32(row["GroupId"]);
                if (!colorMap.ContainsKey(groupId))
                {
                    colorMap[groupId] = GenerateColor(groupId);
                }
            }

            return colorMap;
        }



        private Color GenerateColor(int groupId)
        {
            // Simple color wheel approach
            var colors = new List<Color> { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange, Color.Purple };
            return colors[groupId % colors.Count];
        }



    }
}
