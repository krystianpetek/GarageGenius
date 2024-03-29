﻿using GarageGenius.Modules.Reservations.Application.Queries.GetCurrentNotCompletedReservations;
using GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
using GarageGenius.Modules.Reservations.Application.Queries.GetVehicleReservations;
using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
using GarageGenius.Shared.Abstractions.Helpers;
using GarageGenius.Shared.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Reservations.Infrastructure.QueryStorage;
internal class ReservationQueryStorage : IReservationQueryStorage
{
	private readonly ReservationsDbContext _reservationsDbContext;
	private readonly ISystemDateService _systemDateService;

	public ReservationQueryStorage(
		ReservationsDbContext reservationsDbContext,
		ISystemDateService systemDateService)
	{
		_reservationsDbContext = reservationsDbContext;
		_systemDateService = systemDateService;
	}

	public async Task<GetReservationQueryDto?> GetReservationAsync(Guid reservationId, CancellationToken cancellationToken = default)
	{
		GetReservationQueryDto? getReservationQueryDto = await _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where(reservation => reservation.ReservationId == reservationId)
		.Select<Reservation, GetReservationQueryDto>(reservation => new GetReservationQueryDto(reservation.ReservationId, reservation.VehicleId, reservation.ReservationState, reservation.ReservationDate.Value, reservation.ReservationNote))
		.SingleOrDefaultAsync<GetReservationQueryDto>(cancellationToken)
		.ConfigureAwait(false);

		return getReservationQueryDto;
	}

	public async Task<GetReservationHistoryQueryDtos> GetReservationHistoryAsync(Guid reservationId, CancellationToken cancellationToken = default)
	{
		IReadOnlyList<ReservationHistoriesDto> reservationHistoriesDto = await _reservationsDbContext.ReservationHistories
		.AsNoTracking()
		.AsQueryable()
		.Where(reservationHistory => reservationHistory.ReservationId == reservationId)
		.Select(reservationHistory => new ReservationHistoriesDto(reservationHistory.ReservationHistoryId, reservationHistory.Created, reservationHistory.ReservationState, reservationHistory.Comment, reservationHistory.CreatedBy))
		.ToListAsync(cancellationToken)
		.ConfigureAwait(false);

		var orderedReservationHistoriesDto = reservationHistoriesDto
			.OrderByDescending(reservationHistory => reservationHistory.UpdateDate)
			.ToList();

		GetReservationHistoryQueryDtos getReservationHistoryQueryDto = new GetReservationHistoryQueryDtos(reservationId, orderedReservationHistoriesDto);

		return getReservationHistoryQueryDto;
	}

	public async Task<GetCustomerReservationsQueryDto> GetCustomerReservationsAsync(GetCustomerReservationsQuery getCustomerReservationsQuery, CancellationToken cancellationToken = default)
	{
		var customerReservationsDtosQuery = _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where(reservation => reservation.CustomerId == getCustomerReservationsQuery.CustomerId);

		if (getCustomerReservationsQuery.OnlyPending)
        {
            customerReservationsDtosQuery = customerReservationsDtosQuery
				.Where(reservation => reservation.ReservationState != ReservationState.Completed &&
				                      reservation.ReservationState != ReservationState.Canceled);
        }

        PaginatedList<CustomerReservationsDto> customerReservationsDtos = await customerReservationsDtosQuery.OrderByDescending(reservation => reservation.ReservationDate)
		.Select(reservation => new CustomerReservationsDto(reservation.ReservationId, reservation.VehicleId, reservation.ReservationState, reservation.ReservationDate.Value, reservation.ReservationNote))
		.PaginateAsync(getCustomerReservationsQuery.PageNumber, getCustomerReservationsQuery.PageSize, cancellationToken)
		.ConfigureAwait(false);

		GetCustomerReservationsQueryDto getCustomerReservationsQueryDto = new GetCustomerReservationsQueryDto(getCustomerReservationsQuery.CustomerId, customerReservationsDtos);

		return getCustomerReservationsQueryDto;
	}

	public async Task<GetVehicleReservationsQueryDto> GetVehicleReservationsAsync(Guid vehicleId, CancellationToken cancellationToken = default)
	{
		List<VehicleReservationsDto> vehicleReservationsDto = await _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where(reservation => reservation.VehicleId == vehicleId)
		.Select(reservation => new VehicleReservationsDto(reservation.ReservationId, reservation.ReservationState, reservation.ReservationDate.Value, reservation.ReservationNote))
		.ToListAsync(cancellationToken)
		.ConfigureAwait(false);

		GetVehicleReservationsQueryDto getVehicleReservationsQueryDto = new GetVehicleReservationsQueryDto(vehicleId, vehicleReservationsDto);
		return getVehicleReservationsQueryDto;
	}

	public async Task<GetCurrentNotCompletedReservationsQueryDto> GetCurrentNotCompletedReservationsAsync(GetCurrentNotCompletedReservationsQuery getCurrentNotCompletedReservationsQuery, CancellationToken cancellationToken)
	{
		var query =
			_reservationsDbContext.Reservations
				.AsNoTracking()
				.AsQueryable()
				// .Where(reservation => reservation.ReservationDate < _systemDateService.GetCurrentDate());
				.Where(reservation => !reservation.ReservationDeleted);

		if(getCurrentNotCompletedReservationsQuery.ToDecision)
		{
			query = query
				.Where(reservation =>
					reservation.ReservationState == ReservationState.Pending);
		}
		else
		{
			query = query
				.Where(reservation =>
					reservation.ReservationState != ReservationState.Completed &&
					reservation.ReservationState != ReservationState.Canceled &&
					reservation.ReservationState != ReservationState.Pending);
		}

        var currentNotCompletedReservationsDtos = await query
			.Select<Reservation, CurrentNotCompletedReservationsDto>(reservation =>
				new CurrentNotCompletedReservationsDto(
					reservation.ReservationId,
					reservation.VehicleId,
					reservation.CustomerId,
					reservation.ReservationState,
					reservation.ReservationDate.Value,
					reservation.ReservationNote))
			.PaginateAsync(getCurrentNotCompletedReservationsQuery.PageNumber,
				getCurrentNotCompletedReservationsQuery.PageSize, cancellationToken)
			.ConfigureAwait(false);

		GetCurrentNotCompletedReservationsQueryDto getCurrentNotCompletedReservationsQueryDto = new GetCurrentNotCompletedReservationsQueryDto(currentNotCompletedReservationsDtos);

		return getCurrentNotCompletedReservationsQueryDto;
	}
}
