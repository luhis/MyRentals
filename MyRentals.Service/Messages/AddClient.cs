namespace MyRentals.Service.Messages
{
    using MediatR;
    using MyRentals.Domain.StorageModels;

    public class AddClient : IRequest<ulong>
    {
        public AddClient(Client client)
        {
            this.Client = client;
        }

        public Client Client { get; }
    }
}