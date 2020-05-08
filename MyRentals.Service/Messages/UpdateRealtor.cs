namespace MyRentals.Service.Messages
{
    using MediatR;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public class UpdateRealtor : IRequest<OneOf<Success, NotFound>>
    {
        public UpdateRealtor(ulong realtorId, Realtor realtor)
        {
            this.RealtorId = realtorId;
            this.Realtor = realtor;
        }

        public ulong RealtorId { get; }
        public Realtor Realtor { get; }
    }
}