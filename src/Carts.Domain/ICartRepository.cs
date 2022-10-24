using Carts.Domain.ValueObjects;

namespace Carts.Domain;

public interface ICartRepository
{
    public Task<CartId> CreateAsync(Cart cart, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default);
    public Task<Cart?> GetAsync(UserId userId, CancellationToken cancellationToken = default);
}
