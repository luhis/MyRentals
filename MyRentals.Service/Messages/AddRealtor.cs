namespace MyRentals.Service.Messages
{
    using MediatR;
    using MyRentals.Domain.StorageModels;

    public class AddRealtor : IRequest<ulong>
    {
        public AddRealtor(Realtor realtor)
        {
            this.Realtor = realtor;
        }

        public Realtor Realtor { get; }
    }
}