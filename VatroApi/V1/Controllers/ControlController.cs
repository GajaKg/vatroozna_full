using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Dto.Control;
using VatroApi.V1.Interfaces;

namespace VatroApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ControlController : ApiControllerBase
    {
        private readonly IControlService _controlService;

        public ControlController(IControlService controlService)
        {
            _controlService = controlService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var controls = await _controlService.GetAllAsync(cancellationToken);

            return Ok(controls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var control = await _controlService.GetByIdAsync(id, cancellationToken);

            if (!control.IsSuccess) return HandleError(control);

            return Ok(control.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ControlDto>> Create([FromBody] ControlPostDto controlPostDto, CancellationToken cancellationToken)
        {
            var newControl = await _controlService.CreateAsync(controlPostDto, cancellationToken);
            if (!newControl.IsSuccess) return HandleError(newControl);

            // return Ok(newControl);
            return CreatedAtAction(nameof(GetById), new { Id = newControl.Value?.Id }, newControl.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ControlDto>> Update(int id, [FromBody] ControlEditDto controlEditDto, CancellationToken cancellationToken)
        {
            var control = await _controlService.UpdateAsync(id, controlEditDto, cancellationToken);

            if (!control.IsSuccess) return HandleError(control);

            return CreatedAtAction(nameof(GetById), new { id = control.Value?.Id }, control.Value);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var response = await _controlService.DeleteAsync(id, cancellationToken);

            if (!response.IsSuccess) return HandleError(response);
            return Ok(id);
        }
    }
}