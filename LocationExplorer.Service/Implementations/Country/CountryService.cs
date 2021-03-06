﻿namespace LocationExplorer.Service.Implementations.Country
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Models;
    using Infrastructure;
    using Interfaces.Country;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Country;

    public class CountryService : ICountryService
    {
        private readonly LocationExplorerDbContext database;

        public CountryService(LocationExplorerDbContext database)
        {
            this.database = database;
        }

        public async Task<CountryDetailsServiceModel> GetByIdAsync(int id)
            => await database.Countries
                .Where(c => c.Id == id)
                .ProjectTo<CountryDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<PagedCountryListingServiceModel> AllCountriesAsync(int page, int? itemsPerPage = null)
        {
            itemsPerPage = !itemsPerPage.HasValue || itemsPerPage < 1
                ? PagingConstants.DefaultItemsPerPage
                : itemsPerPage.Value;

            page = page < 1 ? 1 : page;

            return new PagedCountryListingServiceModel
            {
                Countries = await database.Countries
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * itemsPerPage.Value)
                    .Take(itemsPerPage.Value)
                    .ProjectTo<CountryListingServiceModel>()
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = itemsPerPage.Value,
                    TotalItems = await database.Countries.CountAsync()
                }
            };
        }

        public async Task<IEnumerable<SelectListItem>> AllAsync()
            => await database.Countries
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync();

        public async Task<int> AddAsync(string name)
        {
            if (await ExistsAsync(name))
            {
                return 0;
            }

            var country = new Country
            {
                Name = name
            };

            await database.Countries.AddAsync(country);
            await database.SaveChangesAsync();
            return country.Id;
        }

        public async Task<bool> ExistsAsync(string name)
            => await database.Countries.AnyAsync(c => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase));

        public async Task<bool> ExistsAsync(int id)
            => await database.Countries.AnyAsync(c => c.Id == id);

        public async Task<bool> DeleteAsync(int id)
        {
            var country = await database.Countries.Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (country != null)
            {
                database.Countries.Remove(country);
                await database.SaveChangesAsync();
            }

            return true;
        }
    }
}
