namespace MyRentals.Persistence.Setup
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyRentals.Domain.StorageModels;

    public static class SetupRealtor
    {
        public static void Setup(EntityTypeBuilder<Realtor> entity)
        {
            entity.HasKey(e => e.RealtorId);
            entity.Property(e => e.RealtorId).ValueGeneratedNever().IsRequired();
            entity.Property(e => e.RealtorName).IsRequired();
        }
    }
}