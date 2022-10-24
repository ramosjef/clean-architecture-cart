using System.Diagnostics.CodeAnalysis;

using Carts.Domain.DomainEvents;

using MediatR;

namespace Carts.Application.EventHandlers.CartSkuRemovedEvent;

[ExcludeFromCodeCoverage]
public sealed class SendNotificationSkuRemovedEventHandler : INotificationHandler<CartSkuRemoved>
{
    public Task Handle(CartSkuRemoved request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
