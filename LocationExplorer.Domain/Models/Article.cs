namespace LocationExplorer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.DomainConstants;

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ArticelTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ArticleContentMaxLength)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int DestinationId { get; set; }

        public Destination Destination { get; set; }
        
        public IEnumerable<Gallery> Galleries { get; set; } = new List<Gallery>();
    }
}
