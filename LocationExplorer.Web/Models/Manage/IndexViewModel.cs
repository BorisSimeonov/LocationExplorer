namespace LocationExplorer.Web.Models.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Domain.Infrastructure.DomainConstants;

    public class IndexViewModel
    {
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(MaxNamesLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(MaxNamesLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string StatusMessage { get; set; }
    }
}
