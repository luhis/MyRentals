namespace MyRentals.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Service.Messages;
    using MyRentals.Web.Authorization;
    using MyRentals.Web.Models;

    [Authorize(Policies.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClientController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<Client>> GetAll(CancellationToken cancellationToken)
        {
            return this.mediator.Send(new GetClients(), cancellationToken);
        }

        [HttpPost]
        public Task<ulong> Post(SaveClient client, CancellationToken cancellationToken)
        {
            return this.mediator.Send(new AddClient(Map(0, client)), cancellationToken);
        }

        [HttpPut("{clientId}")]
        public async Task<StatusCodeResult> Put(ulong clientId, SaveClient client, CancellationToken cancellationToken)
        {
            var res = await this.mediator.Send(new UpdateClient(Map(clientId, client)), cancellationToken);
            return res.Match<StatusCodeResult>(success => this.Ok(), _ => this.NotFound());
        }

        [HttpDelete("{clientId}")]
        public async Task<StatusCodeResult> Delete(ulong clientId, CancellationToken cancellationToken)
        {
            var res = await this.mediator.Send(new DeleteClient(clientId), cancellationToken);
            return res.Match<StatusCodeResult>(success => this.Ok(), _ => this.NotFound());
        }

        private static Client Map(ulong clientId, SaveClient r) => new Client(clientId, r.ClientName);
    }
}