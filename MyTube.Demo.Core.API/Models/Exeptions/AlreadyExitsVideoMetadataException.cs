// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace MyTube.Demo.Core.API.Models.Exeptions
{
    public class AlreadyExitsVideoMetadataException : Xeption
    {
        public AlreadyExitsVideoMetadataException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        { }
    }
}
