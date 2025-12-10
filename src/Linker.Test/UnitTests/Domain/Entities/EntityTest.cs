using Linker.Domain.Entities;

namespace Linker.Test.UnitTests.Domain.Entities;

public class EntityTest
{
    private const string FAKE_ERROR = $"{nameof(FakeEntity.Name)} cannot be empty";

    [Fact]
    public void ShouldAddError()
    {
        // Arrange
        var entity = new FakeEntity(
            name: string.Empty);
        entity.Errors.ShouldBeEmpty();

        // Act
        entity.Validate();

        // Assert
        entity.Errors.Count().ShouldBe(1);
        entity.Errors.First().ShouldBe(FAKE_ERROR);
        entity.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void ShouldInstatiateAndCreateNewId()
    {
        // Arrange - Act
        var entity = new FakeEntity(name: "Name");

        // Assert
        entity.Id.ShouldNotBeEmpty();
        entity.Errors.ShouldBeEmpty();
        entity.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void ShouldInstatiateAndSetPassedId()
    {
        // Arrange - Act
        var id = "12345678";
        var entity = new FakeEntity(
            name: "Name",
            id);

        // Assert
        entity.Id.ShouldBe(id);
        entity.Errors.ShouldBeEmpty();
        entity.IsValid.ShouldBeTrue();
    }

    public class FakeEntity : Entity
    {
        public FakeEntity(string name) =>
            Name = name;

        public FakeEntity(
            string name,
            string id) : base(id) =>
            Name = name;

        public string Name { get; }

        public override void Validate()
        {
            if (Name == string.Empty)
                AddError(FAKE_ERROR);
        }
    }
}
