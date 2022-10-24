using Carts.Application.Common.Responses;
using Carts.Application.ExternalServices.Users;
using Carts.Domain;
using Carts.Domain.ValueObjects;

using Mapster;

namespace Carts.Application.UseCases.ChangeQty;

internal sealed class ChangeQtyUseCase : IChangeQtyUseCase
{
    private readonly IUserService _userService;
    private readonly ICartRepository _cartRepository;
    private IOutputPort _outputPort;

    public ChangeQtyUseCase(IUserService userService,
                            ICartRepository cartRepository)
    {
        _userService = userService;
        _cartRepository = cartRepository;

        _outputPort = new OutputPort();
    }

    public async Task ExecuteAsync(ChangeQtyRequest request, CancellationToken cancellationToken)
    {
        UserId userId = _userService.GetCurrentUserId();

        if (await _cartRepository.GetAsync(userId, cancellationToken) is Cart cart)
        {
            if (cart.TryGetCartItem(request.SkuId, out _))
            {
                cart.ChangeQuantity(request.SkuId, request.Qty);
                await _cartRepository.UpdateAsync(cart, cancellationToken);
            }

            _outputPort.Success(cart.Adapt<CartResponse>());
            return;
        }

        _outputPort.NotFound(new ApplicationErrorResponse("CART_NOT_FOUND", "Não é possivel alterar a quantidade", "O carrinho não existe."));
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
