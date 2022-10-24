using Carts.Api.Common.Authentication;
using Carts.Api.Common.Extensions;
using Carts.Application.UseCases;
using Carts.Infrastructure.OutboxMessages;

using Mapster;

using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddControllers();

builder.Services
       .AddEndpointsApiExplorer()
       .AddSwagger()
       .AddAuthentication(builder.Configuration)
       .AddExternalServices()
       .AddRepositories(builder.Configuration)
       .AddUseCases()
       .AddOutboxMessages(builder.Configuration)
       .AddHealthChecks(builder.Configuration)
       .AddVersioning();

TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHealthChecks();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();