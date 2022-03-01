using DemographicDetails.Infrastructure.Implementation;
using DemographicDetails.Infrastructure.Interfaces;
using DemographicDetails.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemographicDetails.Services
{
    public class DistanceCalcualtionService : IGeoLocationRepository, IDistanceCalcualtionService 
    {
        private readonly IGeoLocationRepository _geoLocationRepository;
        private readonly ILogger _logger;

        public DistanceCalcualtionService(IGeoLocationRepository geoLocationRepository,ILogger<DistanceCalcualtionService> logger)
        {
            _geoLocationRepository = geoLocationRepository;
            _logger = logger;
        }

        public GeoLocationDistance DistanceCalculation(GeoLocationInput geoLocationInputs)
        {
            _logger.LogDebug($" Entering calculation Method");
            var coordinatesList = _geoLocationRepository.GetCoordinates();
            var cityList = _geoLocationRepository.GetGeoLocations();
            var zipCodes = new GeoLocationInput();
            var calculatedDistance = new GeoLocationDistance();
            try
            {                             
                var fromZipCodeCoordinates = coordinatesList.Find(a => a.ZipCode == geoLocationInputs.FromZipCode);
                var toZipCodeCoordinates = coordinatesList.Find(a => a.ZipCode == geoLocationInputs.ToZipCode);

                double rlat1 = Math.PI * fromZipCodeCoordinates.Latitude / 180;
                double rlat2 = Math.PI * toZipCodeCoordinates.Latitude / 180;
                double theta = fromZipCodeCoordinates.Longitude - toZipCodeCoordinates.Longitude;
                double rtheta = Math.PI * theta / 180;
                double dist =
                    Math.Sin(rlat1) * Math.Sin(rlat2) + (Math.Cos(rlat1) *
                    Math.Cos(rlat2) * Math.Cos(rtheta));
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                dist = dist * 0.8684;
                calculatedDistance.Distance = Math.Round(dist,2);
                calculatedDistance.FromCity = cityList.Find(a => a.ZipCode == geoLocationInputs.FromZipCode).City;
                calculatedDistance.ToCity = cityList.Find(a => a.ZipCode == geoLocationInputs.ToZipCode).City;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occured ", ex);
                throw;
            }
            return calculatedDistance;
        }

        public List<Coordinates> GetCoordinates()
        {
            return _geoLocationRepository.GetCoordinates();
        }        

        public List<GeoLocationCity> GetGeoLocations()
        {
            return _geoLocationRepository.GetGeoLocations();
        }
    }
}
