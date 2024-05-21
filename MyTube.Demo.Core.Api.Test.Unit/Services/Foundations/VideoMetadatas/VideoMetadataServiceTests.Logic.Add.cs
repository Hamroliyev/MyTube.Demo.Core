// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Moq;
using MyTube.Demo.Core.API.Models.Metadatas;

namespace MyTube.Demo.Core.Api.Test.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldAddVideoMetadataAsync()
        {
            //given
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata();
            VideoMetadata inputVideoMetadata = randomVideoMetadata;
            VideoMetadata persistedVideoMetadata = inputVideoMetadata;
            VideoMetadata expectedVideoMetadata = persistedVideoMetadata;

            this.storageBrokerMock.Setup(broker =>
                broker.InsertVideoMetadataAsync(inputVideoMetadata))
                    .ReturnsAsync(persistedVideoMetadata);

            //when
            VideoMetadata actualVideoMetadata = await this.videoMetadataService
                .AddVideoMetadataAsync(inputVideoMetadata);

            //then
            actualVideoMetadata.Should().BeEquivalentTo(
                expectedVideoMetadata);
        }
    }
}
