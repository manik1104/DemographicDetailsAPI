using DemographicDetails.Infrastructure.Interfaces;
using DemographicDetails.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemographicDetails.Infrastructure.Implementation
{
    public class GeoLocationRepository : IGeoLocationRepository
    {
       
        public GeoLocationRepository()
        {

        }

        public List<GeoLocation> GetGeoLocations()
        {
            return GetGeoLocationDetails();
        }

        public List<Coordinates> GetCoordinates()
        {
            return GetCoordinateDetails();
        }
        #region private methods
        private List<GeoLocation> GetGeoLocationDetails()
        {
            
            var geolocationList = new List<GeoLocation>();

            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Cities.json");

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                geolocationList = JsonConvert.DeserializeObject<List<GeoLocation>>(json);
            }
            return geolocationList;
        }

        private List<Coordinates> GetCoordinateDetails()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DemographicData.json");
            var coordinatesList = new List<Coordinates>();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                coordinatesList = JsonConvert.DeserializeObject<List<Coordinates>>(json);
            }
            return coordinatesList;
        }
        #endregion
    }
}
