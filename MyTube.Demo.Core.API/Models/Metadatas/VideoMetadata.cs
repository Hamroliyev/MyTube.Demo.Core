﻿using System;

namespace MyTube.Demo.Core.API.Models.Metadatas
{
    public class VideoMetadata
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BlobPath { get; set; }
        public string Thubnail { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}