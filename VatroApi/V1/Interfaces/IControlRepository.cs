using VatroApi.V1.Dto.Control;

namespace VatroApi.V1.Interfaces
{
    public interface IControlRepository
    {
        Task<List<ControlWithClientDto>> GetAllAsync();
        Task<ControlWithClientDto?> GetByIdAsync(int id);
        Task<ControlDto?> CreateAsync(ControlPostDto controlPostDto);
        Task<ControlDto?> UpdateAsync(int id, ControlEditDto controlEditDto);
        Task<bool> Delete(int id);
        Task<bool> SaveAllAsync();
    }
}