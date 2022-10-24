using System.Diagnostics.CodeAnalysis;

using Carts.Domain.DomainEvents;

using MediatR;

namespace Carts.Application.EventHandlers.CartSkuAddedEvent;

[ExcludeFromCodeCoverage]
public sealed class SendNotificationSkuAddedEventHandler : INotificationHandler<CartSkuAdded>
{
    public Task Handle(CartSkuAdded request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}