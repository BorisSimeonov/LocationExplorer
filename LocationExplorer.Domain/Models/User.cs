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
        [StringLength(MaxNamesLength)]
        public string FirstName { get; set; }

        [StringLength(MaxNamesLength)]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
