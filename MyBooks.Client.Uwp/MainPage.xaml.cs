using System;
using System.Net.Http;
using System.Text;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using IdentityModel.OidcClient;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyBooks.Client.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        HttpClient _client;

        public MainPage()
        {
            this.InitializeComponent();
        }

        //private async void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var options = new OidcClientOptions
        //    {
        //        Authority = "https://demo.identityserver.io",
        //        ClientId = "native.hybrid",
        //        Scope = "openid profile api offline_access",
        //        RedirectUri = "io.identityserver.demo.uwp://callback",
        //        ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
        //        Browser = new SystemBrowser()
        //    };

        //    var client = new OidcClient(options);
        //    var result = await client.LoginAsync(new LoginRequest());


        //    if (!string.IsNullOrEmpty(result.Error))
        //    {
        //        resultTextBox.Text = result.Error;
        //        return;
        //    }

        //    var sb = new StringBuilder(128);

        //    foreach (var claim in result.User.Claims)
        //    {
        //        sb.AppendLine($"{claim.Type}: {claim.Value}");
        //    }

        //    sb.AppendLine($"refresh token: {result.RefreshToken}");
        //    sb.AppendLine($"access token: {result.AccessToken}");

        //    resultTextBox.Text = sb.ToString();

        //    _client = new HttpClient(result.RefreshTokenHandler);
        //    _client.BaseAddress = new Uri("https://demo.identityserver.io/");
        //}

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var options = new OidcClientOptions
            {
                Authority = "http://localhost:5001",
                
                ClientId = "native.hybrid",
                Scope = "openid profile myBooksApi offline_access",
                RedirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().AbsoluteUri,

                Browser = new WabBrowser(enableWindowsAuthentication: false)
            };

            var client = new OidcClient(options);
            var result = await client.LoginAsync(new LoginRequest());

            if (!string.IsNullOrEmpty(result.Error))
            {
                resultTextBox.Text = result.Error;
                return;
            }

            var sb = new StringBuilder(128);

            foreach (var claim in result.User.Claims)
            {
                sb.AppendLine($"{claim.Type}: {claim.Value}");
            }

            sb.AppendLine($"refresh token: {result.RefreshToken}");
            sb.AppendLine($"access token: {result.AccessToken}");

            resultTextBox.Text = sb.ToString();

            _client = new HttpClient(result.RefreshTokenHandler);
            _client.BaseAddress = new Uri("http://localhost:5000");
        }

        private async void CallApiButton_Click(object sender, RoutedEventArgs e)
        {
            if (_client == null)
            {
                return;
            }

            var result = await _client.GetAsync("api/books");
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                resultTextBox.Text = JArray.Parse(response).ToString();
            }
            else
            {
                resultTextBox.Text = result.ReasonPhrase;
            }
        }
    }
}
