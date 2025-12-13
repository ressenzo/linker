using Linker.Link.Domain.Entities;

namespace Linker.Link.Test.UnitTests.Shared.Builders;

internal class LinkBuilder : BaseBuilder<Link.Domain.Entities.Link>
{
    private string? _name;
    private string? _url;
    private string? _userId;

    public LinkBuilder()
    {
        var faker = new Faker();
        _name = faker.Lorem.Word();
        _url = faker.Internet.Url();
        _userId = faker.Random.String(length: 8);
    }

    public LinkBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public LinkBuilder WithUrl(string? url)
    {
        _url = url;
        return this;
    }

    public LinkBuilder WithUserId(string? userId)
    {
        _userId = userId;
        return this;
    }

    public override Link.Domain.Entities.Link Build() =>
        new(_name!, _url!, _userId!);
}
