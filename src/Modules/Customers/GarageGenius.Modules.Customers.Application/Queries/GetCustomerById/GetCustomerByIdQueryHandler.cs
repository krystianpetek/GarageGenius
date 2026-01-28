using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal class GetCustomerByIdQueryHandler(
	ICustomerRepository customerRepository,
	ICustomerMapperService customerMapperService)
	: IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdDto>
{
	public async Task<GetCustomerByIdDto> HandleQueryAsync(GetCustomerByIdQuery query,
		CancellationToken cancellationToken = default)
	{
		Customer? customer = await customerRepository.GetCustomerByIdAsync(query.Id, cancellationToken)
			.ConfigureAwait(false);

		GetCustomerByIdDto getCustomerByIdDto = customerMapperService.MapToGetCustomerByIdDto(customer);
		return getCustomerByIdDto;
	}
}
