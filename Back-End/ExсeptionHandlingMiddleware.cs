using System.Net;

namespace Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(KeyNotFoundException ex)
            {
                LogError(ex, "Key not found");
                await HandleException(httpContext,HttpStatusCode.NotFound,"Key not found");
            }
            catch (Exception ex)
            {

                LogError(ex, "Internal server error");
                await HandleException(httpContext, HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        private async Task HandleException(HttpContext httpContext,
            HttpStatusCode httpStatusCode ,string message)
        {

            var response = httpContext.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            var errorDataTransferObject = new
            {
                Message = message,
                statusCode = httpStatusCode
            };

            await response.WriteAsJsonAsync(errorDataTransferObject);
        }



        private void LogError(Exception ex, string message)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}: {ex.Message}. StackTrace: {ex.StackTrace}";
            _logger.LogError(logMessage);
        }
    }
}
