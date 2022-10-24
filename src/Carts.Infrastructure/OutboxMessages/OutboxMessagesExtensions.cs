using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Carts.Application.EventHandlers.CartSkuAddedEvent;
using Carts.Domain;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Quartz;

namespace Carts.Infrastructure.OutboxMessages;

[ExcludeFromCodeCoverage]
public static class OutboxMessagesExtensions
{
    public static IServiceCollection AddOutboxMessages(this IServiceCollection services, IConfiguration configuration)
    {
        var outboxConfiguration = new OutboxConfiguration();
        configuration.Bind("OutboxMessages", outboxConfiguration);

        services.AddMediatR(Assembly.GetAssembly(typeof(SendNotificationSkuAddedEventHandler))!);

        services.AddQuartz(cfg =>
        {
            JobKey jobKey = new(nameof(OutboxMessagesBackgroundJob));

            cfg.AddJob<OutboxMessagesBackgroundJob>(jobKey)
               .AddTrigger(trigger => trigger.ForJob(jobKey)
                                             .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(outboxConfiguration.JobExecutionInterval)
                                                                                     .RepeatForever()));

            cfg.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        services.Decorate<ICartRepository, CartRepositoryOutboxDecorator>();
        services.Decorate(typeof(INotificationHandler<>), typeof(OutboxNotificationIdempotentHandler<>));

        return services;
    }
}
