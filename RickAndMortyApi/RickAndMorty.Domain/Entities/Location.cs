namespace RickAndMorty.Domain.Entities
{
    public class Location
    {
        public Location(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
