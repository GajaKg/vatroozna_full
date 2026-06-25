
using Microsoft.EntityFrameworkCore;
using VatroApi.V1.Data;
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Entities;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Mappers;

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

        public async Task<Client> CreateAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<Client?> UpdateAsync(int id, Client client)
        {
            var foundClient = await _context.Clients.FindAsync(id);
            if (foundClient == null) return null;

            foundClient.Name = client.Name;
            foundClient.City = client.City;
            foundClient.Address = client.Address;
            foundClient.Email = client.Email;
            foundClient.Phone = client.Phone;
            foundClient.Phone2 = client.Phone2;
            foundClient.Note = client.Note;
            foundClient.Referent = client.Referent;
            foundClient.Archived = client.Archived;

            await _context.SaveChangesAsync();

            return foundClient;
        }

        public async Task<bool> DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ClientExists(string name)
        {
            return await _context.Clients
                .AnyAsync(c => c.Name.ToLower() == name.Trim().ToLower());
        }
    }
}