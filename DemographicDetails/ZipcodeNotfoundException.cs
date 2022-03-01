using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemographicDetails.Api
{
    public class ZipcodeNotfoundException : Exception
    {
        public ZipcodeNotfoundException(string message = "Zipcode not found in the Orange County", Exception innerException = null) : base(message,innerException) { }
        
    }
}
