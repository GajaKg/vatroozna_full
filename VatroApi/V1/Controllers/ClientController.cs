using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Interfaces;

namespace VatroApi.V1.Controllers
{
    public class ClientController : ApiControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ClientDto>> GetById([FromRoute] int id)
        {
            var result = await _clientService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return HandleError(result);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> Create([FromBody] PostClientDto postClientDto)
        {
            // this code is not necessary, dotnet do this automatically
            // if (!ModelState.IsValid) return BadRequest();

            var result = await _clientService.CreateAsync(postClientDto);

            if (!result.IsSuccess)
            {
                return HandleError(result);
            }

            return CreatedAtAction(nameof(GetById), new { Id = result.Value!.Id }, result.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ClientDto>> Update([FromRoute] int id, [FromBody] EditClientDto editClientDto)
        {

            var result = await _clientService.UpdateAsync(id, editClientDto);

            if (!result.IsSuccess)
            {
                return HandleError(result);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var result = await _clientService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return HandleError(result);
            }

            return Ok();
        }



    }
}