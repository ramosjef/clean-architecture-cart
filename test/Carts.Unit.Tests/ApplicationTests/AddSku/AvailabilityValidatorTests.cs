using Carts.Application.UseCases.AddSku;
using Carts.Application.UseCases.AddSku.Validation;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.ApplicationTests.AddSku;

public class AvailabilityValidatorTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;
    public AvailabilityValidatorTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Validate_Availability_Success()
    {
        OutputPort outputPort = new();
        _fixture.AddSkuUseCaseMockSuccess.SetOutputPort(outputPort);

        AddSkuAvailabilityValidator useCase = new(_fixture.AddSkuUseCaseMockSuccess, _fixture.CatalogService);
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(new AddSkuRequest() { SkuId = SeedData.DefaultSkuId }, CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
    }

    [Fact]
    public async Task Validate_Availability_Unavailable_Sku()
    {
        OutputPort outputPort = new();
        _fixture.AddSkuUseCaseMockSuccess.SetOutputPort(outputPort);

        AddSkuAvailabilityValidator useCase = new(_fixture.AddSkuUseCaseMockSuccess, _fixture.CatalogService);
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(new AddSkuRequest() { SkuId = SeedData.SecondarySkuId }, CancellationToken.None);

        Assert.True(outputPort.IsSkuUnavailable);
        Assert.NotNull(outputPort.ApplicationErrorResponse);
    }
}
