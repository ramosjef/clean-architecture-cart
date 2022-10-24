using System.Diagnostics.CodeAnalysis;

namespace Carts.Infrastructure.OutboxMessages;

[ExcludeFromCodeCoverage]
public sealed record OutboxConsumer(Guid EventId, string Consumer)
{
    public Guid Id { get; set; } = Guid.NewGuid();
}