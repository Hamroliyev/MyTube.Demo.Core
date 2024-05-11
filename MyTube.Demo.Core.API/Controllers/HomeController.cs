using Microsoft.AspNetCore.Mvc;

namespace MyTube.Demo.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetHi()
        {
            return Ok("Hi, I'm Ahmadjon Hamroliyev");
        }
    }
}
