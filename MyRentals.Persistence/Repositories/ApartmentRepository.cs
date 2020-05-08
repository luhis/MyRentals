namespace MyRentals.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyRentals.Domain.CommandModels;
    using MyRentals.Domain.Repositories;
    using MyRentals.Domain.StorageModels;
    using OneOf;
    using OneOf.Types;

    public class ApartmentRepository : IApartmentRepository
    {
        private readonly MyRentalsContext context;

        public ApartmentRepository(MyRentalsContext context)
        {
            this.context = context;
        }

        async Task<IEnumerable<Apartment>> IApartmentRepository.GetAll(PropertyFilters propertyFilters, CancellationToken token)
        {
            return await ApplyFilters(this.context.Apartments, propertyFilters).OrderBy(a => a.Name).ToArrayAsync(token);
        }

        Task IApartmentRepository.Create(Apartment apartment, CancellationToken cancellationToken)
        {
            this.context.Add(apartment);
            return this.context.SaveChangesAsync(cancellationToken);
        }

        async Task<OneOf<Success, NotFound>> IApartmentRepository.Update(Apartment apartment,
            CancellationToken cancellationToken)
        {
            if (this.context.Apartments != null && await this.context.Apartments.AnyAsync(a => a.ApartmentId == apartment.ApartmentId, cancellationToken))
            {
                this.context.Update(apartment);
                await this.context.SaveChangesAsync(cancellationToken);
                return new Success();
            }
            else
            {
                return new NotFound();
            }
        }

        async Task<OneOf<Success, NotFound>> IApartmentRepository.Delete(ulong requestApartmentId, CancellationToken cancellationToken)
        {
            if (this.context.Apartments != null && await this.context.Apartments.AnyAsync(a => a.ApartmentId == requestApartmentId, cancellationToken))
            {
                var found = await this.context.Apartments.SingleAsync(a => a.ApartmentId == requestApartmentId,
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

        private static IQueryable<Apartment> ApplyFilters(IQueryable<Apartment>? dbSet, PropertyFilters propertyFilters)
        {
            var priceAsNullable = propertyFilters.PricePerMonth.Match(s => (decimal?)s, _ => null);
            var rooms = propertyFilters.NumberOfRooms.Match(s => (int?)s, _ => null);
            var size = propertyFilters.FloorArea.Match(s => (int?)s, _ => null);
            return dbSet
                .Where(a => (propertyFilters.IncludeRented || !a.IsRented) && (priceAsNullable == null || a.PricePerMonth == priceAsNullable.Value) && (rooms == null || a.NumberOfRooms == rooms.Value) && (size == null || a.FloorArea == size.Value));
        }
    }
}
