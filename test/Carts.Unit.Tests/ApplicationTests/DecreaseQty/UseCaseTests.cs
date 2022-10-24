using Carts.Application.UseCases.DecreaseQty;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.ApplicationTests.DecreaseQty;

public sealed class UseCaseTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;

    public UseCaseTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Decrease_Sku_Quantity_UseCase_Success()
    {
        DecreaseQtyUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepository);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new DecreaseQtyRequest(SeedData.DefaultSkuId),
            CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
        Assert.NotNull(outputPort.CartResponse);
        Assert.Null(outputPort.ApplicationErrorResponse);
        Assert.Empty(outputPort.CartResponse!.Items);
    }

    [Fact]
    public async Task Decrease_Sku_Quantity_UseCase_Cart_Not_Found()
    {
        DecreaseQtyUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepositoryEmpty);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new DecreaseQtyRequest(Guid.NewGuid()),
            CancellationToken.None);

        Assert.True(outputPort.IsNotFound);
        Assert.Null(outputPort.CartResponse);
        Assert.NotNull(outputPort.ApplicationErrorResponse);
        Assert.Equal("NOT_FOUND", outputPort.ApplicationErrorResponse!.Code, StringComparer.InvariantCultureIgnoreCase);
    }
}
