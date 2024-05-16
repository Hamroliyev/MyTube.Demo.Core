using MyTube.Demo.Core.API.Brokers.Storages;
using MyTube.Demo.Core.API.Models.Metadatas;
using System.Threading.Tasks;

namespace MyTube.Demo.Core.API.Services.VideoMetadatas
{
    public class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;

        public VideoMetadataService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);

    }
}
