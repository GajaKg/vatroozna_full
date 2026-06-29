
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

        public async Task<List<Control>> GetAllAsync()
        {
            return await _context.Controls
                .AsNoTracking()
                .Include(c => c.Client)
                .OrderBy(c => c.Date)
                .ToListAsync();
        }

        public async Task<Control?> GetByIdAsync(int id)
        {
            return await _context.Controls
                .AsNoTracking()
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Control?> GetByIdUntrackedAsync(int id)
        {
            return await _context.Controls
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Control?> CreateAsync(Control control)
        {
            await _context.Controls.AddAsync(control);
            var result = await SaveAllAsync();

            if (result) return control;

            return null;
        }

        public void UpdateAsync(Control controlToUpdate, ControlEditDto controlEditDto)
        {
            controlToUpdate.Subject = controlEditDto.Subject;
            controlToUpdate.Duration = controlEditDto.Duration;
            controlToUpdate.Date = controlEditDto.Date;
            controlToUpdate.NextCheck = controlEditDto.NextCheck;
            controlToUpdate.Note = controlEditDto.Note;
            controlToUpdate.Archive = controlEditDto.Archive;
        }

        public async Task<bool> Delete(Control control)
        {
            _context.Controls.Remove(control);
            return await SaveAllAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ControlExists(int id)
        {
            return await _context.Controls.AnyAsync(c => c.Id == id);
        }
    }
}