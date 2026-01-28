using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
internal class CreateCustomerCommandHandler(
	Serilog.ILogger logger,
	ICustomerRepository customerRepository,
	ICustomerMapperService customerMapperService)
	: ICommandHandler<CreateCustomerCommand>
{
	public async Task HandleCommandAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default)
	{
		var customer = customerMapperService.MapToCustomer(command);
		await customerRepository.AddCustomerAsync(customer, cancellationToken)
			.ConfigureAwait(false);

		logger.Information("Handled {CommandName} in {ModuleName} module, created customer with email: {Email}",
			nameof(CreateCustomerCommand), nameof(Customers), command.EmailAddress);
	}
}
