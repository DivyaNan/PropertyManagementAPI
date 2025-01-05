using PropertyManagementAPI.Errors;
using System.Net;

namespace PropertyManagementAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next
                                   ,ILogger<ExceptionMiddleware> logger
                                   ,IHostEnvironment env ) 
        {
            this._next = next;
            this._logger = logger;
            this._env = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                APIError response;
                HttpStatusCode statusCode=HttpStatusCode.InternalServerError;
                string message;
                var exceptionType=ex.GetType();
                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = "You are not authorized";
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Some unknown error occured";
                }
                if (_env.IsDevelopment())
                {
                    response = new APIError((int) statusCode,ex.Message,ex.StackTrace.ToString());
                }
                else
                {
                    response = new APIError((int)statusCode, message);
                }
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
