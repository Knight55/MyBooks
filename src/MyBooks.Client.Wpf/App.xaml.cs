using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBooks.Client.Infrastructure;
using Serilog;

namespace MyBooks.Client.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// IHost
        /// </summary>
        private IHost _host;

        /// <summary>
        /// Overridden event handler on application startup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            _host = HostBuilderConfig.Configure(e.Args).Build();

            var logger = _host.Services.GetRequiredService<ILogger<App>>();
            logger.LogInformation("The application has started successfully.");

            await _host.StartAsync();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        /// <summary>
        /// Overridden event handler on application exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnApplicationExit(object sender, ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
                Log.CloseAndFlush();
            }
        }
    }
}
