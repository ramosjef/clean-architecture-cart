using System.Diagnostics.CodeAnalysis;

namespace Carts.Application.UseCases.RemoveSku;

[ExcludeFromCodeCoverage]
public class RemoveSkuRequest
{
    public RemoveSkuRequest(Guid skuId)
    {
        SkuId = skuId;
    }

    public Guid SkuId { get; set; }
}
