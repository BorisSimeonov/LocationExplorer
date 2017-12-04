namespace LocationExplorer.Data.EntityConfigurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder
                .HasOne(r => r.Country)
                .WithMany(c => c.Regions)
                .HasForeignKey(r => r.CountryId);

            builder
                .HasMany(r => r.Destinations)
                .WithOne(d => d.Region)
                .HasForeignKey(d => d.RegionId);

            builder
                .HasMany(r => r.Articles)
                .WithOne(a => a.Region)
                .HasForeignKey(a => a.RegionId);

        }
    }
}
