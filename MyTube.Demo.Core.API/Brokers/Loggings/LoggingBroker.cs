// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.Extensions.Logging;
using System;

namespace MyTube.Demo.Core.API.Brokers.Loggings
{
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;


        public void LogTrace(string message) =>
            this.logger.LogTrace(message);

        public void LogDebug(string message) =>
            this.logger.LogDebug(message);

        public void LogWarning(string message) =>
            this.logger.LogWarning(message);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception, exception.Message);

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);
    }
}
