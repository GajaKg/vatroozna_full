using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Mappers;

namespace VatroApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAll()
        {
            var clients = await _clientRepository.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ClientDto>> GetById([FromRoute] int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null) return NotFound("Ne postoji traženi klijent");

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> Create([FromBody] PostClientDto postClientDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            // var customer = postClientDto.FromClientPostToClientModel();
            var response = await _clientRepository.CreateAsync(postClientDto);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ClientDto>> Update([FromRoute] int id, [FromBody] EditClientDto editClientDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _clientRepository.UpdateAsync(id, editClientDto);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var isDeleted = await _clientRepository.Delete(id);

            if (isDeleted == null) return BadRequest();
            if (isDeleted == false) return NotFound();

            return Ok();
        }
    }
}