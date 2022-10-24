using System.Diagnostics.CodeAnalysis;

using Carts.Application.ExternalServices.Users;
using Carts.Infrastructure.Database;

namespace Carts.Infrastructure.ExternalServices.Authentication;

[ExcludeFromCodeCoverage]
public sealed class UserServiceMock : IUserService
{
    public Guid GetCurrentUserId() => SeedData.DefaultUserId;
}
