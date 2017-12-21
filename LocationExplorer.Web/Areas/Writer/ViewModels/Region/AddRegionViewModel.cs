namespace LocationExplorer.Web.Areas.Writer.ViewModels.Region
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static Domain.Infrastructure.DomainConstants;

    public class AddRegionViewModel
    {
        [Required]
        [MaxLength(RegionNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(RegionDescriptionMaxLength)]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries;
    }
}
