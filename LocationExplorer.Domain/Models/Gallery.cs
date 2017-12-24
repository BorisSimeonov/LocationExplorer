namespace LocationExplorer.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using static Infrastructure.DomainConstants;

    public class Gallery
    {
        public int Id { get; set; }

        [Required]
        [StringLength(GalleryNameMaxLength)]
        public string Name { get; set; }

        [StringLength(PhotographerNameMaxLength)]
        public string PhotographerName { get; set; }

        public bool IsPrivate { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
