namespace MyRentals.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyRentals.Domain.Repositories;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public class ClientRepository : IClientRepository
    {
        private readonly MyRentalsContext context;

        public ClientRepository(MyRentalsContext context)
        {
            this.context = context;
        }

        async Task<IEnumerable<Client>> IClientRepository.GetAll( CancellationToken token)
        {
            return await this.context.Clients.OrderBy(a => a.ClientName).ToArrayAsync(token);
        }

        Task IClientRepository.Create(Client client, CancellationToken cancellationToken)
        {
            this.context.Add(client);
            return this.context.SaveChangesAsync(cancellationToken);
        }

        async Task<OneOf<Success, NotFound>> IClientRepository.Update(Client client,
            CancellationToken cancellationToken)
        {
            if (this.context.Apartments != null && await this.context.Clients.AnyAsync(a => a.ClientId == client.ClientId, cancellationToken))
            {
                this.context.Update(client);
                await this.context.SaveChangesAsync(cancellationToken);
                return new Success();
            }
            else
            {
                return new NotFound();
            }
        }

        async Task<OneOf<Success, NotFound>> IClientRepository.Delete(ulong clientId, CancellationToken cancellationToken)
        {
            if (this.context.Apartments != null && await this.context.Clients.AnyAsync(a => a.ClientId == clientId, cancellationToken))
            {
                var found = await this.context.Clients.SingleAsync(a => a.ClientId == clientId,
                    cancellationToken);
                this.context.Remove(found);
                await this.context.SaveChangesAsync(cancellationToken);
                return new Success();
            }
            else
            {
                return new NotFound();
            }
        }
    }
}
