using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;

namespace Carts.Application.UseCases.DecreaseQty;

public interface IOutputPort :
     ISuccess<CartResponse>,
     INotFound<ApplicationErrorResponse>
{
}
