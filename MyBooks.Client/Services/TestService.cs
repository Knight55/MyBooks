using System.Diagnostics;
using System.Net.Http;
using IdentityModel.Client;

namespace MyBooks.Client.Services
{
    public class TestService
    {
        public async void Test()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Debug.WriteLine(disco.Error);
            }
            else
            {
                // request token
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,

                    ClientId = "ro.client",
                    ClientSecret = "secret",
                    Scope = "api1"
                });

                if (tokenResponse.IsError)
                {
                    Debug.WriteLine(tokenResponse.Error);
                }
                else
                {
                    Debug.WriteLine(tokenResponse.Json);
                }
            }
        }
    }
}