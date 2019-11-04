using System.Reactive;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.ViewModels
{
    /// <summary>
    /// View model for MainWindow. It is used for navigation routing.
    /// </summary>
    public class MainViewModel : ReactiveObject, IScreen
    {
        private readonly ILogger<MainViewModel> _logger;

        public RoutingState Router { get; }

        public ReactiveCommand<Unit, Unit> UserManagerCommand;

        public MainViewModel(ILogger<MainViewModel> logger)
        {
            _logger = logger;

            Router = new RoutingState();

            UserManagerCommand = ReactiveCommand.Create(
                () => _logger.LogInformation($"User manager called."));
        }
    }
}