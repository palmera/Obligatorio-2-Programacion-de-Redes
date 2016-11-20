using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Exceptions
{
    [Serializable]
    public class UserException : ArgumentException
    {
        public UserException(string message) : base(message) { }
        public UserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
