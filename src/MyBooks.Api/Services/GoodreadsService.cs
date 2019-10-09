using System.Net.Http;
using System.Threading.Tasks;

namespace MyBooks.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodreadsService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 
        /// </summary>
        public GoodreadsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        // TODO: Define methods to get books, authors etc.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task GetBooks()
        {
            var response = await _httpClient.GetAsync("");
        }
    }
}