namespace MyRentals.Service.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Service.Messages;

    public class GetClientsHandler : IRequestHandler<GetClients, IEnumerable<Client>>
    {
        private readonly IClientRepository clientRepository;

        public GetClientsHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        Task<IEnumerable<Client>> IRequestHandler<GetClients, IEnumerable<Client>>.Handle(GetClients request, CancellationToken cancellationToken)
        {
            return this.clientRepository.GetAll(cancellationToken);
        }
    }
}