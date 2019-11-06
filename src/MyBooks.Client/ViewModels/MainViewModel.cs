using System.Reactive;
using System.Security.Claims;
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
        private readonly IUserManagerService _userManagerService;

        private bool _isUserLoggedIn;
        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            set => this.RaiseAndSetIfChanged(ref _isUserLoggedIn, value);
        }

        private string _userName = "asd";
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }

        public RoutingState Router { get; }

        public ReactiveCommand<Unit, Unit> UserManagerCommand;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            ITokenService tokenService,
            IUserManagerService userManagerService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userManagerService = userManagerService;

            Router = new RoutingState();

            UserManagerCommand = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    _logger.LogInformation($"User manager called.");
                    await _userManagerService.Login();
                    if (_userManagerService.LoginResult != null && !_userManagerService.LoginResult.IsError)
                    {
                        IsUserLoggedIn = true;
                        UserName = _userManagerService.LoginResult.User.Identity.Name;
                    }
                });
        }
    }
}