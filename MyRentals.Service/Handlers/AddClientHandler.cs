namespace MyRentals.Service.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using UniqueIdGenerator.Net;

    public class AddClientHandler : IRequestHandler<AddClient, ulong>
    {
        private readonly IClientRepository clientRepository;
        private static readonly Generator Generator = new Generator(0, new DateTime(2020, 04, 17));

        public AddClientHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        async Task<ulong> IRequestHandler<AddClient, ulong>.Handle(AddClient request, CancellationToken cancellationToken)
        {
            var apt = request.Client;
            var id = Generator.NextLong();
            apt.SetId(id);
            await this.clientRepository.Create(apt, cancellationToken);
            return id;
        }
    }
}