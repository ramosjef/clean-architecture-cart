namespace Carts.Domain.ValueObjects;

public sealed class SkuId : IEquatable<SkuId>
{
    public Guid Id { get; }

    public SkuId(Guid id) => Id = id;

    public override bool Equals(object? obj) => obj is SkuId o && Equals(o);

    public bool Equals(SkuId? other) => Id == other?.Id;

    public override int GetHashCode() => HashCode.Combine(Id);

    public static bool operator ==(SkuId left, SkuId right) => left.Equals(right);

    public static bool operator !=(SkuId left, SkuId right) => !(left == right);

    public override string ToString() => Id.ToString();

    public static SkuId New() => new(Guid.NewGuid());

    public static implicit operator SkuId(Guid guid) => new(guid);
    public static implicit operator Guid(SkuId skuId) => skuId.Id;
}