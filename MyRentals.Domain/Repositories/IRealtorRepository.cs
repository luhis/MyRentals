namespace MyRentals.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public interface IRealtorRepository
    {

        Task<IEnumerable<Realtor>> GetAll(CancellationToken token);

        Task Create(Realtor realtor, CancellationToken cancellationToken);

        Task<OneOf<Success, NotFound>> Update(Realtor realtor, CancellationToken cancellationToken);

        Task<OneOf<Success, NotFound>> Delete(ulong realtorId, CancellationToken cancellationToken);

        Task<OneOf<Realtor, NotFound>> GetByEmail(string email, CancellationToken cancellationToken);
    }
}