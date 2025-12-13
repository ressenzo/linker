using Linker.Link.Application.UpdateLink;

namespace Linker.Link.Test.UnitTests.Shared.Builders;

internal class UpdateLinkRequestBuilder : BaseBuilder<UpdateLinkRequest>
{
    private readonly string _id;
    private string? _name;
    private readonly string _url;

    public UpdateLinkRequestBuilder()
    {
        var faker = new Faker();
        _id = faker.Random.String(length: 8);
        _name = faker.Random.Word();
        _url = faker.Internet.Url();
    }

    public UpdateLinkRequestBuilder WithInvalidRequest()
    {
        _name = null;
        return this;
    }

    public override UpdateLinkRequest Build() =>
        new(_id, _name!, _url);
}
