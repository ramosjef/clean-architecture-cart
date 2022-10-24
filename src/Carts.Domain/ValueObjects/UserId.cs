namespace Carts.Domain.ValueObjects;

public class UserId : IEquatable<UserId>
{
    public Guid Id { get; }

    public UserId(Guid id) => Id = id;

    public override bool Equals(object? obj) => obj is UserId o && Equals(o);

    public bool Equals(UserId? other) => Id == other?.Id;

    public override int GetHashCode() => HashCode.Combine(Id);

    public static bool operator ==(UserId left, UserId right) => left.Equals(right);

    public static bool operator !=(UserId left, UserId right) => !(left == right);

    public override string ToString() => Id.ToString();

    public static UserId New() => new(Guid.NewGuid());

    public static implicit operator UserId(Guid guid) => new(guid);
    public static implicit operator Guid(UserId UserId) => UserId.Id;
}