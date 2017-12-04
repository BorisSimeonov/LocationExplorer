namespace LocationExplorer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static Infrastructure.DomainConstants;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(MaxNamesLength)]
        public string FirstName { get; set; }

        [MaxLength(MaxNamesLength)]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }
    }
}
