
using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected ActionResult HandleError<T>(Result<T> result)
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