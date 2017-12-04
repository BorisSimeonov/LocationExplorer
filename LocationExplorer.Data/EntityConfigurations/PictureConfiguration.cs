namespace LocationExplorer.Data.EntityConfigurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder
                .HasOne(p => p.Photographer)
                .WithMany(ph => ph.Pictures)
                .HasForeignKey(p => p.PhotographerId);

            builder
                .HasOne(p => p.Gallery)
                .WithMany(g => g.Pictures)
                .HasForeignKey(p => p.GalleryId);
        }
    }
}
