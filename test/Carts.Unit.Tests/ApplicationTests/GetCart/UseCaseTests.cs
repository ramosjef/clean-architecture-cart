using Carts.Application.UseCases.GetCart;

namespace Carts.Unit.Tests.ApplicationTests.GetCart;

public sealed class UseCaseTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;
    public UseCaseTests(StandardFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Get_Cart_UseCase_Success()
    {
        GetCartUseCase useCase = new(
            _fixture.UserService,
            _fixture.CartRepository);

        OutputPort outputPort = new();
        useCase.SetOutputPort(outputPort);

        await useCase.ExecuteAsync(CancellationToken.None);

        Assert.True(outputPort.IsSuccess);
        Assert.NotNull(outputPort.CartResponse);
    }
}
