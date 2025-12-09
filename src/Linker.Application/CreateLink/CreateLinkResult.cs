using Linker.Domain.Entities;

namespace Linker.Application.CreateLink;

public record CreateLinkResult(
    string Id,
    string Name,
    string Url,
    string UserId)
{
    public static implicit operator CreateLinkResult(Link link) =>
        new(
            link.Id,
            link.Name,
            link.Url,
            link.UserId);
}
