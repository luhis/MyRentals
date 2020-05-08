namespace MyRentals.Service.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyRentals.Domain.Repositories;
    using MyRentals.Service.Messages;
    using OneOf;
    using OneOf.Types;

    public class UpdateRealtorHandler : IRequestHandler<UpdateRealtor, OneOf<Success, NotFound>>
    {
        private readonly IRealtorRepository realtorRepository;

        public UpdateRealtorHandler(IRealtorRepository realtorRepository)
        {
            this.realtorRepository = realtorRepository;
        }

        Task<OneOf<Success, NotFound>> IRequestHandler<UpdateRealtor, OneOf<Success, NotFound>>.Handle(UpdateRealtor request, CancellationToken cancellationToken)
        {
            var apt = request.Realtor;
            apt.SetId(request.RealtorId);
            return this.realtorRepository.Update(apt, cancellationToken);
        }
    }
}