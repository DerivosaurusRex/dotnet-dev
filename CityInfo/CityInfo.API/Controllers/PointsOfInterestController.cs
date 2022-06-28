using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            try
            {
                //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with the ID {cityId} wasn't found when accessing points of interests");
                    return NotFound();
                }
                return Ok(city.PointOfInterests);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            try
            {
                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                    return NotFound();

                var pointOfInterest = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

                if (pointOfInterest == null)
                    return NotFound();

                return Ok(pointOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"The city with ID {cityId} has no points of interest.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
                 
            }
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest) {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(c => c.PointOfInterests).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };

            city.PointOfInterests.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, pointOfInterestId = finalPointOfInterest.Id}, finalPointOfInterest);
        }

        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
                return NotFound();

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();  
        }

        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
                return NotFound();

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!TryValidateModel(pointOfInterestToPatch))
                return BadRequest(ModelState);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
                return NotFound();

            city.PointOfInterests.Remove(pointOfInterestFromStore);

            _mailService.Send("Point of interest is deleted", $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} is deleted.");

            return NoContent();
        }
    }
}
