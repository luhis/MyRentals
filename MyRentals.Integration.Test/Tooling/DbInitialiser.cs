namespace MyRentals.Integration.Test.Tooling
{
    using System;
    using System.Linq;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Persistence;

    public static class DbInitialiser
    {
        public static void InitializeDbForTests(MyRentalsContext db)
        {
            if (!db.Realtors.Any(a => a.RealtorId == 0))
            {
                db.Realtors.Add(new Realtor(0, "realtor", "realtor@email.com"));
                db.SaveChanges();
            }

            if (!db.Apartments.Any(a => !a.IsRented))
            {
                db.Apartments.Add(new Apartment(0, "Apartment", "desc", 1200, 1400, 3, -0.1m, 50, DateTime.UtcNow,
                    db.Realtors.First().RealtorId, false));
                db.SaveChanges();
            }
        }
    }
}
