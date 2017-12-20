namespace LocationExplorer.Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.DomainConstants;

    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Region> Regions { get; set; } = new List<Region>();
    }
}
