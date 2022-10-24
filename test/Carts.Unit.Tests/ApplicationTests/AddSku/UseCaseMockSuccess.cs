using Carts.Application.Common.Responses;
using Carts.Application.UseCases.AddSku;

namespace Carts.Unit.Tests.ApplicationTests.AddSku;

internal class UseCaseMockSuccess : IAddSkuUseCase
{
    private IOutputPort _outputPort = new OutputPort();

    public Task ExecuteAsync(AddSkuRequest request, CancellationToken cancellationToken)
    {
        _outputPort.Success(new CartResponse());
        return Task.CompletedTask;
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
