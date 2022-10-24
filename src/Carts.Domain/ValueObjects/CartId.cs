namespace Carts.Domain.ValueObjects;

public class CartId : IEquatable<CartId>
{
    public Guid Id { get; }

    public CartId(Guid id) => Id = id;

    public override bool Equals(object? obj) => obj is CartId o && Equals(o);

    public bool Equals(CartId? other) => Id == other?.Id;

    public override int GetHashCode() => HashCode.Combine(Id);

    public static bool operator ==(CartId left, CartId right) => left.Equals(right);

    public static bool operator !=(CartId left, CartId right) => !(left == right);

    public override string ToString() => Id.ToString();

    public static CartId New() => new(Guid.NewGuid());

    public static implicit operator CartId(Guid guid) => new(guid);
    public static implicit operator Guid(CartId CartItemId) => CartItemId.Id;
}