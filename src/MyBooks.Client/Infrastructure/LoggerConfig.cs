using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MyBooks.Client.Infrastructure
{
    /// <summary>
    /// Contains configuration for <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfig
    {
        /// <summary>
        /// Indicates whether the logger has been configured or not.
        /// </summary>
        public static bool IsConfigured { get; private set; }

        /// <summary>
        /// Configures a <see cref="LoggerConfiguration"/> with the specified options.
        /// </summary>
        /// <exception cref="Exception">
        /// Thrown if the logger is already configured.
        /// </exception>
        /// <returns>
        /// A configured <see cref="LoggerConfiguration"/>.
        /// </returns>
        public static LoggerConfiguration Configure(IConfiguration configuration)
        {
            if (IsConfigured)
                throw new Exception("The logger can only be configured once.");

            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            IsConfigured = true;
            return loggerConfig;
        }
    }
}