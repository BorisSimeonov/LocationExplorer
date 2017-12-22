namespace LocationExplorer.Web.Areas.Writer.ViewModels.Destination
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static Domain.Infrastructure.DomainConstants;
    using static Infrastructure.WebConstants;

    public class AddDestinationViewModel
    {
        [Required]
        [MaxLength(DestinationNameMaxLength)]
        [MinLength(DestinationNameMinLength)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }

        [MaxIntCollectionSize(DestinationMaxTagCount, ErrorMessage = InvalidTagsCountErrorMessage)]
        [Display(Name = "Selected Tags")]
        public IList<int> SelectedTags { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }
    }
}
