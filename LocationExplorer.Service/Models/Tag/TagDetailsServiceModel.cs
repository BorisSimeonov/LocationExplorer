namespace LocationExplorer.Service.Models.Tag
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;
    using Destination;
    using Domain.Models;

    public class TagDetailsServiceModel : IMapFrom<Tag>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public IEnumerable<DestinationListingServiceModel> Destinations { get; set; }


        public void ConfigureMapping(Profile mapper)
        {
            var tagId = 0;
            mapper.CreateMap<Tag, TagDetailsServiceModel>()
                .ForMember(t => t.Destinations, cfg => cfg.MapFrom(t => t.Destinations
                    .Where(d => d.TagId == tagId)
                    .Select(d => d.Destination)));
        }
    }
}
