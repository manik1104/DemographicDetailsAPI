using DemographicDetails.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicDetails.Services
{
    public interface IDistanceCalcualtionService
    {
        GeoLocationDetails DistanceCalculation(GeoLocationInputs objGeoLocationInputs);        
    }
}
