namespace Carts.Application.ExternalServices.Catalog;

public interface ICatalogService
{
    Task<AvailabilityResult> GetAvailabilityAsync(Guid productId, CancellationToken cancellationToken);
}
