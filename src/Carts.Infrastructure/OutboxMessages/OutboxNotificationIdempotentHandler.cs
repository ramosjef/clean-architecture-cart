using System.Diagnostics.CodeAnalysis;

using Carts.Domain.Common.Models;
using Carts.Infrastructure.Database;

using MediatR;

using MongoDB.Driver;

namespace Carts.Infrastructure.OutboxMessages;

[ExcludeFromCodeCoverage]
public sealed class OutboxNotificationIdempotentHandler<TDomainEvent>
    : INotificationHandler<TDomainEvent> where TDomainEvent : DomainEvent
{
    private readonly IMongoContext _mongoContext;
    private readonly INotificationHandler<TDomainEvent> _decorated;

    public OutboxNotificationIdempotentHandler(IMongoContext mongoContext,
                                               INotificationHandler<TDomainEvent> decorated)
    {
        _mongoContext = mongoContext;
        _decorated = decorated;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;

        IAsyncCursor<OutboxConsumer> isConsumed = await _mongoContext.OutboxConsumers.FindAsync(x =>
            x.EventId == notification.Id && x.Consumer == consumer,
            cancellationToken: cancellationToken);

        if (isConsumed.Any(cancellationToken: cancellationToken))
            return;

        await _decorated.Handle(notification, cancellationToken);

        await _mongoContext.OutboxConsumers.InsertOneAsync(
            new OutboxConsumer(EventId: notification.Id, Consumer: consumer),
            cancellationToken: cancellationToken);
    }
}
