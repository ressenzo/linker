using Linker.Application.CreateLink;
using Linker.Application.GetLinksByUserId;
using Microsoft.Extensions.DependencyInjection;

namespace Linker.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICreateLinkUseCase, CreateLinkUseCase>();
        services.AddScoped<IGetLinksByUserIdUseCase, GetLinksByUserIdUseCase>();
        return services;
    }
}
