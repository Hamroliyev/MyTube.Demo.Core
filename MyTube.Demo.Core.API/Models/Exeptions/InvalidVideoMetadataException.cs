﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Xeptions;

namespace MyTube.Demo.Core.API.Models.Exeptions
{
    public class InvalidVideoMetadataException : Xeption
    {
        public InvalidVideoMetadataException(string message)
            : base(message)
        { }
    }
}