using System.Diagnostics.CodeAnalysis;

namespace Carts.Application.UseCases.AddSku;

[ExcludeFromCodeCoverage]
public class AddSkuRequest
{
    public Guid SkuId { get; set; }
}
