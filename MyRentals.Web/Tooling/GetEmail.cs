namespace MyRentals.Web.Tooling
{
    using System.Security.Claims;

    public static class GetEmail
    {
        public static string GetEmailAddress(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
