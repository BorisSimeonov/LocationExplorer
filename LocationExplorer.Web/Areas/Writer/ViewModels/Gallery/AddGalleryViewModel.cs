namespace LocationExplorer.Web.Areas.Writer.ViewModels.Gallery
{
    using System.ComponentModel.DataAnnotations;
    using static Domain.Infrastructure.DomainConstants;

    public class AddGalleryViewModel
    {
        public int ArticleId { get; set; }

        [Required]
        [MaxLength(GalleryNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(PhotographerNameMaxLength)]
        [Display(Name = "Photographer Name")]
        public string PhotographerName { get; set; }
    }
}
