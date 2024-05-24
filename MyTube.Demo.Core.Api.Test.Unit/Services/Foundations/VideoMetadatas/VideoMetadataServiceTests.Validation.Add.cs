// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Moq;
using MyTube.Demo.Core.API.Models.Exeptions;
using MyTube.Demo.Core.API.Models.Metadatas;
using System;
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
                    "Video Metadata Validation Exception occured, fix the errors and try again.",
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfVideoMetadataIsInvalidDataAndLogItAsync(string invalidData)
        {
            //given
            var invalidVideoMetadata = new VideoMetadata()
            {
                Title = invalidData
            };

            InvalidVideoMetadataException invalidVideoMetadataException = new("Video Metadata is invalid.");

            invalidVideoMetadataException.AddData(key: nameof(VideoMetadata.Id),
                values: "Id is required.");

            invalidVideoMetadataException.AddData(key: nameof(VideoMetadata.Title),
                values: "Text is required.");

            invalidVideoMetadataException.AddData(key: nameof(VideoMetadata.BlobPath),
                values: "Text is required.");

            invalidVideoMetadataException.AddData(key: nameof(VideoMetadata.CreatedDate),
                values: "Date is required.");

            invalidVideoMetadataException.AddData(key: nameof(VideoMetadata.UpdatedDate),
                values: "Date is required.");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException("Video Metadata Validation Exception occured, fix the errors and try again.",
                    invalidVideoMetadataException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(invalidVideoMetadata);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(addVideoMetadataTask.AsTask);


            //then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(It.IsAny<VideoMetadata>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreateAndUpdateDatesIsNotSameAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            int randomNumber = GetRandomNumber();
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata(randomDateTime);
            VideoMetadata invalidVideoMetadata = randomVideoMetadata;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTime);

            invalidVideoMetadata.UpdatedDate =
                invalidVideoMetadata.CreatedDate.AddDays(randomNumber);

            var invalidPostException =
                new InvalidVideoMetadataException(message: "Invalid Date");

            invalidPostException.AddData(
                key: nameof(VideoMetadata.UpdatedDate),
                values: $"Date is not the same as {nameof(VideoMetadata.CreatedDate)}");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException("Invalid date",invalidPostException);

            // when
            ValueTask<VideoMetadata> addPostTask =
                this.videoMetadataService.AddVideoMetadataAsync(invalidVideoMetadata);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    addPostTask.AsTask);

            // then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                        Times.Once);
            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(It.IsAny<VideoMetadata>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        //[Theory]
        //[MemberData(nameof(MinutesBeforeOrAfter))]
        //public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotRecentAndLogItAsync(
        //    int minutesBeforeOrAfter)
        //{
        //    // given
        //    DateTimeOffset randomDateTime =
        //        GetRandomDateTimeOffset();

        //    DateTimeOffset invalidDateTime =
        //        randomDateTime.AddMinutes(minutesBeforeOrAfter);

        //    Post randomPost = CreateRandomPost(invalidDateTime);
        //    Post invalidPost = randomPost;
        //    var invalidPostException =
        //        new InvalidPostException();
        //    invalidPostException.AddData(
        //        key: nameof(Post.CreatedDate),
        //        values: "Date is not recent");

        //    var expectedPostValidationException =
        //        new PostValidationException(invalidPostException);

        //    this.dateTimeBrokerMock.Setup(broker =>
        //        broker.GetCurrentDateTimeOffset())
        //            .Returns(randomDateTime);

        //    // when
        //    ValueTask<Post> addPostTask =
        //        this.postService.AddPostAsync(invalidPost);

        //    VideoMetadataValidationException actualPostValidationException =
        //       await Assert.ThrowsAsync<VideoMetadataValidationException>(
        //           addPostTask.AsTask);

        //    // then
        //    actualPostValidationException.Should().BeEquivalentTo(
        //        expectedPostValidationException);

        //    this.dateTimeBrokerMock.Verify(broker =>
        //        broker.GetCurrentDateTimeOffset(),
        //            Times.Once());

        //    this.loggingBrokerMock.Verify(broker =>
        //        broker.LogError(It.Is(SameExceptionAs(
        //            expectedPostValidationException))),
        //                Times.Once);
        //    this.storageBrokerMock.Verify(broker =>
        //        broker.InsertPostAsync(It.IsAny<Post>()),
        //            Times.Never);

        //    this.dateTimeBrokerMock.VerifyNoOtherCalls();
        //    this.loggingBrokerMock.VerifyNoOtherCalls();
        //    this.storageBrokerMock.VerifyNoOtherCalls();
        //}
    }
}
