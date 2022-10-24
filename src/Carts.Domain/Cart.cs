using Carts.Domain.Common.Models;
using Carts.Domain.DomainEvents;
using Carts.Domain.Entities;
using Carts.Domain.ValueObjects;

namespace Carts.Domain;

public sealed class Cart : Aggregate<CartId>
{
    private Cart()
    {
        Id = Guid.Empty;
        Items = new();
    }

    public Cart(CartId cartId, UserId userId, SkuId productId) : this()
    {
        Id = cartId;
        UserId = userId;

        RaiseEvent(new CartCreatedEvent(cartId, UserId));

        AddSku(productId);
    }

    public UserId UserId { get; private set; } = Guid.Empty;
    public List<CartItem> Items { get; private set; }

    public int AddSku(SkuId skuId)
    {
        if (TryGetCartItem(skuId, out _))
        {
            return IncreaseQuantity(skuId);
        }

        Items.Add(CartItem.New(Id, skuId));
        RaiseEvent(new CartSkuAdded(UserId, skuId));

        return 1;
    }

    public void RemoveSku(SkuId skuId)
    {
        if (Items.RemoveAll(i => i.SkuId.Equals(skuId)) > 0)
        {
            RaiseEvent(new CartSkuRemoved(UserId, skuId));
        }
    }

    public int IncreaseQuantity(SkuId skuId)
    {
        if (TryGetCartItem(skuId, out CartItem? item))
        {
            int currentQty = item!.IncreaseQuantity();

            RaiseEvent(new CartSkuQuantityChanged(UserId, skuId, currentQty));

            return currentQty;
        }

        return 0;
    }

    public int DecreaseQuantity(SkuId skuId)
    {
        if (Items.FirstOrDefault(x => x.SkuId.Equals(skuId)) is CartItem item)
        {
            int currentQty = item.DecreaseQuantity();
            if (currentQty <= 0)
            {
                RemoveSku(skuId);
                return 0;
            }

            RaiseEvent(new CartSkuQuantityChanged(UserId, skuId, currentQty));
            return currentQty;
        }

        return 0;
    }

    public void ChangeQuantity(SkuId skuId, int qty)
    {
        if (TryGetCartItem(skuId, out CartItem? cartItem))
        {
            cartItem!.ChangeQuantity(qty);
            RaiseEvent(new CartSkuQuantityChanged(UserId, skuId, qty));
        }
    }

    public bool TryGetCartItem(SkuId skuId, out CartItem? item)
    {
        item = Items.FirstOrDefault(x => x.SkuId.Equals(skuId));
        return item is not null;
    }

    public static Cart New(UserId userId, SkuId skuId)
        => new(CartId.New(), userId, skuId);
}
