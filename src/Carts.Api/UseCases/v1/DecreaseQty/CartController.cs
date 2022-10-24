using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;
using Carts.Application.UseCases.DecreaseQty;

using Microsoft.AspNetCore.Mvc;

namespace Carts.Api.UseCases.v1.DecreaseQty;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CartController : ControllerBase, IOutputPort
{
    private IActionResult? _viewModel;
    private readonly IDecreaseQtyUseCase _useCase;

    public CartController(IDecreaseQtyUseCase useCase)
    {
        _useCase = useCase;
    }

    /// <summary>    
    /// </summary>
    /// <param name="success"></param>
    void ISuccess<CartResponse>.Success(CartResponse success) => _viewModel = Ok(success);

    /// <summary>    
    /// </summary>
    /// <param name="response"></param>
    void INotFound<ApplicationErrorResponse>.NotFound(ApplicationErrorResponse response) => _viewModel = base.NotFound(response);


    /// <summary>
    /// Diminui a quantidade de dado sku
    /// </summary>
    /// <param name="skuId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApplicationErrorResponse))]
    [HttpPut("sku/{skuId:guid}/decrease-qty")]
    public async Task<IActionResult?> DecreaseQty([FromRoute] Guid skuId, CancellationToken cancellationToken)
    {
        _useCase.SetOutputPort(this);

        await _useCase.ExecuteAsync(new(skuId), cancellationToken);

        return _viewModel;
    }
}