﻿using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
internal class GetReservationQueryHandler : IQueryHandler<GetReservationQuery, GetReservationQueryDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly IReservationQueryStorage _reservationQueryStorage;

	public GetReservationQueryHandler(
		Serilog.ILogger logger,
		IReservationQueryStorage reservationQueryStorage)
	{
		_logger = logger;
		_reservationQueryStorage = reservationQueryStorage;
	}

	public async Task<GetReservationQueryDto> HandleQueryAsync(GetReservationQuery query, CancellationToken cancellationToken = default)
	{
		GetReservationQueryDto getReservationQueryDto = await _reservationQueryStorage.GetReservationAsync(query.ReservationId, cancellationToken) ?? throw new ReservationNotFoundException(query.ReservationId);

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved reservation with ID: {ReservationId}",
			nameof(GetReservationQuery),
			nameof(Reservations),
			query.ReservationId);

		return getReservationQueryDto;
	}
}
