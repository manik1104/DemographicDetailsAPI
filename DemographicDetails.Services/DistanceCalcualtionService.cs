using DemographicDetails.Infrastructure.Implementation;
using DemographicDetails.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemographicDetails.Services
{
    public class DistanceCalcualtionService : IDistanceCalcualtionService
    {        
        public GeoLocationDetails DistanceCalculation(GeoLocationInputs objGeoLocationInputs)
        {
            GeoLocation objDistanceCal = null;
            GeoLocationDetails objGeoLocationOutput = null;            
            List<GeoLocation> objGeoLocationList = new List<GeoLocation>();
            List<Coordinates> objCoordinatesList = new List<Coordinates>();
            try
            {
                objDistanceCal = new GeoLocation();
                objGeoLocationOutput = new GeoLocationDetails();
                objGeoLocationList = new GeoLocationRepository().GetGeoLocations();
                objCoordinatesList = new GeoLocationRepository().GetCoordinates();
                Coordinates objFromLocationDetails = objCoordinatesList.Find(a => a.ZipCode == objGeoLocationInputs.FromZipCode);
                Coordinates objToLocationDetails = objCoordinatesList.Find(a => a.ZipCode == objGeoLocationInputs.ToZipCode);              
                if(objFromLocationDetails != null && objToLocationDetails != null)
                {
                    double rlat1 = Math.PI * objFromLocationDetails.Latitude / 180;
                    double rlat2 = Math.PI * objToLocationDetails.Latitude / 180;
                    double theta = objFromLocationDetails.Longitude - objToLocationDetails.Longitude;
                    double rtheta = Math.PI * theta / 180;
                    double dist =
                        Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                        Math.Cos(rlat2) * Math.Cos(rtheta);
                    dist = Math.Acos(dist);
                    dist = dist * 180 / Math.PI;
                    dist = dist * 60 * 1.1515;
                    dist = dist * 0.8684;
                    objGeoLocationOutput.Distance = Math.Round(dist,2);
                    objGeoLocationOutput.FromCity = objGeoLocationList.Find(a => a.ZipCode == objGeoLocationInputs.FromZipCode).City;
                    objGeoLocationOutput.ToCity = objGeoLocationList.Find(a => a.ZipCode == objGeoLocationInputs.ToZipCode).City;

                }
                else
                {
                    throw new Exception("Zipcode not found");
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
            return objGeoLocationOutput;
        }
    }
}
