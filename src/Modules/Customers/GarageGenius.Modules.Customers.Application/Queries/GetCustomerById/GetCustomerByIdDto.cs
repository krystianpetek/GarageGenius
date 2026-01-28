using GarageGenius.Modules.Customers.Core.ValueObjects;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal record GetCustomerByIdDto(
	Guid CustomerId,
	FirstName? FirstName,
	LastName? LastName,
	PhoneNumber? PhoneNumber,
	EmailAddress EmailAddress);

