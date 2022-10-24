using Carts.Domain;
using Carts.Domain.DomainEvents;
using Carts.Domain.Entities;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.DomainTests;

public sealed class IncreaseQtyTests
{
    [Fact]
    public void Increase_One_To_The_Quantity()
    {
        Cart cart = new(SeedData.DefaultCartId, SeedData.DefaultUserId, SeedData.DefaultSkuId);
        cart.ChangeQuantity(SeedData.DefaultSkuId, 99);

        cart.IncreaseQuantity(SeedData.DefaultSkuId);
        Assert.Equal(100, cart.Items[0].Quantity);

        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuQuantityChanged);
    }

    [Fact]
    public void Nothing_Happens_When_Try_To_Increase_Not_Found_Sku()
    {
        Cart cart = new(SeedData.DefaultCartId, SeedData.DefaultUserId, SeedData.DefaultSkuId);

        int currentQty = cart.IncreaseQuantity(SeedData.SecondarySkuId);

        Assert.Equal(0, currentQty);
        Assert.DoesNotContain(cart.GetDomainEvents(), x => x is CartSkuQuantityChanged);
    }

    [Fact]
    public void Increase_CartItem_Qty()
    {
        CartItem item = CartItem.New(SeedData.DefaultCartId, SeedData.DefaultSkuId);
        item.IncreaseQuantity();

        Assert.Equal(2, item.Quantity);
        Assert.Equal(SeedData.DefaultCartId, item.CartId);
        Assert.Equal(SeedData.DefaultSkuId, item.SkuId);
    }
}
