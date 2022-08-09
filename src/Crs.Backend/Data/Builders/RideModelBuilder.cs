using Crs.Backend.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crs.Backend.Data.Builders
{
    internal sealed class RideModelBuilder : IEntityTypeConfiguration<Ride>
    {
        public void Configure(EntityTypeBuilder<Ride> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.Mileage).IsRequired(false);
            builder.Property(x => x.Status).IsRequired().HasConversion<string>();

            builder
                .HasOne(x => x.Car)
                .WithMany(x => x.Rides)
                .HasForeignKey(x => x.CarId)
                .IsRequired();

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.Rides)
                .HasForeignKey(x => x.ClientId)
                .IsRequired();
        }
    }
}
