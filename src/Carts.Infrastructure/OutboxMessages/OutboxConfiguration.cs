using System.Diagnostics.CodeAnalysis;

namespace Carts.Infrastructure.OutboxMessages;

[ExcludeFromCodeCoverage]
public sealed class OutboxConfiguration
{
    public int JobExecutionInterval { get; set; }
}
