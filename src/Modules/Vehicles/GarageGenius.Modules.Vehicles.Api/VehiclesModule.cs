﻿using GarageGenius.Modules.Vehicles.Application;
using GarageGenius.Modules.Vehicles.Core;
using GarageGenius.Modules.Vehicles.Infrastructure;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;

namespace GarageGenius.Modules.Vehicles.Api;
internal class VehiclesModule : IModule
{
	public const string BasePath = "vehicles-module";
	public string Name => "Vehicles";
	public IEnumerable<string>? Policies { get; } = new string[] { "vehicles" };

	public void Register(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services
			.AddVehiclesCore()
			.AddVehiclesApplication()
			.AddVehiclesInfrastructure(webApplicationBuilder.Environment);
	}

	public void Use(WebApplication app)
	{
		app.MapHealthCheck(Name);
	}
}
