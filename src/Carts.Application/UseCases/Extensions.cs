using System.Diagnostics.CodeAnalysis;

using Carts.Application.UseCases.AddSku;
using Carts.Application.UseCases.AddSku.Validation;
using Carts.Application.UseCases.ChangeQty;
using Carts.Application.UseCases.DecreaseQty;
using Carts.Application.UseCases.GetCart;
using Carts.Application.UseCases.IncreaseQty;
using Carts.Application.UseCases.RemoveSku;

using Microsoft.Extensions.DependencyInjection;

namespace Carts.Application.UseCases;

[ExcludeFromCodeCoverage]
public static class Extensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddSkuUseCase, AddSkuUseCase>();
        services.Decorate<IAddSkuUseCase, AddSkuAvailabilityValidator>();
        services.Decorate<IAddSkuUseCase, AddSkuRequestValidator>();

        services.AddScoped<IChangeQtyUseCase, ChangeQtyUseCase>();

        services.AddScoped<IDecreaseQtyUseCase, DecreaseQtyUseCase>();

        services.AddScoped<IIncreaseQtyUseCase, IncreaseQtyUseCase>();

        services.AddScoped<IRemoveSkuUseCase, RemoveSkuUseCase>();

        services.AddScoped<IGetCartUseCase, GetCartUseCase>();

        return services;
    }
}
