namespace MyRentals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using MyRentals.Domain.CommandModels;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Service.Messages;
    using MyRentals.Web.Authorization;
    using MyRentals.Web.Tooling;
    using OneOf;
    using OneOf.Types;

    [ApiController]
    [Route("[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ISet<string> adminEmailAddresses;

        public ApartmentController(IMediator mediator, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.adminEmailAddresses = new HashSet<string>(configuration.GetSection("AdminEmails").Get<IEnumerable<string>>());
        }

        [HttpGet]
        public async Task<IEnumerable<Apartment>> GetAll(int? floorArea, decimal? pricePerMonth, int? numberOfRooms, CancellationToken cancellationToken)
        {
            var filters = new PropertyFilters(ToOption(floorArea), ToOption(pricePerMonth), ToOption(numberOfRooms), await this.IsAdminOrRealtor(cancellationToken));
            return await this.mediator.Send(new GetApartments(filters), cancellationToken);
        }

        [Authorize(Policies.RealtorOrAdmin)]
        [HttpPost]
        public async Task<ulong> Post(Models.SaveApartment saveApartment, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(new AddApartment(Map(saveApartment, await this.GetRealtorId(saveApartment.RealtorId, cancellationToken))), cancellationToken);
        }

        [Authorize(Policies.RealtorOrAdmin)]
        [HttpPut("{apartmentId}")]
        public async Task<StatusCodeResult> Put(ulong apartmentId, Models.SaveApartment saveApartment, CancellationToken cancellationToken)
        {
            var res = await this.mediator.Send(new UpdateApartment(apartmentId, Map(saveApartment, await this.GetRealtorId(saveApartment.RealtorId, cancellationToken))), cancellationToken);
            return res.Match<StatusCodeResult>(success => this.Ok(), _ => this.NotFound());
        }

        [Authorize(Policies.RealtorOrAdmin)]
        [HttpDelete("{apartmentId}")]
        public async Task<StatusCodeResult> Delete(ulong apartmentId, CancellationToken cancellationToken)
        {
            var res = await this.mediator.Send(new DeleteApartment(apartmentId), cancellationToken);
            return res.Match<StatusCodeResult>(success => this.Ok(), _ => this.NotFound()); 
        }

        private static Apartment Map(Models.SaveApartment apt, ulong realtorId)
        {
            return new Apartment(0, apt.Name, apt.Description, apt.FloorArea, apt.PricePerMonth, apt.NumberOfRooms, apt.Lat, apt.Lon, DateTime.UtcNow, realtorId, apt.IsRented);
        }

        private async Task<ulong> GetRealtorId(ulong sentValue, CancellationToken cancellationToken)
        {
            var r = await this.mediator.Send(new GetRealtorId(this.HttpContext.User.GetEmailAddress()), cancellationToken);
            return r.Match(some => some, _ => sentValue);
        }

        private async Task<bool> IsAdminOrRealtor(CancellationToken cancellationToken)
        {
            var currentEmail = this.HttpContext.User.GetEmailAddress();
            return this.adminEmailAddresses.Contains(currentEmail) || (await this.mediator.Send(new GetRealtorId(currentEmail), cancellationToken)).IsT0;
        }

        private static OneOf<T, None> ToOption<T>(T? t) where T : struct
        {
            if (t == null)
            {
                return new None();
            }
            else
            {
                return t.Value;
            }
        }
    }
}