namespace Linker.Api.Endpoints.CreateLinkEndpoint;

public record CreateLinkRequest(
    string Name,
    string Url,
    Guid UserId);
