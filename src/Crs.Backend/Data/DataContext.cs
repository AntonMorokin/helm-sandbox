using Crs.Backend.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Crs.Backend.Data
{
    public sealed class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Ride> Rides { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseDatabaseTemplate("template0")
                .UseCollation("ru_RU.utf8")
                .HasDefaultSchema("crs");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Builders.ClientModelBuilder).Assembly);
        }
    }
}
