// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using MyTube.Demo.Core.API.Brokers.DateTimes.DateTimes;
using MyTube.Demo.Core.API.Brokers.Loggings;
using MyTube.Demo.Core.API.Brokers.Storages;
using MyTube.Demo.Core.API.Models.Metadatas;
using System.Threading.Tasks;

namespace MyTube.Demo.Core.API.Services.VideoMetadatas
{
    public partial class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public VideoMetadataService(IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
            TryCatch(async () =>
            {
                ValidateVideoMetadataOnAdd(videoMetadata);

                return await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);
            });
    }
}
