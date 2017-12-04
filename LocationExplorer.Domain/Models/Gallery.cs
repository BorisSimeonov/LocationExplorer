namespace LocationExplorer.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class Gallery
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhotographerName { get; set; }

        public bool IsPrivate { get; set; }

        public IEnumerable<User> SharedWith { get; set; }

        public DateTime GalleryPeriodStart { get; set; }

        public DateTime GalleryPeriodEnd { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
