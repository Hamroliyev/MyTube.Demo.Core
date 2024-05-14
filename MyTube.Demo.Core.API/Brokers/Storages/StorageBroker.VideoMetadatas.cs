using Microsoft.EntityFrameworkCore;
using MyTube.Demo.Core.API.Models.Metadatas;

namespace MyTube.Demo.Core.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<VideoMetadata> VideoMetadatas { get; set; }
    }
}
