namespace Carts.Application.ExternalServices.Catalog;

public class AvailabilityResult
{
    public AvailabilityResult() { }
    public AvailabilityResult(Guid productId, string description, bool isAvailable) : this()
    {
        ProductId = productId;
        Description = description;
        IsAvailable = isAvailable;
    }

    public Guid ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public bool Unavailable => !IsAvailable;
}