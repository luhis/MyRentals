namespace MyRentals.Web.Authorization
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using MyRentals.Web.Tooling;

    public class RealtorOrAdminRequirementHandler : AuthorizationHandler<RealtorOrAdminRequirement>
    {
        private readonly ISimpleRequirements simpleRequirements;

        public RealtorOrAdminRequirementHandler(ISimpleRequirements simpleRequirements)
        {
            this.simpleRequirements = simpleRequirements;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RealtorOrAdminRequirement requirement)
        {
            var email = context.User.GetEmailAddress();
            var isAdminOrRequirement = await this.simpleRequirements.IsAdminOrRealtor(email);
            if (isAdminOrRequirement)
            {
                context.Succeed(requirement);
            }
        }
    }
}