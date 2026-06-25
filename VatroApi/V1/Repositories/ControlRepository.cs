
using Microsoft.EntityFrameworkCore;
using VatroApi.V1.Data;
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Mappers;

namespace VatroApi.V1.Repositories
{
    public class ControlRepository : IControlRepository
    {
        private readonly ApplicationDBContext _context;
        public ControlRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ControlWithClientDto>> GetAllAsync()
        {
            return await _context.Controls
                .AsNoTracking()
                // .Include(c => c.Client)
                .OrderBy(c => c.Date)
                // .Select(c => c.ToControlWithClientDto())
                //This lets EF generate a single optimized SQL query and avoids materializing full entities.
                .Select(c => new ControlWithClientDto
                {
                    Id = c.Id,
                    Subject = c.Subject,
                    Duration = c.Duration,
                    Date = c.Date,
                    NextCheck = c.NextCheck,
                    Note = c.Note,
                    Archive = c.Archive,
                    Client = new ClientWithoutControlDto
                    {
                        Id = c.Client.Id,
                        Name = c.Client.Name,
                        City = c.Client.City,
                        Address = c.Client.Address,
                        Email = c.Client.Email,
                        Phone = c.Client.Phone,
                        Phone2 = c.Client.Phone2,
                        Note = c.Client.Note,
                        Referent = c.Client.Referent,
                        Archived = c.Client.Archived,
                        Date = c.Client.Date,
                    },
                })
                .ToListAsync();
        }

        public async Task<ControlWithClientDto?> GetByIdAsync(int id)
        {
            var control = await _context.Controls
                .AsNoTracking()
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);

            return control?.ToControlWithClientDto();
        }

        public async Task<ControlDto?> CreateAsync(ControlPostDto controlPostDto)
        {
            var clientExist = await _context.Clients.AnyAsync(c => c.Id == controlPostDto.ClientId);
            if (!clientExist) return null;

            var controlModel = controlPostDto.FromControlPostDtoToControlModel();
            await _context.Controls.AddAsync(controlModel);
            var result = await SaveAllAsync();

            if (result) return controlModel.ToControlDto();

            return null;
        }

        public async Task<ControlDto?> UpdateAsync(int id, ControlEditDto controlEditDto)
        {
            var control = await _context.Controls.FindAsync(id);

            if (control is null) return null;

            control.Subject = controlEditDto.Subject;
            control.Duration = controlEditDto.Duration;
            control.NextCheck = controlEditDto.NextCheck;
            control.Note = controlEditDto.Note;
            control.Archive = controlEditDto.Archive;

            if (await SaveAllAsync()) return control.ToControlDto();

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var control = await _context.Controls.FindAsync(id);
            if (control is null) return false;

            _context.Controls.Remove(control);
            return await SaveAllAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}