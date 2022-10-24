using System.Diagnostics.CodeAnalysis;

using Carts.Domain;
using Carts.Domain.ValueObjects;

namespace Carts.Infrastructure.Database.Repositories.Mock;

[ExcludeFromCodeCoverage]
public sealed class CartRepositoryMock : ICartRepository
{
    private readonly List<Cart> Carts = new()
    {
        Cart.New(SeedData.DefaultUserId, SeedData.DefaultSkuId),
        Cart.New(SeedData.SecondaryUserId, SeedData.SecondarySkuId),
    };

    public CartRepositoryMock()
    {
        Carts.ForEach(x => x.ClearEvents());
    }

    internal static ICartRepository Empty()
    {
        CartRepositoryMock cartRepositoryMock = new();

        cartRepositoryMock.Carts.Clear();

        return cartRepositoryMock;
    }

    public Task<CartId> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        Carts.Add(cart);
        return Task.FromResult(cart.Id);
    }

    public Task<Cart?> GetAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Carts.FirstOrDefault(c => c.UserId.Equals(userId)));
    }

    public Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        if (Carts.FirstOrDefault(c => c.Id.Equals(cart.Id)) is Cart found)
            found = cart;

        return Task.CompletedTask;
    }
}
