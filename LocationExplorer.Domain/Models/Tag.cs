namespace LocationExplorer.Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.DomainConstants;

    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TagNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<DestinationTag> Destinations { get; set; } = new List<DestinationTag>();
    }
}
