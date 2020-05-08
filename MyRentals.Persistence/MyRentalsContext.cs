namespace MyRentals.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Persistence.Setup;

    public class MyRentalsContext : DbContext
    {
        public MyRentalsContext(DbContextOptions<MyRentalsContext> options)
            : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Apartment>? Apartments { get; private set; }

        public DbSet<Client>? Clients { get; private set; }

        public DbSet<Realtor>? Realtors { get; private set; }

        public void SeedDatabase()
        {
            if (this.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                this.Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupApartments.Setup(modelBuilder.Entity<Apartment>());
            SetupClients.Setup(modelBuilder.Entity<Client>());
            SetupRealtor.Setup(modelBuilder.Entity<Realtor>());
        }
    }
}
