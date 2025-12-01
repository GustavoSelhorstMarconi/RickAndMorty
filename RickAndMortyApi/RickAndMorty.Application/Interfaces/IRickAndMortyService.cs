using RickAndMorty.Application.Dto;
using RickAndMorty.Domain.Entities;

namespace RickAndMorty.Application.Interfaces
{
    public interface IRickAndMortyService
    {
        Task<ApiResponse<List<Character>>> GetCharactersByEpisode(int episodeId);

        Task<ApiResponse<List<Episode>>> SearchEpisodesAsync(string name, string episode);
    }
}
