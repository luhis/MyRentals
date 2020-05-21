namespace MyRentals.Web.Models
{
    public class AccessModel
    {
        public AccessModel(bool canViewApartments, bool canEditApartments, bool canViewClients, bool canViewRealtors)
        {
            this.CanViewRealtors = canViewRealtors;
            this.CanEditApartments = canEditApartments;
            this.CanViewClients = canViewClients;
            this.CanViewApartments = canViewApartments;
        }

        public bool CanViewRealtors { get; }
        public bool CanViewClients { get; }
        public bool CanViewApartments { get; }
        public bool CanEditApartments { get; }
    }
}