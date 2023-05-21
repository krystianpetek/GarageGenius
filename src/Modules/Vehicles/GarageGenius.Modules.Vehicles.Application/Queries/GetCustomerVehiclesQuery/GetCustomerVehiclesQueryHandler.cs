﻿using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
internal class GetCustomerVehiclesQueryHandler : IQueryHandler<GetCustomerVehiclesQuery, IReadOnlyList<GetCustomerVehiclesQueryDto>>
{
    private readonly Serilog.ILogger _logger;
    private readonly IVehicleQueryStorage _vehicleQueryStorage;

    public GetCustomerVehiclesQueryHandler(
        Serilog.ILogger logger,
       IVehicleQueryStorage vehicleQueryStorage)
    {
        _logger = logger;
        _vehicleQueryStorage = vehicleQueryStorage;
    }

    public async Task<IReadOnlyList<GetCustomerVehiclesQueryDto>> HandleAsync(GetCustomerVehiclesQuery query, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<GetCustomerVehiclesQueryDto> customerVehicles = await _vehicleQueryStorage.GetCustomerVehiclesAsync(query.CustomerId, cancellationToken);

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicles for customer with ID: {CustomerId}",
            nameof(GetCustomerVehiclesQuery),
            nameof(Vehicles),
            query.CustomerId);

        return customerVehicles;
    }
}