
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

        public async Task<List<ClientDto>> GetAllAsync()
        {
            return await _context.Clients
                .AsNoTracking()
                .Select(c => c.ToClientDto())
                .ToListAsync();
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = await _context.Clients
                .AsNoTracking()
                .Include(c => c.Controls)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null) return null;

            return client.ToClientDto();
        }

        public async Task<ClientDto?> CreateAsync(PostClientDto postClientDto)
        {
            var clientModel = postClientDto.FromClientPostToClientModel();
            await _context.Clients.AddAsync(clientModel);
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return clientModel.ToClientDto();

            return null;
        }

        public async Task<ClientDto?> UpdateAsync(int id, EditClientDto editClientDto)
        {
            var foundClient = await _context.Clients.FindAsync(id);
            if (foundClient == null) return null;

            foundClient.Name = editClientDto.Name;
            foundClient.City = editClientDto.City;
            foundClient.Address = editClientDto.Address;
            foundClient.Email = editClientDto.Email;
            foundClient.Phone = editClientDto.Phone;
            foundClient.Phone2 = editClientDto.Phone2;
            foundClient.Note = editClientDto.Note;
            foundClient.Referent = editClientDto.Referent;
            foundClient.Archived = editClientDto.Archived;

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return foundClient.ToClientDto();

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client == null) return false;

            _context.Clients.Remove(client);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}