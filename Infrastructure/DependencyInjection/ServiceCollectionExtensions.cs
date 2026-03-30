using Infrastructure.Authorization.Fakes;
using Infrastructure.Authorization.Meta;
using Infrastructure.CQRS.Implementation;
using Infrastructure.CQRS.Meta;
using Infrastructure.Events.Fakes;
using Infrastructure.Events.Meta;
using Infrastructure.ExternalServices.Fakes;
using Infrastructure.ExternalServices.Meta;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("LiveCodingDb"));

        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IAuthorizationAccessor, FakeAuthorizationAccessor>();

        services.AddSingleton<FakeEventBus>();
        services.AddSingleton<IEventBus>(sp => sp.GetRequiredService<FakeEventBus>());

        services.AddScoped<IComplianceService, FakeComplianceService>();

        return services;
    }

    public static IServiceCollection AddHandlers(this IServiceCollection services, System.Reflection.Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                 i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))))
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            var handlerInterfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType &&
                    (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                     i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)));

            foreach (var handlerInterface in handlerInterfaces)
            {
                services.AddScoped(handlerInterface, handlerType);
            }
        }

        return services;
    }
}
