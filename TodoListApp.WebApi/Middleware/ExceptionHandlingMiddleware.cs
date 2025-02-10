namespace TodoListApp.WebApi.Middleware
{
    using System.Net;
    using System.Text.Json;
    using Microsoft.AspNetCore.Http;
    using TodoListApp.WebApi.Exceptions;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
#pragma warning disable CA1031
            catch (Exception ex)
#pragma warning restore CA1031
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            int statusCode;
            string errorMessage;

            switch (exception)
            {
                case KeyNotFoundException ex:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errorMessage = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorMessage = "Unauthorized access.";
                    break;

                case ArgumentException ex:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = ex.Message;
                    break;

                case SessionExpiredException:
                    statusCode = 440;
                    errorMessage = "Session expired";
                    break;

                default:
                    this.logger.LogError(exception, "Internal exception");
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = "Internal server error";
                    break;
            }

            response.StatusCode = statusCode;

            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = errorMessage,
            };

            return response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
