using System.Reflection;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.PlatformAbstractions;

namespace Carts.Api.Common.Extensions;

public static class ConfigurationExtensions
{
    private static readonly Dictionary<string, string> _actionsOrder = new()
    {
        { HttpMethods.Get, "A" },
        { HttpMethods.Post, "B" },
        { HttpMethods.Put, "C" },
        { HttpMethods.Patch, "D" },
        { HttpMethods.Delete, "E" },
    };

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services
            .AddSwaggerGen(x =>
            {
                x.OrderActionsBy(x =>
                {
                    _actionsOrder.TryGetValue(x.HttpMethod ?? string.Empty, out string? value);
                    return value ?? "F";
                });

                x.IncludeXmlComments(
                    Path.Combine(
                        PlatformServices.Default.Application.ApplicationBasePath,
                        typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml"));
            });

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            })
            .AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddMongoDb(configuration.GetValue<string>("MongoDb:Connection"));

        services
            .AddHealthChecksUI()
            .AddInMemoryStorage();

        return services;
    }

    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        app.UseHealthChecksUI(x =>
        {
            x.UIPath = "/hc-ui";
        });

        return app;
    }
}
