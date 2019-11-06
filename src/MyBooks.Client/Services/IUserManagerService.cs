using System.Threading.Tasks;
using IdentityModel.OidcClient;

namespace MyBooks.Client.Services
{
    public interface IUserManagerService
    {
        LoginResult LoginResult { get; set; }

        Task Login();
    }
}