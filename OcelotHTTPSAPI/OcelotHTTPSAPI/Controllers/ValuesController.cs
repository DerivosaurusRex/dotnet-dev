using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OcelotHTTPSAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public async Task<string> GetName()
        {
            await Task.Delay(3600);
            return "Jonathan";
        }
    }
}
