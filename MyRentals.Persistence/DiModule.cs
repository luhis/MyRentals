namespace MyRentals.Persistence
{
    using Microsoft.Extensions.DependencyInjection;
    using MyRentals.Domain.Repositories;
    using MyRentals.Persistence.Repositories;

    public static class DiModule
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRealtorRepository, RealtorRepository>();

            services.AddDbContext<MyRentalsContext>();
        }
    }
}