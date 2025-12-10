using Linker.Application.GetLinksByUserId;
using Linker.Application.Repositories;
using Linker.Domain.Entities;
using Linker.Test.UnitTests.Shared.Builders;
using Microsoft.Extensions.Logging;

namespace Linker.Test.UnitTests.Application.GetLinksByUserId;

public class GetLinksByUserIdUseCaseTests
{
    private readonly Mock<ILogger<GetLinksByUserIdUseCase>> _logger;
    private readonly Mock<ILinkRepository> _linkRepository;
    private readonly GetLinksByUserIdUseCase _useCase;

    public GetLinksByUserIdUseCaseTests()
    {
        _logger = new();
        _linkRepository = new();
        _useCase = new(
            _logger.Object,
            _linkRepository.Object);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task ShouldReturnValidationError_WhenUserIdIsInvalid(string? userId)
    {
        // Act
        var result = await _useCase.GetLinksByUserId(
            userId!,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ShouldBe("User ID cannot be null or empty");
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenNoLinksFoundForUser()
    {
        // Arrange
        var userId = "12345678";
        _linkRepository
            .Setup(x => x.GetLinks(
                userId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<Link>?)null);

        // Act
        var result = await _useCase.GetLinksByUserId(
            userId,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ShouldBe("No links found for the user");
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenLinksCollectionIsEmpty()
    {
        // Arrange
        var userId = "12345678";
        _linkRepository
            .Setup(x => x.GetLinks(
                userId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await _useCase.GetLinksByUserId(
            userId,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.First().ShouldBe("No links found for the user");
    }

    [Fact]
    public async Task ShouldReturnSuccess_WhenLinksFound()
    {
        // Arrange
        var userId = "12345678";
        var links = new[]
        {
            new LinkBuilder().Build()
        };
        _linkRepository
            .Setup(x => x.GetLinks(
                userId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(links);
        var linksResult = new GetLinksByUserIdResult(links);

        // Act
        var result = await _useCase.GetLinksByUserId(
            userId,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Content.ShouldNotBeNull();
        result.Content.ShouldBeEquivalentTo(linksResult);
    }

    [Fact]
    public async Task ShouldReturnInternalError_WhenRepositoryThrows()
    {
        // Arrange
        var userId = "12345678";
        _linkRepository
            .Setup(x => x.GetLinks(
                userId,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("repo failure"));

        // Act
        var result = await _useCase.GetLinksByUserId(
            userId,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        _linkRepository.Verify(r => r.GetLinks(
                userId,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
