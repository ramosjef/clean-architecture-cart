using Carts.Domain.Common.Models;

namespace Carts.Domain.DomainEvents;

public sealed record CartSkuQuantityChanged(Guid UserId, Guid SkuId, int Qty) : DomainEvent
{
}
