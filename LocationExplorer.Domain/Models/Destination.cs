namespace LocationExplorer.Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Destination
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<DestinationTag> Tags { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }
    }
}
