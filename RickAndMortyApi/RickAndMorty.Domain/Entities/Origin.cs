namespace RickAndMorty.Domain.Entities
{
    public class Origin
    {
        public Origin(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
