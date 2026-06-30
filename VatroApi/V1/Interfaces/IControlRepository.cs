using VatroApi.V1.Dto.Control;
using VatroApi.V1.Models;

namespace VatroApi.V1.Interfaces
{
    public interface IControlRepository
    {
        Task<IReadOnlyList<Control>> GetAllAsync(CancellationToken cancellationToken);
        Task<Control?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Control?> GetByIdUntrackedAsync(int id, CancellationToken cancellationToken);
        Task<Control?> CreateAsync(Control control, CancellationToken cancellationToken);
        void UpdateAsync(Control controlToUpdate, ControlEditDto controlEditDto, CancellationToken cancellationToken);
        Task<bool> Delete(Control control, CancellationToken cancellationToken);
        Task<bool> SaveAllAsync(CancellationToken cancellationToken);
        Task<bool> ControlExists(int id, CancellationToken cancellationToken);
    }
}