using Carts.Application.Common.Responses;
using Carts.Application.UseCases.ChangeQty;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.ApplicationTests.ChangeQty;

public sealed class UseCaseTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;

    public UseCaseTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task ChangeQty_UseCase_Cart_Not_Found()
    {
        ChangeQtyUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepositoryEmpty);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new ChangeQtyRequest() { SkuId = Guid.NewGuid(), Qty = 100 },
            CancellationToken.None);

        Assert.True(outputPort.IsNotFound);
        Assert.NotNull(outputPort.ApplicationErrorResponse);
        Assert.Equal("CART_NOT_FOUND", outputPort.ApplicationErrorResponse!.Code);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(2000)]
    public async Task ChangeQty_Success(int qty)
    {
        ChangeQtyUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepository);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new ChangeQtyRequest() { SkuId = SeedData.DefaultSkuId, Qty = qty },
            CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
        Assert.NotNull(outputPort.CartResponse);

        CartItemResponse? item = outputPort.CartResponse!.Items.FirstOrDefault(x => x.SkuId.Equals(SeedData.DefaultSkuId));
        Assert.NotNull(item);
        Assert.Equal(qty, item!.Quantity);
    }
}

