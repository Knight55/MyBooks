using System;
using System.Reactive;
using Microsoft.Extensions.Logging;
using MyBooks.Client.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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

        [Reactive] public bool IsUserLoggedIn { get; set; }

        [Reactive] public string UserName { get; set; }

        [Reactive] public string UserAvatarImageUrl { get; set; } =
            "https://www.gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50";

        public RoutingState Router { get; }

        public ReactiveCommand<Unit, Unit> UserManagerCommand;
        public ReactiveCommand<Unit, Unit> LogoutCommand;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            ITokenService tokenService,
            IUserManagerService userManagerService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userManagerService = userManagerService;

            Router = new RoutingState();

            UserManagerCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                _logger.LogInformation($"Logging in...");
                await _userManagerService.LoginAsync();
                if (_userManagerService.LoginResult != null && !_userManagerService.LoginResult.IsError)
                {
                    IsUserLoggedIn = true;
                    UserName = _userManagerService.LoginResult.User.Identity.Name;
                    await _userManagerService.GetUserInfoAsync(_userManagerService.LoginResult.AccessToken);
                }
            });

            LogoutCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                _logger.LogInformation("Logging out...");
                await _userManagerService.LogoutAsync();
                if (IsUserLoggedIn)
                {
                    IsUserLoggedIn = false;
                    UserName = "";
                }
            });
        }
    }
}