namespace LocationExplorer.Domain.Models
{
    using System;

    public class Picture
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime DateTaken { get; set; }

        public int? PhotographerId { get; set; }

        public User Photographer { get; set; }

        public int GalleryId { get; set; }

        public Gallery Gallery { get; set; }
    }
}
