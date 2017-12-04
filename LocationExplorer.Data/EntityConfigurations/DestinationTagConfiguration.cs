namespace LocationExplorer.Data.EntityConfigurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DestinationTagConfiguration : IEntityTypeConfiguration<DestinationTag>
    {
        public void Configure(EntityTypeBuilder<DestinationTag> builder)
        {
            builder
                .HasKey(dt => new {dt.DestinationId, dt.TagId});

            builder
                .HasOne(dt => dt.Destination)
                .WithMany(d => d.Tags)
                .HasForeignKey(dt => dt.DestinationId);

            builder
                .HasOne(dt => dt.Tag)
                .WithMany(d => d.Destinations)
                .HasForeignKey(dt => dt.TagId);
        }
    }
}
