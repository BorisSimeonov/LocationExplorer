namespace LocationExplorer.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class Article
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public bool ApprovedBySupport { get; set; }

        public DateTime CreationDate { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        public int DestinationId { get; set; }

        public Destination Destination { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public IEnumerable<Gallery> Galleries { get; set; }
    }
}
