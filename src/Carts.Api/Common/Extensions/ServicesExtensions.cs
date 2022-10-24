using Carts.Application.ExternalServices.Catalog;
using Carts.Infrastructure.ExternalServices.Catalog;

namespace Carts.Api.Common.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddSingleton<ICatalogService, CatalogServiceMock>();

        return services;
    }
}
