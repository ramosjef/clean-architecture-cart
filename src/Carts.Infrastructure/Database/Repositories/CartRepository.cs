using System.Diagnostics.CodeAnalysis;

using Carts.Domain;
using Carts.Domain.ValueObjects;

using MongoDB.Driver;

namespace Carts.Infrastructure.Database.Repositories;

[ExcludeFromCodeCoverage]
public class CartRepository : ICartRepository
{
    private readonly IMongoContext _mongoContext;

    public CartRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public async Task<CartId> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _mongoContext.Carts.InsertOneAsync(cart, cancellationToken: cancellationToken);
        return cart.Id;
    }

    public async Task<Cart?> GetAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        IAsyncCursor<Cart> carts = await _mongoContext.Carts.FindAsync(x => x.UserId.Equals(userId), cancellationToken: cancellationToken);
        return carts.FirstOrDefault(cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _mongoContext.Carts.FindOneAndReplaceAsync(x => x.UserId.Equals(cart.UserId), cart, cancellationToken: cancellationToken);
    }
}
