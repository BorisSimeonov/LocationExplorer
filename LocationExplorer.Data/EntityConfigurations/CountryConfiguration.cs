namespace LocationExplorer.Data.EntityConfigurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasMany(c => c.Regions)
                .WithOne(r => r.Country)
                .HasForeignKey(r => r.CountryId);
        }
    }
}
