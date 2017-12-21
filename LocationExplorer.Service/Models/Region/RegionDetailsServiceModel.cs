namespace LocationExplorer.Service.Models.Region
{
    using System.Collections.Generic;
    using AutoMapper;
    using Common.Mapping;
    using Destination;
    using Domain.Models;

    public class RegionDetailsServiceModel : IMapFrom<Region>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<DestinationListingServiceModel> Destinations { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper.CreateMap<Region, RegionDetailsServiceModel>()
                .ForMember(r => r.CountryName, cfg => cfg.MapFrom(r => r.Country.Name));
    }
}
