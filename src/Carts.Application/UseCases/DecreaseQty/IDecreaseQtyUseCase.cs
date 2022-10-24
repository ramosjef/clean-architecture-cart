using Carts.Application.Common.Contracts;

namespace Carts.Application.UseCases.DecreaseQty;

public interface IDecreaseQtyUseCase : ISetOutputPort<IOutputPort>
{
    Task ExecuteAsync(DecreaseQtyRequest request, CancellationToken cancellationToken);
}
