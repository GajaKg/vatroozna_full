using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Interfaces;

namespace VatroApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ControlController : ControllerBase
    {
        private readonly IControlRepository _controlRepository;

        public ControlController(IControlRepository controlRepository)
        {
            _controlRepository = controlRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var controls = await _controlRepository.GetAllAsync();

            return Ok(controls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var controls = await _controlRepository.GetByIdAsync(id);

            if (controls == null) return BadRequest();

            return Ok(controls);
        }

        [HttpPost]
        public async Task<ActionResult<ControlDto>> Create([FromBody] ControlPostDto controlPostDto)
        {
            var newControl = await _controlRepository.CreateAsync(controlPostDto);
            if (newControl == null) return BadRequest();

            // return Ok(newControl);
            return CreatedAtAction(nameof(GetById), new {Id = newControl.Id}, newControl);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ControlDto>> Update(int id, [FromBody] ControlEditDto controlEditDto)
        {
            var response = await _controlRepository.UpdateAsync(id, controlEditDto);

            return CreatedAtAction(nameof(GetById), new {id = response?.Id}, response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _controlRepository.Delete(id);

            if (response) return Ok();

            return BadRequest();
        }
    }
}