﻿namespace GarageGenius.Modules.Users.Core.Queries.GetUser;
public record GetUserQueryDto
{
	public Guid Id { get; init; }
	public Guid CustomerId { get; init; }
	public string Role { get; init; }
	public string Email { get; init; }
	public string State { get; init; }
	public DateTime Created { get; init; }

	public GetUserQueryDto(Guid id, Guid customerId, string role, string email, string state, DateTime created)
	{
		Id = id;
		CustomerId = customerId;
		Role = role;
		Email = email;
		State = state;
		Created = created;
	}
}