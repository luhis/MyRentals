namespace MyRentals.Domain.StorageModels
{
    using System;

    public class Apartment
    {
        public Apartment(ulong apartmentId, string name, string description, int floorArea, decimal pricePerMonth, int numberOfRooms, decimal lat, decimal lon, DateTime dateAdded, ulong realtorId, bool isRented)
        {
            this.ApartmentId = apartmentId;
            this.Name = name;
            this.Description = description;
            this.FloorArea = floorArea;
            this.PricePerMonth = pricePerMonth;
            this.NumberOfRooms = numberOfRooms;
            this.Lat = lat;
            this.Lon = lon;
            this.DateAdded = dateAdded;
            this.RealtorId = realtorId;
            this.IsRented = isRented;
        }

        public ulong ApartmentId { get; private set; }

        public string Name { get; }

        public string Description { get; }

        public int FloorArea { get; }

        public decimal PricePerMonth { get; }

        public int NumberOfRooms { get; }

        public decimal Lat { get; }

        public decimal Lon { get; }

        public DateTime DateAdded { get; }

        public ulong RealtorId { get; }

        public bool IsRented { get; }

        public void SetId(ulong id)
        {
            this.ApartmentId = id;
        }
    }
}
