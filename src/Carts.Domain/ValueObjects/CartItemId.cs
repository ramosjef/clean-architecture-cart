namespace Carts.Domain.ValueObjects;

public class CartItemId : IEquatable<CartItemId>
{
    public Guid Id { get; }

    public CartItemId(Guid id) => Id = id;

    public override bool Equals(object? obj) => obj is CartItemId o && Equals(o);

    public bool Equals(CartItemId? other) => Id == other?.Id;

    public override int GetHashCode() => HashCode.Combine(Id);

    public static bool operator ==(CartItemId left, CartItemId right) => left.Equals(right);

    public static bool operator !=(CartItemId left, CartItemId right) => !(left == right);

    public override string ToString() => Id.ToString();

    public static CartItemId New() => new(Guid.NewGuid());

    public static implicit operator CartItemId(Guid guid) => new(guid);
    public static implicit operator Guid(CartItemId CartItemId) => CartItemId.Id;
}