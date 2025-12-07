namespace Linker.Api.Endpoints.CreateLinkEndpoint;

public record CreateLinkResponse(
    Guid Id,
    string Name,
    string Url,
    Guid UserId);
