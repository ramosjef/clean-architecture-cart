using Carts.Application.Common.Responses;

namespace Carts.Application.UseCases.AddSku.Validation;

internal sealed class AddSkuRequestValidator : IAddSkuUseCase
{
    private readonly IAddSkuUseCase _decorated;

    private IOutputPort _outputPort;

    public AddSkuRequestValidator(IAddSkuUseCase decorated)
    {
        _decorated = decorated;
        _outputPort = new OutputPort();
    }

    public async Task ExecuteAsync(AddSkuRequest request, CancellationToken cancellationToken)
    {
        if (request.SkuId.Equals(Guid.Empty))
        {
            _outputPort.Invalid(
                new ApplicationErrorResponse(
                    "REQUIRED_FIELD", "Não foi possível adicionar o Sku", $"O campo {nameof(request.SkuId)} é obrigatório."));
            return;
        }

        await _decorated.ExecuteAsync(request, cancellationToken);
    }

    public void SetOutputPort(IOutputPort outputPort)
    {
        _outputPort = outputPort;
        _decorated.SetOutputPort(outputPort);
    }
}
