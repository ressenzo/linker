using Linker.Link.Test.UnitTests.Shared.Builders;

namespace Linker.Link.Test.UnitTests.Domain.Entities;

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

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldReturnFalse_WhenUserIdIsInvalid(string? userId)
    {
        // Arrange
        var link = new LinkBuilder()
            .WithUserId(userId)
            .Build();
        
        // Act
        link.Validate();
        
        // Assert
        link.IsValid.ShouldBeFalse();
        link.Errors.ShouldHaveSingleItem();
        link.Errors.First().ShouldBe("Invalid user id");
    }

    [Fact]
    public void ShouldReturnTrue_WhenValuesAreValid()
    {
        // Arrange
        var name = "Link";
        var url = "http://teste.com";
        var userId = "12345678";
        var link = new LinkBuilder()
            .WithName(name)
            .WithUrl(url)
            .WithUserId(userId)
            .Build();
        
        // Act
        link.Validate();
        
        // Assert
        link.IsValid.ShouldBeTrue();
        link.Name.ShouldBe(name);
        link.Url.ShouldBe(url);
        link.UserId.ShouldBe(userId);
        link.Errors.ShouldBeEmpty();
    }
}
