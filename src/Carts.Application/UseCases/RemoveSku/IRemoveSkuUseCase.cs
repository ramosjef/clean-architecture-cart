using Carts.Application.Common.Contracts;

namespace Carts.Application.UseCases.RemoveSku;

public interface IRemoveSkuUseCase : ISetOutputPort<IOutputPort>
{
    Task ExecuteAsync(RemoveSkuRequest request, CancellationToken cancellationToken);
}
