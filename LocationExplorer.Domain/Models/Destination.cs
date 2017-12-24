namespace LocationExplorer.Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.DomainConstants;

    public class Destination
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DestinationNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<DestinationTag> Tags { get; set; } = new List<DestinationTag>();

        public IEnumerable<Article> Articles { get; set; } = new List<Article>();

        public int RegionId { get; set; }

        public Region Region { get; set; }
    }
}
