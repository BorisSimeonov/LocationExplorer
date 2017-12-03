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
    }
}
