namespace MyRentals.Service.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using UniqueIdGenerator.Net;

    public class AddApartmentHandler : IRequestHandler<AddApartment, ulong>
    {
        private readonly IApartmentRepository apartmentRepository;
        private static readonly Generator Generator = new Generator(0, new DateTime(2020, 04, 17));

        public AddApartmentHandler(IApartmentRepository apartmentRepository)
        {
            this.apartmentRepository = apartmentRepository;
        }

        async Task<ulong> IRequestHandler<AddApartment, ulong>.Handle(AddApartment request, CancellationToken cancellationToken)
        {
            var apt = request.Apartment;
            var id = Generator.NextLong();
            apt.SetId(id);
            await this.apartmentRepository.Create(apt, cancellationToken);
            return id;
        }
    }
}