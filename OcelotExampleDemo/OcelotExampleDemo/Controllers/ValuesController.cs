using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OcelotExampleDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public async Task<string> GetName()
        {
            await Task.Delay(6000);
            return "Jonathan";
        }
    }
}
