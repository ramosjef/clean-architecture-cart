using Carts.Application.ExternalServices.Catalog;
using Carts.Application.ExternalServices.Users;
using Carts.Application.UseCases.AddSku;
using Carts.Domain;
using Carts.Infrastructure.Database.Repositories.Mock;
using Carts.Infrastructure.ExternalServices.Authentication;
using Carts.Infrastructure.ExternalServices.Catalog;
using Carts.Unit.Tests.ApplicationTests.AddSku;

namespace Carts.Unit.Tests;

public class StandardFixture
{
    public StandardFixture()
    {
        CartRepository = new CartRepositoryMock();
        CartRepositoryEmpty = CartRepositoryMock.Empty();
        CatalogService = new CatalogServiceMock();
        UserService = new UserServiceMock();
        AddSkuUseCaseMockSuccess = new UseCaseMockSuccess();
    }

    public ICartRepository CartRepository { get; private set; }
    public ICartRepository CartRepositoryEmpty { get; private set; }
    public ICatalogService CatalogService { get; private set; }
    public IUserService UserService { get; private set; }
    public IAddSkuUseCase AddSkuUseCaseMockSuccess { get; private set; }
}