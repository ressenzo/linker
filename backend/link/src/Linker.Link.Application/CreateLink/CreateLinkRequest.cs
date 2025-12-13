namespace Linker.Link.Application.CreateLink;

public record CreateLinkRequest(
    string Name,
    string Url,
    string UserId)
{
    public Domain.Entities.Link ToDomainEntity() =>
        new(
            Name,
            Url,
            UserId);
}
