using System.Diagnostics.CodeAnalysis;

namespace Carts.Application.UseCases.DecreaseQty;

[ExcludeFromCodeCoverage]
public class DecreaseQtyRequest
{
    public DecreaseQtyRequest(Guid skuId)
    {
        SkuId = skuId;
    }

    public Guid SkuId { get; set; }
}
