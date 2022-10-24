namespace Carts.Application.Common.Contracts;

public interface IUnitOfWork
{
    Task StartTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task AbortTransactionAsync(CancellationToken cancellationToken);
}