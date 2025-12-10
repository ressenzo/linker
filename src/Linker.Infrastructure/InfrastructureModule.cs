using Linker.Application.Repositories;
using Linker.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Linker.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ILinkRepository, LinkRepository>();
        return services;
    }
}
