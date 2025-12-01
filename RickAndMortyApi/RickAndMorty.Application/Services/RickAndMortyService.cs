using RickAndMorty.Application.Dto;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Domain.Entities;
using RickAndMorty.Domain.Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace RickAndMorty.Application.Services
{
    public class RickAndMortyService : IRickAndMortyService
    {
        private readonly IRickAndMortyClient _rickAndMortyClient;

        public RickAndMortyService(IRickAndMortyClient rickAndMortyClient)
        {
            _rickAndMortyClient = rickAndMortyClient;
        }

        public async Task<ApiResponse<List<Character>>> GetCharactersByEpisode(int episodeId)
        {
            try
            {
                var episode = await _rickAndMortyClient.GetEpisodeAsync(episodeId);

                if (episode == null)
                    return new ApiResponse<List<Character>>(["Episode not found."], 404);

                var characterIds = episode.Characters
                    .Select(url => url.Split('/').Last())
                    .ToList();

                var charactersUrl = "character/" + string.Join(",", characterIds);

                var characters = await _rickAndMortyClient.GetCharactersAsync(charactersUrl);

                if (characters == null)
                    return new ApiResponse<List<Character>>(["Characters not found."], 404);

                var ordered = characters.OrderBy(c => c.Name).ToList();

                return new ApiResponse<List<Character>>(ordered, 200);
            }
            catch
            {
                return new ApiResponse<List<Character>>(["Error during search."], 500);
            }
        }

        public async Task<ApiResponse<List<Episode>>> SearchEpisodesAsync(string? name = null, string? episodeCode = null)
        {
            try
            {
                var queryParams = new List<string>();

                if (!string.IsNullOrWhiteSpace(name))
                    queryParams.Add($"name={Uri.EscapeDataString(name)}");

                if (!string.IsNullOrWhiteSpace(episodeCode))
                    queryParams.Add($"episode={Uri.EscapeDataString(episodeCode)}");

                var url = "episode";
                if (queryParams.Any())
                    url += "?" + string.Join("&", queryParams);

                var episodes = await _rickAndMortyClient.GetEpisodeSearchAsync(url);

                if (episodes == null)
                    return new ApiResponse<List<Episode>>(["Episode not found."], 404);

                return new ApiResponse<List<Episode>>(episodes.Results, 200);
            }
            catch
            {
                return new ApiResponse<List<Episode>>(["Error during search."], 500);
            }
        }
    }
}
