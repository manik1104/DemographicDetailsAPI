using DemographicDetails.Infrastructure;
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
        private readonly ILogger<GeoLocationController> _logger;
        private readonly IDistanceCalcualtionService _distanceCalculationService;

        public GeoLocationController(IDistanceCalcualtionService distanceCalculationService, ILogger<GeoLocationController> logger)
        {
            _logger = logger;
            _distanceCalculationService = distanceCalculationService;
        }

        [HttpPost]
        [ActionName("GetPostalCodeDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPostalCodeDetails([FromBody] GeoLocationInputs objGeoLocationInputs)
        {
            string result = null;
            try
            {
                if (objGeoLocationInputs.FromZipCode != null && objGeoLocationInputs.ToZipCode != null)
                    result = JsonConvert.SerializeObject(_distanceCalculationService.DistanceCalculation(objGeoLocationInputs));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unknown exception", ex);
                  return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return this.Ok(result);
        }
    }
}
