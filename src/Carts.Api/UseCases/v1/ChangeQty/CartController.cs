using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;
using Carts.Application.UseCases.ChangeQty;

using Microsoft.AspNetCore.Mvc;

namespace Carts.Api.UseCases.v1.ChangeQty;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CartController : ControllerBase, IOutputPort
{
    private IActionResult? _viewModel;
    private readonly IChangeQtyUseCase _useCase;

    public CartController(IChangeQtyUseCase useCase)
    {
        _useCase = useCase;
    }

    void ISuccess<CartResponse>.Success(CartResponse success) => _viewModel = Ok(success);
    void INotFound<ApplicationErrorResponse>.NotFound(ApplicationErrorResponse response) => _viewModel = base.NotFound(response);

    /// <summary>
    /// Alterar a quantidade de dado sku
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApplicationErrorResponse))]
    [HttpPut("sku/{skuId}/change-qty/")]
    public async Task<IActionResult?> ChangeQty([FromBody] ChangeQtyRequest request,
                                                CancellationToken cancellationToken)
    {
        _useCase.SetOutputPort(this);

        await _useCase.ExecuteAsync(request, cancellationToken);

        return _viewModel;
    }
}