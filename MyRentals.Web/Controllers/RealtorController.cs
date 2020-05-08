namespace MyRentals.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Service.Messages;
    using MyRentals.Web.Authorization;
    using MyRentals.Web.Models;
    using MyRentals.Web.Tooling;

    [ApiController]
    [Route("[controller]")]
    public class RealtorController : ControllerBase
    {
        private readonly IMediator mediator;

        public RealtorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policies.RealtorOrAdmin)]
        [HttpGet]
        public async Task<IEnumerable<Realtor>> GetAll(CancellationToken cancellationToken)
        {
            var realtors = await this.mediator.Send(new GetRealtors(), cancellationToken);
            var r = await this.mediator.Send(new GetRealtorId(this.HttpContext.User.GetEmailAddress()), cancellationToken);
            return r.Match(some => realtors.Where(a => a.RealtorId == some), _ => realtors);
        }

        [Authorize(Policies.Admin)]
        [HttpPost]
        public Task<ulong> Post(SaveRealtor realtor, CancellationToken cancellationToken)
        {
            return this.mediator.Send(new AddRealtor(Map(0, realtor)), cancellationToken);
        }

        [Authorize(Policies.Admin)]
        [HttpPut("{realtorId}")]
        public async Task<StatusCodeResult> Put(ulong realtorId, SaveRealtor realtor, CancellationToken cancellationToken)
        {
            var res = await this.mediator.Send(new UpdateRealtor(realtorId, Map(realtorId, realtor)), cancellationToken);
            return res.Match<StatusCodeResult>(success => this.Ok(), _ => this.NotFound());
        }

        [Authorize(Policies.Admin)]
        [HttpDelete("{realtorId}")]
        public async Task<StatusCodeResult> Delete(ulong realtorId, CancellationToken cancellationToken)
        {
            var res = await this.mediator.Send(new DeleteRealtor(realtorId), cancellationToken);
            return res.Match<StatusCodeResult>(success => this.Ok(), _ => this.NotFound());
        }

        private static Realtor Map(ulong realtorId, SaveRealtor r) => new Realtor(realtorId,r.RealtorName, r.RealtorEmail);
    }
}