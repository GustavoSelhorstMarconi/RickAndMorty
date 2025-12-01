using RickAndMorty.Domain.Entities;
using System;

namespace RickAndMorty.Domain.Interfaces
{
    public interface IRickAndMortyClient
    {
        Task<Episode?> GetEpisodeAsync(int episodeId);

        Task<List<Character>?> GetCharactersAsync(string url);

        Task<ApiResultWrapper<Episode>?> GetEpisodeSearchAsync(string url);
    }
}
