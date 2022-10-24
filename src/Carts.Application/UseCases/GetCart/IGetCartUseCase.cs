using Carts.Application.Common.Contracts;

namespace Carts.Application.UseCases.GetCart;

public interface IGetCartUseCase : ISetOutputPort<IOutputPort>
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}
