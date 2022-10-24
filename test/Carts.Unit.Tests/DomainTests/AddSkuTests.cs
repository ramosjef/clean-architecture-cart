using Carts.Domain;
using Carts.Domain.DomainEvents;
using Carts.Domain.Entities;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.DomainTests;

public sealed class AddSkuTests
{
    [Fact]
    public void Adding_Existing_Sku_Should_Increase_Quantity()
    {
        Cart cart = new(
            SeedData.DefaultCartId,
            SeedData.DefaultUserId,
            SeedData.DefaultSkuId);

        cart.AddSku(SeedData.DefaultSkuId);
        bool hasSku = cart.TryGetCartItem(SeedData.DefaultSkuId, out CartItem? cartItem);

        Assert.True(hasSku);
        Assert.Single(cart.Items);
        Assert.Equal(2, cartItem!.Quantity);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartCreatedEvent);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuAdded);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuQuantityChanged);
    }

    [Fact]
    public void Creating_New_Cart_Should_Produce_CartSkuAdded()
    {
        Cart cart = Cart.New(SeedData.DefaultUserId, SeedData.DefaultSkuId);
        bool hasItem = cart.TryGetCartItem(SeedData.DefaultSkuId, out CartItem? item);

        Assert.NotNull(cart.Id);
        Assert.Equal(SeedData.DefaultUserId, cart.UserId);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartCreatedEvent);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuAdded);
        Assert.True(hasItem);
        Assert.Equal(1, item!.Quantity);

        cart.ClearEvents();
        Assert.Empty(cart.GetDomainEvents());
    }
}
