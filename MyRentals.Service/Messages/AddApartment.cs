namespace MyRentals.Service.Messages
{
    using MediatR;
    using MyRentals.Domain.StorageModels;

    public class AddApartment : IRequest<ulong>
    {
        public AddApartment(Apartment apartment)
        {
            this.Apartment = apartment;
        }

        public Apartment Apartment { get; }
    }
}
