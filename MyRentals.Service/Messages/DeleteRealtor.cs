namespace MyRentals.Service.Messages
{
    using MediatR;
    using OneOf;
    using OneOf.Types;

    public class DeleteRealtor : IRequest<OneOf<Success, NotFound>>
    {
        public DeleteRealtor(ulong realtorId)
        {
            this.RealtorId = realtorId;
        }

        public ulong RealtorId { get; }
    }
}