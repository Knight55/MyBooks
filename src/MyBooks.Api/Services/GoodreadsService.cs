﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MyBooks.Api.Exceptions;
using MyBooks.Api.Options;
using MyBooks.Dto.Goodreads;

namespace MyBooks.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodreadsService
    {
        private readonly HttpClient _httpClient;
        private readonly GoodreadsOptions _goodreadsOptions;

        /// <summary>
        /// 
        /// </summary>
        public GoodreadsService(
            HttpClient httpClient,
            IOptions<GoodreadsOptions> options)
        {
            _httpClient = httpClient;
            _goodreadsOptions = options.Value;

            _httpClient.BaseAddress = new Uri(_goodreadsOptions.BaseUrl);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
    }
}