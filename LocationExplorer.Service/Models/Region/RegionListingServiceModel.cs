namespace LocationExplorer.Service.Models.Region
{
    using Common.Mapping;
    using Domain.Models;

    public class RegionListingServiceModel : IMapFrom<Region>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
