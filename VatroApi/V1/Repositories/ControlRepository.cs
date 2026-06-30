
using Microsoft.EntityFrameworkCore;
using VatroApi.V1.Data;
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Models;

namespace VatroApi.V1.Repositories
{
    public class ControlRepository : IControlRepository
    {
        private readonly ApplicationDBContext _context;
        public ControlRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Control>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Controls
                .AsNoTracking()
                .Include(c => c.Client)
                .OrderBy(c => c.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<Control?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Controls
                .AsNoTracking()
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<Control?> GetByIdUntrackedAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Controls
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<Control?> CreateAsync(Control control, CancellationToken cancellationToken)
        {
            await _context.Controls.AddAsync(control, cancellationToken);
            var result = await SaveAllAsync(cancellationToken);

            if (result) return control;

            return null;
        }

        public void UpdateAsync(Control controlToUpdate, ControlEditDto controlEditDto, CancellationToken cancellationToken)
        {
            controlToUpdate.Subject = controlEditDto.Subject;
            controlToUpdate.Duration = controlEditDto.Duration;
            controlToUpdate.Date = controlEditDto.Date;
            controlToUpdate.NextCheck = controlEditDto.NextCheck;
            controlToUpdate.Note = controlEditDto.Note;
            controlToUpdate.Archive = controlEditDto.Archive;
        }

        public async Task<bool> Delete(Control control, CancellationToken cancellationToken)
        {
            _context.Controls.Remove(control);
            return await SaveAllAsync(cancellationToken);
        }

        public async Task<bool> SaveAllAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ControlExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Controls.AnyAsync(c => c.Id == id, cancellationToken: cancellationToken);
        }
    }
}