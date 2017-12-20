namespace LocationExplorer.Service.Models.Region
{
    using System.Collections.Generic;
    using Infrastructure;

    public class PagedRegionListingServiceModel
    {
        public IEnumerable<RegionListingServiceModel> Regions { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
