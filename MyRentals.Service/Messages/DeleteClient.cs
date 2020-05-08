namespace MyRentals.Service.Messages
{
    using MediatR;
    using OneOf;
    using OneOf.Types;

    public class DeleteClient : IRequest<OneOf<Success, NotFound>>
    {
        public DeleteClient(ulong clientId)
        {
            this.ClientId = clientId;
        }

        public ulong ClientId { get; }
    }
}
