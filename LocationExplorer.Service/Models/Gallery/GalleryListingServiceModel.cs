namespace LocationExplorer.Service.Models.Gallery
{
    using Common.Mapping;
    using Domain.Models;

    public class GalleryListingServiceModel : IMapFrom<Gallery>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
