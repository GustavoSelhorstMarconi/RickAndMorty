using RickAndMorty.Domain.Entities;
using RickAndMorty.Domain.Interfaces;
using System.Text.Json;

namespace RickAndMorty.Infra.Clients
{
    public class RickAndMortyClient : IRickAndMortyClient
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public RickAndMortyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Episode?> GetEpisodeAsync(int episodeId)
        {
            var response = await _httpClient.GetAsync("episode/" + episodeId);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Episode>(json, _jsonOptions);
        }

        public async Task<List<Character>?> GetCharactersAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Character>>(json, _jsonOptions);
        }

        public async Task<ApiResultWrapper<Episode>?> GetEpisodeSearchAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResultWrapper<Episode>>(json, _jsonOptions);
        }
    }
}
