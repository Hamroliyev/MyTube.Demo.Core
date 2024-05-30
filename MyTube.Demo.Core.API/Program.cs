using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTube.Demo.Core.API.Brokers.DateTimes.DateTimes;
using MyTube.Demo.Core.API.Brokers.Loggings;
using MyTube.Demo.Core.API.Brokers.Storages;
using MyTube.Demo.Core.API.Services.VideoMetadatas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<IDateTimeBroker,  DateTimeBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
builder.Services.AddTransient<IVideoMetadataService, VideoMetadataService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
