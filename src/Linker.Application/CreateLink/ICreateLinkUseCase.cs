using Linker.Application.Commons;

namespace Linker.Application.CreateLink;

public interface ICreateLinkUseCase
{
    Task<Result<CreateLinkResult>> CreateLink(
        CreateLinkRequest request,
        CancellationToken cancellationToken);
}
