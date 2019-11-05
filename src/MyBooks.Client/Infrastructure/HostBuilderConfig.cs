﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBooks.Client.Options;
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
                .UseSerilog((context, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                })
                .ConfigureServices((context, services) =>
                {
                    // Shutdown timeout in seconds
                    var timeout = context.Configuration.GetSection("shutdownTimeoutSeconds").Value;
                    services.AddOptions<HostOptions>().Configure(options =>
                    {
                        options.ShutdownTimeout = TimeSpan.FromSeconds(int.Parse(timeout));
                    });

                    // REST services
                    var apiUrl = context.Configuration.GetSection("Api:UrlHttp").Value;
                    services.AddHttpClient("myBooksApi", c => { c.BaseAddress = new Uri(apiUrl); })
                        .AddTypedClient(c => RestService.For<IMyBooksApiClient>(c));

                    var tokenServiceUrl = context.Configuration.GetSection("TokenService:UrlHttp").Value;
                    services.AddHttpClient("tokenService", c => { c.BaseAddress = new Uri(tokenServiceUrl); });

                    // Other services
                    services.Configure<TokenRequestOptions>(context.Configuration.GetSection("TokenService:Token"));
                    services.AddTransient<ITokenService, TokenService>();

                    // View models
                    services.AddSingleton<IScreen, MainViewModel>();
                    services.AddSingleton(provider => (MainViewModel) provider.GetService<IScreen>());
                    services.AddTransient<BookSearchViewModel>();
                    services.AddTransient<BookDetailsViewModel>();
                    services.AddTransient<NewBookViewModel>();

                    // Splat and ReactiveUI
                    services.UseMicrosoftDependencyResolver();
                    var resolver = Locator.CurrentMutable;
                    resolver.InitializeSplat();
                    resolver.InitializeReactiveUI();
                    resolver.RegisterViewsForViewModels(Assembly.GetEntryAssembly());
                    resolver.UseSerilogFullLogger();
                });

            return hostBuilder;
        }
    }
}