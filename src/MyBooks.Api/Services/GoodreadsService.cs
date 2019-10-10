using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MyBooks.Dto;

namespace MyBooks.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodreadsService
    {
        private readonly HttpClient _httpClient;
        private readonly GoodreadsOptions _options;

        /// <summary>
        /// 
        /// </summary>
        public GoodreadsService(HttpClient httpClient, IOptionsMonitor<GoodreadsOptions> optionsMonitor)
        {
            _httpClient = httpClient;
            _options = optionsMonitor.CurrentValue;

            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }
        
        // TODO: Define methods to get books, authors etc.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GoodreadsResponse> SearchBooks(string searchTerm)
        {
            var query = new Dictionary<string, string>
            {
                ["key"] = _options.Key,
                ["q"] = searchTerm
            };
            var url = QueryHelpers.AddQueryString("/search", query);
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var buffer = Encoding.UTF8.GetBytes(result);
                using (var stream = new MemoryStream(buffer))
                {
                    var serializer = new XmlSerializer(typeof(GoodreadsResponse));
                    var goodreadsResponse = (GoodreadsResponse) serializer.Deserialize(stream);

                    return goodreadsResponse;
                }
            }

            return null;
        }
    }
}