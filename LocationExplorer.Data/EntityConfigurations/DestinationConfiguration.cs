namespace LocationExplorer.Data.EntityConfigurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder
                .HasAlternateKey(d => d.Name)
                .HasName("AlternateKey_DestinationName");

            builder
                .HasMany(d => d.Articles)
                .WithOne(a => a.Destination)
                .HasForeignKey(a => a.DestinationId);

            builder
                .HasOne(d => d.Region)
                .WithMany(r => r.Destinations)
                .HasForeignKey(r => r.RegionId);
        }
    }
}
