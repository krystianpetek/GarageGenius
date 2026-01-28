using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Customers.Application.Events.External;
internal sealed class UserCreatedEventHandler(
	Serilog.ILogger logger,
	ICustomerRepository customerRepository,
	ICustomerMapperService customerMapperService)
	: IEventHandler<UserCreatedEvent>
{
	public async Task HandleEventAsync(UserCreatedEvent @event, CancellationToken cancellationToken = default)
	{
		customerMapperService.MapToCustomer(@event);
		await customerRepository.AddCustomerAsync(new Customer(@event.CustomerId, @event.UserId, @event.EmailAddress),
				cancellationToken)
			.ConfigureAwait(false);

		logger.Information(
			messageTemplate: "Event {EventName} handled by {ModuleName} module, added customer with user ID: {UserId}",
			nameof(UserCreatedEvent),
			nameof(Customers),
			@event.UserId);
	}
}

