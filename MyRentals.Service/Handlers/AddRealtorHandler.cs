namespace MyRentals.Service.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using UniqueIdGenerator.Net;

    public class AddRealtorHandler : IRequestHandler<AddRealtor, ulong>
    {
        private readonly IRealtorRepository realtorRepository;
        private static readonly Generator Generator = new Generator(0, new DateTime(2020, 04, 17));

        public AddRealtorHandler(IRealtorRepository realtorRepository)
        {
            this.realtorRepository = realtorRepository;
        }

        async Task<ulong> IRequestHandler<AddRealtor, ulong>.Handle(AddRealtor request, CancellationToken cancellationToken)
        {
            var apt = request.Realtor;
            var id = Generator.NextLong();
            apt.SetId(id);
            await this.realtorRepository.Create(apt, cancellationToken);
            return id;
        }
    }
}