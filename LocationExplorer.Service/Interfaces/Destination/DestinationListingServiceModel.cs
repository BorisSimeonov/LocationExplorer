namespace LocationExplorer.Service.Interfaces.Destination
{
    using Common.Mapping;
    using Domain.Models;

    public class DestinationListingServiceModel : IMapFrom<Destination>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
