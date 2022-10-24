using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Carts.Domain.Common.Models;
using Carts.Infrastructure.Database;

using MediatR;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

using Newtonsoft.Json;

using Polly;

using Quartz;

namespace Carts.Infrastructure.OutboxMessages;

[ExcludeFromCodeCoverage]
[DisallowConcurrentExecution]
public sealed class OutboxMessagesBackgroundJob : IJob
{
    private readonly ILogger<OutboxMessagesBackgroundJob> _logger;
    private readonly IMongoContext _mongoContext;
    private readonly IPublisher _publisher;

    public OutboxMessagesBackgroundJob(ILogger<OutboxMessagesBackgroundJob> logger,
                                       IMongoContext mongoContext,
                                       IPublisher publisher)
    {
        _logger = logger;
        _mongoContext = mongoContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        IAsyncCursor<OutboxMessage> messages = await _mongoContext.OutboxMessages.FindAsync(x =>
            x.ProcessedAt == null,
            new FindOptions<OutboxMessage, OutboxMessage>()
            {
                Skip = 0,
                Limit = 10,
            },
            context.CancellationToken);

        foreach (OutboxMessage message in messages.ToEnumerable())
        {
            try
            {
                DomainEvent? domainEvent = JsonConvert.DeserializeObject<DomainEvent>(
                    message.Content,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                if (domainEvent is null)
                    continue;

                PolicyResult? result = await Policy
                    .Handle<Exception>()
                    .RetryAsync(5, onRetry: (ex, count, context) => _logger.LogError(ex, ex.Message))
                    .ExecuteAndCaptureAsync(async () =>
                    {
                        await _publisher.Publish(domainEvent, context.CancellationToken);
                        message.ProcessedAt = DateTime.UtcNow;
                    });

                message.Error = result?.FinalException?.Demystify()?.ToString();

                await _mongoContext.OutboxMessages.FindOneAndReplaceAsync(x =>
                    x.Id == message.Id,
                    message,
                    cancellationToken: context.CancellationToken);
            }
            catch (Exception ex)
            {
                ex.Demystify();

                _logger.LogError(ex, ex.Message);

                continue;
            }
        }
    }
}
