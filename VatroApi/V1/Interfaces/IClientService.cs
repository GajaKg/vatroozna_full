
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Interfaces
{
    public interface IClientService
    {
        Task<Result<ClientDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<ClientDto>> CreateAsync(PostClientDto postClientDto, CancellationToken cancellationToken);
        Task<Result<ClientDto>> UpdateAsync(int id, EditClientDto editClientDto, CancellationToken cancellationToken);
        Task<Result<int>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}