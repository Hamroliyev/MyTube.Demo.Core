using Microsoft.EntityFrameworkCore;
using MyTube.Demo.Core.API.Models.Metadatas;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MyTube.Demo.Core.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<VideoMetadata> VideoMetadatas { get; set; }
        public async ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await InsertAsync(videoMetadata);

        public IQueryable<VideoMetadata> SelectAllVideoMetadatas() =>
            SelectAll<VideoMetadata>();

        public async ValueTask<VideoMetadata> SelectVideoMetadataByIdAsync(Guid videoMetadataId) =>
        await SelectAsync<VideoMetadata>(videoMetadataId);

        public async ValueTask<VideoMetadata> UpdateVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await UpdateAsync(videoMetadata);

        public async ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await DeleteAsync(videoMetadata);
    }
}
