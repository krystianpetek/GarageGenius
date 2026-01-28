using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Dispatcher;
using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Shared.Infrastructure.Dispatchers;

internal sealed class InMemoryDispatcher(
	ICommandDispatcher commandDispatcher,
	IEventDispatcher eventDispatcher,
	IQueryDispatcher queryDispatcher,
	IPagedQueryDispatcher pagedQueryDispatcher)
	: IDispatcher
{
	public Task DispatchCommandAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
	{
		return commandDispatcher.DispatchCommandAsync(command, cancellationToken);
	}

	public Task DispatchEventAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
	{
		return eventDispatcher.DispatchEventAsync(@event, cancellationToken);
	}

	public Task<TResult> DispatchQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
	{
		return queryDispatcher.DispatchQueryAsync(query, cancellationToken);
	}

	public Task<TPagedQueryResult> DispatchPagedQueryAsync<TPagedQueryResult>(IPagedQuery<TPagedQueryResult> query,
		CancellationToken cancellationToken = default)
	{
		return pagedQueryDispatcher.DispatchPagedQueryAsync(query, cancellationToken);
	}
}
