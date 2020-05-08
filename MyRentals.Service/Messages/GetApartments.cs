namespace MyRentals.Service.Messages
{
    using System.Collections.Generic;
    using MediatR;
    using MyRentals.Domain.CommandModels;
    using MyRentals.Domain.StorageModels;

    public class GetApartments : IRequest<IEnumerable<Apartment>>
    {
        public GetApartments(PropertyFilters propertyFilters)
        {
            this.PropertyFilters = propertyFilters;
        }

        public PropertyFilters PropertyFilters { get; }
    }
}
