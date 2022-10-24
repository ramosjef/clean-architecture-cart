using Carts.Domain.Common.Models;

namespace Carts.Domain.DomainEvents;

public sealed record CartSkuRemoved(Guid UserId, Guid SkuId) : DomainEvent
{
}