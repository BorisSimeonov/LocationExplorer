namespace LocationExplorer.Service.Test.TestHelpers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.AdminArea;
    using Models.Country;

    public static class TestHelper
    {
        private static bool MapperInitialized;

        public static readonly List<User> GetUsersSeedList
            = new List<User>
            {
                new User { Id = "11", UserName = "User1", FirstName = "FirstName1" },
                new User { Id = "15", UserName = "User7", FirstName = "FirstName7" },
                new User { Id = "13", UserName = "User2", FirstName = "FirstName2" },
                new User { Id = "18", UserName = "User8", FirstName = "FirstName8" },
                new User { Id = "12", UserName = "User3", FirstName = "FirstName3" },
                new User { Id = "14", UserName = "User4", FirstName = "FirstName4" },
                new User { Id = "16", UserName = "User6", FirstName = "FirstName6" },
                new User { Id = "17", UserName = "User5", FirstName = "FirstName5" },
                new User { Id = "19", UserName = "User9", FirstName = "FirstName9" }
            };

        public static readonly List<Country> GetCountrySeedList
            = new List<Country>
            {
                new Country { Id = 11, Name = "Country1" },
                new Country { Id = 15, Name = "Country7" },
                new Country { Id = 13, Name = "Country2" },
                new Country { Id = 18, Name = "Country8" },
                new Country { Id = 12, Name = "Country3" },
                new Country { Id = 14, Name = "Country4" },
                new Country { Id = 16, Name = "Country6" },
                new Country { Id = 17, Name = "Country5" },
                new Country { Id = 19, Name = "Country9" }
            };

        public static void InitializeMapper()
        {
            if (!MapperInitialized)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<User, UserListingModelService>();
                    cfg.CreateMap<Country, CountryListingServiceModel>();
                });

                MapperInitialized = true;
            }
        }

        public static LocationExplorerDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<LocationExplorerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var context = new LocationExplorerDbContext(options);
            context.Users.AddRange(new List<User>(GetUsersSeedList));
            context.Countries.AddRange(new List<Country>(GetCountrySeedList));
            context.SaveChanges();

            return context;
        }
    }
}
