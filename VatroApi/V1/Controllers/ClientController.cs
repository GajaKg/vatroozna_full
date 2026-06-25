using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Dto.Client;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
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
                return HandleError<ClientDto>(result);
                // return (result.Error?.Code) switch
                // {
                //     RecordErrorType.NotFound => (ActionResult<ClientDto>)NotFound(result?.Error),
                //     RecordErrorType.ServerError => (ActionResult<ClientDto>)StatusCode(500, result.Error),
                //     _ => (ActionResult<ClientDto>)StatusCode(500, result.Error),
                // };
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
                return HandleError<ClientDto>(result);
                // return (result.Error?.Code) switch
                // {
                //     RecordErrorType.NotFound => (ActionResult<ClientDto>)NotFound(result?.Error),
                //     RecordErrorType.ServerError => (ActionResult<ClientDto>)StatusCode(500, result.Error),
                //     _ => (ActionResult<ClientDto>)StatusCode(500, result.Error),
                // };
            }
            // if (!result.IsSuccess && result.Error?.Code == RecordErrorType.Exists)
            // {
            //     return BadRequest(result.Error);
            // }

            return CreatedAtAction(nameof(GetById), new { Id = result.Value!.Id }, result.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ClientDto>> Update([FromRoute] int id, [FromBody] EditClientDto editClientDto)
        {

            var result = await _clientService.UpdateAsync(id, editClientDto);

            if (!result.IsSuccess)
            {
                return HandleError<ClientDto>(result);
                // return NotFound(response.Error);
                // return (result.Error?.Code) switch
                // {
                //     RecordErrorType.NotFound => (ActionResult<ClientDto>)NotFound(result?.Error),
                //     RecordErrorType.ServerError => (ActionResult<ClientDto>)StatusCode(500, result.Error),
                //     _ => (ActionResult<ClientDto>)StatusCode(500, result.Error),
                // };
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var result = await _clientService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return HandleError<int>(result);
                // return (result.Error?.Code) switch
                // {
                //     RecordErrorType.NotFound => (ActionResult<bool>)NotFound(result?.Error),
                //     RecordErrorType.ServerError => (ActionResult<bool>)StatusCode(500, result.Error),
                //     _ => (ActionResult<bool>)StatusCode(500, result.Error),
                // };
            }

            return Ok();
        }


        private ActionResult HandleError<T>(Result<T> result)
        {
            switch (result.Error?.Code)
            {
                case RecordErrorType.NotFound:
                    return NotFound(result?.Error);
                case RecordErrorType.ServerError:
                    return StatusCode(500, result?.Error);
                case RecordErrorType.Exists:
                    return BadRequest(result?.Error);
                default:
                    return StatusCode(500, result?.Error);
            }

        }
    }
}