using Carts.Application.Common.Contracts;

namespace Carts.Application.UseCases.IncreaseQty;

public interface IIncreaseQtyUseCase : ISetOutputPort<IOutputPort>
{
    Task ExecuteAsync(IncreaseQtyRequest request, CancellationToken cancellationToken);
}
