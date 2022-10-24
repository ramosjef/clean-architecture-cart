using Carts.Domain;
using Carts.Domain.DomainEvents;
using Carts.Domain.Entities;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.DomainTests;

public sealed class ChangeQtyTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;

    public ChangeQtyTests(StandardFixture fixture) => _fixture = fixture;

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(17)]
    public void Accept_New_Quantity(int qty)
    {
        Cart cart = Cart.New(SeedData.DefaultUserId, SeedData.DefaultSkuId);

        cart.ChangeQuantity(SeedData.DefaultSkuId, qty);

        Assert.Equal(qty, cart.Items[0].Quantity);
    }

    [Fact]
    public void Nothing_Happens_When_Try_To_Change_Not_Found_Sku()
    {
        Cart cart = Cart.New(SeedData.DefaultUserId, SeedData.DefaultSkuId);

        cart.ChangeQuantity(SeedData.SecondarySkuId, 1);
        bool hasItem = cart.TryGetCartItem(SeedData.SecondarySkuId, out CartItem? item);

        Assert.False(hasItem);
        Assert.Null(item);
        Assert.DoesNotContain(cart.GetDomainEvents(), x => x is CartSkuQuantityChanged);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(15)]
    public void Produce_Cart_Sku_Qty_Changed_Event(int qty)
    {
        Cart cart = Cart.New(SeedData.DefaultUserId, SeedData.DefaultSkuId);

        cart.ChangeQuantity(SeedData.DefaultSkuId, qty);

        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuQuantityChanged);
        CartSkuQuantityChanged? ev = cart.GetDomainEvents().First(x => x is CartSkuQuantityChanged) as CartSkuQuantityChanged;
        Assert.Equal(qty, ev!.Qty);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(749)]
    public void Change_CartItem_Qty(int qty)
    {
        CartItem item = CartItem.New(SeedData.DefaultCartId, SeedData.DefaultSkuId);

        item.ChangeQuantity(qty);

        Assert.Equal(qty, item.Quantity);
        Assert.Equal(SeedData.DefaultCartId, item.CartId);
        Assert.Equal(SeedData.DefaultSkuId, item.SkuId);
    }
}

