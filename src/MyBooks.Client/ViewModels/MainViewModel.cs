using System.Reactive;
using Microsoft.Extensions.Logging;
using MyBooks.Client.Services;
using ReactiveUI;

namespace MyBooks.Client.ViewModels
{
    /// <summary>
    /// View model for MainWindow. It is used for navigation routing.
    /// </summary>
    public class MainViewModel : ReactiveObject, IScreen
    {
        private readonly ILogger<MainViewModel> _logger;
        private readonly ITokenService _tokenService;

        public RoutingState Router { get; }

        public ReactiveCommand<Unit, Unit> UserManagerCommand;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;

            Router = new RoutingState();

            UserManagerCommand = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    _logger.LogInformation($"User manager called.");
                    await _tokenService.GetToken();
                });
        }
    }
}