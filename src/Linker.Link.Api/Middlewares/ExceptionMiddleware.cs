using System.Net;
using System.Text.Json;
using Linker.Link.Api.Models;
using Linker.Link.Application.Commons;

namespace Linker.Link.Api.Middlewares;

internal sealed class ExceptionMiddleware(
    ILogger<ExceptionMiddleware> logger,
    RequestDelegate next
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Error: {Message}",
                exception.Message);
            await HandleException(context);
        }
    }

    private static async Task HandleException(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = new ErrorModel(
            ["Something went wrong. Try again!"],
            (int)ResultType.INTERNAL_ERROR
        );
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(error)
        );
    }
}
