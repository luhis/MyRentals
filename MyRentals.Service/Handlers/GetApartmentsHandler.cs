namespace MyRentals.Service.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Service.Messages;

    public class GetApartmentsHandler : IRequestHandler<GetApartments, IEnumerable<Apartment>>
    {
        private readonly IApartmentRepository apartmentRepository;

        public GetApartmentsHandler(IApartmentRepository apartmentRepository)
        {
            this.apartmentRepository = apartmentRepository;
        }

        Task<IEnumerable<Apartment>> IRequestHandler<GetApartments, IEnumerable<Apartment>>.Handle(GetApartments request, CancellationToken cancellationToken)
        {
            return this.apartmentRepository.GetAll(request.PropertyFilters, cancellationToken);
        }
    }
}