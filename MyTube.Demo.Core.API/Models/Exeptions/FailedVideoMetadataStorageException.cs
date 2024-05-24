// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Azure.Messaging;
using System;
using Xeptions;

namespace MyTube.Demo.Core.API.Models.Exeptions
{
    public class FailedVideoMetadataStorageException : Xeption
    {
        public FailedVideoMetadataStorageException(string message, Exception innerException) 
            : base(message, innerException)
        { }
    }
}
