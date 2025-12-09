namespace Linker.Domain.Entities;

public class Link(string name, string url, string userId) : Entity
{
    public string Name { get; private set; } = name;

    public string Url { get; private set; } = url;

    public string UserId { get; private set; } = userId;

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            AddError("Invalid name");
        if (string.IsNullOrWhiteSpace(Url))
            AddError("Invalid url");
        if (!string.IsNullOrWhiteSpace(Url) &&
            !Uri.TryCreate(Url, UriKind.Absolute, out Uri? _))
            AddError("Url is in invalid format");
        if (string.IsNullOrWhiteSpace(UserId))
            AddError("Invalid user id");
    }
}
