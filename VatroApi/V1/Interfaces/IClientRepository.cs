
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Entities;

namespace VatroApi.V1.Interfaces
{
    public interface IClientRepository
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<ClientDto?> GetByIdAsync(int id);
        Task<ClientDto?> CreateAsync(PostClientDto postClientDto);
        Task<ClientDto?> UpdateAsync(int id, EditClientDto editClientDto);
        Task<bool> Delete(int id);
    }
}