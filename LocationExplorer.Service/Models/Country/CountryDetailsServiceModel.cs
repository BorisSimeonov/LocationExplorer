namespace LocationExplorer.Service.Models.Country
{
    using System.Collections.Generic;
    using Common.Mapping;
    using Domain.Models;
    using Region;

    public class CountryDetailsServiceModel : IMapFrom<Country>
    {
        public string Name { get; set; }

        public IEnumerable<RegionListingServiceModel> Regions { get; set; }
    }
}
