using DemographicDetails.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicDetails.Infrastructure.Interfaces
{
    public interface IGeoLocationRepository
    {
        public List<GeoLocation> GetGeoLocations();
    }
}
