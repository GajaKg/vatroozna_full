
using Microsoft.AspNetCore.Mvc;
using VatroApi.V1.Shared;

namespace VatroApi.V1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")] // The 'v' is a convention, not required.
    [ApiVersion("1.0")]
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