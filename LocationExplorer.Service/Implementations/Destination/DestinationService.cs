using System;

namespace LocationExplorer.Service.Implementations.Destination
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Models;
    using Infrastructure;
    using Interfaces.Destination;
    using Interfaces.Region;
    using Interfaces.Tag;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Destination;

    public class DestinationService : IDestinationService
    {
        private readonly LocationExplorerDbContext database;

        private readonly IRegionService regionService;

        private readonly ITagService tagService;

        public DestinationService(
            LocationExplorerDbContext database,
            IRegionService regionService,
            ITagService tagService)
        {
            this.database = database;
            this.regionService = regionService;
            this.tagService = tagService;
        }

        public async Task<DestinationDetailsServiceModel> GetByIdAsync(int id)
            => await database.Destinations
                .Where(c => c.Id == id)
                .ProjectTo<DestinationDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<PagedDestinationListingServiceModel> AllDestinationsAsync(int page, int? itemsPerPage = null)
        {
            itemsPerPage = !itemsPerPage.HasValue || itemsPerPage < 1
                ? PagingConstants.DefaultItemsPerPage
                : itemsPerPage.Value;

            page = page < 1 ? 1 : page;

            return new PagedDestinationListingServiceModel
            {
                Destinations = await database.Regions
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * itemsPerPage.Value)
                    .Take(itemsPerPage.Value)
                    .ProjectTo<DestinationListingServiceModel>()
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = itemsPerPage.Value,
                    TotalItems = await database.Destinations.CountAsync()
                }
            };
        }

        public async Task<int> AddAsync(string name, int regionId, IList<int> tags)
        {
            var invalidTags = await tagService.CheckForInvalidTagIds(tags);

            if (await ExistsAsync(name) 
                || !await regionService.ExistsAsync(regionId)
                || invalidTags.Any())
            {
                return 0;
            }
            
            var destination = new Destination
            {
                Name = name,
                RegionId = regionId
            };

            await database.Destinations.AddAsync(destination);
            await database.SaveChangesAsync();

            foreach (var tagId in tags.Distinct())
            {
                if (!await database.DestinationTags.AnyAsync(d => d.DestinationId == destination.Id && d.TagId == tagId))
                {
                    await database.DestinationTags.AddAsync(new DestinationTag
                    {
                        DestinationId = destination.Id,
                        TagId = tagId
                    });
                }
            }
            await database.SaveChangesAsync();

            return destination.Id;
        }

        public async Task<IEnumerable<SelectListItem>> AllAsync()
            => await database.Destinations
                .OrderBy(d => d.Name)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.Name} ({d.Region.Name})"
                })
                .ToListAsync();

        public async Task<bool> ExistsAsync(string name)
            => await database.Destinations.AnyAsync(d => string.Equals(d.Name, name, StringComparison.InvariantCultureIgnoreCase));

        public async Task<bool> ExistsAsync(int id)
            => await database.Destinations.AnyAsync(d => d.Id == id);
    }
}
