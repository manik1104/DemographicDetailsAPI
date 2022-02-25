using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicDetails.Infrastructure.Models
{
    public class GeoLocation
    {       
        public string ZipCode { get; set; }

        public string City { get; set; }        
    }

    public class GeoLocationInputs
    {
        public string FromZipCode { get; set; }
        public string ToZipCode { get; set; }
       
    }

    public class GeoLocationDetails
    {
        public double Distance { get; set; }

        public string FromCity { get; set; }

        public string ToCity { get; set; }
    }

    public class Coordinates
    {
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
