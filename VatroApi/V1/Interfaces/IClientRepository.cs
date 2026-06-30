
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Entities;

namespace VatroApi.V1.Interfaces
{
    public interface IClientRepository
    {
        Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken);
        Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Client?> GetByIdUntrackedAsync(int id, CancellationToken cancellationToken);
        Task<Client?> CreateAsync(Client client, CancellationToken cancellationToken);
        void UpdateAsync(Client clientToUpdate, EditClientDto editClientDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Client client, CancellationToken cancellationToken);
        Task<bool> ClientExists(string name, CancellationToken cancellationToken);
        Task<bool> SaveAllAsync(CancellationToken cancellationToken);
    }
}