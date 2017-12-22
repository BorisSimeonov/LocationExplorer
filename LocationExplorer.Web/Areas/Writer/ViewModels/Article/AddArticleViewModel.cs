namespace LocationExplorer.Web.Areas.Writer.ViewModels.Article
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static Domain.Infrastructure.DomainConstants;

    public class AddArticleViewModel
    {

        [Required]
        [MaxLength(ArticelTitleMaxLength)]
        [MinLength(ArticelTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ArticleContentMaxLength)]
        public string Content { get; set; }

        [Display(Name = "Destination")]
        public int DestinationId { get; set; }

        public IEnumerable<SelectListItem> Destinations { get; set; }
    }
}
