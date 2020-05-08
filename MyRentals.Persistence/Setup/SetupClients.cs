namespace MyRentals.Persistence.Setup
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyRentals.Domain.StorageModels;

    public static class SetupClients
    {
        public static void Setup(EntityTypeBuilder<Client> entity)
        {
            entity.HasKey(e => e.ClientId);
            entity.Property(e => e.ClientId).ValueGeneratedNever().IsRequired();
            entity.Property(e => e.ClientName).IsRequired();
        }
    }
}