using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.ApplicationCore;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
