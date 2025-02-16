using System.Net;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
#pragma warning disable CA1031
            catch (Exception ex)
#pragma warning restore CA1031
            {
                await this.HandleExceptionAsync(context, ex);
            }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string redirectUrl;

        switch (exception)
        {
            case HttpRequestException ex:
                redirectUrl = ex.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => "/Auth/Login",
                    HttpStatusCode.NotFound => "/Error/NotFound",
                    HttpStatusCode.BadRequest => "/Error/BadRequest",
                    _ => "/Error/Server"
                };
                break;

            default:
                redirectUrl = "/Error/Server";
                break;
        }

        context.Response.Redirect(redirectUrl);
    }

}
