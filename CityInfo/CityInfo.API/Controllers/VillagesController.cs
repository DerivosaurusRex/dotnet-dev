using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/villages")]
    [ApiController]
    public class VillagesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillageDto>> GetVillages()
        {
            return Ok(VillagesDataStore.CurrentVillages.Villages);
        }

        [HttpGet("{villageId}")]
        public ActionResult<VillageDto> GetVillage(int villageId)
        {
            var village = VillagesDataStore.CurrentVillages.Villages.FirstOrDefault(c => c.Id == villageId);

            if (village == null)
                return NotFound();
            return Ok(village);
        }
    }
}
