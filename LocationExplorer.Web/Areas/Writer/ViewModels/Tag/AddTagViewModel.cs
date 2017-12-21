namespace LocationExplorer.Web.Areas.Writer.ViewModels.Tag
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static Domain.Infrastructure.DomainConstants;

    public class AddTagViewModel
    {
        [Required]
        [MaxLength(TagNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<int> SelectedDestinations { get; set; }

        public IEnumerable<SelectListItem> Destinations { get; set; }
    }
}
