using Carts.Domain.Common.Models;
using Carts.Domain.ValueObjects;

namespace Carts.Domain.DomainEvents;

public sealed record CartCreatedEvent(Guid CartId, UserId UserId) : DomainEvent
{
}
