using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistence.DbContexts;
using GarageGenius.Modules.Customers.Infrastructure.Persistence.Repositories;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using GarageGenius.Shared.Infrastructure.Persistance.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Customers.Infrastructure;

internal static class Extensions
{
	public static IServiceCollection AddCustomersInfrastructure(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
	{
		services.AddMsSqlServerDbContext<CustomersDbContext>(webHostEnvironment);
		//services.AddPostgreSqlServerDbContext<CustomersDbContext>(webHostEnvironment);
		services.AddScoped<ICustomerRepository, CustomerRepository>();

		return services;
	}
}