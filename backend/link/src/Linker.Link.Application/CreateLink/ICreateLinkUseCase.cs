using Linker.Link.Application.Commons;

namespace Linker.Link.Application.CreateLink;

public interface ICreateLinkUseCase
{
    Task<Result<CreateLinkResult>> CreateLink(
        CreateLinkRequest request,
        CancellationToken cancellationToken);
}
