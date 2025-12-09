using Linker.Application.CreateLink;

namespace Linker.Test.UnitTests.Shared.Builders;

internal class CreateLinkRequestBuilder : BaseBuilder<CreateLinkRequest>
{
    private string? _name;
    private string? _url;
    private string? _userId;

    public CreateLinkRequestBuilder()
    {
        var faker = new Faker();
        _name = faker.Lorem.Word();
        _url = faker.Internet.Url();
        _userId = faker.Random.String(length: 8);
    }

    public CreateLinkRequestBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public CreateLinkRequestBuilder WithUrl(string? url)
    {
        _url = url;
        return this;
    }

    public CreateLinkRequestBuilder WithUserId(string? userId)
    {
        _userId = userId;
        return this;
    }

    public override CreateLinkRequest Build() =>
        new(
            _name!,
            _url!,
            _userId!);
}
