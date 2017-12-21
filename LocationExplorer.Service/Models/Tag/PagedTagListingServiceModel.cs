namespace LocationExplorer.Service.Models.Tag
{
    using System.Collections.Generic;
    using Infrastructure;

    public class PagedTagListingServiceModel
    {
        public IEnumerable<TagListingServiceModel> Tags { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
