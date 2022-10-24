using System.Diagnostics.CodeAnalysis;

namespace Carts.Infrastructure.OutboxMessages;

[ExcludeFromCodeCoverage]
public sealed class OutboxMessage
{
    public OutboxMessage(Guid id, string type, string content)
    {
        Id = id;
        Type = type;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Error { get; set; }
}
