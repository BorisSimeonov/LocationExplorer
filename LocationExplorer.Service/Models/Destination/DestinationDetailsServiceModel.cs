namespace LocationExplorer.Service.Models.Destination
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DestinationDetailsServiceModel : IMapFrom<Destination>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Destination, DestinationDetailsServiceModel>()
                .ForMember(d => d.RegionName, cfg => cfg.MapFrom(d => d.Region.Name));

            int destinationId = 0;

            mapper.CreateMap<Destination, DestinationDetailsServiceModel>()
                .ForMember(d => d.Tags,
                    cfg => cfg.MapFrom(d => d.Tags
                        .Where(dt => dt.DestinationId == destinationId)
                        .Select(dt => new SelectListItem
                        {
                            Value = dt.Tag.Id.ToString(),
                            Text = dt.Tag.Name
                        })));
        }
    }
}
