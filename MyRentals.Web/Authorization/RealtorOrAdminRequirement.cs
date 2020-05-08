namespace MyRentals.Web.Authorization
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;

    public class RealtorOrAdminRequirement : IAuthorizationRequirement
    {
        public RealtorOrAdminRequirement(ISet<string> adminEmailAddresses)
        {
            this.AdminEmailAddresses = adminEmailAddresses;
        }

        public ISet<string> AdminEmailAddresses { get; }
    }
}