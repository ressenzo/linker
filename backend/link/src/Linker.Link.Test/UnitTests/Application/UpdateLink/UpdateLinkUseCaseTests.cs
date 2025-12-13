using Linker.Link.Application.Commons;
using Linker.Link.Application.Repositories;
using Linker.Link.Application.UpdateLink;
using Linker.Link.Test.UnitTests.Shared.Builders;
using Microsoft.Extensions.Logging;

namespace Linker.Link.Test.UnitTests.Application.UpdateLink;

public class UpdateLinkUseCaseTests
{
    private readonly UpdateLinkUseCase _useCase;
    private readonly Mock<ILogger<UpdateLinkUseCase>> _logger;
    private readonly Mock<ILinkRepository> _linkRepository;

    public UpdateLinkUseCaseTests()
    {
        _logger = new();
        _linkRepository = new();
        _useCase = new(
            _logger.Object,
            _linkRepository.Object
        );
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenLinkDoesNotExist()
    {
        // Arrange
        var request = new UpdateLinkRequestBuilder()
            .Build();
        _linkRepository
            .Setup(
                x => x.GetById(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Link.Domain.Entities.Link?)null);

        // Act
        var result = await _useCase.UpdateLink(
            request,
            CancellationToken.None
        );

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.ResultType.ShouldBe(ResultType.NOT_FOUND);
        _linkRepository
            .Verify(
                x => x.UpdateLink(
                    It.IsAny<Link.Domain.Entities.Link>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
    }

    [Fact]
    public async Task ShouldReturnValidationError_WhenLinkIsInvalid()
    {
        // Arrange
        var request = new UpdateLinkRequestBuilder()
            .WithInvalidRequest()
            .Build();
        var link = new LinkBuilder()
            .Build();
        _linkRepository
            .Setup(
                x => x.GetById(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(link);

        // Act
        var result = await _useCase.UpdateLink(
            request,
            CancellationToken.None
        );

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.ResultType.ShouldBe(ResultType.VALIDATION_ERROR);
        _linkRepository
            .Verify(
                x => x.UpdateLink(
                    It.IsAny<Link.Domain.Entities.Link>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
    }

    [Fact]
    public async Task ShouldUpdateLinkSuccessfully()
    {
        // Arrange
        var request = new UpdateLinkRequestBuilder()
            .Build();
        var link = new LinkBuilder()
            .Build();
        _linkRepository
            .Setup(
                x => x.GetById(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(link);

        // Act
        var result = await _useCase.UpdateLink(
            request,
            CancellationToken.None
        );

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.ResultType.ShouldBe(ResultType.SUCCESS);
        _linkRepository
            .Verify(
                x => x.UpdateLink(
                    It.Is<Link.Domain.Entities.Link>(x =>
                        x.Id.Equals(link.Id) &&
                        x.Name.Equals(request.Name) &&
                        x.Url.Equals(request.Url) &&
                        x.UserId.Equals(link.UserId)
                    ),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}
