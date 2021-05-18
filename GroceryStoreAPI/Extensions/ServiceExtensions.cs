using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Contracts;
using LoggerService;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Infrastructure.Configuration;

namespace GroceryStoreAPI.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureDbContext(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddDbContext<RepositoryContext>(opt => opt.UseSqlite(configuration["ConnectionString"]));
            service.AddScoped<RepositoryContext>();

        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureLoggerService(this IServiceCollection service)
        {

            service.AddSingleton<ILoggerManager, LoggerManager>();
          

        }

        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.CreateScope().ServiceProvider.GetService<RepositoryContext>();
                context.Database.Migrate();
                DataSeeder.SeedCustomers(context);
            }
            return host;
        }
    }
}
