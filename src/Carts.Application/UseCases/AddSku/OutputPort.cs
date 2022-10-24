using System.Diagnostics.CodeAnalysis;

using Carts.Application.Common.Responses;

namespace Carts.Application.UseCases.AddSku;

[ExcludeFromCodeCoverage]
internal sealed class OutputPort : IOutputPort
{
    public bool IsInvalidRequest { get; private set; }
    public bool IsSuccess { get; private set; }
    public bool IsSkuUnavailable { get; private set; }
    public ApplicationErrorResponse? ApplicationErrorResponse { get; set; }
    public CartResponse? CartResponse { get; set; }

    public void Invalid(ApplicationErrorResponse applicationError)
    {
        IsInvalidRequest = true;
        ApplicationErrorResponse = applicationError;
    }

    public void SkuUnavailable(ApplicationErrorResponse applicationError)
    {
        IsSkuUnavailable = true;
        ApplicationErrorResponse = applicationError;
    }

    public void Success(CartResponse success)
    {
        IsSuccess = true;
        CartResponse = success;
    }
}
