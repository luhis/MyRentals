namespace MyRentals.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll(CancellationToken token);

        Task Create(Client client, CancellationToken cancellationToken);

        Task<OneOf<Success, NotFound>> Update(Client client, CancellationToken cancellationToken);

        Task<OneOf<Success, NotFound>> Delete(ulong clientId, CancellationToken cancellationToken);
    }
}