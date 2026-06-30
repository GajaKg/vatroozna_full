using VatroApi.V1.Dto.Client;
using VatroApi.V1.Entities;

namespace VatroApi.V1.Mappers
{
    public static class ClientMappers
    {
        public static ClientDto ToClientDto(this Client client)
        {
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                City = client.City,
                Address = client.Address,
                Email = client.Email,
                Phone = client.Phone,
                Phone2 = client.Phone2,
                Note = client.Note,
                Referent = client.Referent,
                Archived = client.Archived,
                Date = client.Date,
                Controls = client.Controls.Select(c => c.ToControlDto()).ToList(),
            };
        }
        public static Client FromClientPostToClientModel(this PostClientDto postClientDto)
        {
            return new Client
            {
                Name = postClientDto.Name,
                City = postClientDto.City,
                Address = postClientDto.Address,
                Email = postClientDto.Email,
                Phone = postClientDto.Phone,
                Phone2 = postClientDto.Phone2,
                Note = postClientDto.Note,
                Referent = postClientDto.Referent,
                Archived = postClientDto.Archived,
                // Date = postClientDto.Date,
                //   Controls = postClientDto.Controls,
            };
        }
        public static Client FromClientEditToClientModel(this EditClientDto editClientDto)
        {
            return new Client
            {
                Name = editClientDto.Name,
                City = editClientDto.City,
                Address = editClientDto.Address,
                Email = editClientDto.Email,
                Phone = editClientDto.Phone,
                Phone2 = editClientDto.Phone2,
                Note = editClientDto.Note,
                Referent = editClientDto.Referent,
                Archived = editClientDto.Archived,
                // Date = postClientDto.Date,
                //   Controls = postClientDto.Controls,
            };
        }

        public static ClientWithoutControlDto ToClientWithoutControlDto(this Client client)
        {
            return new ClientWithoutControlDto
            {
                Id = client.Id,
                Name = client.Name,
                // City = client.City,
                // Address = client.Address,
                Email = client.Email,
                Phone = client.Phone,
                // Phone2 = client.Phone2,
                // Note = client.Note,
                // Referent = client.Referent,
                // Archived = client.Archived,
                // Date = client.Date,
            };
        }
    }
}