using VatroApi.V1.Dto.Client;
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Mappers;
using VatroApi.V1.Shared;
using VatroApi.V1.Shared.ErrorHandling;

namespace VatroApi.V1.Services
{
    public class ControlService : IControlService
    {
        private readonly IControlRepository _controlRepository;

        public ControlService(IControlRepository controlRepository)
        {
            _controlRepository = controlRepository;
        }

        public async Task<IReadOnlyList<ControlWithClientDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var controls = await _controlRepository.GetAllAsync(cancellationToken);

            return controls.Select(c => new ControlWithClientDto
            {
                Id = c.Id,
                Subject = c.Subject,
                Duration = c.Duration,
                Date = c.Date,
                NextCheck = c.NextCheck,
                Note = c.Note,
                Archive = c.Archive,
                Client = new ClientWithoutControlDto
                {
                    Id = c.Client.Id,
                    Name = c.Client.Name,
                    // City = c.Client.City,
                    // Address = c.Client.Address,
                    Email = c.Client.Email,
                    Phone = c.Client.Phone,
                    // Phone2 = c.Client.Phone2,
                    // Note = c.Client.Note,
                    // Referent = c.Client.Referent,
                    // Archived = c.Client.Archived,
                    // Date = c.Client.Date,
                },
            }).ToList();
        }

        public async Task<Result<ControlWithClientDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var control = await _controlRepository.GetByIdAsync(id, cancellationToken);

            if (control is null)
            {
                return Result<ControlWithClientDto>.Failure(
                    ResultErrors.RecordNotFound(id.ToString())
                );
            }

            var controlDto = new ControlWithClientDto
            {
                Id = control.Id,
                Subject = control.Subject,
                Duration = control.Duration,
                Date = control.Date,
                NextCheck = control.NextCheck,
                Note = control.Note,
                Archive = control.Archive,
                Client = new ClientWithoutControlDto
                {
                    Id = control.ClientId,
                    Name = control.Client.Name,
                    Email = control.Client.Email,
                    Phone = control.Client.Phone,
                }
            };

            return Result<ControlWithClientDto>.Success(controlDto);
        }

        public async Task<Result<ControlDto>> CreateAsync(ControlPostDto controlPostDto, CancellationToken cancellationToken)
        {
            var controlModel = controlPostDto.FromControlPostDtoToControlModel();
            var control = await _controlRepository.CreateAsync(controlModel, cancellationToken);

            if (control is null)
            {
                return Result<ControlDto>.Failure(
                    ResultErrors.ServerError()
                );
            }

            return Result<ControlDto>.Success(control.ToControlDto());
        }

        public async Task<Result<ControlDto>> UpdateAsync(int id, ControlEditDto controlEditDto, CancellationToken cancellationToken)
        {
            var controlToUpdate = await _controlRepository.GetByIdUntrackedAsync(id, cancellationToken);
            if (controlToUpdate is null)
            {
                return Result<ControlDto>.Failure(ResultErrors.RecordNotFound(id.ToString()));
            }

            _controlRepository.UpdateAsync(controlToUpdate, controlEditDto, cancellationToken);

            return await _controlRepository.SaveAllAsync(cancellationToken)
                ? Result<ControlDto>.Success(controlToUpdate.ToControlDto())
                : Result<ControlDto>.Failure(ResultErrors.ServerError());
        }

        public async Task<Result<int>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var control = await _controlRepository.GetByIdAsync(id, cancellationToken);
            if (control is null)
            {
                return Result<int>.Failure(ResultErrors.RecordNotFound(id.ToString()));
            }

            return await _controlRepository.Delete(control, cancellationToken)
                ? Result<int>.Success(id)
                : Result<int>.Failure(ResultErrors.ServerError());

        }
    }
}