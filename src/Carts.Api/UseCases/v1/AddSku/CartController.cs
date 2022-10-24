using Carts.Application.Common.Contracts;
using Carts.Application.Common.Responses;
using Carts.Application.UseCases.AddSku;

using Microsoft.AspNetCore.Mvc;

namespace Carts.Api.UseCases.v1.AddSku;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public sealed class CartController : ControllerBase, IOutputPort
{
    private IActionResult? _viewModel;
    private readonly IAddSkuUseCase _useCase;

    public CartController(IAddSkuUseCase useCase)
    {
        _useCase = useCase;
    }

    void IInvalidRequest<ApplicationErrorResponse>.Invalid(ApplicationErrorResponse applicationError)
        => _viewModel = BadRequest(applicationError);

    void IOutputPort.SkuUnavailable(ApplicationErrorResponse applicationError)
        => _viewModel = new ObjectResult(applicationError) { StatusCode = StatusCodes.Status422UnprocessableEntity };

    void ISuccess<CartResponse>.Success(CartResponse success)
        => _viewModel = Ok(success);

    /// <summary>
    /// Adicionar o Sku no carrinho
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApplicationErrorResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApplicationErrorResponse))]
    [HttpPost("sku")]
    public async Task<IActionResult?> AddSku([FromBody] AddSkuRequest request, CancellationToken cancellationToken)
    {
        _useCase.SetOutputPort(this);

        await _useCase.ExecuteAsync(request, cancellationToken);

        return _viewModel;
    }
}