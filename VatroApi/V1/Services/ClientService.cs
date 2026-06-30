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

        public async Task<Result<ClientDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(id, cancellationToken);

            if (client is null)
            {
                return Result<ClientDto>.Failure(
                    ResultErrors.RecordNotFound(id.ToString())
                );
            }
            return Result<ClientDto>.Success(client.ToClientDto());
        }

        public async Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllAsync(cancellationToken);

            return clients
                .Select(c => c.ToClientDto())
                .ToList();
        }

        public async Task<Result<ClientDto>> CreateAsync(PostClientDto postClientDto, CancellationToken cancellationToken)
        {
            // Race condition,Two requests can arrive simultaneously 
            // add modelBuilder.Entity<Client>().HasIndex(c => c.Name).IsUnique();
            if (await _clientRepository.ClientExists(postClientDto.Name, cancellationToken))
            {
                return Result<ClientDto>.Failure(
                    ResultErrors.RecordAlreadyExists(postClientDto.Name)
                );
            }

            var clientModel = postClientDto.FromClientPostToClientModel();

            var createdClient = await _clientRepository.CreateAsync(clientModel, cancellationToken);

            return createdClient is null
                ? Result<ClientDto>.Failure(ResultErrors.ServerError())
                : Result<ClientDto>.Success(createdClient.ToClientDto());
        }

        public async Task<Result<ClientDto>> UpdateAsync(int id, EditClientDto editClientDto, CancellationToken cancellationToken)
        {
            var clientToUpdate = await _clientRepository.GetByIdUntrackedAsync(id, cancellationToken);
            if (clientToUpdate is null)
            {
                return Result<ClientDto>.Failure(ResultErrors.RecordNotFound(id.ToString()));
            }

            _clientRepository.UpdateAsync(clientToUpdate, editClientDto, cancellationToken);
            var saved = await _clientRepository.SaveAllAsync(cancellationToken);

            return saved
                ? Result<ClientDto>.Success(clientToUpdate.ToClientDto())
                : Result<ClientDto>.Failure(ResultErrors.ServerError());
        }

        public async Task<Result<int>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var clientExists = await _clientRepository.GetByIdAsync(id, cancellationToken);

            if (clientExists is null)
            {
                return Result<int>.Failure(
                   ResultErrors.RecordNotFound(id.ToString())
                );
            }

            var isDeleted = await _clientRepository.DeleteAsync(clientExists, cancellationToken);

            return isDeleted
                ? Result<int>.Success(id)
                : Result<int>.Failure(new Error(RecordErrorType.ServerError, $"Neuspešno, molimo vas probajte kasnije."));
        }
    }
}