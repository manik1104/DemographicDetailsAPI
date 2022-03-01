using DemographicDetails.Infrastructure;
using DemographicDetails.Infrastructure.Interfaces;
using DemographicDetails.Infrastructure.Models;
using DemographicDetails.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemographicDetails.Api.Controllers
{

    [Route("GeoLocation/[Action]")]
    public class GeoLocationController : Controller
    {        
        private readonly IDistanceCalcualtionService _distanceCalculationService;
        private readonly IGeoLocationRepository _geoLocationRepository;
        private readonly ILogger _logger;

        public GeoLocationController(IDistanceCalcualtionService distanceCalculationService, IGeoLocationRepository geoLocationRepository, ILogger<GeoLocationController> logger) 
        {
            _distanceCalculationService = distanceCalculationService;
            _geoLocationRepository = geoLocationRepository;
            _logger = logger;
        }

        [HttpPost]
        [ActionName("GetPostalCodeDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPostalCodeDetails([FromBody] GeoLocationInput geoLocationInputs)
        {
            string result = string.Empty;
            try
            {                
                if (!string.IsNullOrWhiteSpace(geoLocationInputs.FromZipCode) && !string.IsNullOrWhiteSpace(geoLocationInputs.ToZipCode))
                {
                    result = JsonConvert.SerializeObject(_distanceCalculationService.DistanceCalculation(geoLocationInputs));
                }
                else
                {
                    _logger.LogError($"Exception Occured -  {0}", new InvalidInputException().Message);
                    return this.StatusCode(StatusCodes.Status400BadRequest, new InvalidInputException());
                }
                    
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occured - {0}", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                               
            }
            _logger.LogInformation($"{0}", result);
            return this.Ok(result);
        }
    }
}
