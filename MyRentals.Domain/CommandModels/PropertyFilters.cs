namespace MyRentals.Domain.CommandModels
{
    using OneOf;
    using OneOf.Types;

    public class PropertyFilters
    {
        public PropertyFilters(OneOf<int, None> floorArea, OneOf<decimal, None> pricePerMonth, OneOf<int, None> numberOfRooms, bool includeRented)
        {
            this.FloorArea = floorArea;
            this.PricePerMonth = pricePerMonth;
            this.NumberOfRooms = numberOfRooms;
            this.IncludeRented = includeRented;
        }

        public OneOf.OneOf<int, None> FloorArea { get; }
        public OneOf.OneOf<decimal, None> PricePerMonth { get; }
        public OneOf.OneOf<int, None> NumberOfRooms { get; }

        public bool IncludeRented { get; }
    }
}
