using Carts.Application.Common.Contracts;
using Carts.Domain;
using Carts.Infrastructure.Database;
using Carts.Infrastructure.Database.Repositories;

namespace Carts.Api.Common.Extensions;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection mongoConfig = configuration.GetSection("MongoDb");

        string conn = mongoConfig.GetValue<string>("Connection");
        string db = mongoConfig.GetValue<string>("Database");

        services.AddSingleton<IMongoContext>(x => new MongoDbContext(conn, db));

        services.AddSingleton<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<ICartRepository, CartRepository>();

        return services;
    }
}
