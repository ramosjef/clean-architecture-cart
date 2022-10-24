using Carts.Application.Common.Responses;
using Carts.Application.ExternalServices.Users;
using Carts.Domain;
using Carts.Domain.ValueObjects;

using Mapster;

namespace Carts.Application.UseCases.GetCart;

internal sealed class GetCartUseCase : IGetCartUseCase
{
    private readonly IUserService _userService;
    private readonly ICartRepository _cartRepository;

    private IOutputPort _outputPort = new OutputPort();

    public GetCartUseCase(IUserService userService,
                          ICartRepository cartRepository)
    {
        _userService = userService;
        _cartRepository = cartRepository;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        UserId userId = _userService.GetCurrentUserId();

        Cart? cart = await _cartRepository.GetAsync(userId, cancellationToken);

        _outputPort.Success(cart?.Adapt<CartResponse>() ?? new CartResponse());
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
