namespace LocationExplorer.Service.Implementations.Region
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Models;
    using Infrastructure;
    using Interfaces.Region;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Region;

    public class RegionService : IRegionService
    {
        private readonly LocationExplorerDbContext database;

        public RegionService(LocationExplorerDbContext database)
        {
            this.database = database;
        }

        public async Task<RegionDetailsServiceModel> GetByIdAsync(int id)
            => await database.Regions
                .Where(c => c.Id == id)
                .ProjectTo<RegionDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<PagedRegionListingServiceModel> AllRegionsAsync(int page, int? itemsPerPage = null)
        {
            itemsPerPage = !itemsPerPage.HasValue || itemsPerPage < 1
                ? PagingConstants.DefaultItemsPerPage
                : itemsPerPage.Value;

            page = page < 1 ? 1 : page;

            return new PagedRegionListingServiceModel
            {
                Regions = await database.Regions
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * itemsPerPage.Value)
                    .Take(itemsPerPage.Value)
                    .ProjectTo<RegionListingServiceModel>()
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = itemsPerPage.Value,
                    TotalItems = await database.Regions.CountAsync()
                }
            };
        }

        public async Task<int> AddAsync(string name, string description, int countryId)
        {
            if (await ExistsAsync(name))
            {
                return 0;
            }

            var region = new Region
            {
                Name = name,
                Description = description,
                CountryId = countryId
            };

            await database.Regions.AddAsync(region);
            await database.SaveChangesAsync();
            return region.Id;
        }

        public async Task<IEnumerable<SelectListItem>> AllAsync()
            => await database.Regions
                .OrderBy(r => r.Name)
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Name} ({r.Country.Name})"
                })
                .ToListAsync();

        public async Task<bool> ExistsAsync(string name)
            => await database.Regions.AnyAsync(c => c.Name == name);

        public async Task<bool> ExistsAsync(int id)
            => await database.Regions.AnyAsync(c => c.Id == id);
    }
}
