using System.Threading.Tasks;

namespace MyBooks.Client.Services
{
    public interface ITokenService
    {
        Task GetToken();
    }
}