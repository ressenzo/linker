using Linker.Domain.Entities;

namespace Linker.Test.UnitTests.Shared.Builders;

internal class LinkBuilder : BaseBuilder<Link>
{
    private string? _name;
    private string? _url;

    public LinkBuilder()
    {
        var faker = new Faker();
        _name = faker.Lorem.Word();
        _url = faker.Internet.Url();
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

    public override Link Build() =>
        new(_name!, _url!);
}
