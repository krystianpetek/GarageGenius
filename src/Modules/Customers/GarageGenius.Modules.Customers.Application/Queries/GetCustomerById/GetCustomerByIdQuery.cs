using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
/// <summary>
/// Query to get a customer by their Id
/// </summary>
/// <param name="Id"> The Id of the customer to get </param>
internal record GetCustomerByIdQuery(Guid Id) : IQuery<GetCustomerByIdDto>;

