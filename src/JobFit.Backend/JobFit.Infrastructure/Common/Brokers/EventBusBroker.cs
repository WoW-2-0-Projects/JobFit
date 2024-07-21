using JobFit.Application.Common.EventBus.Brokers;
using JobFit.Domain.Common.Events;
using MediatR;

namespace JobFit.Infrastructure.Common.Brokers;

public class EventBusBroker(IPublisher mediator) : IEventBusBroker
{
    public ValueTask PublishLocalAsync<TEvent>(TEvent @event) where TEvent : EventBase
    {
        return new ValueTask(mediator.Publish(@event));
    }
}