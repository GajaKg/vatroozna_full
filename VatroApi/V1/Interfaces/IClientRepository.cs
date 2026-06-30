
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Entities;

namespace VatroApi.V1.Interfaces
{
    public interface IClientRepository
    {
        Task<IReadOnlyList<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<Client?> GetByIdUntrackedAsync(int id);
        Task<Client?> CreateAsync(Client client);
        void UpdateAsync(Client clientToUpdate, EditClientDto editClientDto);
        Task<bool> DeleteAsync(Client client);
        Task<bool> ClientExists(string name);
        Task<bool> SaveAllAsync();
    }
}