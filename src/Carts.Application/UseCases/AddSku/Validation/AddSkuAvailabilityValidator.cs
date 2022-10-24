using Carts.Application.Common.Responses;
using Carts.Application.ExternalServices.Catalog;

namespace Carts.Application.UseCases.AddSku.Validation;

internal sealed class AddSkuAvailabilityValidator : IAddSkuUseCase
{
    private readonly IAddSkuUseCase _decorated;
    private readonly ICatalogService _catalogService;
    private IOutputPort _outputPort = new OutputPort();

    public AddSkuAvailabilityValidator(IAddSkuUseCase decorated,
                                       ICatalogService catalogService)
    {
        _decorated = decorated;
        _catalogService = catalogService;
    }

    public async Task ExecuteAsync(AddSkuRequest request, CancellationToken cancellationToken)
    {
        AvailabilityResult availabilityResult = await _catalogService.GetAvailabilityAsync(request.SkuId, cancellationToken);

        if (availabilityResult.Unavailable)
        {
            _outputPort.SkuUnavailable(new ApplicationErrorResponse("CHECK_AVAILABILITY", "Não foi possível adicionar o sku", availabilityResult.Description));
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
