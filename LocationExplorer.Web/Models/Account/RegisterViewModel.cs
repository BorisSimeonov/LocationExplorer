namespace LocationExplorer.Web.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Domain.Infrastructure.DomainConstants;

    public class RegisterViewModel
    {
        [Required]
        [MaxLength(MaxNamesLength)]
        [MinLength(MinNamesLength)]
        [RegularExpression("[a-zA-Z]+", 
            ErrorMessage = "Username must contain only letters")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(MaxNamesLength)]
        [MinLength(MinNamesLength)]
        [RegularExpression("[a-zA-Z]+(-[a-zA-Z]+){0,}", 
            ErrorMessage = "First name must contain only letter and '-' character.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(MaxNamesLength)]
        [MinLength(MinNamesLength)]
        [RegularExpression("[a-zA-Z]+(-[a-zA-Z]+){0,}", 
            ErrorMessage = "Last name must contain only letter and '-' character.")]
        public string LastName { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(PhoneNumberMaxLength)]
        [MinLength(PhoneNumberMinLength)]
        [RegularExpression("\\+?([0-9]{2,5}[ -]{1})+[0-9]+", 
            ErrorMessage = "Phone number must contain only digits, '+' and '-' in format +1234-567-890")]
        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(UserPasswordMaxLength, 
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", 
            MinimumLength = UserPasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
