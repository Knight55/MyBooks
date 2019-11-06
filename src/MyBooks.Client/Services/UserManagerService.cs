using System;
using System.Linq;
using System.Reflection;
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

        public UserManagerService(
            ILogger<UserManagerService> logger,
            IOptions<OidcClientOptions> oidcClientOptions)
        {
            _logger = logger;
            _oidcClientOptions = oidcClientOptions.Value;
            _oidcClient = new OidcClient(_oidcClientOptions);
        }

        public async Task Login()
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
    }
}