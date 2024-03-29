﻿using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Repositories;
using GarageGenius.Modules.Vehicles.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Vehicles.Infrastructure.Persistance.Repositories;
using GarageGenius.Modules.Vehicles.Infrastructure.QueryStorage;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using GarageGenius.Shared.Infrastructure.Persistance.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Infrastructure;

internal static class Extensions
{
	public static IServiceCollection AddVehiclesInfrastructure(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
	{
		services.AddMsSqlServerDbContext<VehiclesDbContext>(webHostEnvironment);
		//services.AddPostgreSqlServerDbContext<VehiclesDbContext>(webHostEnvironment);
		services.AddScoped<IVehicleRepository, VehicleRepository>();
		services.AddScoped<IVehicleQueryStorage, VehicleQueryStorage>();
		return services;
	}
}