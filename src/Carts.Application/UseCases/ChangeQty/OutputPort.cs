using System.Diagnostics.CodeAnalysis;

using Carts.Application.Common.Responses;

namespace Carts.Application.UseCases.ChangeQty;

[ExcludeFromCodeCoverage]
internal sealed class OutputPort : IOutputPort
{
    public bool IsNotFound { get; private set; }
    public bool IsSuccess { get; private set; }
    public ApplicationErrorResponse? ApplicationErrorResponse { get; set; }
    public CartResponse? CartResponse { get; set; }

    public void NotFound(ApplicationErrorResponse response)
    {
        IsNotFound = true;
        ApplicationErrorResponse = response;
    }

    public void Success(CartResponse success)
    {
        IsSuccess = true;
        CartResponse = success;
    }
}