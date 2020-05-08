namespace MyRentals.Service.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using OneOf;
    using OneOf.Types;

    public class GetRealtorIdHandler : IRequestHandler<GetRealtorId, OneOf<ulong, None>>
    {
        private readonly IRealtorRepository realtorRepository;

        public GetRealtorIdHandler(IRealtorRepository realtorRepository)
        {
            this.realtorRepository = realtorRepository;
        }

        async Task<OneOf<ulong, None>> IRequestHandler<GetRealtorId, OneOf<ulong, None>>.Handle(GetRealtorId request, CancellationToken cancellationToken)
        {
            var r = await this.realtorRepository.GetByEmail(request.Email, cancellationToken);
            return r.Match<OneOf<ulong, None>>(some => some.RealtorId, _ => new None());
        }
    }
}