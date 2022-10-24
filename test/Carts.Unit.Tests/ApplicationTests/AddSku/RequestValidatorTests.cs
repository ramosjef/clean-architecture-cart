using Carts.Application.UseCases.AddSku;
using Carts.Application.UseCases.AddSku.Validation;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.ApplicationTests.AddSku;

public class RequestValidatorTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;
    public RequestValidatorTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Validate_Request_Success()
    {
        OutputPort outputPort = new();
        _fixture.AddSkuUseCaseMockSuccess.SetOutputPort(outputPort);

        AddSkuRequestValidator useCase = new(_fixture.AddSkuUseCaseMockSuccess);
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(new AddSkuRequest() { SkuId = SeedData.DefaultSkuId }, CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
    }

    [Fact]
    public async Task Validate_Request_Invalid_Request()
    {
        OutputPort outputPort = new();
        _fixture.AddSkuUseCaseMockSuccess.SetOutputPort(outputPort);

        AddSkuRequestValidator useCase = new(_fixture.AddSkuUseCaseMockSuccess);
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(new AddSkuRequest() { }, CancellationToken.None);

        Assert.True(outputPort.IsInvalidRequest);
        Assert.NotNull(outputPort.ApplicationErrorResponse);
    }
}
