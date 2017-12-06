namespace LocationExplorer.Data.EntityConfigurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GalleryConfiguration : IEntityTypeConfiguration<Gallery>
    {
        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
            builder
                .HasAlternateKey(g => g.Name)
                .HasName("AlternateKey_GalleryName");

            builder
                .HasMany(g => g.Pictures)
                .WithOne(p => p.Gallery)
                .HasForeignKey(p => p.GalleryId);
        }
    }
}
