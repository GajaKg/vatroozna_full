
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Interfaces
{
    public interface IClientService
    {
        Task<Result<ClientDto>> GetByIdAsync(int id);
        Task<IReadOnlyList<ClientDto>> GetAllAsync();
        Task<Result<ClientDto>> CreateAsync(PostClientDto postClientDto);
        Task<Result<ClientDto>> UpdateAsync(int id, EditClientDto editClientDto);
        Task<Result<int>> DeleteAsync(int id);
    }
}