using System.Text.Json.Serialization;

namespace RickAndMorty.Domain.Entities
{
    public class Episode
    {
        public Episode(
        int id,
        string name,
        string airDate,
        string episodeCode,
        List<string> characters,
        string url,
        DateTime created)
        {
            Id = id;
            Name = name;
            AirDate = airDate;
            EpisodeCode = episodeCode;
            Characters = characters;
            Url = url;
            Created = created;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("air_date")]
        public string AirDate { get; set; }

        [JsonPropertyName("episode")]
        public string EpisodeCode { get; set; }

        public List<string> Characters { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }
    }
}
