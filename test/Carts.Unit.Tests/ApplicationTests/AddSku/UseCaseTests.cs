using Carts.Application.UseCases.AddSku;
using Carts.Infrastructure.Database;

namespace Carts.Unit.Tests.ApplicationTests.AddSku;

public sealed class UseCaseTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;

    public UseCaseTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Adding_Item_Creates_A_New_Cart_When_It_Does_Not_Exist()
    {
        AddSkuUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepositoryEmpty,
            _fixture.CatalogService);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new AddSkuRequest() { SkuId = SeedData.DefaultSkuId },
            CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
        Assert.Null(outputPort.ApplicationErrorResponse);
        Assert.NotNull(outputPort.CartResponse);
        Assert.Contains(outputPort.CartResponse!.Items, x => x.SkuId.Equals(SeedData.DefaultSkuId) && x.Quantity == 1);
    }

    [Fact]
    public async Task Should_Add_Item_To_Existing_Cart()
    {
        AddSkuUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepository,
            _fixture.CatalogService);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(
            new AddSkuRequest() { SkuId = SeedData.SecondarySkuId },
            CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
        Assert.Null(outputPort.ApplicationErrorResponse);
        Assert.NotNull(outputPort.CartResponse);
        Assert.Contains(outputPort.CartResponse!.Items,
            x => x.SkuId.Equals(SeedData.DefaultSkuId) && x.Quantity == 1);
    }
}
