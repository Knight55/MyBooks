using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBooks.Client.Options;
using MyBooks.Client.Services;
using MyBooks.Client.ViewModels;
using ReactiveUI;
using Refit;
using Serilog;
using Serilog.Hosting;
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
                    services.AddRefitClient<IMyBooksApiClient>(new RefitSettings
                        {
                            AuthorizationHeaderValueGetter = () => Task.FromResult("access token??")
                        })
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));

                    var tokenServiceUrl = context.Configuration.GetSection("TokenService:UrlHttp").Value;
                    services.AddHttpClient("tokenService", c =>
                    {
                        c.BaseAddress = new Uri(tokenServiceUrl);
                    });

                    // Other services
                    services.Configure<TokenRequestOptions>(context.Configuration.GetSection("TokenService:Token"));
                    services.AddTransient<ITokenService, TokenService>();

                    var browser = Assembly.GetEntryAssembly()
                        ?.GetTypes().FirstOrDefault(t => typeof(IBrowser).IsAssignableFrom(t));
                    services.Configure<OidcClientOptions>(options =>
                    {
                        options.Authority = tokenServiceUrl;
                        options.ClientId = "native.code";
                        options.Scope = "openid profile email";
                        options.RedirectUri = "https://notused";
                        options.PostLogoutRedirectUri = "https://notused";
                        options.ResponseMode = OidcClientOptions.AuthorizeResponseMode.FormPost;
                        options.Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode;
                        options.LoggerFactory.AddSerilog();
                        if (browser != null)
                            options.Browser = (IBrowser) Activator.CreateInstance(browser);
                        else
                            options.Browser = null;
                    });
                    services.AddSingleton<IUserManagerService, UserManagerService>();

                    // View models
                    services.AddSingleton<IScreen, MainViewModel>();
                    services.AddSingleton(provider => (MainViewModel) provider.GetService<IScreen>());
                    services.AddTransient<BookSearchViewModel>();
                    services.AddTransient<BookDetailsViewModel>();
                    services.AddTransient<SettingsViewModel>();

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