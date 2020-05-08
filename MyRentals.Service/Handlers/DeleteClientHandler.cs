namespace MyRentals.Service.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using OneOf;
    using OneOf.Types;

    public class DeleteClientHandler : IRequestHandler<DeleteClient, OneOf<Success, NotFound>>
    {
        private readonly IClientRepository clientRepository;

        public DeleteClientHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        Task<OneOf<Success, NotFound>> IRequestHandler<DeleteClient, OneOf<Success, NotFound>>.Handle(DeleteClient request, CancellationToken cancellationToken)
        {
            return this.clientRepository.Delete(request.ClientId, cancellationToken);
        }
    }
}