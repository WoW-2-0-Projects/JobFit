using JobFit.Application.Common.EventBus.Brokers;
using JobFit.Domain.Common.Events;
using MediatR;

namespace JobFit.Infrastructure.Common.EventBus.Brokers;

/// <summary>
/// Implementation of <see cref="IEventBusBroker"/> that utilizes a mediator for publishing events.
/// </summary>
/// <param name="mediator">The mediator instance to be used for event publication.</param>
public class RabbitMqEventBusBroker(IPublisher mediator) : IEventBusBroker
{
    public async ValueTask PublishLocalAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : EventBase =>
        await mediator.Publish(@event, cancellationToken);

    public async ValueTask PublishAsync<TEvent>(TEvent @event, EventOptions eventOptions, CancellationToken cancellationToken = default) where TEvent : EventBase =>
        await mediator.Publish(@event, cancellationToken);
}