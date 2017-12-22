namespace LocationExplorer.Service.Test.TestHelpers
{
    using AutoMapper;
    using Data;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.AdminArea;
    using Models.Country;
    using System;
    using System.Collections.Generic;

    public static class TestHelper
    {
        private static bool MapperInitialized;

        public static void InitializeMapper()
        {
            if (!MapperInitialized)
            {
                try
                {
                    Mapper.Initialize(cfg =>
                    {
                        AddMappingIfMissing<User, UserListingModelService>(cfg);
                        AddMappingIfMissing<Country, CountryListingServiceModel>(cfg);
                    });
                }
                catch { }
                
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
            context.Users.AddRange(new List<User>(TestHelperDataProvider.GetUsersSeedList));
            context.Countries.AddRange(new List<Country>(TestHelperDataProvider.GetCountrySeedList));
            context.SaveChanges();

            return context;
        }

        private static void AddMappingIfMissing<TSource, TDestination>(IMapperConfigurationExpression cfg)
        {
                cfg.CreateMap<TSource, TDestination>();
        }
    }
}
