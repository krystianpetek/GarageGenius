namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
public sealed record class GetReservationHistoryQueryDtos(Guid ReservationId, IReadOnlyList<ReservationHistoriesDto> ReservationHistoriesDtos);

public sealed record class ReservationHistoriesDto(Guid ReservationHistoryId, string ReservationState, string Comment);