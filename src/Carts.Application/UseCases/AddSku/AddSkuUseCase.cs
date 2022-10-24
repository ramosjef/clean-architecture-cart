using Carts.Application.Common.Responses;
using Carts.Application.ExternalServices.Catalog;
using Carts.Application.ExternalServices.Users;
using Carts.Domain;
using Carts.Domain.ValueObjects;

using Mapster;

namespace Carts.Application.UseCases.AddSku;

internal sealed class AddSkuUseCase : IAddSkuUseCase
{
    private readonly IUserService _userService;
    private readonly ICartRepository _cartRepository;
    private readonly ICatalogService _catalogService;
    private IOutputPort _outputPort = new OutputPort();

    public AddSkuUseCase(IUserService userService,
                         ICartRepository cartRepository,
                         ICatalogService catalogService)
    {
        _cartRepository = cartRepository;
        _catalogService = catalogService;
        _userService = userService;
    }

    /// <summary>
    /// Adds the specified Sku (if available) to a new cart or an existing one.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(AddSkuRequest request, CancellationToken cancellationToken)
    {
        UserId userId = _userService.GetCurrentUserId();

        if (await _cartRepository.GetAsync(userId, cancellationToken) is Cart cartFound)
        {
            cartFound.AddSku(request.SkuId);

            await _cartRepository.UpdateAsync(cartFound, cancellationToken);

            _outputPort.Success(cartFound.Adapt<CartResponse>());
            return;
        }

        Cart newCart = Cart.New(userId, request.SkuId);

        await _cartRepository.CreateAsync(newCart, cancellationToken);

        _outputPort.Success(newCart.Adapt<CartResponse>());
    }

    public void SetOutputPort(IOutputPort presenter) => _outputPort = presenter;
}
