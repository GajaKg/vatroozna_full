
using Microsoft.EntityFrameworkCore;
using VatroApi.V1.Data;
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Entities;
using VatroApi.V1.Interfaces;

namespace VatroApi.V1.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDBContext _context;

        public ClientRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients
                .AsNoTracking()
                .Include(c => c.Controls)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> GetByIdUntrackedAsync(int id)
        {
            return await _context.Clients
                .Include(c => c.Controls)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> CreateAsync(Client client)
        {
            await _context.Clients.AddAsync(client);

            return await _context.SaveChangesAsync() > 0
                ? client
                : null;
        }

        public async void UpdateAsync(Client clientToUpdate, EditClientDto editClientDto)
        {
            clientToUpdate.Name = editClientDto.Name;
            clientToUpdate.City = editClientDto.City;
            clientToUpdate.Address = editClientDto.Address;
            clientToUpdate.Email = editClientDto.Email;
            clientToUpdate.Phone = editClientDto.Phone;
            clientToUpdate.Phone2 = editClientDto.Phone2;
            clientToUpdate.Note = editClientDto.Note;
            clientToUpdate.Referent = editClientDto.Referent;
            clientToUpdate.Archived = editClientDto.Archived;
        }

        public async Task<bool> DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ClientExists(string name)
        {
            return await _context.Clients
                .AnyAsync(c => EF.Functions.ILike(c.Name, name));
            // .AnyAsync(c => c.Name.ToLower() == name.Trim().ToLower());//This prevents index usage in SQL.
        }
    }
}