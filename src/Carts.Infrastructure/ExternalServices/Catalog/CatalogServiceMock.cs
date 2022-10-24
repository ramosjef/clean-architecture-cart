using System.Diagnostics.CodeAnalysis;

using Carts.Application.ExternalServices.Catalog;
using Carts.Infrastructure.Database;

namespace Carts.Infrastructure.ExternalServices.Catalog;

[ExcludeFromCodeCoverage]
public sealed class CatalogServiceMock : ICatalogService
{
    private readonly List<AvailabilityResult> AvailabilityResults = new()
    {
        new AvailabilityResult()
        {
            ProductId = SeedData.DefaultSkuId,
            Description = "TESTE 1",
            IsAvailable = true
        },
        new AvailabilityResult()
        {
            ProductId = SeedData.SecondarySkuId,
            Description = "TESTE 2",
            IsAvailable = false
        }
    };

    public Task<AvailabilityResult> GetAvailabilityAsync(Guid productId, CancellationToken cancellationToken)
    {
        AvailabilityResult? availabilityResult = AvailabilityResults.FirstOrDefault(a => a.ProductId == productId, new(productId, "Produto não encontrado.", false));
        return Task.FromResult(availabilityResult);
    }
}
