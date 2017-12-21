namespace LocationExplorer.Service.Models.Destination
{
    using System.Collections.Generic;
    using AutoMapper;
    using Common.Mapping;
    using Domain.Models;
    using Tag;

    public class DestinationDetailsServiceModel : IMapFrom<Destination>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public IEnumerable<TagListingServiceModel> Tags { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper.CreateMap<Destination, DestinationDetailsServiceModel>()
                .ForMember(d => d.RegionName, cfg => cfg.MapFrom(d => d.Region.Name));
    }
}
