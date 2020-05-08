namespace MyRentals.Service.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Service.Messages;

    public class GetRealtorsHandler : IRequestHandler<GetRealtors, IEnumerable<Realtor>>
    {
        private readonly IRealtorRepository realtorRepository;

        public GetRealtorsHandler(IRealtorRepository realtorRepository)
        {
            this.realtorRepository = realtorRepository;
        }

        Task<IEnumerable<Realtor>> IRequestHandler<GetRealtors, IEnumerable<Realtor>>.Handle(GetRealtors request, CancellationToken cancellationToken)
        {
            return this.realtorRepository.GetAll(cancellationToken);
        }
    }
}