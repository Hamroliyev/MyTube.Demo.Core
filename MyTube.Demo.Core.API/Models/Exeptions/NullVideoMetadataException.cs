// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Xeptions;

namespace MyTube.Demo.Core.API.Models.Exeptions
{
    public class NullVideoMetadataException : Xeption
    {
        public NullVideoMetadataException(string message)
            : base(message: message)
        { }
    }
}
