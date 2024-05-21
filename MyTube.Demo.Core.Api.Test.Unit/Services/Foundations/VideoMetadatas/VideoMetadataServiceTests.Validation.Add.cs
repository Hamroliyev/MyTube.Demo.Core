﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;
using MyTube.Demo.Core.API.Models.Exeptions;
using MyTube.Demo.Core.API.Models.Metadatas;
using System.Threading.Tasks;

namespace MyTube.Demo.Core.Api.Test.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfIsNullAndLogError()
        {
            //given
            VideoMetadata nullVideoMetadata = null;
            var nullVideoMetadataException = new NullVideoMetadataException("VideoMetadata is null.");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    "VideoMetadata validation error occured, fix errors and try again.",
                        nullVideoMetadataException);

            //when
            ValueTask<VideoMetadata> addVideoMetadata =
                this.videoMetadataService.AddVideoMetadataAsync(nullVideoMetadata);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(addVideoMetadata.AsTask);

            //then
            actualVideoMetadataValidationException.Should()
                .BeEquivalentTo(expectedVideoMetadataValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(It.IsAny<VideoMetadata>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
