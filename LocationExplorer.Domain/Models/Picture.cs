namespace LocationExplorer.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.DomainConstants;

    public class Picture
    {
        public string Id { get; set; }

        [MaxLength(PictureLocationNameMaxLength)]
        public string Location { get; set; }

        public string ContentType { get; set; }

        public int GalleryId { get; set; }

        public Gallery Gallery { get; set; }
    }
}
