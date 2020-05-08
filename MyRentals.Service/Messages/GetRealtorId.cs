namespace MyRentals.Service.Messages
{
    using MediatR;
    using OneOf;
    using OneOf.Types;

    public class GetRealtorId : IRequest<OneOf<ulong, None>>
    {
        public GetRealtorId(string email)
        {
            this.Email = email;
        }

        public string Email { get; }
    }
}