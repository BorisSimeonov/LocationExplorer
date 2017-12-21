namespace LocationExplorer.Service.Models.Destination
{
    using Common.Mapping;
    using Domain.Models;

    public class DestinationListingServiceModel : IMapFrom<Destination>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
