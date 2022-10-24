using System.Diagnostics.CodeAnalysis;

namespace Carts.Application.UseCases.ChangeQty;

[ExcludeFromCodeCoverage]
public class ChangeQtyRequest
{
    public Guid SkuId { get; set; }
    public int Qty { get; set; }
}