using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Application.Dto;
using RickAndMorty.Domain.Entities;

namespace RickAndMorty.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RickAndMortyController : ControllerBase
    {
        private readonly IRickAndMortyService _rickAndMortyService;

        public RickAndMortyController(IRickAndMortyService rickAndMortyService)
        {
            _rickAndMortyService = rickAndMortyService;
        }

        /// <summary>
        ///     Retrieves all characters that appear in the specified episode, identified by its numeric ID, and returns them sorted alphabetically by name.
        /// </summary>
        /// <param name="episodeId">
        ///     The numeric Id of the episode used to fetch character data.
        /// </param>
        /// <returns>
        ///     A list of characters that appear in the given episode, ordered alphabetically.
        /// </returns>
        /// <response code="200">
        ///     Successfully returns the list of characters for the episode.
        /// </response>
        /// <response code="404">Episode or characters not found.</response>
        /// <response code="500">Error when searching for characters.</response>
        [ProducesResponseType(typeof(List<Character>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<Character>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<List<Character>>), StatusCodes.Status500InternalServerError)]
        [HttpGet("{episodeId:int}")]
        public async Task<IActionResult> Get(int episodeId)
        {
            var response = await _rickAndMortyService.GetCharactersByEpisode(episodeId);

            if (response.StatusCode is >= 200 and < 300)
                return Ok(response.Data);
            else if (response.StatusCode is >= 400 and < 500)
                return NotFound(response);
            else
                return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        ///     Searches for episodes by name or episode code (partial or full).
        /// </summary>
        /// <param name="name">
        ///     (Optional) Partial or full name of the episode to filter by.
        /// </param>
        /// <param name="episode">
        ///     (Optional) Partial or full episode code (e.g. "S01E01") to filter by.
        /// </param>
        /// <returns>
        ///     A list of episodes matching the given filters (could be empty).
        /// </returns>
        /// <response code="200">Returns filtered list of episodes.</response>
        /// <response code="404">Episodes not found.</response>
        /// <response code="500">Error fetching episodes from external API.</response>
        [ProducesResponseType(typeof(List<Episode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<Episode>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<List<Episode>>), StatusCodes.Status500InternalServerError)]
        [HttpGet("search")]
        public async Task<IActionResult> SearchEpisodes([FromQuery] string? name, [FromQuery] string? episode)
        {
            var response = await _rickAndMortyService.SearchEpisodesAsync(name, episode);

            if (response.StatusCode is >= 200 and < 300)
                return Ok(response.Data);
            else if (response.StatusCode is >= 400 and < 500)
                return NotFound(response);
            else
                return StatusCode(response.StatusCode, response);
        }

    }
}
