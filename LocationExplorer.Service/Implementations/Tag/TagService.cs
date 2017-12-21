namespace LocationExplorer.Service.Implementations.Tag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Models;
    using Infrastructure;
    using Interfaces.Tag;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Tag;

    public class TagService : ITagService
    {
        private readonly LocationExplorerDbContext database;

        public TagService(LocationExplorerDbContext database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<SelectListItem>> AllAsync()
            => await database.Tags
            .OrderBy(t => t.Name)
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            })
            .ToListAsync();

        public async Task<int> AddAsync(string name, IEnumerable<int> destinations)
        {
            if (await ExistsAsync(name))
            {
                return 0;
            }

            var tag = new Tag
            {
                Name = name
            };

            await database.Tags.AddAsync(tag);
            await database.SaveChangesAsync();

            if (tag.Id == 0)
            {
                return 0;
            }

            if (destinations != null)
            {
                foreach (var destinationId in destinations)
                {
                    if (await database.Destinations.AnyAsync(d => d.Id == destinationId))
                    {
                        await database.DestinationTags.AddAsync(new DestinationTag
                        {
                            DestinationId = destinationId,
                            TagId = tag.Id
                        });
                    }
                }

                await database.SaveChangesAsync();
            }
            
            return tag.Id;
        }

        public async Task<TagDetailsServiceModel> GetByIdAsync(int id)
            => await database.Tags
                .Where(t => t.Id == id)
                .ProjectTo<TagDetailsServiceModel>(new {tagId = id})
                .FirstOrDefaultAsync();

        public async Task<PagedTagListingServiceModel> AllTagsAsync(int page, int? itemsPerPage = null)
        {
            itemsPerPage = !itemsPerPage.HasValue || itemsPerPage < 1
                ? PagingConstants.DefaultItemsPerPage
                : itemsPerPage.Value;

            page = page < 1 ? 1 : page;

            return new PagedTagListingServiceModel
            {
                Tags = await database.Tags
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * itemsPerPage.Value)
                    .Take(itemsPerPage.Value)
                    .ProjectTo<TagListingServiceModel>()
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = itemsPerPage.Value,
                    TotalItems = await database.Tags.CountAsync()
                }
            };
        }

        public async Task<IList<int>> CheckForInvalidTagIds(IList<int> tagIdList)
        {
            if (tagIdList == null)
            {
                return new List<int>();
            }

            var invalidIdList = new List<int>();

            if (tagIdList != null && tagIdList.Any())
            {
                foreach (var tagId in tagIdList.Distinct())
                {
                    if (!await database.Tags.AnyAsync(t => t.Id == tagId))
                    {
                        invalidIdList.Add(tagId);
                    }
                }
            }

            return invalidIdList;
        }

        public async Task<bool> ExistsAsync(string name)
            => await database.Tags.AnyAsync(t => string.Equals(t.Name, name, StringComparison.InvariantCultureIgnoreCase));

        public async Task<bool> ExistsAsync(int id)
            => await database.Tags.AnyAsync(t => t.Id == id);
    }
}
