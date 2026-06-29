
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace VatroApi.V1.Middleware
{
    public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> _logger, IHostEnvironment _env) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext _context, RequestDelegate _next)
        {
            try
            {
                await _next(_context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _context.Response.ContentType = "application/json";
                _context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                ProblemDetails response;

                if (_env.IsDevelopment())
                {
                    response = new ProblemDetails
                    {
                        Title = "An unexpected error occurred.",
                        Detail = ex.Message,
                        Status = StatusCodes.Status500InternalServerError,
                        Instance = _context.Request.Path
                    };

                    response.Extensions["stackTrace"] = ex.StackTrace;
                }
                else
                {
                    response = new ProblemDetails
                    {
                        Title = "Internal Server Error",
                        Detail = "An unexpected error occurred.",
                        Status = StatusCodes.Status500InternalServerError,
                        Instance = _context.Request.Path
                    };
                }

                JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await _context.Response.WriteAsync(json);
            }
        }
    }
}