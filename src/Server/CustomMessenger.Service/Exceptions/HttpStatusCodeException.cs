using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.Exceptions
{
    public class HttpStatusCodeException(int code, string message) : Exception(message)
    {
        public int Code { get { return code; } }
    }
}
