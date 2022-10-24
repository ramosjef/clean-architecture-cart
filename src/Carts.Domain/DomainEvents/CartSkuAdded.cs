using Carts.Domain.Common.Models;

namespace Carts.Domain.DomainEvents;

public sealed record CartSkuAdded(Guid UserId, Guid SkuId, int Qty = 1) : DomainEvent
{
}
