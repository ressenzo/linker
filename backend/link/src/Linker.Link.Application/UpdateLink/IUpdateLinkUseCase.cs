using Linker.Link.Application.Commons;

namespace Linker.Link.Application.UpdateLink;

public interface IUpdateLinkUseCase
{
    Task<Result> UpdateLink(
        UpdateLinkRequest request,
        CancellationToken cancellationToken
    );
}
