namespace MyRentals.Web.Authorization
{
    using System.Threading.Tasks;

    public interface ISimpleRequirements
    {
        bool IsAdmin(string emailAddress);
        Task<bool> IsAdminOrRealtor(string emailAddress);
    }
}