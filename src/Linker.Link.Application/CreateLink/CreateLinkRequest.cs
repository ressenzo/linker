namespace Linker.Link.Application.CreateLink;

public record CreateLinkRequest(
    string Name,
    string Url,
    string UserId)
{
    public static implicit operator Domain.Entities.Link(CreateLinkRequest request) =>
        new(
            request.Name,
            request.Url,
            request.UserId);
}
