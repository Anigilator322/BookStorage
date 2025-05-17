using BookStorage.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStorageApp
{
    public class LibraryApiClient
    {
        private readonly HttpClient _http;

        public LibraryApiClient(string baseUrl)
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<Book>> SearchBooksAsync(string query)
        {
            var response = await _http.GetAsync($"/books?query={Uri.EscapeDataString(query)}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Book>>(json);
        }

        public async Task<byte[]> DownloadBookAsync(int id)
        {
            var response = await _http.GetAsync($"/books/{id}/download");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

    }
}
