namespace LocationExplorer.Web.Areas.Writer.ViewModels.Country
{
    using System.ComponentModel.DataAnnotations;
    using static Domain.Infrastructure.DomainConstants;
    using static Infrastructure.ViewModelConstants.CountryConstants;

    public class AddCountryViewModel
    {
        [Required]
        [MaxLength(CountryNameMaxLength)]
        [Display(Name = "Country Name:")]
        [RegularExpression(CountryNameRegex, 
            ErrorMessage = CountryNameRegexErrorMessage)]
        public string Name { get; set; }
    }
}
