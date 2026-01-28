using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.MessageBroker;
using Microsoft.Extensions.Hosting;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal sealed class EventDispatcherWorker(
	IEventChannel eventChannel,
	IEventDispatcher eventDispatcher,
	Serilog.ILogger logger)
	: BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await foreach (var @event in eventChannel.Reader.ReadAllAsync(stoppingToken))
		{
			try
			{
				await eventDispatcher.DispatchEventAsync(@event, stoppingToken)
					.ConfigureAwait(false);
				logger.Information("Executed event: {EventName} using {BackgroundService}",
					@event.GetType().Name, nameof(EventDispatcherWorker));
			}
			catch (Exception exception)
			{
				logger.Error(exception, exception.Message);
			}
		}
	}
}
