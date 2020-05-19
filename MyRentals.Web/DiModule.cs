namespace MyRentals.Web
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyRentals.Persistence;
    using MyRentals.Web.Authorization;

    public static class DiModule
    {
        public static void AddWeb(this IServiceCollection collection)
        {
            collection.AddScoped<IAuthorizationHandler, RealtorOrAdminRequirementHandler>();
            collection.AddScoped<ISimpleRequirements, SimpleRequirements>();
            collection.AddSingleton(GetDbOptions);
        }

        private static DbContextOptions<MyRentalsContext> GetDbOptions(IServiceProvider a) => new DbContextOptionsBuilder<MyRentalsContext>()
            .UseSqlite(a.GetService<IConfiguration>().GetSection("DbPath").Get<string>()).Options;
    }
}