using Carts.Domain.Common.Models;
using Carts.Domain.ValueObjects;

namespace Carts.Domain.Entities;

public sealed class CartItem : Entity<CartItemId>
{
    private CartItem(CartItemId cartItemId,
                     CartId cartId,
                     SkuId skuId,
                     int quantity)
    {
        Id = cartItemId;
        CartId = cartId;
        SkuId = skuId;
        Quantity = quantity;
    }

    public CartId CartId { get; private set; }
    public SkuId SkuId { get; private set; }
    public int Quantity { get; private set; }

    internal int IncreaseQuantity()
    {
        Quantity++;
        return Quantity;
    }

    internal int DecreaseQuantity()
    {
        Quantity--;
        return Quantity;
    }

    internal void ChangeQuantity(int quantity) => Quantity = quantity;

    internal static CartItem New(CartId cartId, SkuId skuId) => new(CartItemId.New(), cartId, skuId, 1);
}
