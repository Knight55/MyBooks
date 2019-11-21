using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MyBooks.Client.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly ILogger<UserManagerService> _logger;
        private readonly OidcClientOptions _oidcClientOptions;

        private readonly OidcClient _oidcClient;

        public LoginResult LoginResult { get; set; }
        public LogoutResult LogoutResult { get; set; }

        public UserManagerService(
            ILogger<UserManagerService> logger,
            IOptions<OidcClientOptions> options)
        {
            _logger = logger;
            _oidcClientOptions = options.Value;

            _oidcClient = new OidcClient(_oidcClientOptions);
        }

        public async Task LoginAsync()
        {
            try
            {
                LoginResult = await _oidcClient.LoginAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected Error: {ex.Message}");
                return;
            }

            if (LoginResult.IsError)
            {
                if (LoginResult.Error == "UserCancel")
                {
                    _logger.LogError("The sign-in window was closed before authorization was completed.");
                }
                else
                {
                    _logger.LogInformation($"Error: {LoginResult.Error}");
                }
            }
            else
            {
                var name = LoginResult.User.Identity.Name;
                _logger.LogInformation($"User {name} logged in successfully.");
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                LogoutResult = await _oidcClient.LogoutAsync(new LogoutRequest
                {
                    BrowserDisplayMode = DisplayMode.Hidden,
                    IdTokenHint = LoginResult.IdentityToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error while logging out: {ex.Message}");
                return;
            }

            if (LogoutResult.IsError)
            {
                if (LogoutResult.Error == "UserCancel")
                {
                    _logger.LogError("The sign-in window was closed before authorization was completed.");
                }
                else
                {
                    _logger.LogInformation($"Error: {LogoutResult.Error}");
                }
            }
            else
            {
                _logger.LogInformation($"User {LoginResult.User.Identity.Name} logged out successfully.");
                LoginResult = null;
            }
        }

        public async Task GetUserInfoAsync(string accessToken)
        {
            var userInfoResult = await _oidcClient.GetUserInfoAsync(accessToken);
            // TODO: error handling
            if (!userInfoResult.IsError)
            {
                _logger.LogInformation($"User info request was successful: {userInfoResult}");
            }
        }

        public async Task RefreshAccessTokenAsync(string refreshToken)
        {
            var refreshTokenResult = await _oidcClient.RefreshTokenAsync(refreshToken);
            // TODO: error handling
            if (!refreshTokenResult.IsError)
            {
                _logger.LogInformation($"Successfully refreshed the access token. {refreshTokenResult}");
            }
        }
    }
}