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
                .HasAlternateKey(r => r.Name)
                .HasName("AlternateKey_RegionName");

            builder
                .HasMany(r => r.Destinations)
                .WithOne(d => d.Region)
                .HasForeignKey(d => d.RegionId);
        }
    }
}
