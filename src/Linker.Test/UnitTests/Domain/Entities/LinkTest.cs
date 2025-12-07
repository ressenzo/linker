using Linker.Test.UnitTests.Shared.Builders;

namespace Linker.Test.UnitTests.Domain.Entities;

public class LinkTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldReturnFalse_WhenNameIsInvalid(string? name)
    {
        // Arrange
        var link = new LinkBuilder()
            .WithName(name)
            .Build();
        
        // Act
        link.Validate();
        
        // Assert
        link.IsValid.ShouldBeFalse();
        link.Errors.ShouldHaveSingleItem();
        link.Errors.First().ShouldBe("Invalid name");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldReturnFalse_WhenUrlIsInvalid(string? url)
    {
        // Arrange
        var link = new LinkBuilder()
            .WithUrl(url)
            .Build();
        
        // Act
        link.Validate();
        
        // Assert
        link.IsValid.ShouldBeFalse();
        link.Errors.ShouldHaveSingleItem();
        link.Errors.First().ShouldBe("Invalid url");
    }

    [Theory]
    [InlineData("invalid-url")]
    [InlineData("not a url")]
    [InlineData("http://")]
    public void ShouldReturnFalse_WhenUrlIsInInvalidFormat(string url)
    {
        // Arrange
        var link = new LinkBuilder()
            .WithUrl(url)
            .Build();
        
        // Act
        link.Validate();
        
        // Assert
        link.IsValid.ShouldBeFalse();
        link.Errors.ShouldHaveSingleItem();
        link.Errors.First().ShouldBe("Url is in invalid format");
    }

    [Fact]
    public void ShouldReturnTrue_WhenValuesAreValid()
    {
        // Arrange
        var link = new LinkBuilder()
            .Build();
        
        // Act
        link.Validate();
        
        // Assert
        link.IsValid.ShouldBeTrue();
        link.Errors.ShouldBeEmpty();
    }
}
