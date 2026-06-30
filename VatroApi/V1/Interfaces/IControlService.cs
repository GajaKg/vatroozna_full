
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Interfaces
{
    public interface IControlService
    {
        Task<Result<ControlWithClientDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<ControlWithClientDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<ControlDto>> CreateAsync(ControlPostDto controlPostDto, CancellationToken cancellationToken);
        Task<Result<ControlDto>> UpdateAsync(int id, ControlEditDto controlEditDto, CancellationToken cancellationToken);
        Task<Result<int>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}