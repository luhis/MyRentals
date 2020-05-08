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

    public class RealtorRepository : IRealtorRepository
    {
        private readonly MyRentalsContext context;

        public RealtorRepository(MyRentalsContext context)
        {
            this.context = context;
        }

        async Task<IEnumerable<Realtor>> IRealtorRepository.GetAll(CancellationToken token)
        {
            return await this.context.Realtors.OrderBy(a => a.RealtorName).ToArrayAsync(token);
        }

        Task IRealtorRepository.Create(Realtor client, CancellationToken cancellationToken)
        {
            this.context.Add(client);
            return this.context.SaveChangesAsync(cancellationToken);
        }

        async Task<OneOf<Success, NotFound>> IRealtorRepository.Update(Realtor client,
            CancellationToken cancellationToken)
        {
            if (this.context.Apartments != null && await this.context.Realtors.AnyAsync(a => a.RealtorId == client.RealtorId, cancellationToken))
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

        async Task<OneOf<Success, NotFound>> IRealtorRepository.Delete(ulong realtorId, CancellationToken cancellationToken)
        {
            if (this.context.Apartments != null && await this.context.Realtors.AnyAsync(a => a.RealtorId == realtorId, cancellationToken))
            {
                var found = await this.context.Realtors.SingleAsync(a => a.RealtorId == realtorId,
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

        async Task<OneOf<Realtor, NotFound>> IRealtorRepository.GetByEmail(string email, CancellationToken cancellationToken)
        {
            var found = await this.context.Realtors.FirstOrDefaultAsync(a => a.RealtorEmail == email, cancellationToken);
            if (found == null)
            {
                return new NotFound();
            }
            else
            {
                return found;
            }
        }
    }
}