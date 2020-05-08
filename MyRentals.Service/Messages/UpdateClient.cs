namespace MyRentals.Service.Messages
{
    using MediatR;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public class UpdateClient : IRequest<OneOf<Success, NotFound>>
    {
        public UpdateClient(Client client)
        {
            this.Client = client;
        }

        public Client Client { get; }
    }
}