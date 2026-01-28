using FluentValidation;
using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Shared.Abstractions.Commands.Decorators;
/// <summary> Decorator that validates the command before passing it to the decorated handler. </summary>
/// <param name="decorated"> The decorated handler. </param>
/// <param name="validator"> The validator to use for the command.</param>
/// <typeparam name="TCommand"> The type of command to handle. </typeparam>
[CommandHandlerDecorator]
public class ValidationCommandHandlerDecorator<TCommand>(
	ICommandHandler<TCommand> decorated,
	IValidator<TCommand> validator)
	: ICommandHandler<TCommand>
	where TCommand : class, ICommand
{
	/// <summary> Handles the command by validating it first. </summary>
	/// <param name="command"> The command to handle.</param>
	/// <param name="cancellationToken"> The cancellation token.</param>
	/// <exception cref="GarageGeniusValidationException"> Thrown when the command is invalid.</exception>
	public async Task HandleCommandAsync(TCommand command, CancellationToken cancellationToken = default)
	{
		var validationResult = await validator.ValidateAsync(command, cancellationToken)
			.ConfigureAwait(false);

		if (validationResult.Errors.Count != 0)
			throw new GarageGeniusValidationException(validationResult.Errors);

		await decorated.HandleCommandAsync(command, cancellationToken)
			.ConfigureAwait(false);
	}
}
