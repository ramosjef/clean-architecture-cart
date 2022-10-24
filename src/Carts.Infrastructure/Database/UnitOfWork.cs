using System.Diagnostics.CodeAnalysis;

using Carts.Application.Common.Contracts;

using MongoDB.Driver;

namespace Carts.Infrastructure.Database;

[ExcludeFromCodeCoverage]
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IMongoContext _mongoContext;
    private IClientSessionHandle? _clientSessionHandle;

    public UnitOfWork(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public async Task StartTransactionAsync(CancellationToken cancellationToken = default)
    {
        _clientSessionHandle = await _mongoContext.MongoClient.StartSessionAsync(cancellationToken: cancellationToken);
        _clientSessionHandle.StartTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_clientSessionHandle is null)
            return;

        await TryAsync(_clientSessionHandle.CommitTransactionAsync, cancellationToken);
    }

    public async Task AbortTransactionAsync(CancellationToken cancellationToken)
    {
        if (_clientSessionHandle is null)
            return;

        await TryAsync(_clientSessionHandle.AbortTransactionAsync, cancellationToken);
    }

    private async Task TryAsync(Func<CancellationToken, Task> sessionHandlerDelegate, CancellationToken cancellationToken)
    {
        try
        {
            if (_clientSessionHandle!.IsInTransaction)
            {
                await sessionHandlerDelegate(cancellationToken);
            }
        }
        finally
        {
            _clientSessionHandle!.Dispose();
            _clientSessionHandle = null;
        }
    }
}
