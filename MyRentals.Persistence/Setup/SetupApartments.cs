namespace MyRentals.Persistence.Setup
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyRentals.Domain.StorageModels;

    public static class SetupApartments
    {
        public static void Setup(EntityTypeBuilder<Apartment> entity)
        {
            entity.HasKey(e => e.ApartmentId);
            entity.Property(e => e.ApartmentId).ValueGeneratedNever().IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.FloorArea).IsRequired();
            entity.Property(e => e.PricePerMonth).IsRequired();
            entity.Property(e => e.NumberOfRooms).IsRequired();
            entity.Property(e => e.DateAdded).HasDefaultValueSql("(getdate())").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
            entity.Property(e => e.IsRented).IsRequired();
            entity.Property(e => e.Lat).IsRequired();
            entity.Property(e => e.Lon).IsRequired();
            entity.HasOne<Realtor>().WithMany().HasForeignKey(p => p.RealtorId);
        }
    }
}
