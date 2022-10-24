using Carts.Domain;
using Carts.Domain.DomainEvents;
using Carts.Domain.Entities;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.DomainTests;

public sealed class DecreaseQtyTests
{
    [Fact]
    public void Decrease_One_To_The_Quantity()
    {
        Cart cart = new(SeedData.DefaultCartId, SeedData.DefaultUserId, SeedData.DefaultSkuId);
        cart.ChangeQuantity(SeedData.DefaultSkuId, 101);

        cart.DecreaseQuantity(SeedData.DefaultSkuId);

        Assert.Equal(100, cart.Items[0].Quantity);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuQuantityChanged);
    }

    [Fact]
    public void Remove_Sku_When_Quantity_Reaches_Zero()
    {
        Cart cart = new(SeedData.DefaultCartId, SeedData.DefaultUserId, SeedData.DefaultSkuId);

        cart.DecreaseQuantity(SeedData.DefaultSkuId);

        Assert.Empty(cart.Items);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuRemoved);
    }

    [Fact]
    public void Nothing_Happens_When_Try_To_Decrease_Not_Found_Sku()
    {
        Cart cart = new(SeedData.DefaultCartId, SeedData.DefaultUserId, SeedData.DefaultSkuId);

        int currentQty = cart.DecreaseQuantity(SeedData.SecondarySkuId);

        Assert.Equal(0, currentQty);
        Assert.DoesNotContain(cart.GetDomainEvents(), x => x is CartSkuQuantityChanged);
    }

    [Fact]
    public void Decrease_CartItem_Qty()
    {
        CartItem item = CartItem.New(SeedData.DefaultCartId, SeedData.DefaultSkuId);

        item.DecreaseQuantity();

        Assert.Equal(0, item.Quantity);
        Assert.Equal(SeedData.DefaultCartId, item.CartId);
        Assert.Equal(SeedData.DefaultSkuId, item.SkuId);
    }
}
