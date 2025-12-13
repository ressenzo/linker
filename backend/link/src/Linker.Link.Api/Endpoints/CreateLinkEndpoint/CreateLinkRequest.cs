namespace Linker.Link.Api.Endpoints.CreateLinkEndpoint;

public record CreateLinkRequest(
    string Name,
    string Url,
    string UserId)
{
    public Application.CreateLink.CreateLinkRequest ToApplicationRequest() =>
        new(
            Name,
            Url,
            UserId);
}
