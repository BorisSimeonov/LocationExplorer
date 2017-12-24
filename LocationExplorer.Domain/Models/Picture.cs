namespace LocationExplorer.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.DomainConstants;

    public class Picture
    {
        public string Id { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ContentType { get; set; }

        public int GalleryId { get; set; }

        public Gallery Gallery { get; set; }
    }
}
