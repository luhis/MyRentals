namespace MyRentals.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SaveApartment
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public int FloorArea { get; set; }

        public decimal PricePerMonth { get; set; }

        public int NumberOfRooms { get; set; }

        public decimal Lat { get; set; }

        public decimal Lon { get; set; }

        public ulong RealtorId { get; set; }

        public bool IsRented { get; set; }
    }
}
