namespace MyRentals.Web.Authorization
{
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Options;
    using MyRentals.Service.Messages;

    public class SimpleRequirements : ISimpleRequirements
    {
        public SimpleRequirements(IOptions<AdminEmailsSetting> adminEmails, IMediator mediator)
        {
            this.mediator = mediator;
            this.adminEmails = adminEmails.Value;
        }

        private AdminEmailsSetting adminEmails { get; }
        private readonly IMediator mediator;

        bool ISimpleRequirements.IsAdmin(string emailAddress)
        {
            return this.adminEmails.AdminEmails.Contains(emailAddress);
        }

        async Task<bool> ISimpleRequirements.IsAdminOrRealtor(string emailAddress)
        {

            return this.adminEmails.AdminEmails.Contains(emailAddress) || (await this.mediator.Send(new GetRealtorId(emailAddress))).IsT0;
        }
    }
}