namespace LocationExplorer.Service.Models.Country
{
    using System.Collections.Generic;
    using Infrastructure;

    public class PagedCountryListingServiceModel
    {
        public IEnumerable<CountryListingServiceModel> Countries { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
