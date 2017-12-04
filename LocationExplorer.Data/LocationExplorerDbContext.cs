namespace LocationExplorer.Data
{
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
