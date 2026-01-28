using FluentValidation;
using System.Text.RegularExpressions;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
	public CreateCustomerCommandValidator()
	{
		RuleFor(createCustomerCommand => createCustomerCommand.EmailAddress)
			.NotEmpty()
			.NotNull()
			.WithMessage("Email address is required.")
			.EmailAddress()
			.WithMessage("Email address is not valid.");

		RuleFor(createCustomerCommand => createCustomerCommand.PhoneNumber)
			.NotEmpty()
			.NotNull()
			.WithMessage("Phone number is required.")
			.MinimumLength(9)
			.WithMessage("Phone number must not be less than 9 characters.")
			.MaximumLength(14)
			.WithMessage("Phone number must not exceed 14 characters.")
			.Matches(new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$"))
			.WithMessage("Phone number is not valid.");

		#region RestOfTheRules

		RuleFor(createCustomerCommand => createCustomerCommand.FirstName)
			.NotEmpty()
			.NotNull()
			.WithMessage("First name is required.")
			.MinimumLength(2)
			.WithMessage("First name must not be less than 2 characters.")
			.MaximumLength(30)
			.WithMessage("First name must not exceed 30 characters.");

		RuleFor(createCustomerCommand => createCustomerCommand.LastName)
			.NotEmpty()
			.NotNull()
			.WithMessage("Last name is required.")
			.MinimumLength(2)
			.WithMessage("Last name must not be less than 2 characters.")
			.MaximumLength(50)
			.WithMessage("Last name must not exceed 50 characters.");

		#endregion

	}
}
