using Linker.Link.Application.CreateLink;
using Linker.Link.Application.GetLinksByUserId;
using Microsoft.Extensions.DependencyInjection;

namespace Linker.Link.Application;

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
