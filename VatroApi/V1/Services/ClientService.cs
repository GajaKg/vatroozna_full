using VatroApi.V1.Dto.Client;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Mappers;
using VatroApi.V1.Shared;
using VatroApi.V1.Shared.ErrorHandling;

namespace VatroApi.V1.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result<ClientDto>> GetByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client is null)
            {
                return Result<ClientDto>.Failure(
                    ResultErrors.RecordNotFound(id.ToString())
                );
            }
            return Result<ClientDto>.Success(client.ToClientDto());
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients
                .Select(c => c.ToClientDto())
                .ToList();
        }

        public async Task<Result<ClientDto>> CreateAsync(PostClientDto postClientDto)
        {
            // Race condition,Two requests can arrive simultaneously 
            // add modelBuilder.Entity<Client>().HasIndex(c => c.Name).IsUnique();
            if (await _clientRepository.ClientExists(postClientDto.Name))
            {
                return Result<ClientDto>.Failure(
                    ResultErrors.RecordAlreadyExists(postClientDto.Name)
                );
            }

            var clientModel = postClientDto.FromClientPostToClientModel();

            var createdClient = await _clientRepository.CreateAsync(clientModel);

            return Result<ClientDto>.Success(
                createdClient.ToClientDto()
            );
        }

        public async Task<Result<ClientDto>> UpdateAsync(int id, EditClientDto editClientDto)
        {
            var clientModel = editClientDto.FromClientEditToClientModel();
            var updatedClient = await _clientRepository.UpdateAsync(id, clientModel);

            if (updatedClient is null)
            {
                return Result<ClientDto>.Failure(
                    ResultErrors.RecordNotFound(editClientDto.Name)
                );
            }

            return Result<ClientDto>.Success(
                updatedClient.ToClientDto()
            );
        }

        public async Task<Result<int>> DeleteAsync(int id)
        {
            var clientExists = await _clientRepository.GetByIdAsync(id);

            if (clientExists is null)
            {
                return Result<int>.Failure(
                   ResultErrors.RecordNotFound(id.ToString())
                );
            }

            var isDeleted = await _clientRepository.DeleteAsync(clientExists);

            return isDeleted
                ? Result<int>.Success(id)
                : Result<int>.Failure(new Error(RecordErrorType.ServerError, $"Neuspešno, molimo vas probajte kasnije."));
        }
    }
}