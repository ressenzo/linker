using Linker.Domain.Entities;

namespace Linker.Application.CreateLink;

public record CreateLinkRequest(
    string Name,
    string Url,
    string UserId)
{
    public static implicit operator Link(CreateLinkRequest request) =>
        new(
            request.Name,
            request.Url,
            request.UserId);
}
