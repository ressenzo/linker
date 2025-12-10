using Linker.Link.Application.Commons;

namespace Linker.Link.Application.GetLinkById;

public interface IGetLinkByIdUseCase
{
    Task<Result<GetLinkByIdResult>> GetLinkById(
        string id,
        CancellationToken cancellationToken);
}
