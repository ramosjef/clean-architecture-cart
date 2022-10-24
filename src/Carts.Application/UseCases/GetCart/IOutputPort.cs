using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;

namespace Carts.Application.UseCases.GetCart;

public interface IOutputPort :
    ISuccess<CartResponse>
{
}

