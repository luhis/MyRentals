namespace MyRentals.Service.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using OneOf;
    using OneOf.Types;

    public class DeleteApartmentHandler : IRequestHandler<DeleteApartment, OneOf<Success, NotFound>>
    {
        private readonly IApartmentRepository apartmentRepository;

        public DeleteApartmentHandler(IApartmentRepository apartmentRepository)
        {
            this.apartmentRepository = apartmentRepository;
        }

        Task<OneOf<Success, NotFound>> IRequestHandler<DeleteApartment, OneOf<Success, NotFound>>.Handle(DeleteApartment request, CancellationToken cancellationToken)
        {
            return this.apartmentRepository.Delete(request.ApartmentId, cancellationToken);
        }
    }
}