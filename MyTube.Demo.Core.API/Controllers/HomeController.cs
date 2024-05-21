// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using MyTube.Demo.Core.API.Brokers.Storages;
using MyTube.Demo.Core.API.Models.Metadatas;
using System.Threading.Tasks;

namespace MyTube.Demo.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IStorageBroker storageBroker;
        public HomeController(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
        [HttpGet]
        public ActionResult<string> GetHi()
        {
            return Ok("Hi, I'm Ahmadjon Hamroliyev");
        }

        [HttpPost]
        public async ValueTask<ActionResult<VideoMetadata>> Post(VideoMetadata videoMetadata)
        {
            var addedVideoMetadata = await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);
            return Ok(addedVideoMetadata);
        }
    }
}
