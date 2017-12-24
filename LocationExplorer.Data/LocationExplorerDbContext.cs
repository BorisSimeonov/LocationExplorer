namespace LocationExplorer.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Domain.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class LocationExplorerDbContext : IdentityDbContext<User>
    {
        public LocationExplorerDbContext(DbContextOptions<LocationExplorerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var applyConfigurationMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration", BindingFlags.Instance | BindingFlags.Public);

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        var applyConcreteMethod = applyConfigurationMethod.MakeGenericMethod(iface.GenericTypeArguments.First());
                        applyConcreteMethod.Invoke(builder, new object[] { Activator.CreateInstance(type) });
                        break;
                    }
                }
            }
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<DestinationTag> DestinationTags { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<Picture> Pictures { get; set; }
    }
}
