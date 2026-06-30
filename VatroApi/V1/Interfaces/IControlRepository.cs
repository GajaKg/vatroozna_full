using VatroApi.V1.Dto.Control;
using VatroApi.V1.Models;

namespace VatroApi.V1.Interfaces
{
    public interface IControlRepository
    {
        Task<IReadOnlyList<Control>> GetAllAsync();
        Task<Control?> GetByIdAsync(int id);
        Task<Control?> GetByIdUntrackedAsync(int id);
        Task<Control?> CreateAsync(Control control);
        void UpdateAsync(Control controlToUpdate, ControlEditDto controlEditDto);
        Task<bool> Delete(Control control);
        Task<bool> SaveAllAsync();
        Task<bool> ControlExists(int id);
    }
}