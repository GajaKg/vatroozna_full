using VatroApi.V1.Dto.Control;
using VatroApi.V1.Models;

namespace VatroApi.V1.Mappers
{
    public static class ControlMappers
    {
        public static ControlDto ToControlDto(this Control control)
        {
            return new ControlDto
            {
                Id = control.Id,
                Subject = control.Subject,
                Duration = control.Duration,
                Date = control.Date,
                NextCheck = control.NextCheck,
                Note = control.Note,
                Archive = control.Archive,
                // Client = control.Client.ToClientDto()
            };
        }
        public static ControlWithClientDto ToControlWithClientDto(this Control control)
        {
            return new ControlWithClientDto
            {
                Id = control.Id,
                Subject = control.Subject,
                Duration = control.Duration,
                Date = control.Date,
                NextCheck = control.NextCheck,
                Note = control.Note,
                Archive = control.Archive,
                Client = control.Client.ToClientWithoutControlDto()
            };
        }

        public static Control FromControlPostDtoToControlModel(this ControlPostDto controlPostDto)
        {
            return new Control
            {
                ClientId = controlPostDto.ClientId,
                Subject = controlPostDto.Subject,
                Duration = controlPostDto.Duration,
                // Date = controlPostDto.Date,
                NextCheck = controlPostDto.NextCheck,
                Note = controlPostDto.Note,
                Archive = controlPostDto.Archive,
                // Client = control.Client.ToClientDto()
            };
        }

        public static Control FromControlEditDtoToControlModel(this Control control)
        {
            return new Control
            {
                Id = control.Id,
                // ClientId = control.ClientId,
                Subject = control.Subject,
                Duration = control.Duration,
                Date = control.Date,
                NextCheck = control.NextCheck,
                Note = control.Note,
                Archive = control.Archive,
                // Client = control.Client.ToClientDto()
            };
        }
    }
}