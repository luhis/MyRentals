namespace MyRentals.Web.Authorization
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using MyRentals.Service.Messages;
    using MyRentals.Web.Tooling;

    public class RealtorOrAdminRequirementHandler : AuthorizationHandler<RealtorOrAdminRequirement>
    {
        private readonly IMediator mediator;

        public RealtorOrAdminRequirementHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RealtorOrAdminRequirement requirement)
        {
            var email = context.User.GetEmailAddress();
            if (requirement.AdminEmailAddresses.Contains(email))
            {
                context.Succeed(requirement);
            }
            else
            {
                var id = await this.mediator.Send(new GetRealtorId(email));
                if (id.IsT0)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}