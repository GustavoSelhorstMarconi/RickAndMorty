namespace RickAndMorty.Domain.Entities
{
    public class Character
    {
        public Character(
        int id,
        string name,
        string status,
        string species,
        string type,
        string gender,
        Origin origin,
        Location location,
        string image,
        List<string> episode,
        string url,
        DateTime created)
        {
            Id = id;
            Name = name;
            Status = status;
            Species = species;
            Type = type;
            Gender = gender;
            Origin = origin;
            Location = location;
            Image = image;
            Episode = episode;
            Url = url;
            Created = created;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Species { get; set; }

        public string Type { get; set; }

        public string Gender { get; set; }

        public Origin Origin { get; set; }

        public Location Location { get; set; }

        public string Image { get; set; }

        public List<string> Episode { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }
    }
}
