using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using GarageGenius.Modules.Customers.Application.Commands.UpdateCustomer;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Customers.Api.Controllers;
/// <summary>
/// Customers controller
/// </summary>
/// <param name="dispatcher"> // Dispatcher for handling commands and queries, events and paginated queries </param>
public class CustomersController(IDispatcher dispatcher) : BaseController
{
	[HttpGet("{id:guid}")]
	[Authorize]
	[SwaggerOperation("Get customer id")]
	[SwaggerResponse(StatusCodes.Status200OK, "Customer found", typeof(GetCustomerByIdDto))]
	public async Task<ActionResult> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var customer = await dispatcher.DispatchQueryAsync<GetCustomerByIdDto>(new GetCustomerByIdQuery(id), cancellationToken);
		return Ok(customer);
	}

	[HttpGet("user/{id:guid}")]
	[Authorize]
	[SwaggerOperation("Get customer by user id")]
	[SwaggerResponse(StatusCodes.Status200OK, "Customer found", typeof(GetCustomerByUserIdDto))]
	public async Task<ActionResult> GetCustomerByUserIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var customer = await dispatcher.DispatchQueryAsync<GetCustomerByUserIdDto>(new GetCustomerByUserIdQuery(id), cancellationToken);
		return Ok(customer);
	}

	[HttpPost]
	[Authorize]
	[SwaggerOperation("Create customer")]
	[SwaggerResponse(StatusCodes.Status202Accepted, "Customer created")]
	public async Task<ActionResult> CreateCustomerAsync(CreateCustomerCommand createCustomerCommand, 
		CancellationToken cancellationToken)
	{
		await dispatcher.DispatchCommandAsync<CreateCustomerCommand>(createCustomerCommand, cancellationToken);
		return Accepted();
	}

	[HttpPut]
	[Authorize]
	[SwaggerOperation("Update customer")]
	[SwaggerResponse(StatusCodes.Status204NoContent, "Customer updated")]
	public async Task<ActionResult> UpdateCustomerAsync(UpdateCustomerCommand updateCustomerCommand, 
		CancellationToken cancellationToken)
	{
		await dispatcher.DispatchCommandAsync<UpdateCustomerCommand>(updateCustomerCommand, cancellationToken);
		return NoContent();
	}
}

// TODO - handling for timeout / cancel request
