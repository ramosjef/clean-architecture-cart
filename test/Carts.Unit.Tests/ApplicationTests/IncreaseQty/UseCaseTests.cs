using Carts.Application.Common.Responses;
using Carts.Application.UseCases.IncreaseQty;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.ApplicationTests.IncreaseQty;

public sealed class UseCaseTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;

    public UseCaseTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Increase_Sku_Quantity_UseCase_Cart_Success()
    {
        IncreaseQtyUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepository);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new IncreaseQtyRequest(SeedData.DefaultSkuId),
            CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
        Assert.NotNull(outputPort.CartResponse);
        CartItemResponse? item = outputPort.CartResponse!.Items.FirstOrDefault(x => x.SkuId.Equals(SeedData.DefaultSkuId));
        Assert.NotNull(item);
        Assert.Equal(2, item!.Quantity);
    }

    [Fact]
    public async Task Increase_Sku_Quantity_UseCase_Cart_Not_Found()
    {
        IncreaseQtyUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepositoryEmpty);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new IncreaseQtyRequest(Guid.NewGuid()),
            CancellationToken.None);

        Assert.True(outputPort.IsNotFound);
        Assert.NotNull(outputPort.ApplicationErrorResponse);
        Assert.Equal("NOT_FOUND", outputPort.ApplicationErrorResponse!.Code, StringComparer.InvariantCultureIgnoreCase);
    }
}
