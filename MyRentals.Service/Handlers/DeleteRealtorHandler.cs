namespace MyRentals.Service.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using OneOf;
    using OneOf.Types;

    public class DeleteRealtorHandler : IRequestHandler<DeleteRealtor, OneOf<Success, NotFound>>
    {
        private readonly IRealtorRepository realtorRepository;

        public DeleteRealtorHandler(IRealtorRepository realtorRepository)
        {
            this.realtorRepository = realtorRepository;
        }

        Task<OneOf<Success, NotFound>> IRequestHandler<DeleteRealtor, OneOf<Success, NotFound>>.Handle(DeleteRealtor request, CancellationToken cancellationToken)
        {
            return this.realtorRepository.Delete(request.RealtorId, cancellationToken);
        }
    }
}