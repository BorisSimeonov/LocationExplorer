namespace LocationExplorer.Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.DomainConstants;

    public class Picture
    {
        public int Id { get; set; }

        [MaxLength(PictureNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(PictureLocationNameMaxLength)]
        public string Location { get; set; }

        [MaxLength(PictureDescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime DateTaken { get; set; }

        public int GalleryId { get; set; }

        public Gallery Gallery { get; set; }
    }
}
