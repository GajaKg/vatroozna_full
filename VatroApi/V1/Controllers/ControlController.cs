using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ControlController : ApiControllerBase
    {
        private readonly IControlService _controlService;
        private readonly IControlRepository _controlRepository;

        public ControlController(IControlService controlService, IControlRepository controlRepository)
        {
            _controlService = controlService;
            _controlRepository = controlRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var controls = await _controlRepository.GetAllAsync();
            var controls = await _controlService.GetAllAsync();

            return Ok(controls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var control = await _controlService.GetByIdAsync(id);

            if (!control.IsSuccess) return HandleError(control);

            return Ok(control.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ControlDto>> Create([FromBody] ControlPostDto controlPostDto)
        {
            var newControl = await _controlService.CreateAsync(controlPostDto);
            if (!newControl.IsSuccess) return HandleError(newControl);

            // return Ok(newControl);
            return CreatedAtAction(nameof(GetById), new { Id = newControl.Value?.Id }, newControl.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ControlDto>> Update(int id, [FromBody] ControlEditDto controlEditDto)
        {
            var control = await _controlService.UpdateAsync(id, controlEditDto);

            if (!control.IsSuccess) return HandleError(control);

            return CreatedAtAction(nameof(GetById), new { id = control.Value?.Id }, control.Value);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _controlService.DeleteAsync(id);

            if (!response.IsSuccess) return HandleError(response);
            return Ok(id);
        }
    }
}