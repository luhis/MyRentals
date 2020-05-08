namespace MyRentals.Service.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using OneOf;
    using OneOf.Types;

    public class UpdateApartmentHandler : IRequestHandler<UpdateApartment, OneOf<Success, NotFound>>
    {
        private readonly IApartmentRepository apartmentRepository;

        public UpdateApartmentHandler(IApartmentRepository apartmentRepository)
        {
            this.apartmentRepository = apartmentRepository;
        }
        
        Task<OneOf<Success, NotFound>> IRequestHandler<UpdateApartment, OneOf<Success, NotFound>>.Handle(UpdateApartment request, CancellationToken cancellationToken)
        {
            var apt = request.Apartment;
            apt.SetId(request.ApartmentId);
            return this.apartmentRepository.Update(apt, cancellationToken);
        }
    }
}