using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.MessageBroker;
namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal sealed class InMemoryMessageBroker(IEventChannel eventChannel, Serilog.ILogger logger)
	: IMessageBroker
{
	public async Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default)
	{
		var name = @event.GetType().Name;
		logger.Information("Publishing an event: {Name}...", name);

		await eventChannel.Writer.WriteAsync(@event, cancellationToken).ConfigureAwait(false);
	}
}
