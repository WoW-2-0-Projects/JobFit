using MediatR;

namespace JobFit.Domain.Common.Events;

public interface IEvent : INotification
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public bool IsRedelivered { get; set; }
}