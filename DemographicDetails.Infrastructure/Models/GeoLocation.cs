using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicDetails.Infrastructure.Models
{
    public class GeoLocationCity
    {       
        public string ZipCode { get; set; }

        public string City { get; set; }        
    }

    public class GeoLocationInput
    {

        public string FromZipCode { get; set; }
        public string ToZipCode { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(FromZipCode))
            {
                yield return new ValidationResult("Required From ZipCode");
            }

            if (string.IsNullOrEmpty(ToZipCode))
            {
                yield return new ValidationResult("Required To ZipCode");
            }
        }
       
    }

    public class GeoLocationDistance
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
