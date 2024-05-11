using Microsoft.AspNetCore.Mvc;

namespace MyTube.Demo.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public ActionResult GetHi()
        {
            return Ok("Hi, I'm Ahmadjon Hamroliyev");
        }
    }
}
