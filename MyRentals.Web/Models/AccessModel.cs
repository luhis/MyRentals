namespace MyRentals.Web.Models
{
    public class AccessModel
    {
        public AccessModel(bool canViewApartments, bool canViewClients, bool canViewRealtors)
        {
            CanViewRealtors = canViewRealtors;
            CanViewClients = canViewClients;
            CanViewApartments = canViewApartments;
        }

        public bool CanViewRealtors { get; }
        public bool CanViewClients { get; }
        public bool CanViewApartments { get; }
    }
}