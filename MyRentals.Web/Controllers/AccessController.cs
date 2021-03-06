﻿namespace MyRentals.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MyRentals.Web.Authorization;
    using MyRentals.Web.Models;
    using MyRentals.Web.Tooling;

    [ApiController]
    [Route("[controller]")]
    public class AccessController : ControllerBase
    {
        private readonly ISimpleRequirements simpleRequirements;

        public AccessController(ISimpleRequirements simpleRequirements)
        {
            this.simpleRequirements = simpleRequirements;
        }

        [HttpGet]
        public async Task<AccessModel> GetAccess()
        {
            var email = this.User.GetEmailAddress();
            var realtorOrAdmin = await this.simpleRequirements.IsAdminOrRealtor(email);
            var admin = this.simpleRequirements.IsAdmin(email);
            return new AccessModel(true, realtorOrAdmin, realtorOrAdmin, admin);
        }
    }
}
