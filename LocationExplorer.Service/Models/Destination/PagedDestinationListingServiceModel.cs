namespace LocationExplorer.Service.Models.Destination
{
    using System.Collections.Generic;
    using Infrastructure;

    public class PagedDestinationListingServiceModel
    {
        public IEnumerable<DestinationListingServiceModel> Destinations { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
