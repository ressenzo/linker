namespace Linker.Api.Endpoints.CreateLinkEndpoint;

public record CreateLinkRequest(
    string Name,
    string Url,
    string UserId)
{
    public static implicit operator Application.CreateLink.CreateLinkRequest(
        CreateLinkRequest request) =>
        new(
            request.Name,
            request.Url,
            request.UserId);
}
