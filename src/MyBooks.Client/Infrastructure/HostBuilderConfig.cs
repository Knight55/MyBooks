using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBooks.Client.Services;
using MyBooks.Client.ViewModels;
using ReactiveUI;
using Refit;
using Serilog;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.Serilog;

namespace MyBooks.Client.Infrastructure
{
    /// <summary>
    /// Contains configuration for <see cref="HostBuilder"/>.
    /// Configuration includes: dependency injection, logging, services etc.
    /// </summary>
    public static class HostBuilderConfig
    {
        /// <summary>
        /// Configures a new <see cref="HostBuilder"/> which will be used to create host
        /// for the application.
        /// </summary>
        /// <param name="args">Command-line arguments as an array of string.></param>
        /// <returns>
        /// A configured <see cref="HostBuilder"/>.
        /// </returns>
        public static IHostBuilder Configure(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables("DOTNET_");
                    config.AddJsonFile("hostsettings.json", optional: false);
                    if (args != null)
                        config.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    if (args != null)
                        config.AddCommandLine(args);
                })
                .ConfigureLogging((context, logging) =>
                {
                    Log.Logger = LoggerConfig.Configure(context.Configuration).CreateLogger();
                    logging.AddSerilog(Log.Logger, true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.UseMicrosoftDependencyResolver();
                    var resolver = Locator.CurrentMutable;
                    resolver.InitializeSplat();
                    resolver.InitializeReactiveUI();
                    resolver.UseSerilogFullLogger(Log.Logger);

                    var timeout = context.Configuration.GetSection("shutdownTimeoutSeconds").Value;
                    services.AddOptions<HostOptions>().Configure(options =>
                    {
                        options.ShutdownTimeout = TimeSpan.FromSeconds(int.Parse(timeout));
                    });

                    var apiUrl = context.Configuration.GetSection("apiUrl").Value;
                    services.AddTransient(serviceProvider => RestService.For<IMyBookApiService>(apiUrl));

                    services.AddSingleton<IScreen, AppBootstrapper>();
                    services.AddSingleton<AppViewModel>();
                    services.AddTransient<BookSearchViewModel>();
                    services.AddTransient<BookDetailsViewModel>();
                    services.AddTransient<NewBookViewModel>();
                });

            return hostBuilder;
        }
    }
}