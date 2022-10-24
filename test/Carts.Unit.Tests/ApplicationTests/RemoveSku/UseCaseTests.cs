using Carts.Application.UseCases.RemoveSku;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.ApplicationTests.RemoveSku;

public sealed class UseCaseTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;

    public UseCaseTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Remove_Sku_UseCase_Cart_Success()
    {
        RemoveSkuUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepository);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new RemoveSkuRequest(SeedData.DefaultSkuId),
            CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
        Assert.NotNull(outputPort.CartResponse);
        Assert.Empty(outputPort.CartResponse!.Items);
    }

    [Fact]
    public async Task Remove_Sku_UseCase_Cart_Not_Found()
    {
        RemoveSkuUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepositoryEmpty);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new RemoveSkuRequest(Guid.NewGuid()),
            CancellationToken.None);

        Assert.True(outputPort.IsNotFound);
        Assert.NotNull(outputPort.ApplicationErrorResponse);
        Assert.Equal("NOT_FOUND", outputPort.ApplicationErrorResponse!.Code, StringComparer.InvariantCultureIgnoreCase);
    }
}
