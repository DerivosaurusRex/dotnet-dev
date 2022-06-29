using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase 
    {
        //private readonly CitiesDataStore _citiesDataStore;
        
        private readonly ICityInfoRepository _cityInfoRepository;

        //Works with local in-memory database
        //public CitiesController(CitiesDataStore citiesDataStore)
        //{
        //    _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        //}

        public CitiesController(ICityInfoRepository cityInfoRepository )
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();
            var results = new List<CityWithoutPointsOfInterestDto>();

            foreach(var city in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                });
            }

            return Ok(results);

           //return Ok(_citiesDataStore.Cities);
    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityWithoutPointsOfInterestDto>> GetCity(int id)
        {
            //var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
            
            var cityToReturn = await _cityInfoRepository.GetCityAsync(id, false);
            var result = new CityWithoutPointsOfInterestDto()
            {
                Id = cityToReturn.Id,
                Name = cityToReturn.Name,
                Description = cityToReturn.Description
            };

            if (cityToReturn == null) return NotFound();
            else return Ok(result);
        }
    }
}
