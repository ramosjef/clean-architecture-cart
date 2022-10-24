using System.Diagnostics.CodeAnalysis;

using Carts.Domain.ValueObjects;

namespace Carts.Infrastructure.Database;

[ExcludeFromCodeCoverage]
internal static class SeedData
{
    public static CartId DefaultCartId => Guid.Parse("204cfe06-e2a9-4c58-9669-8280f4a6f15f");
    public static UserId DefaultUserId => Guid.Parse("dcbb4217-cace-4c3a-8c51-44ff7b885960");
    public static UserId SecondaryUserId => Guid.Parse("14e365e2-6bf1-4f27-9337-b5852e3266ca");
    public static SkuId DefaultSkuId => Guid.Parse("c0e09f30-9810-4c81-a3f3-4fb1dcbc6d87");
    public static SkuId SecondarySkuId => Guid.Parse("bb7fb760-8322-41e8-a055-56b1f6c810ba");
}
