using Carts.Application.Common.Contracts;

namespace Carts.Application.UseCases.AddSku;

public interface IAddSkuUseCase : ISetOutputPort<IOutputPort>
{
    Task ExecuteAsync(AddSkuRequest request, CancellationToken cancellationToken);
}