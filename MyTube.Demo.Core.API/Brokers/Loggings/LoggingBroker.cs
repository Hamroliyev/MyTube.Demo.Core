using Microsoft.Extensions.Logging;
using System;

namespace MyTube.Demo.Core.API.Brokers.Loggings
{
    public class LoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public void LogCritical(Exception exception) =>
              this.logger.LogCritical(exception, exception.Message);

        public void LogError(Exception exception) =>
             this.logger.LogError(exception, exception.Message);
    }
}
