namespace MyRentals.Service.Messages
{
    using MediatR;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public class UpdateApartment : IRequest<OneOf<Success, NotFound>>
    {
        public UpdateApartment(ulong apartmentId,Apartment apartment)
        {
            this.ApartmentId = apartmentId;
            this.Apartment = apartment;
        }

        public ulong ApartmentId { get; }
        public Apartment Apartment { get; }
    }
}
