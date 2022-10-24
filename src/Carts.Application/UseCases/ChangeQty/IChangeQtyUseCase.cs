using Carts.Application.Common.Contracts;

namespace Carts.Application.UseCases.ChangeQty;

public interface IChangeQtyUseCase : ISetOutputPort<IOutputPort>
{
    Task ExecuteAsync(ChangeQtyRequest request, CancellationToken cancellationToken);
}
