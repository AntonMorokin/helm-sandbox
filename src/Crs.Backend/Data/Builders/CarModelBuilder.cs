using Crs.Backend.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crs.Backend.Data.Builders
{
    internal sealed class CarModelBuilder : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Brand).IsRequired();
            builder.Property(x => x.Model).IsRequired();
            builder.Property(x => x.Mileage).IsRequired();

            builder.HasIndex(x => x.Number).IsUnique();
        }
    }
}
