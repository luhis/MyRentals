namespace MyRentals.Service.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using OneOf;
    using OneOf.Types;

    public class UpdateClientHandler : IRequestHandler<UpdateClient, OneOf<Success, NotFound>>
    {
        private readonly IClientRepository apartmentRepository;

        public UpdateClientHandler(IClientRepository apartmentRepository)
        {
            this.apartmentRepository = apartmentRepository;
        }

        Task<OneOf<Success, NotFound>> IRequestHandler<UpdateClient, OneOf<Success, NotFound>>.Handle(UpdateClient request, CancellationToken cancellationToken)
        {
            return this.apartmentRepository.Update(request.Client, cancellationToken);
        }
    }
}