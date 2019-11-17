using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBooks.Api.Exceptions;
using MyBooks.Api.Options;
using MyBooks.Dto.Goodreads;

namespace MyBooks.Api.Services
{
    /// <summary>
    /// Default implementation of <see cref="IGoodreadsService"/>.
    /// </summary>
    public class GoodreadsService : IGoodreadsService
    {
        private readonly ILogger<GoodreadsService> _logger;
        private readonly HttpClient _httpClient;
        private readonly GoodreadsOptions _goodreadsOptions;

        /// <inheritdoc />
        public GoodreadsService(
            ILogger<GoodreadsService> logger,
            HttpClient httpClient,
            IOptions<GoodreadsOptions> options)
        {
            _logger = logger;
            _httpClient = httpClient;
            _goodreadsOptions = options.Value;

            _httpClient.BaseAddress = new Uri(_goodreadsOptions.BaseUrl);
        }

        /// <inheritdoc />
        public async Task<GoodreadsResponse> SearchBooks(string searchTerm)
        {
            var query = new Dictionary<string, string>
            {
                ["key"] = _goodreadsOptions.Key,
                ["q"] = searchTerm
            };

            var url = QueryHelpers.AddQueryString("/search", query);
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new GoodreadsEntityNotFoundException($"Status code: {response.StatusCode}");
            }

            var result = await response.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(GoodreadsResponse));
            var goodreadsResponse = (GoodreadsResponse)serializer.Deserialize(result);

            return goodreadsResponse;
        }

        /// <inheritdoc />
        public async Task<GoodreadsResponse> GetBook(string id)
        {
            var query = new Dictionary<string, string>
            {
                ["key"] = _goodreadsOptions.Key,
                ["id"] = id
            };

            var url = QueryHelpers.AddQueryString("/book/show", query);
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new GoodreadsEntityNotFoundException($"Status code: {response.StatusCode}");
            }

            var result = await response.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(GoodreadsResponse));
            var goodreadsResponse = (GoodreadsResponse)serializer.Deserialize(result);

            return goodreadsResponse;
        }

        /// <inheritdoc />
        public async Task<GoodreadsResponse> GetAuthor(string id)
        {
            var query = new Dictionary<string, string>
            {
                ["key"] = _goodreadsOptions.Key,
                ["id"] = id
            };

            var url = QueryHelpers.AddQueryString("/book/author", query);
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new GoodreadsEntityNotFoundException($"Status code: {response.StatusCode}");
            }

            var result = await response.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(GoodreadsResponse));
            var goodreadsResponse = (GoodreadsResponse)serializer.Deserialize(result);

            return goodreadsResponse;
        }
    }
}