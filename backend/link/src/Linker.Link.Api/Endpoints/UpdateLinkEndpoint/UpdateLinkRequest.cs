namespace Linker.Link.Api.Endpoints.UpdateLinkEndpoint;

public record UpdateLinkRequest(string Name, string Url)
{
    public Application.UpdateLink.UpdateLinkRequest ToApplicationRequest(
        string id
    ) =>
        new(
            id,
            Name,
            Url);
}
