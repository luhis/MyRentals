namespace MyRentals.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRentals.Web.Authorization;
    using MyRentals.Web.Models;
    using MyRentals.Web.Tooling;

    [ApiController]
    [Route("[controller]")]
    public class AccessController : ControllerBase
    {
        private readonly AuthorizationHandler<RealtorOrAdminRequirement> realtorOrAdmin;
        private readonly ISimpleRequirements simpleRequirements;

        public AccessController(AuthorizationHandler<RealtorOrAdminRequirement> realtorOrAdmin, ISimpleRequirements simpleRequirements)
        {
            this.realtorOrAdmin = realtorOrAdmin;
            this.simpleRequirements = simpleRequirements;
        }

        [HttpGet]
        public async Task<AccessModel> GetAccess()
        {
            var realtorOrAdmin = await simpleRequirements.IsAdminOrRealtor(this.User.GetEmailAddress());
            var admin = simpleRequirements.IsAdmin(this.User.GetEmailAddress());
            return new AccessModel(true, realtorOrAdmin, admin);
        }
    }
}
