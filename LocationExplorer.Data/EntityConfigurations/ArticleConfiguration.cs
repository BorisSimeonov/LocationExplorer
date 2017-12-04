namespace LocationExplorer.Data.EntityConfigurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                .HasMany(a => a.Galleries)
                .WithOne(g => g.Article)
                .HasForeignKey(g => g.ArticleId);

            builder
                .HasOne(a => a.Destination)
                .WithMany(d => d.Articles)
                .HasForeignKey(a => a.DestinationId);

            builder
                .HasOne(a => a.Author)
                .WithMany(au => au.Articles)
                .HasForeignKey(a => a.AuthorId);
        }
    }
}
