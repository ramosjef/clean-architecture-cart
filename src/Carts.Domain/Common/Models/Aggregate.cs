namespace Carts.Domain.Common.Models;

public abstract class Aggregate<TKey> : Entity<TKey>
{
    private readonly List<DomainEvent> _events = new();

    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _events.AsReadOnly();

    protected void RaiseEvent(DomainEvent domainEvent) => _events.Add(domainEvent);

    public void ClearEvents() => _events.Clear();
}
