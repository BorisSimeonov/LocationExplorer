namespace LocationExplorer.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using static Infrastructure.DomainConstants;

    public class Gallery
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(GalleryNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(PhotographerNameMaxLength)]
        public string PhotographerName { get; set; }

        public bool IsPrivate { get; set; }

        public DateTime GalleryPeriodStart { get; set; }

        public DateTime GalleryPeriodEnd { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
