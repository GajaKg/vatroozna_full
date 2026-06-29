
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Interfaces
{
    public interface IControlService
    {
        Task<Result<ControlWithClientDto>> GetByIdAsync(int id);
        Task<List<ControlWithClientDto>> GetAllAsync();
        Task<Result<ControlDto>> CreateAsync(ControlPostDto controlPostDto);
        Task<Result<ControlDto>> UpdateAsync(int id, ControlEditDto controlEditDto);
        Task<Result<int>> DeleteAsync(int id);
    }
}