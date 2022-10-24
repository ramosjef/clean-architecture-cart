using System.Diagnostics.CodeAnalysis;

using Carts.Application.Common.Responses;

namespace Carts.Application.UseCases.GetCart;

[ExcludeFromCodeCoverage]
internal class OutputPort : IOutputPort
{
    public bool IsSuccess { get; private set; }
    public CartResponse? CartResponse { get; set; }
    public void Success(CartResponse success)
    {
        IsSuccess = true;
        CartResponse = success;
    }
}