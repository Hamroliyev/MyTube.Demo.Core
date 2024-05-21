// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Moq;
using MyTube.Demo.Core.API.Brokers.Loggings;
using MyTube.Demo.Core.API.Brokers.Storages;
using MyTube.Demo.Core.API.Models.Metadatas;
using MyTube.Demo.Core.API.Services.VideoMetadatas;
using System;
using Tynamix.ObjectFiller;

namespace MyTube.Demo.Core.Api.Test.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.videoMetadataService = new VideoMetadataService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static VideoMetadata CreateRandomVideoMetadata() =>
           CreateRandomVideoMetadata(date: GetRandomDateTimeOffset()).Create();

        public static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<VideoMetadata> CreateRandomVideoMetadata(DateTimeOffset date)
        {
            var filler = new Filler<VideoMetadata>();

            filler.Setup()
               .OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
