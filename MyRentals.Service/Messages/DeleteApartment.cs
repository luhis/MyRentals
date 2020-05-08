namespace MyRentals.Service.Messages
{
    using MediatR;
    using OneOf;
    using OneOf.Types;

    public class DeleteApartment : IRequest<OneOf<Success, NotFound>>
    {
        public DeleteApartment(ulong apartmentId)
        {
            this.ApartmentId = apartmentId;
        }

        public ulong ApartmentId { get; }
    }
}