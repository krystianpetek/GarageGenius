using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
/// <summary>
/// Command to create a new customer
/// </summary>
public record CreateCustomerCommand : ICommand
{
	/// <summary>
	/// Email address of the customer
	/// </summary>
	[DefaultValue("krystianpetek2@gmail.com")]
	public string? EmailAddress { get; init; }

	/// <summary>
	/// First name of the customer
	/// </summary>
	[DefaultValue("Krystian")]
	public string? FirstName { get; init; }

	/// <summary>
	/// Last name of the customer
	/// </summary>
	[DefaultValue("Petek")]
	public string? LastName { get; init; }
	/// <summary>
	/// Phone number of the customer
	/// </summary>
	[DefaultValue("123456789")]
	public string? PhoneNumber { get; init; }
}
