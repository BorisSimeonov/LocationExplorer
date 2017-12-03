namespace LocationExplorer.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using static Domain.Infrastructure.DomainConstants;

    public class LoginViewModel
    {
        [Required]
        [MaxLength(MaxNamesLength)]
        [MinLength(MinNamesLength)]
        [RegularExpression("[a-zA-Z]+",
            ErrorMessage = "Username must contain only letters")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
