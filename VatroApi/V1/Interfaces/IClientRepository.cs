
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Entities;

namespace VatroApi.V1.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
        Task<Client?> UpdateAsync(int id, Client client);
        Task<bool> DeleteAsync(Client client);
        Task<bool> ClientExists(string name);
    }
}