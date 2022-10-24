using System.Diagnostics.CodeAnalysis;

using Carts.Application.Common.Contracts;
using Carts.Domain;
using Carts.Domain.Common.Models;
using Carts.Domain.ValueObjects;
using Carts.Infrastructure.Database;

using Newtonsoft.Json;

namespace Carts.Infrastructure.OutboxMessages;

[ExcludeFromCodeCoverage]
public sealed class CartRepositoryOutboxDecorator : ICartRepository
{
    private readonly IMongoContext _mongoContext;
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CartRepositoryOutboxDecorator(IMongoContext mongoContext,
                                         ICartRepository cartRepository,
                                         IUnitOfWork unitOfWork)
    {
        _mongoContext = mongoContext;
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartId> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.StartTransactionAsync(cancellationToken);

            CartId cartId = await _cartRepository.CreateAsync(cart, cancellationToken);
            await SaveMessagesOutboxAsync<Cart, CartId>(cart, cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return cartId;
        }
        catch
        {
            await _unitOfWork.AbortTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Cart?> GetAsync(UserId userId, CancellationToken cancellationToken = default)
        => await _cartRepository.GetAsync(userId, cancellationToken);

    public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.StartTransactionAsync(cancellationToken);

            await _cartRepository.UpdateAsync(cart, cancellationToken);
            await SaveMessagesOutboxAsync<Cart, CartId>(cart, cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.AbortTransactionAsync(cancellationToken);
            throw;
        }
    }

    private async Task SaveMessagesOutboxAsync<T, TKey>(T aggregate, CancellationToken cancellationToken = default)
        where T : Aggregate<TKey>
    {
        if (aggregate.GetDomainEvents() is IReadOnlyCollection<DomainEvent> events && events.Any())
        {
            IEnumerable<OutboxMessage> outbox = events.Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })));

            await _mongoContext.OutboxMessages.InsertManyAsync(outbox, cancellationToken: cancellationToken);

            aggregate.ClearEvents();
        }
    }
}
