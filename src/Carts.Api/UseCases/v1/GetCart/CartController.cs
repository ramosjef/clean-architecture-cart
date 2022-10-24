using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;
using Carts.Application.UseCases.GetCart;

using Microsoft.AspNetCore.Mvc;

namespace Carts.Api.UseCases.v1.GetCart;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CartController : ControllerBase, IOutputPort
{
    private IActionResult? _viewModel;
    private readonly IGetCartUseCase _useCase;

    public CartController(IGetCartUseCase useCase)
    {
        _useCase = useCase;
    }

    /// <summary>    
    /// </summary>
    /// <param name="success"></param>
    void ISuccess<CartResponse>.Success(CartResponse success) => _viewModel = Ok(success);

    /// <summary>
    /// Obtém o carrinho
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApplicationErrorResponse))]
    [HttpGet]
    public async Task<IActionResult?> GetCart(CancellationToken cancellationToken)
    {
        _useCase.SetOutputPort(this);

        await _useCase.ExecuteAsync(cancellationToken);

        return _viewModel;
    }
}