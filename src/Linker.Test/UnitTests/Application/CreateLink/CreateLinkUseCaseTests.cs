using Linker.Application.CreateLink;
using Linker.Application.Repositories;
using Linker.Domain.Entities;
using Linker.Test.UnitTests.Shared.Builders;
using Microsoft.Extensions.Logging;

namespace Linker.Test.UnitTests.Application.CreateLink;

public class CreateLinkUseCaseTests
{
    private readonly CreateLinkUseCase _useCase;
    private readonly Mock<ILogger<CreateLinkUseCase>> _logger;
    private readonly Mock<ILinkRepository> _linkRepository;

    public CreateLinkUseCaseTests()
    {
        _logger = new();
        _linkRepository = new();
        _useCase = new(
            _logger.Object,
            _linkRepository.Object);
    }

    [Fact]
    public async Task ShouldReturnValidationError_WhenLinkIsInvalid()
    {
        // Arrange
        var request = new CreateLinkRequestBuilder()
            .WithName("")
            .Build();

        // Act
        var result = await _useCase.CreateLink(
            request,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain("Invalid name");
        _linkRepository.Verify(r => 
            r.CreateLink(
                It.IsAny<Link>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _logger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString()!.Contains("Link validation failed")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task ShouldCreateLink_WhenValuesAreValid()
    {
        // Arrange
        _linkRepository
            .Setup(r => r.CreateLink(
                It.IsAny<Link>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var name = "Link";
        var url = "http://teste.com";
        var userId = "12345678";

        var request = new CreateLinkRequestBuilder()
            .WithName(name)
            .WithUrl(url)
            .WithUserId(userId)
            .Build();

        // Act
        var result = await _useCase.CreateLink(
            request,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Content.ShouldNotBeNull();
        result.Content.Name.ShouldBe(name);
        result.Content.Url.ShouldBe(url);
        result.Content.UserId.ShouldBe(userId);
        result.Errors.ShouldBeEmpty();

        _linkRepository.Verify(r => r.CreateLink(
                It.Is<Link>(l =>
                    l.Name == name &&
                    l.Url == url &&
                    l.UserId == userId),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _logger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Never);
    }

    [Fact]
    public async Task ShouldReturnInternalError_WhenRepositoryThrows()
    {
        // Arrange
        _linkRepository
            .Setup(r => r.CreateLink(
                It.IsAny<Link>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("repo failure"));

        var request = new CreateLinkRequestBuilder()
            .Build();

        // Act
        var result = await _useCase.CreateLink(request, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        _linkRepository.Verify(r => r.CreateLink(
                It.IsAny<Link>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
