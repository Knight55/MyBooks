using System.Threading.Tasks;
using IdentityModel.OidcClient;

namespace MyBooks.Client.Services
{
    public interface IUserManagerService
    {
        LoginResult LoginResult { get; set; }

        Task LoginAsync();
        Task LogoutAsync();
        Task GetUserInfoAsync(string accessToken);
    }
}