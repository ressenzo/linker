using Linker.Link.Application.Repositories;
using Linker.Link.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Linker.Link.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ILinkRepository, LinkRepository>();
        return services;
    }
}
