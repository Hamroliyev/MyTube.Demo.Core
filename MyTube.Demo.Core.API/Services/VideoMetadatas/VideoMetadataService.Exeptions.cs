﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;
using MyTube.Demo.Core.API.Models.Exeptions;
using MyTube.Demo.Core.API.Models.Metadatas;
using STX.EFxceptions.Abstractions.Models.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace MyTube.Demo.Core.API.Services.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private delegate ValueTask<VideoMetadata> ReturningVideoMetadataFunction();

        private async ValueTask<VideoMetadata> TryCatch(ReturningVideoMetadataFunction returningVideoMetadataFunction)
        {
            try
            {
                return await returningVideoMetadataFunction();
            }
            catch (NullVideoMetadataException nullVideoMetadataException)
            {
                throw CreateAndLogValidationException(nullVideoMetadataException);
            }
            catch (InvalidVideoMetadataException invalidVideoMetadataException)
            {
                throw CreateAndLogValidationException(invalidVideoMetadataException);
            }
            catch (SqlException sqlException)
            {
                FailedVideoMetadataStorageException failedVideoMetadataStorageException =
                    new FailedVideoMetadataStorageException(
                        message: "Failed Video metadata error occured, contact support.",
                        innerException: sqlException);

                throw CreateAndLogCriticalDependencyException(failedVideoMetadataStorageException);
            }
            catch (DuplicateKeyException dublicateKeyException)
            {
                var alreadyExistsVideoMetadataException = new AlreadyExitsVideoMetadataException(
                    message: "Video metadata already exists.",
                    innerException: dublicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsVideoMetadataException);
            }
        }

        private Exception CreateAndLogDependencyValidationException(Xeption exception)
        {
            var videoMetadataDependencyValidationException = new VideoMetadataDependencyValidationException(
                message: "Video metadata Dependency validation error occured , fix the errors and try again.",
                innerException: exception);

            this.loggingBroker.LogError(videoMetadataDependencyValidationException);

            return videoMetadataDependencyValidationException;
        }

        private VideoMetadataDependencyException CreateAndLogCriticalDependencyException(
            Xeption exception)
        {
            var videoMetadataDependencyException = new VideoMetadataDependencyException(
                "Video metadata dependency error occured, fix the errors and try again.",
                    exception);

            this.loggingBroker.LogCritical(videoMetadataDependencyException);

            return videoMetadataDependencyException;
        }

        private VideoMetadataValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var videoMetadataValidationException = new VideoMetadataValidationException(
                "Video Metadata Validation Exception occured, fix the errors and try again.",
                    exception);

            this.loggingBroker.LogError(videoMetadataValidationException);

            return videoMetadataValidationException;
        }
    }
}
