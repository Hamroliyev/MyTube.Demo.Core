// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using MyTube.Demo.Core.API.Models.Metadatas;
using System.Threading.Tasks;

namespace MyTube.Demo.Core.API.Services.VideoMetadatas
{
    public interface IVideoMetadataService
    {
        ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}