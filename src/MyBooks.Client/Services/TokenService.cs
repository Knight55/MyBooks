using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBooks.Client.Options;
using Newtonsoft.Json.Linq;

namespace MyBooks.Client.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly TokenRequestOptions _tokenRequestOptions;

        public TokenService(
            ILogger<TokenService> logger,
            IHttpClientFactory clientFactory,
            IOptions<TokenRequestOptions> options)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _tokenRequestOptions = options.Value;
        }

        public async Task GetToken()
        {
            var client = _clientFactory.CreateClient("tokenService");
            var discoveryResponse = await client.GetDiscoveryDocumentAsync();
            if (discoveryResponse.IsError)
            {
                _logger.LogError($"Error while getting discovery document: {discoveryResponse.Error}");
            }
            else
            {
                _logger.LogInformation($"Discovery document token endpoint: {discoveryResponse.TokenEndpoint}");

                // Request token
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,

                    ClientId = _tokenRequestOptions.ClientId,
                    ClientSecret = _tokenRequestOptions.ClientSecret,
                    Scope = _tokenRequestOptions.Scope
                });

                //var tokenResponse2 = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
                //{
                //    Address = discoveryResponse.TokenEndpoint,

                //    ClientId = _tokenRequestOptions.ClientId,
                //    ClientSecret = _tokenRequestOptions.ClientSecret
                //});

                if (tokenResponse.IsError)
                {
                    _logger.LogError($"Error while getting token: {tokenResponse.Error}");
                }
                else
                {
                    _logger.LogInformation($"Token: {tokenResponse.Json}");

                    // TODO: Move this to UserManagerService
                    // Call API
                    var apiClient = new HttpClient();
                    apiClient.SetBearerToken(tokenResponse.AccessToken);
                    apiClient.DefaultRequestHeaders.Add("api-version", "1.0");

                    var response = await apiClient.GetAsync("http://localhost:5000/api/Identity");
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"Status code: {response.StatusCode}");
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"Content: {JArray.Parse(content)}");
                    }
                }
            }
        }
    }
}