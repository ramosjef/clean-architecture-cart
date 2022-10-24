using Carts.Domain;
using Carts.Domain.DomainEvents;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.DomainTests;

public sealed class RemoveSkuTests
{
    [Fact]
    public void Remove_Sku_From_Cart()
    {
        Cart cart = new(SeedData.DefaultCartId, SeedData.DefaultUserId, SeedData.DefaultSkuId);

        cart.RemoveSku(SeedData.DefaultSkuId);

        Assert.Empty(cart.Items);
        Assert.Contains(cart.GetDomainEvents(), x => x is CartSkuRemoved);
    }

    [Fact]
    public void Nothing_Happens_When_Try_To_Remove_Not_Found_Sku()
    {
        Cart cart = new(SeedData.DefaultCartId, SeedData.DefaultUserId, SeedData.DefaultSkuId);

        cart.RemoveSku(SeedData.SecondarySkuId);

        Assert.DoesNotContain(cart.GetDomainEvents(), x => x is CartSkuRemoved);
    }
}
