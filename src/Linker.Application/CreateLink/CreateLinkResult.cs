using Linker.Application.Commons.Results;
using Linker.Domain.Entities;

namespace Linker.Application.CreateLink;

public record CreateLinkResult : LinkResult
{
    public CreateLinkResult(
        string Id,
        string Name,
        string Url,
        string UserId) : base(
            Id,
            Name,
            Url,
            UserId)
    { }

    public static implicit operator CreateLinkResult(Link link) =>
        new(
            link.Id,
            link.Name,
            link.Url,
            link.UserId);
}
