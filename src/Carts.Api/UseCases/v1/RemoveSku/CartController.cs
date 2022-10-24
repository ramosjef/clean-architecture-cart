using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;
using Carts.Application.UseCases.RemoveSku;

using Microsoft.AspNetCore.Mvc;

namespace Carts.Api.UseCases.v1.RemoveSku;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CartController : ControllerBase, IOutputPort
{
    private IActionResult? _viewModel;
    private readonly IRemoveSkuUseCase _useCase;

    public CartController(IRemoveSkuUseCase useCase)
    {
        _useCase = useCase;
    }

    void ISuccess<CartResponse>.Success(CartResponse success) => _viewModel = Ok(success);
    void INotFound<ApplicationErrorResponse>.NotFound(ApplicationErrorResponse response) => _viewModel = base.NotFound(response);

    /// <summary>
    /// Remover o sku do carrinho
    /// </summary>
    /// <param name="skuId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApplicationErrorResponse))]
    [HttpDelete("sku/{skuId}")]
    public async Task<IActionResult?> RemoveSku([FromRoute] Guid skuId, CancellationToken cancellationToken)
    {
        _useCase.SetOutputPort(this);

        await _useCase.ExecuteAsync(new(skuId), cancellationToken);

        return _viewModel;
    }
}