using MediatR;

namespace Carts.Domain.Common.Models;

public abstract record DomainEvent : INotification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime RaisedAt { get; set; } = DateTime.UtcNow;
}
