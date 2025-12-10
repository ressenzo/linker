using Linker.Link.Application.Commons;
using Linker.Link.Application.GetLinkById;
using Linker.Link.Application.Repositories;
using Linker.Link.Test.UnitTests.Shared.Builders;
using Microsoft.Extensions.Logging;

namespace Linker.Link.Test.UnitTests.Application.GetLinkById;

public class GetLinkByIdUseCaseTests
{
    private readonly GetLinkByIdUseCase _useCase;
    private readonly Mock<ILogger<GetLinkByIdUseCase>> _logger;
    private readonly Mock<ILinkRepository> _linkRepository;

    public GetLinkByIdUseCaseTests()
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
    public async Task ShouldReturnValidationError_WhenIdIsInvalid(string? id)
    {
        // Act
        var result = await _useCase.GetLinkById(
            id!,
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain("Invalid id");
        _linkRepository.Verify(r => r.GetById(
                id!,
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenLinkIsNotFound()
    {
        // Arrange
        var id = "non-existent-id";
        _linkRepository
            .Setup(x => x.GetById(id, CancellationToken.None))
            .ReturnsAsync((Link.Domain.Entities.Link?)null);

        // Act
        var result = await _useCase.GetLinkById(id, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
        result.Errors.First().ShouldBe("Link not found");
        _linkRepository.Verify(r => r.GetById(
                id!,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ShouldReturnInternalError_WhenRepositoryThrows()
    {
        // Arrange
        var id = "error-id";
        _linkRepository
            .Setup(x => x.GetById(id, CancellationToken.None))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _useCase.GetLinkById(id, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.IsSuccess.ShouldBeFalse();
        result.ResultType.ShouldBe(ResultType.INTERNAL_ERROR);
    }

    [Fact]
    public async Task GetLinkById_WithValidId_ShouldReturnSuccess()
    {
        // Arrange
        var id = "valid-id";
        var link = new LinkBuilder().Build();
        _linkRepository
            .Setup(x => x.GetById(id, CancellationToken.None))
            .ReturnsAsync(link);

        // Act
        var result = await _useCase.GetLinkById(id, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Content.ShouldNotBeNull();
        GetLinkByIdResult linkResult = link;
        result.Content.ShouldBeEquivalentTo(linkResult);
    }
}
