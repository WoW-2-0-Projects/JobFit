using JobFit.Domain.Common.Events;

namespace JobFit.Application.Common.EventBus.Brokers;

public interface IEventBusBroker
{
    ValueTask PublishLocalAsync<TEvent>(TEvent @event) where TEvent : EventBase;
}