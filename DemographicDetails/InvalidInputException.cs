using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemographicDetails.Api
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message = "Invalid Input Exception", Exception innerException = null) : base(message, innerException) { }
    }
}
