
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

        public async Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Clients
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Clients
                .AsNoTracking()
                .Include(c => c.Controls)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<Client?> GetByIdUntrackedAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Clients
                .Include(c => c.Controls)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<Client?> CreateAsync(Client client, CancellationToken cancellationToken)
        {
            await _context.Clients.AddAsync(client, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0
                ? client
                : null;
        }

        public async void UpdateAsync(Client clientToUpdate, EditClientDto editClientDto, CancellationToken cancellationToken)
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

        public async Task<bool> DeleteAsync(Client client, CancellationToken cancellationToken)
        {
            _context.Clients.Remove(client);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SaveAllAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ClientExists(string name, CancellationToken cancellationToken)
        {
            return await _context.Clients
                .AnyAsync(c => EF.Functions.ILike(c.Name, name), cancellationToken: cancellationToken);
            // .AnyAsync(c => c.Name.ToLower() == name.Trim().ToLower());//This prevents index usage in SQL.
        }
    }
}