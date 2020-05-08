namespace MyRentals.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MyRentals.Domain.CommandModels;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartment>> GetAll(PropertyFilters propertyFilters, CancellationToken token);

        Task Create(Apartment apartment, CancellationToken cancellationToken);

        Task<OneOf<Success, NotFound>> Update(Apartment apartment, CancellationToken cancellationToken);

        Task<OneOf<Success, NotFound>> Delete(ulong apartmentId, CancellationToken cancellationToken);
    }
}
