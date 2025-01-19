using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RFIDUpdateException : Exception
    {
        // Default constructor
        public RFIDUpdateException()
        {
        }

        // Constructor that takes a message
        public RFIDUpdateException(string message)
            : base(message)
        {
        }

        // Constructor that takes a message and an inner exception
        public RFIDUpdateException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // Optional: If you need serialization support, add this constructor too
        protected RFIDUpdateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}

