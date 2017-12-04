namespace LocationExplorer.Domain.Models
{
    using System.Collections.Generic;

    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public IEnumerable<Destination> Destinations { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
