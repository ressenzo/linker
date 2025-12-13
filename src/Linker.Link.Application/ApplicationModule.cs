using Linker.Link.Application.CreateLink;
using Linker.Link.Application.GetLinkById;
using Linker.Link.Application.GetLinksByUserId;
using Linker.Link.Application.UpdateLink;
using Microsoft.Extensions.DependencyInjection;

namespace Linker.Link.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICreateLinkUseCase, CreateLinkUseCase>();
        services.AddScoped<IGetLinksByUserIdUseCase, GetLinksByUserIdUseCase>();
        services.AddScoped<IGetLinkByIdUseCase, GetLinkByIdUseCase>();
        services.AddScoped<IUpdateLinkUseCase, UpdateLinkUseCase>();
        return services;
    }
}
