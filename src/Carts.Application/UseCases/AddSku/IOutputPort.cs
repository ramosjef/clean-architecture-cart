using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;

namespace Carts.Application.UseCases.AddSku;

public interface IOutputPort :
    ISuccess<CartResponse>,
    IInvalidRequest<ApplicationErrorResponse>
{
    void SkuUnavailable(ApplicationErrorResponse applicationError);
}
