using System.Diagnostics.CodeAnalysis;

namespace Carts.Application.UseCases.IncreaseQty;

[ExcludeFromCodeCoverage]
public sealed class IncreaseQtyRequest
{
    public IncreaseQtyRequest(Guid skuId)
    {
        SkuId = skuId;
    }
    public Guid SkuId { get; set; }
}
