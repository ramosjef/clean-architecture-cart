using System.Diagnostics.CodeAnalysis;

using Carts.Domain.DomainEvents;

using MediatR;

namespace Carts.Application.EventHandlers.CartSkuQuantityChangedEvent;

[ExcludeFromCodeCoverage]
public sealed class SendNotificationQtyChanged : INotificationHandler<CartSkuQuantityChanged>
{
    public Task Handle(CartSkuQuantityChanged request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
