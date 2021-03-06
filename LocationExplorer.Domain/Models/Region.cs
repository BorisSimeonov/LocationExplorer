﻿namespace LocationExplorer.Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.DomainConstants;

    public class Region
    {
        public int Id { get; set; }

        [Required]
        [StringLength(RegionNameMaxLength)]
        public string Name { get; set; }

        [StringLength(RegionDescriptionMaxLength)]
        public string Description { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public IEnumerable<Destination> Destinations { get; set; } = new List<Destination>();
    }
}
